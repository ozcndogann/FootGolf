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
    public GameObject MoneyPopupTR;
    public GameObject MoneyPopupENG;
    public GameObject MoneyPopupNL;
    public Button IGotIt;
    public static bool friendlyMatch;
    //public int turkeyfee, englandfee, hollandfee;
    private int coins;

    public void Update()
    {
        //Debug.Log("practice: " + practice + " versus: " + versus + " tournement: " + Tournament);
    }
    public void Start()
    {
        coins = PlayerPrefs.GetInt("Coins");
        friendlyMatch = false;
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
                friendlyMatch = true;


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
                friendlyMatch = true;
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
                friendlyMatch = true;
            }
            else
            {
                panel.transform.gameObject.SetActive(true);
            }
            //coins -= hollandfee;
        }
        else if (Switch.index == 0 && coins < FeesAndUI.turkeyfee)
        {
            Debug.Log("türkiyeye paran yetersiz");
            MoneyPopupTR.SetActive(true);
        }
        else if (Switch.index == 1 && coins < FeesAndUI.englandfee)
        {
            Debug.Log("ingiltereye paran yetersiz");
            MoneyPopupENG.SetActive(true);
        }
        else if (Switch.index == 2 && coins < FeesAndUI.hollandfee)
        {
            Debug.Log("hollandaya paran yetersiz");
            MoneyPopupNL.SetActive(true);
        }

    }
    private string GenerateRandomSixDigitNumber()
    {
        int randomNumber = Random.Range(100000, 1000000);
        return randomNumber.ToString();
    }
    public void JoinRoom()
    {
        friendlyMatch = true;
        if (joinInput.text != "")
        {
            PhotonNetwork.JoinRoom(joinInput.text);
        }
        else
        {
            Debug.Log("bi�i yaz uyar�s� ui gelmelijoin");
        }
    }
    public override void OnJoinedRoom()
    {
        if (!PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Score"))
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "Score", 0 } });
        }
        if (friendlyMatch)
        {
            PhotonNetwork.LoadLevel("Lobby");
            //friendlyMatch = false;
        }
        
        
    }
}
