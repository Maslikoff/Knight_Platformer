using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemis : MonoBehaviour
{
    [SerializeField] private float Speed, TimeToRevert;
    [SerializeField] private float damage;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Rigidbody2D _rigidbody;
    private Vector2 _move;

    private const float IDLE_STATE = 0;
    private const float WALK_STATE = 1;
    private const float REVERT_STATE = 2;

    private float currentHealth, currentState, currentTimeToRevert;

    private void Start()
    {
        currentState = WALK_STATE;
        currentTimeToRevert = 0;
        _rigidbody = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    private void FixedUpdate()
    {
        if (currentTimeToRevert >= TimeToRevert)
        {
            currentTimeToRevert = 0;
            currentState = REVERT_STATE;
        }
        _move.x = Vector2.left.x * Speed;
        _move.y = _rigidbody.velocity.y;

        switch (currentState)
        {
            case IDLE_STATE:
                currentTimeToRevert += Time.deltaTime;
                break;
            case WALK_STATE:
                _rigidbody.MovePosition(_move);
                break;
            case REVERT_STATE:
                spriteRenderer.flipX = !spriteRenderer.flipX;
                Speed *= -1;
                currentState = WALK_STATE;
                break;
        }

        animator.SetFloat("Velocity", _rigidbody.velocity.magnitude);
    }

    public void TakeDamageEnemy(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy Stoper"))
            currentState = IDLE_STATE;
    }
}
