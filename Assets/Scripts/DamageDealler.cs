using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealler : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private Collider2D prefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Damageable"))
            collision.gameObject.GetComponent<Enemis>().TakeDamageEnemy(damage);

        if(!collision.CompareTag("CameraConfiner"))
            Destroy(gameObject);

        if (collision.CompareTag("Interactive"))
            collision.gameObject.SetActive(false);
    }
}
