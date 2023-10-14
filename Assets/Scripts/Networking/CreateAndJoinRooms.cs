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
        //Debug.Log("practice: " + practice + " versus: " + versus + " tournement: " + Tournament);
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

        if (practice || versus || Tournament)
        {
            randomCreate = GenerateRandomSixDigitNumber();
            PhotonNetwork.CreateRoom(randomCreate, roomOptions);

            // Get UI_InGame script
            UI_InGame uiInGameScript = FindObjectOfType<UI_InGame>();

            // Check if the script was found
            if (uiInGameScript != null)
            {
                // Get the PhotonView component from the same GameObject as the UI_InGame script
                PhotonView view = uiInGameScript.GetComponent<PhotonView>();

                // Check if the PhotonView component was found
                if (view != null)
                {
                    view.RPC("UpdateRoomCodeForClients", RpcTarget.AllBuffered, randomCreate);
                }
            }
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
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();

        UI_InGame uiInGameScript = FindObjectOfType<UI_InGame>();
        if (uiInGameScript != null)
        {
            PhotonView view = uiInGameScript.GetComponent<PhotonView>();
            if (view != null)
            {
                view.RPC("UpdateRoomCodeForClients", RpcTarget.AllBuffered, randomCreate);
            }
        }
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }
}
