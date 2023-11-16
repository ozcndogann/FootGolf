using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinDisplay : MonoBehaviour
{
    public TMP_Text totalCoinText;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Coins"))
        {
            PlayerPrefs.SetInt("Coins", 200);
        }
    }
    void Update()
    {
        totalCoinText.text = PlayerPrefs.GetInt("Coins").ToString();
    }
}