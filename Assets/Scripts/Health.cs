using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private Image Bar;

    private float currentHealth;
    private float fill;
    private bool isAlive;

    private void Awake()
    {
        currentHealth = maxHealth;
        isAlive = true;
    }

    private void Update()
    {
        Bar.fillAmount = fill / 100;
        fill = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(isAlive)
            CheckIsAlive();
    }

    private void CheckIsAlive()
    {
        if (currentHealth > 0)
            isAlive = true;
        else
        {
            isAlive = false;
            Destroy(gameObject);
        }
    }
}
