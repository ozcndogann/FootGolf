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
            ShotCounter.ShotCount = 0;
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("MainMenu");
            
        }
    }
}
