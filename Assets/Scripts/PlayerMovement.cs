using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Скорость")]
	[Range(0,10f)][SerializeField] private float speed = 5f;
    [Header("Сила прыжка")]
	[Range(0, 15f)][SerializeField] private float JampForse = 5f;
	[Header("Слой Земли")]
	[SerializeField] private LayerMask groundLayer;
	[Header("Слой Стены")]
	[SerializeField] private LayerMask wallLayer;

	public Animator animator;

	private BoxCollider2D _boxCollider;
	private Rigidbody2D _rigidbody;
	private SpriteRenderer _spriteRenderer;

	private float wallJumpColdown;

	private Vector2 move;

    private void Start()
    {
		_rigidbody = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_boxCollider = GetComponent<BoxCollider2D>();
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
		move.x = Input.GetAxisRaw("Horizontal") * speed;
		move.y = _rigidbody.velocity.y;
		
    }

    private void Update()
    {
		if (Input.GetButtonDown("Fire1")) // Нажатие ЛКМ
			AttackRMB();

        if (wallJumpColdown > 0.2f)
        {
			_rigidbody.velocity = new Vector2 (move.x, move.y);

			if (onWall() && !isGrounded())
			{
				_rigidbody.gravityScale = 0;
				_rigidbody.velocity = Vector2.zero;
			}
			else
				_rigidbody.gravityScale = 7;
			
			if (Input.GetKeyDown(KeyCode.Space)) // Прыжок
				Jump();
        }
		else
			wallJumpColdown += Time.deltaTime;
		
		AnimationNinja();
	}

	/// <summary>
	/// Анимация
	/// </summary>
	private void AnimationNinja()
    {
		animator.SetFloat("Speed", Mathf.Abs(move.x));
		animator.SetBool("Grounded",isGrounded());
    }
	
	/// <summary>
	/// Прыжок
	/// </summary>
	private void Jump()
	{
		if (isGrounded())
        {
			_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JampForse);
			animator.SetTrigger("Jump");
        }
		else if(onWall() && !isGrounded())
        {
            if (move.x == 0)
            {
				_rigidbody.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
				transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			}
			else
				_rigidbody.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

			wallJumpColdown = 0;
        }
	}

	/// <summary>
	/// Атака
	/// </summary>
	private void AttackRMB()
    {
		animator.SetTrigger("Attack");
    }

	/// <summary>
	/// Флип))
	/// </summary>
	private void Flip()
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
        
    }

	private bool isGrounded()
    {
		RaycastHit2D raycastHit = Physics2D.BoxCast(
			_boxCollider.bounds.center, 
			_boxCollider.bounds.size, 
			0, Vector2.down, 0.1f, 
			groundLayer);

		return raycastHit.collider != null;
    }

	private bool onWall()
    {
		RaycastHit2D raycastHit = Physics2D.BoxCast(
			_boxCollider.bounds.center,
			_boxCollider.bounds.size,
			0,
			new Vector2(transform.localScale.x, 0), 
			0.1f,
			wallLayer);

		return raycastHit.collider != null;
	}
}
