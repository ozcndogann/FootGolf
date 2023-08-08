using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    private TMP_InputField createInput;
    public TMP_InputField joinInput;
    private string characters = "0123456789";
    private string randomCreate;

   
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = false;
        roomOptions.MaxPlayers = 2;
        for (int i = 0; i < 6; i++)
        {
            randomCreate += characters[Random.Range(0, characters.Length)];
        }
        Debug.Log(randomCreate);

        PhotonNetwork.CreateRoom(randomCreate, roomOptions);
        //if (createInput.text != "")
        //{
        //    PhotonNetwork.CreateRoom(createInput.text);
        //}
        //else
        //{
        //    Debug.Log("biþi yaz uyarýsý ui gelmeli");
        //}
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
        PhotonNetwork.LoadLevel("Hole1");
    }
}
