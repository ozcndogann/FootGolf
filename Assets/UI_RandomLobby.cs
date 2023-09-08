using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class UI_RandomLobby : MonoBehaviour
{
    [SerializeField] private GameObject StartButton;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
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
        if (PhotonNetwork.PlayerList.Length < 2 && CreateAndJoinRandomRooms.versus)
        {
            //eksik kisi ui
            Debug.Log("eksik kisi");
        }
        else if (PhotonNetwork.PlayerList.Length >= 3 && CreateAndJoinRandomRooms.Tournament)
        {
            //eksik kisi
            Debug.Log("eksik kisi");
        }
    }

    public void StartRandomGame()
    {
        //buraya sahalarýn türlerine göre if state gelcek
        if (PhotonNetwork.PlayerList.Length == 1 && CreateAndJoinRandomRooms.practice)
        {
            Debug.Log("önce");
            PhotonNetwork.LoadLevel("Hole1");
            Debug.Log("sonra");
        }
        else if (PhotonNetwork.PlayerList.Length == 2 && CreateAndJoinRandomRooms.versus)
        {
            PhotonNetwork.LoadLevel("Hole1");
        }
        else if (PhotonNetwork.PlayerList.Length >= 3 && CreateAndJoinRandomRooms.Tournament)
        {
            PhotonNetwork.LoadLevel("Hole1");
        }
    }
}
