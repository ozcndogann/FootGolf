using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    bool practice;
    bool versus;
    bool Tournament;
    public TMP_InputField joinInput;
    private string characters = "0123456789";
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

    public void CreateRoom()
    {

        //practice = false;
        //versus = false;
        //Tournament = false;
        //for (int i = 0; i < 6; i++)
        //{
        //    randomCreate += characters[Random.Range(0, characters.Length)];
        //}
        if (practice || versus || Tournament)
        {
            randomCreate = GenerateRandomSixDigitNumber();
            PhotonNetwork.CreateRoom(randomCreate, roomOptions);
        }
        else
        {
            //lütfen bi mod seçin pop up'ý
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
        PhotonNetwork.LoadLevel("Lobby");
    }
}
