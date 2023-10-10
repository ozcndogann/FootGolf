using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

using PhotonHashTable = ExitGames.Client.Photon.Hashtable;
public class CreateAndJoinRandomRooms : MonoBehaviourPunCallbacks
{
   
    public static bool practice;
    public static bool versus;
    public static bool Tournament;
    public static string randomCreate;
    public static RoomOptions roomOptions = new RoomOptions();
    public GameObject Panel;
    public Button IGotIt;

    public void ClosePopup()
    {
        if (IGotIt == true)
        {
            Panel.gameObject.SetActive(false);
        }
    }
    public void Start()
    {
    }
    public void Update()
    {
        //Debug.Log("randompractice: " + practice + " randomversus: " + versus + " randomtournement: " + Tournament);
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

    public void JoinRandomRoom()
    {
        if (practice)
        {
            PhotonNetwork.JoinRandomRoom(null ,1);
        }
        else if (versus)
        {
            PhotonNetwork.JoinRandomRoom(null, 2);
        }
        else if (Tournament)
        {
            PhotonNetwork.JoinRandomRoom(null, 4);
        }
        else
        {
            Panel.transform.gameObject.SetActive(true);
        }
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
        CreateAndJoinRoom();
    }
    void CreateAndJoinRoom()
    {
        randomCreate = GenerateRandomSixDigitNumber();
        PhotonNetwork.CreateRoom(randomCreate, roomOptions);

    }
    private string GenerateRandomSixDigitNumber()
    {
        int randomNumber = Random.Range(100000, 1000000);
        return randomNumber.ToString();
    }
    public override void OnJoinedRoom()
    {
        //PhotonNetwork.LoadLevel("RandomLobby");
        if (Switch.index == 0)
        {
            PhotonNetwork.LoadLevel("RandomLobbyTR");
        }
        else if (Switch.index == 1)
        {
            PhotonNetwork.LoadLevel("RandomLobbyENG");
        }
        else if (Switch.index == 2)
        {
            PhotonNetwork.LoadLevel("RandomLobbyNED");
        }
    }
    
}
