using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusHP : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().PlusHP(25);
            gameObject.SetActive(false);
        }
    }
}
