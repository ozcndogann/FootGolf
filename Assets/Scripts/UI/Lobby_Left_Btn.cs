using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Lobby_Left_Btn : MonoBehaviour
{
    public Button Home;
    public Button Yes;
    public Button No;
    public GameObject Panel;


   
    public void OpenPopUp()
    {
        if (Home == true)
        {
            Panel.gameObject.SetActive(true); 
        }
    
    
    }
    public void YesPopUp()
    {
        if (Yes == true)
        {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("MainMenu");
        }
        
    }
    public void NoPopUp()
    {
        if (No == true)
        {
            Panel.gameObject.SetActive(false);
        }
    }

}
