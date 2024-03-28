using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCoin;

    private LevelManager level;
    private int coin = 0;

    private void Start()
    {
        coin = PlayerPrefs.GetInt("Coin");
        textCoin.text = coin.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            coin++;
            textCoin.text = coin.ToString();
            SavingDate();
            Destroy(collision.gameObject);
        }
    }
    void SavingDate()
    {
        PlayerPrefs.SetInt("Coin", coin); // << сохранение кол-во денег
    }
}
