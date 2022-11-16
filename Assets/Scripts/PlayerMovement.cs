using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Shooter))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Характеристики персонажа")]
	[Range(0,10f)][SerializeField] private float Speed = 5f;
	[Range(0, 15f)][SerializeField] private float JampForse = 11f;
	[SerializeField] private float superAttackCooldown;
	[SerializeField] private float attack;
	[SerializeField] private float attackRahge = 0.5f;
    
	[Header("Setting")]
	[SerializeField] private Image barSuperAttack;
	[SerializeField] private Transform attackPoint;
	[SerializeField] private LayerMask enemyLayers;
	[SerializeField] private Transform firePoint, fFPoint, sFPoint;

	[HideInInspector]public Animator animator;
	[HideInInspector] public bool isRight;

	private Rigidbody2D _rigidbody;
	private SpriteRenderer _spriteRenderer;
	private Shooter _spooter;

	private float cooldownTimer = Mathf.Infinity;
	private bool _grounded;
	private Vector2 move;

    private void Awake()
    {
		_rigidbody = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_spooter = GetComponent<Shooter>();
    }
	
	private void FixedUpdate()
	{
		MovementPlayer();
		Flip();
	}

	/// <summary>
	/// Передвижение игрока
	/// </summary>
    private void MovementPlayer()
    {
		move.x = Input.GetAxisRaw("Horizontal") * Speed;
		move.y = _rigidbody.velocity.y;
		
		_rigidbody.velocity = move;
    }

    private void Update()
    {
		Attack(); // Атака

		cooldownTimer += Time.deltaTime; // Перезарядка суперсилы
		barSuperAttack.fillAmount = cooldownTimer / 5;

		if (Input.GetKeyDown(KeyCode.Space) && _grounded) // Прыжок
				Jump();
        
		AnimationNinja(); // Анимация
	}

	/// <summary>
	/// Атаки
	/// </summary>
	private void Attack()
    {
		if (Input.GetButtonDown("Fire1")) // Нажатие ЛКМ
			AttackLBM();

		if (Input.GetButtonUp("Fire2") && cooldownTimer > superAttackCooldown && move == Vector2.zero) // Нажмите ПКМ
			AttackRBM();
    }

	/// <summary>
	/// Анимация
	/// </summary>
	private void AnimationNinja()
    {
		animator.SetFloat("Speed", Mathf.Abs(move.x));
		animator.SetBool("Grounded", _grounded);
    }
	
	/// <summary>
	/// Прыжок
	/// </summary>
	private void Jump()
	{
		_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JampForse);
		_grounded = false;
		animator.SetTrigger("Jump");
	}

	/// <summary>
	/// Атака ЛКМ
	/// </summary>
	private void AttackLBM()
    {
		animator.SetTrigger("Attack");
		Collider2D[] hitEnemis = Physics2D.OverlapCircleAll(attackPoint.position, attackRahge, enemyLayers);

		foreach(Collider2D enemy in hitEnemis)
        {
			enemy.GetComponent<Enemis>().TakeDamageEnemy(attack);
        }
    }

	/// <summary>
	/// Нарисованный радиус атаки
	/// </summary>
    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
			return;

		Gizmos.DrawWireSphere(attackPoint.position, attackRahge);
    }

    /// <summary>
    /// Атака ПКМ
    /// </summary>
    private void AttackRBM()
    {
		animator.SetTrigger("SuperAttack");

		cooldownTimer = 0;
		_spooter.Shoot(move.x);
    }

	/// <summary>
	/// Флип))
	/// </summary>
	public void Flip()
    {
		if (move.x < 0f)
        {
			_spriteRenderer.flipX = true;
			isRight = false;

			firePoint.localPosition = sFPoint.localPosition;
			firePoint.localRotation = sFPoint.localRotation;
		}
		else if (move.x > 0f)
		{
			_spriteRenderer.flipX = false;
			isRight = true;

			firePoint.localPosition = fFPoint.localPosition;
			firePoint.localRotation = fFPoint.localRotation;
		}
    }

	/// <summary>
	/// Касание земли
	/// </summary>
	/// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
			_grounded = true;
    }
}
