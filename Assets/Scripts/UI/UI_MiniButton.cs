using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_MiniButton : MonoBehaviour
{
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
    // Update is called once per frame
    void Update()
    {
    
    }
}
