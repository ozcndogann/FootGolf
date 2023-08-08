using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using TMPro;

public class ServerConnection : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text warningConnectionFailed;
    private void Start()
    {
        //PhotonNetwork.GameVersion = "1.0.2";
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        //burda debug yerine bi pop up veririz
        warningConnectionFailed.text = "Disconnected from server because: " + cause.ToString();
    }
}
