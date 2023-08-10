using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class UI_Lobby : MonoBehaviour
{
    [SerializeField] private TMP_Text codeText;

    private void Start()
    {
        codeText.text = CreateAndJoinRooms.randomCreate;
    }
    public void StartGame()
    {
        //buraya sahalarýn türlerine göre if state gelcek
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LoadLevel("Hole1");
    }
}
