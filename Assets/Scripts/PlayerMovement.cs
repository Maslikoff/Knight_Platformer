using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Скорость")]
	[Range(0,10f)][SerializeField] private float speed = 5f;
    [Header("Сила прыжка")]
	[Range(0, 15f)][SerializeField] private float JampForse = 5f;

	public Animator animator;

	private Rigidbody2D _rigidbody;
	private SpriteRenderer _spriteRenderer;

	private bool _grounded;

	private Vector2 move;

    private void Start()
    {
		_rigidbody = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	private void FixedUpdate()
	{
		_rigidbody.velocity = new Vector2 (move.x, move.y);

		Flip();
	}

    private void Update()
    {
		move.x = Input.GetAxisRaw("Horizontal") * speed;
		move.y = _rigidbody.velocity.y;

		animator.SetFloat("Speed", Mathf.Abs(move.x));
		animator.SetBool("Grounded", _grounded);

		if (Input.GetButtonDown("Fire1"))
			Attack();
		
		if (Input.GetKeyDown(KeyCode.Space) && _grounded)
			MoveJamp();
	}
	
	/// <summary>
	/// Прыжок
	/// </summary>
	private void MoveJamp()
	{
		_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JampForse);
		animator.SetTrigger("Jump");
		_grounded = false;
	}

	/// <summary>
	/// Атака
	/// </summary>
	private void Attack()
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
			_grounded = true;
    }
}
