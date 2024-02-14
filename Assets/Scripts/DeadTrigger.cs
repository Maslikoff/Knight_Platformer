using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadTrigger : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<Health>().TakeDamage(1000);

        if (collision.gameObject.tag == "Enemy")
            collision.gameObject.GetComponent<Enemis>().TakeDamageEnemy(1000);
    }
}
