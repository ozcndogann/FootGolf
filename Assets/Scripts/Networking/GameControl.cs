using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    void Update()
    {
        if (PhotonNetwork.PlayerList.Length < 2 && CreateAndJoinRandomRooms.versus)
        {
            //eksik kisi ui
            Debug.Log("eksik kisi");
        }
        else if (PhotonNetwork.PlayerList.Length >= 3 && CreateAndJoinRandomRooms.Tournament)
        {
            //eksik kisi
            Debug.Log("eksik kisi");
        }
    }
    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(3.0f);
        ReturnToManu();
    }
    public void ReturnToManu()
    {
        PhotonNetwork.LoadLevel("MainMenu");
    }
}
