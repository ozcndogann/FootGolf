using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinDisplay : MonoBehaviour
{
    public TMP_Text totalCoinText;

    void Update()
    {
        totalCoinText.text = PlayerPrefs.GetInt("Coins").ToString();
    }
}