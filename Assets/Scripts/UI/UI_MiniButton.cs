using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_MiniButton : MonoBehaviour
{
    //public GameObject MusicPanel;
    public Button Home;
    public Button Raiting;
    public Button Settings;
    public Button Shop;
    public Button Player;
    public GameObject SinglePlayer;
    public GameObject MultiPlayer;
    public GameObject SingleOptions;
    public GameObject MultiOptions;
    // Start is called before the first frame update
    public void Awake() 
    {
        SinglePlayer.SetActive(true);
        MultiPlayer.SetActive(true);
        SingleOptions.SetActive(false);
        MultiOptions.SetActive(false);
    }
    public void PushHome()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void PushShop()
    {
       
        SceneManager.LoadScene("Shop");
    }
    public void PushPlayer()
    {
        SceneManager.LoadScene("Player");
    }
    public void PushSettings()
    {
        //MusicPanel.SetActive(true);
        SceneManager.LoadScene("Settings");
    }
    public void MultiPlayerButton()
    {
        MultiOptions.SetActive(true);
        SingleOptions.SetActive(false);
        MultiPlayer.SetActive(false);
        SinglePlayer.SetActive(false);
    }
    public void SinglePlayerButton()
    {
        SingleOptions.SetActive(true);
        MultiOptions.SetActive(false);
        MultiPlayer.SetActive(false);
        SinglePlayer.SetActive(false);
    }

    public void PushLeaderboard() 
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ParaKas()
    {
        int totalCoins = PlayerPrefs.GetInt("Coins", 0);

        PlayerPrefs.SetInt("Coins", totalCoins += 100);
        Debug.Log("paraa");

    }
    //public void CloseSettings()
    //{
    //    MusicPanel.SetActive(false);
    //}

    // Update is called once per frame
    void Update()
    {
    
    }
}
