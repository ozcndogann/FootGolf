using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class UI_Lobby : MonoBehaviour
{
    [SerializeField] public TMP_Text codeText;
    [SerializeField] private Button StartButton;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        codeText.text = CreateAndJoinRooms.randomCreate;
        if (PhotonNetwork.IsMasterClient)
        {
            StartButton.enabled = true;
        }
        else
        {
            StartButton.enabled = false;
        }
    }
    public void StartGame()
    {
        //buraya sahalarýn türlerine göre if state gelcek
        PhotonNetwork.LoadLevel("Hole1");
    }
}
