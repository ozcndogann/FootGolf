using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;



public class SecondServerConnection : MonoBehaviourPunCallbacks
{
    
    private void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    //public override void OnConnectedToMaster()
    //{
    //    PhotonNetwork.JoinLobby();
    //}
    //public override void OnJoinedLobby()
    //{
    //    SceneManager.LoadScene("MainMenu");
    //}
}
