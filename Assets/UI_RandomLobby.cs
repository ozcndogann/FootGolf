using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class UI_RandomLobby : MonoBehaviour
{
    [SerializeField] private GameObject StartButton;
    private bool canStart;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
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
        else
        {
            canStart = true;
        }
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    StartButton.SetActive(true);
        //}
        //else
        //{
        //    StartButton.SetActive(false);
        //}
    }
    private void Update()
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
        else
        {
            //
        }
        if (canStart && PhotonNetwork.IsMasterClient)
        {
            canStart = true;
            StartRandomGame();
        }
    }

    public void StartRandomGame()
    {
        //buraya sahalarýn türlerine göre if state gelcek
        if (PhotonNetwork.PlayerList.Length == 1 && CreateAndJoinRandomRooms.practice)
        {
            PhotonNetwork.LoadLevel("Hole1");
        }
        else if (PhotonNetwork.PlayerList.Length == 2 && CreateAndJoinRandomRooms.versus)
        {
            PhotonNetwork.LoadLevel("Hole1");
        }
        else if (PhotonNetwork.PlayerList.Length >= 3 && CreateAndJoinRandomRooms.Tournament)
        {
            PhotonNetwork.LoadLevel("Hole1");
        }
    }
}



//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Photon.Pun;
//using TMPro;
//using UnityEngine.UI;
//using Photon.Realtime;

//public class UI_RandomLobby : MonoBehaviour
//{
//    private GameObject ball;
//    private void Start()
//    {
//        PhotonNetwork.AutomaticallySyncScene = true;
//        ball = GameObject.FindGameObjectWithTag("Ball");
//        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "isReady", false } });
//        //if (PhotonNetwork.IsMasterClient)
//        //{
//        //    StartButton.SetActive(true);
//        //}
//        //else
//        //{
//        //    StartButton.SetActive(false);
//        //}
//    }
//    private void Update()
//    {
//        if (PhotonNetwork.PlayerList.Length < 2 && CreateAndJoinRandomRooms.versus)
//        {
//            //eksik kisi ui
//            Debug.Log("eksik kisi");
//        }
//        else if (PhotonNetwork.PlayerList.Length >= 3 && CreateAndJoinRandomRooms.Tournament)
//        {
//            //eksik kisi
//            Debug.Log("eksik kisi");
//        }
//        else
//        {
//            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "isReady", true } });
//        }
//    }


//    private void CheckAllPlayers()
//    {

//        StartCoroutine(DelayCheck(1f));
//    }

//    [PunRPC]
//    private void NotifyConditionMet()
//    {
//        StartCoroutine(LoadNextSceneWithDelay(1f));
//    }

//    private IEnumerator LoadNextSceneWithDelay(float delay)
//    {
//        yield return new WaitForSeconds(delay);
//        //PhotonNetwork.Destroy(gameObject);
//        //buraya sahalarýn türlerine göre if state gelcek
//        if (PhotonNetwork.PlayerList.Length == 1 && CreateAndJoinRandomRooms.practice)
//        {
//            //StartCoroutine(MyCoroutine());
//            PhotonNetwork.LoadLevel("Hole1");
//        }
//        else if (PhotonNetwork.PlayerList.Length == 2 && CreateAndJoinRandomRooms.versus)
//        {
//            //StartCoroutine(MyCoroutine());
//            PhotonNetwork.LoadLevel("Hole1");
//        }
//        else if (PhotonNetwork.PlayerList.Length >= 3 && CreateAndJoinRandomRooms.Tournament)
//        {
//            //StartCoroutine(MyCoroutine());
//            PhotonNetwork.LoadLevel("Hole1");
//        }
//    }
//    private IEnumerator DelayCheck(float delay)
//    {
//        yield return new WaitForSeconds(delay);
//        bool allPlayersReady = true;
//        foreach (Player player in PhotonNetwork.PlayerList)
//        {
//            if (!(bool)player.CustomProperties["isReady"])// holeC false mu check
//            {
//                allPlayersReady = false;
//                break;
//            }
//        }
//        if (allPlayersReady)
//        {
//            ball.GetComponent<PhotonView>().RPC("NotifyConditionMet", RpcTarget.All);//herkes ayný holeC bool statete
//        }
//    }
//}
