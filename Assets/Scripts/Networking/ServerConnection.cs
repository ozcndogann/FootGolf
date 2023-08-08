using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class ServerConnection : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.GameVersion = "1.0.2";
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        //burda debug yerine bi pop up veririz

        Debug.Log("Disconnected from server because: " + cause.ToString());
    }
}
