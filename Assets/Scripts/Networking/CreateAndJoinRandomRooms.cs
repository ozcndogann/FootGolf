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
    public Button practiceBtn;
    public Button VersusBtn;
    public Button TournamentBtn;
    public void Start()
    {
        practice = false;
        versus = false;
        Tournament = false;
    }
    public void Update()
    {
        if (practice)
        {
            practiceBtn.animator.SetBool("Selected", true);
            VersusBtn.animator.SetBool("Normal", true);
            TournamentBtn.animator.SetBool("Normal", true);

        }
        if (versus)
        {
            VersusBtn.animator.SetBool("Selected", true);
            practiceBtn.animator.SetBool("Normal", true);
            TournamentBtn.animator.SetBool("Normal", true);

        }
        if (Tournament)
        {
            TournamentBtn.animator.SetBool("Selected", true);
            practiceBtn.animator.SetBool("Normal", true);
            VersusBtn.animator.SetBool("Normal", true);

        }
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
            PhotonNetwork.JoinRandomRoom();
        }
        else if (versus)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else if (Tournament)
        {
            PhotonNetwork.JoinRandomRoom();
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
        PhotonNetwork.LoadLevel("Lobby");
    }
}
