using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class UI_Lobby : MonoBehaviour
{
    [SerializeField] public TMP_Text codeText;
    [SerializeField] private GameObject StartButton;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        codeText.text = CreateAndJoinRooms.randomCreate;
        if (PhotonNetwork.IsMasterClient)
        {
            StartButton.SetActive(true);
        }
        else
        {
            StartButton.SetActive(false);
        }
    }
    private void Update()
    {
        
        if (PhotonNetwork.PlayerList.Length < 2 && CreateAndJoinRooms.versus)
        {
            //eksik kisi ui
            Debug.Log("eksik kisi");
        }
        else if (PhotonNetwork.PlayerList.Length >= 3 && CreateAndJoinRooms.Tournament)
        {
            //eksik kisi
            Debug.Log("eksik kisi");
        }
    }
    public void StartGame()
    {
        //buraya sahalarýn türlerine göre if state gelcek
        if (PhotonNetwork.PlayerList.Length == 1 && CreateAndJoinRooms.practice)
        {
            PhotonNetwork.LoadLevel("Hole1");
        }
        else if (PhotonNetwork.PlayerList.Length == 2 && CreateAndJoinRooms.versus)
        {
            PhotonNetwork.LoadLevel("Hole1");
        }
        else if (PhotonNetwork.PlayerList.Length >= 3 && CreateAndJoinRooms.Tournament)
        {
            PhotonNetwork.LoadLevel("Hole1");
        }
    }

}
