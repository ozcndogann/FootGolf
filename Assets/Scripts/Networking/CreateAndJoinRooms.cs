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
            Debug.Log("bi�i yaz uyar�s� ui gelmeli");
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
            Debug.Log("bi�i yaz uyar�s� ui gelmelijoin");
        }
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Hole1");
    }
}
