using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class GameEnder : MonoBehaviour
{
    

    void Update()
    {
        if (Ball.gameEnder)
        {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("MainMenu");
            
        }
    }
}
