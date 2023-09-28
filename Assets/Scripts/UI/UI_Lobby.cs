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
    private GameObject window;
    private Queue<string> popupQueue;
    public GameObject Panel;
    public Button IGotIt;


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
    public void ClosePopup()
    {
        if (IGotIt == true)
        {
            Panel.gameObject.SetActive(false);
        }
    }
    public void StartGame()
    {
        //buraya sahalarýn türlerine göre if state gelcek
        if (PhotonNetwork.PlayerList.Length == 1 && CreateAndJoinRooms.practice)
        {
            if (Switch.index == 0)
            {
                PhotonNetwork.LoadLevel("Hole1");
            }
            else if (Switch.index == 1)
            {
                PhotonNetwork.LoadLevel("Hole3Rainy");
            }
            else if (Switch.index == 2)
            {
                PhotonNetwork.LoadLevel("Hole2Rainy");
            }
            

        }
        else if (PhotonNetwork.PlayerList.Length == 2 && CreateAndJoinRooms.versus)
        {
            if (Switch.index == 0)
            {
                PhotonNetwork.LoadLevel("Hole1");
            }
            else if (Switch.index == 1)
            {
                PhotonNetwork.LoadLevel("Hole3Rainy");
            }
            else if (Switch.index == 2)
            {
                PhotonNetwork.LoadLevel("Hole2Rainy");
            }
           
        }
        

        else if (PhotonNetwork.PlayerList.Length >= 3 && CreateAndJoinRooms.Tournament)
        {
            if (Switch.index == 0)
            {
                PhotonNetwork.LoadLevel("Hole1");
            }
            else if (Switch.index == 1)
            {
                PhotonNetwork.LoadLevel("Hole3Rainy");
            }
            else if (Switch.index == 2)
            {
                PhotonNetwork.LoadLevel("Hole2Rainy");
            }
           
        }
        else
        {
            Panel.transform.gameObject.SetActive(true);
        }
    }

}
