using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class UI_RandomLobby : MonoBehaviour
{
    //[SerializeField] private GameObject StartButton;
    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    StartButton.SetActive(true);
        //}
        //else
        //{
        //    StartButton.SetActive(false);
        //}
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
        else
        {
            StartCoroutine(MyCoroutine());
        }
    }

    public void StartRandomGame()
    {
        
    }

    IEnumerator MyCoroutine()
    {
        yield return new WaitForSeconds(2.0f);

        Debug.Log("Two seconds have passed.");

        //buraya sahalarýn türlerine göre if state gelcek
        if (PhotonNetwork.PlayerList.Length == 1 && CreateAndJoinRandomRooms.practice)
        {
            //StartCoroutine(MyCoroutine());
            PhotonNetwork.LoadLevel("Hole1");
        }
        else if (PhotonNetwork.PlayerList.Length == 2 && CreateAndJoinRandomRooms.versus)
        {
            //StartCoroutine(MyCoroutine());
            PhotonNetwork.LoadLevel("Hole1");
        }
        else if (PhotonNetwork.PlayerList.Length >= 3 && CreateAndJoinRandomRooms.Tournament)
        {
            //StartCoroutine(MyCoroutine());
            PhotonNetwork.LoadLevel("Hole1");
        }
    }
}
