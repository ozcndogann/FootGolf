using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    bool practice;
    bool versus;
    bool Tournament;
    public TMP_InputField joinInput;
    private string characters = "0123456789";
    public static string randomCreate;
    public static RoomOptions roomOptions = new RoomOptions();

    public void Start()
    {
        practice = false;
        versus = false;
        Tournament = false;
    }
    public void IsPractice()
    {
        practice = true;
        versus = false;
        Tournament = false;
        roomOptions.MaxPlayers = 1;
        Debug.Log("kisi sayisi:" + roomOptions.MaxPlayers);
    }
    public void IsVersus()
    {
        practice = false;
        versus = true;
        Tournament = false;
        roomOptions.MaxPlayers = 2;
        Debug.Log("kisi sayisi:" + roomOptions.MaxPlayers);
    }

    public void IsTournament()
    {
        practice = false;
        versus = false;
        Tournament = true;
        roomOptions.MaxPlayers = 4;
        Debug.Log("kisi sayisi:" + roomOptions.MaxPlayers);
    }

    public void CreateRoom()
    {
        Debug.Log(practice);
        Debug.Log(versus);
        Debug.Log(Tournament);

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
            Debug.Log("kisi sayisi mac basladi:" + roomOptions.MaxPlayers);
            Debug.Log(randomCreate);
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
