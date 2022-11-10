using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Shooter))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Скорость")]
	[Range(0,10f)][SerializeField] private float Speed = 5f;
    [Header("Сила прыжка")]
	[Range(0, 15f)][SerializeField] private float JampForse = 5f;
	[Header("Перезарядка суперсилы")]
	[SerializeField] private float attackCooldown;
	[Header("Удар")]
	[SerializeField] private float attack;
	[SerializeField] private Image barSuperAttack;

	public Animator animator;

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
		if (Input.GetButtonDown("Fire1")) // Нажатие ЛКМ
			AttackLBM();

		if (Input.GetButtonUp("Fire2") && cooldownTimer > attackCooldown && _grounded) // Нажмите ПКМ
			AttackRBM();
		cooldownTimer += Time.deltaTime; // Перезарядка суперсилы
		barSuperAttack.fillAmount = cooldownTimer / 5;

		if (Input.GetKeyDown(KeyCode.Space) && _grounded) // Прыжок
				Jump();
        
		AnimationNinja();
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
		//_health.TakeDamage(attack);
    }

	/// <summary>
	/// Атака ПКМ
	/// </summary>
	private void AttackRBM()
    {
		animator.SetTrigger("SuperAttack");
		cooldownTimer = 0;
		_spooter.Shoot(Mathf.Sign(move.x));
    }

	/// <summary>
	/// Флип))
	/// </summary>
	public void Flip()
    {
		if (move.x < 0f)
			_spriteRenderer.flipX = true;
		else if (move.x > 0f)
			_spriteRenderer.flipX = false;
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
