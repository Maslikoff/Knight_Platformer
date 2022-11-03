using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    [Header("Объект")]
    [SerializeField] private GameObject Object;
    [Header("Партикл")]
    [SerializeField] private ParticleSystem particle;
    [Header("PointEffector")]
    [SerializeField] private PointEffector2D pointEffector;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BoxTNT"))
        {
            Destroy(Object, 1.5f);
            Instantiate(particle, transform.position, Quaternion.identity);
            Instantiate(pointEffector, transform.position, Quaternion.identity);
        }
    }
}
