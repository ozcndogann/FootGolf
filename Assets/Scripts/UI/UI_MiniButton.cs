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
    // Start is called before the first frame update
    public void PushHome()
    {
        if (Home == true)
        {
            
            SceneManager.LoadScene("MainMenu");
        }
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
