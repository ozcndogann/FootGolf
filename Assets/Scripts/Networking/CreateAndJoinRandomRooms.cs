using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
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
            PhotonNetwork.JoinRandomOrCreateRoom(expectedMaxPlayers:1);
        }
        else if (versus)
        {
            PhotonNetwork.JoinRandomOrCreateRoom(expectedMaxPlayers: 2);
        }
        else if (Tournament)
        {
            PhotonNetwork.JoinRandomOrCreateRoom(expectedMaxPlayers: 4);
        }
        
    }
    //public void CreateRoom()
    //{
    //    if (practice || versus || Tournament)
    //    {
    //        randomCreate = GenerateRandomSixDigitNumber();
    //        PhotonNetwork.CreateRoom(randomCreate, roomOptions);
    //    }
    //    else
    //    {
    //        //lütfen bi mod seçin pop up'ý
    //    }


    //}
    //private string GenerateRandomSixDigitNumber()
    //{
    //    int randomNumber = Random.Range(100000, 1000000);
    //    return randomNumber.ToString();
    //}
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
