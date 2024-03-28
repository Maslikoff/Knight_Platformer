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
    [SerializeField] private GameObject bloodParticles;

    private Rigidbody2D _rigidbody;

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

    private void Update()
    {
        CheckTimeRevert();

        switch (currentState)
        {
            case IDLE_STATE:
                currentTimeToRevert += Time.deltaTime;
                break;
            case WALK_STATE:
                _rigidbody.velocity = Vector2.right * Speed;
                break;
            case REVERT_STATE:
                spriteRenderer.flipX = !spriteRenderer.flipX;
                Speed *= -1;
                currentState = WALK_STATE;
                break;
        }

        animator.SetFloat("Velocity", _rigidbody.velocity.magnitude);
    }

    private void CheckTimeRevert()
    {
        if (currentTimeToRevert >= TimeToRevert)
        {
            currentTimeToRevert = 0;
            currentState = REVERT_STATE;
        }
    }

    public void TakeDamageEnemy(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            Instantiate(bloodParticles, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EnemyStoper"))
            currentState = IDLE_STATE;
    }
}
