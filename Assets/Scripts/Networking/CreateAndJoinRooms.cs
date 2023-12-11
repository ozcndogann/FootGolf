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
    public GameObject MoneyPopup;
    public Button IGotIt;

    //public int turkeyfee, englandfee, hollandfee;
    private int coins;

    public void Update()
    {
        //Debug.Log("practice: " + practice + " versus: " + versus + " tournement: " + Tournament);
    }
    public void Start()
    {
        coins = PlayerPrefs.GetInt("Coins");

    }
    public void IsPractice()

    {
        practice = true;
        versus = false;
        Tournament = false;
        roomOptions.MaxPlayers = 1;
        PlayerPrefs.SetInt("MatchType", 0);

    }

    public void IsVersus()
    {
        practice = false;
        versus = true;
        Tournament = false;
        roomOptions.MaxPlayers = 2;
        PlayerPrefs.SetInt("MatchType", 1);

    }

    public void IsTournament()
    {
        practice = false;
        versus = false;
        Tournament = true;
        roomOptions.MaxPlayers = 4;
        PlayerPrefs.SetInt("MatchType", 2);
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

        
        if (Switch.index == 0 && coins >= FeesAndUI.turkeyfee)
        {
            if (practice || versus || Tournament)
            {
                randomCreate = GenerateRandomSixDigitNumber();
                // Setting the room property
                roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "roomCode", randomCreate } };
                roomOptions.CustomRoomPropertiesForLobby = new string[] { "roomCode" };
                PhotonNetwork.CreateRoom(randomCreate, roomOptions);
                PlayerPrefs.SetInt("Coins", coins -= FeesAndUI.turkeyfee);
            }
            else
            {
                panel.transform.gameObject.SetActive(true);
            }
            //coins -= turkeyfee;
        }
        else if (Switch.index == 1 && coins >= FeesAndUI.englandfee)
        {
            if (practice || versus || Tournament)
            {
                randomCreate = GenerateRandomSixDigitNumber();
                // Setting the room property
                roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "roomCode", randomCreate } };
                roomOptions.CustomRoomPropertiesForLobby = new string[] { "roomCode" };
                PhotonNetwork.CreateRoom(randomCreate, roomOptions);
                PlayerPrefs.SetInt("Coins", coins -= FeesAndUI.englandfee);
            }
            else
            {
                panel.transform.gameObject.SetActive(true);
            }
            //coins -= englandfee;
        }
        else if (Switch.index == 2 && coins >= FeesAndUI.hollandfee)
        {
            if (practice || versus || Tournament)
            {
                randomCreate = GenerateRandomSixDigitNumber();
                // Setting the room property
                roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "roomCode", randomCreate } };
                roomOptions.CustomRoomPropertiesForLobby = new string[] { "roomCode" };
                PhotonNetwork.CreateRoom(randomCreate, roomOptions);
                PlayerPrefs.SetInt("Coins", coins -= FeesAndUI.hollandfee);
            }
            else
            {
                panel.transform.gameObject.SetActive(true);
            }
            //coins -= hollandfee;
        }
        else
        {
            Debug.Log("paran yetersiz");
            MoneyPopup.SetActive(true);
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
        if (!PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Score"))
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "Score", 0 } });
        }
        PhotonNetwork.LoadLevel("Lobby");
    }
}
