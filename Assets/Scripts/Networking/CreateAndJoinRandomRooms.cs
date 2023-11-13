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
    public int turkeyfee, englandfee, hollandfee;
    private int coins;
    public void ClosePopup()
    {
        if (IGotIt == true)
        {
            Panel.gameObject.SetActive(false);
        }
    }
    public void Start()
    {
        //PlayerPrefs.GetInt("MatchType");

        coins = PlayerPrefs.GetInt("Coins");

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

    public void JoinRandomRoom()
    {
        int playerScore = PlayerPrefs.GetInt("Score");
        string scoreRange = GetScoreRange(playerScore);

        if (practice)
        {
            //PhotonNetwork.JoinRandomRoom(null ,1);
            PhotonHashTable expectedCustomRoomProperties = new PhotonHashTable();
            expectedCustomRoomProperties.Add("GameMode", Switch.index);
            expectedCustomRoomProperties.Add("ScoreRange", scoreRange);
            PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 1);
        }
        if (Switch.index == 0 && coins >= turkeyfee)
        {
            if (versus)
            {
                //PhotonNetwork.JoinRandomRoom(null, 2);
                PhotonHashTable expectedCustomRoomProperties = new PhotonHashTable();
                expectedCustomRoomProperties.Add("GameMode", Switch.index);
                expectedCustomRoomProperties.Add("ScoreRange", scoreRange);
                PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 2);
                PlayerPrefs.SetInt("Coins", coins -= turkeyfee);//2x çýkarabilir ama practice özel de olabilir, test etmek þart
            }
            else if (Tournament)
            {
                //PhotonNetwork.JoinRandomRoom(null, 4);
                PhotonHashTable expectedCustomRoomProperties = new PhotonHashTable();
                expectedCustomRoomProperties.Add("GameMode", Switch.index);
                expectedCustomRoomProperties.Add("ScoreRange", scoreRange);
                PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 4);
                PlayerPrefs.SetInt("Coins", coins -= turkeyfee);//2x çýkarabilir ama practice özel de olabilir, test etmek þart
            }
            else
            {
                Panel.transform.gameObject.SetActive(true);
            }
            //coins -= turkeyfee;
            
        }
        else if (Switch.index == 1 && coins >= englandfee)
        {
            if (versus)
            {
                //PhotonNetwork.JoinRandomRoom(null, 2);
                PhotonHashTable expectedCustomRoomProperties = new PhotonHashTable();
                expectedCustomRoomProperties.Add("GameMode", Switch.index);
                expectedCustomRoomProperties.Add("ScoreRange", scoreRange);
                PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 2);
                PlayerPrefs.SetInt("Coins", coins -= englandfee);
            }
            else if (Tournament)
            {
                //PhotonNetwork.JoinRandomRoom(null, 4);
                PhotonHashTable expectedCustomRoomProperties = new PhotonHashTable();
                expectedCustomRoomProperties.Add("GameMode", Switch.index);
                expectedCustomRoomProperties.Add("ScoreRange", scoreRange);
                PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 4);
                PlayerPrefs.SetInt("Coins", coins -= englandfee);
            }
            else
            {
                Panel.transform.gameObject.SetActive(true);
            }
            
        }
        else if (Switch.index == 2 && coins >= hollandfee)
        {
            if (versus)
            {
                //PhotonNetwork.JoinRandomRoom(null, 2);
                PhotonHashTable expectedCustomRoomProperties = new PhotonHashTable();
                expectedCustomRoomProperties.Add("GameMode", Switch.index);
                expectedCustomRoomProperties.Add("ScoreRange", scoreRange);
                PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 2);
                PlayerPrefs.SetInt("Coins", coins -= hollandfee);
            }
            else if (Tournament)
            {
                //PhotonNetwork.JoinRandomRoom(null, 4);
                PhotonHashTable expectedCustomRoomProperties = new PhotonHashTable();
                expectedCustomRoomProperties.Add("GameMode", Switch.index);
                expectedCustomRoomProperties.Add("ScoreRange", scoreRange);
                PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 4);
                PlayerPrefs.SetInt("Coins", coins -= hollandfee);
            }
            else
            {
                Panel.transform.gameObject.SetActive(true);
            }
            
        }
        else
        {
            Debug.Log("paran yetersiz");
        }
        
    }
    //Ranks
    private string GetScoreRange(int score)
    {
        if (score <= 50) return "0-50";
        else if (score <= 100) return "51-100";
        else if (score <= 150) return "101-150";
        else if (score <= 200) return "151-200";
        else if (score <= 250) return "201-250";
        else if (score <= 300) return "251-300";
        else if (score <= 350) return "301-350";
        else if (score <= 400) return "351-400";
        else if (score <= 450) return "401-450";
        else if (score <= 500) return "451-500";
        else return "Above 500"; // For scores above 500
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
        roomOptions.CustomRoomProperties = new PhotonHashTable();
        roomOptions.CustomRoomProperties.Add("GameMode", Switch.index);
        roomOptions.CustomRoomPropertiesForLobby = new string[] { "GameMode" };
        PhotonNetwork.CreateRoom(randomCreate, roomOptions);
    }

    private string GenerateRandomSixDigitNumber()
    {
        int randomNumber = Random.Range(100000, 1000000);
        return randomNumber.ToString();
    }
    public override void OnJoinedRoom()
    {
        if (!PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Score"))
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "Score", 0 } });
        }
        PhotonNetwork.LoadLevel("RandomLobby");
    }
    
}
