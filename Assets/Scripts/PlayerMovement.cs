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

	private Vector2 move;

    void Start()
    {
		_rigidbody = GetComponent<Rigidbody2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
		move.x = Input.GetAxisRaw("Horizontal");

		animator.SetFloat("Speed", Mathf.Abs(move.x));

		Attack();
	}

	private void Attack()
    {
		if (Input.GetButtonDown("Fire1"))
		{
			animator.SetTrigger("Attack");
		}
    }

	private void FixedUpdate()
	{
		_rigidbody.MovePosition(_rigidbody.position + move * speed * Time.fixedDeltaTime);

		if (move.x < 0f)
		{
			_spriteRenderer.flipX = true;
		} else if (move.x > 0f)
		{
			_spriteRenderer.flipX = false;
		}

		MoveJamp();
	}

	private void MoveJamp()
	{
		if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(_rigidbody.velocity.y) < 0.05f)
			_rigidbody.AddForce(transform.up * JampForse, ForceMode2D.Impulse);
	}
}
