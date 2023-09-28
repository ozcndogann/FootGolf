using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Lobby_Left_Btn : MonoBehaviour
{
    public Button Home;


    public void PushHome()
    {
        if (Home == true)
        {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("MainMenu");
        }
    }
}
