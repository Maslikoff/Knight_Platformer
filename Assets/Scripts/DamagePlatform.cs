using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlatform : MonoBehaviour
{
    [SerializeField] private GameObject bloodParticles;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<Health>().TakeDamage(25);
    }
}
