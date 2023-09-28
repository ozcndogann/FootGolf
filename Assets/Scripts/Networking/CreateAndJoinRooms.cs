using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public static bool practice;
    public static bool versus;
    public static bool Tournament;
    public TMP_InputField joinInput;
    private string characters = "0123456789";
    public static string randomCreate;
    public static RoomOptions roomOptions = new RoomOptions();
    public GameObject panel;
    public Button IGotIt;


    public void Update()
    {
        Debug.Log("practice: " + practice + " versus: " + versus + " tournement: " + Tournament);
    }

    public void IsPractice()

    {
        practice = true;
        versus = false;
        Tournament = false;
        roomOptions.MaxPlayers = 1;
    }

    public void IsVersus()
    {
        practice = false;
        versus = true;
        Tournament = false;
        roomOptions.MaxPlayers = 2;
    }

    public void IsTournament()
    {
        practice = false;
        versus = false;
        Tournament = true;
        roomOptions.MaxPlayers = 4;
    }
    public void ClosePopup()
    {
        if (IGotIt == true)
        {
            panel.gameObject.SetActive(false);
        }
    }

    public void CreateRoom()
    {

        if (practice || versus || Tournament)
        {
            randomCreate = GenerateRandomSixDigitNumber();
            PhotonNetwork.CreateRoom(randomCreate, roomOptions);
        }
        else
        {
            panel.transform.gameObject.SetActive(true);
        }


    }
    private string GenerateRandomSixDigitNumber()
    {
        int randomNumber = Random.Range(100000, 1000000);
        return randomNumber.ToString();
    }
    public void JoinRoom()
    {
        if (joinInput.text != "")
        {
            PhotonNetwork.JoinRoom(joinInput.text);
        }
        else
        {
            Debug.Log("biþi yaz uyarýsý ui gelmelijoin");
        }
    }
    public override void OnJoinedRoom()
    {
        //if (practice)
        //{
        //    PhotonNetwork.LoadLevel("Hole1");
        //}
        //else if (versus)
        //{
        //    PhotonNetwork.LoadLevel("Lobby");
        //}
        //else if (Tournament)
        //{
        //    PhotonNetwork.LoadLevel("Lobby");
        //}
        PhotonNetwork.LoadLevel("Lobby");
    }
}
