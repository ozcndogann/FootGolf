using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            WaitForStart();
        }
        else if (PhotonNetwork.PlayerList.Length >= 3 && CreateAndJoinRandomRooms.Tournament)
        {
            //eksik kisi
            Debug.Log("eksik kisi");
            WaitForStart();
        }
    }
    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(3.0f);
        ReturnToManu();
    }
    public void ReturnToManu()
    {
        PhotonNetwork.LeaveRoom();
        //PhotonNetwork.LoadLevel("MainMenu");
        SceneManager.LoadScene("MainMenu");
    }
}
