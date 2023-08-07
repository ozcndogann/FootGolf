using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;
    
    public void CreateRoom()
    {
        if (createInput.text != "")
        {
            PhotonNetwork.CreateRoom(createInput.text);
        }
        else
        {
            Debug.Log("biþi yaz uyarýsý ui gelmeli");
        }
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
