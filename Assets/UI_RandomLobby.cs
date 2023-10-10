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
    public GameObject Panel;
    public Button IGotIt;
    public void ClosePopup()
    {
        if (IGotIt == true)
        {
            Panel.gameObject.SetActive(false);
        }
    }
        private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        if (PhotonNetwork.PlayerList.Length < 2 && CreateAndJoinRandomRooms.versus)
        {
            //eksik kisi ui
            Panel.transform.gameObject.SetActive(true);
        }
        else if (PhotonNetwork.PlayerList.Length <= 3 && CreateAndJoinRandomRooms.Tournament)
        {
            //eksik kisi
            Panel.transform.gameObject.SetActive(true);
        }
        else
        {
            canStart = false;
            
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
           
        }
        else if (PhotonNetwork.PlayerList.Length < 3 && CreateAndJoinRandomRooms.Tournament)
        {
            
        }
        else
        {
            if (!canStart /*&& PhotonNetwork.IsMasterClient*/)
            {
                canStart = true;
                StartCoroutine(WaitForStart());
            }
        }
        
    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(3.0f); 
        StartRandomGame();
    }
    public void StartRandomGame()
    {
        if (PhotonNetwork.PlayerList.Length == 1 && CreateAndJoinRandomRooms.practice)
        {
            if (Switch.index == 0)
            {
                PhotonNetwork.LoadLevel("Hole1");
            }
            else if (Switch.index == 1)
            {
                PhotonNetwork.LoadLevel("Hole3Rainy");
            }
            else if (Switch.index == 2)
            {
                PhotonNetwork.LoadLevel("Hole2Rainy");
            }
        }
        else if (PhotonNetwork.PlayerList.Length == 2 && CreateAndJoinRandomRooms.versus)
        {
            if (Switch.index == 0)
            {
                PhotonNetwork.LoadLevel("Hole1");
            }
            else if (Switch.index == 1)
            {
                PhotonNetwork.LoadLevel("Hole3Rainy");
            }
            else if (Switch.index == 2)
            {
                PhotonNetwork.LoadLevel("Hole2Rainy");
            }
        }
        else if (PhotonNetwork.PlayerList.Length >= 3 && CreateAndJoinRandomRooms.Tournament)
        {
            if (Switch.index == 0)
            {
                PhotonNetwork.LoadLevel("Hole1");
            }
            else if (Switch.index == 1)
            {
                PhotonNetwork.LoadLevel("Hole3Rainy");
            }
            else if (Switch.index == 2)
            {
                PhotonNetwork.LoadLevel("Hole2Rainy");
            }
        }











        //buraya sahalarýn türlerine göre if state gelcek
        //if (PhotonNetwork.PlayerList.Length == 1 && CreateAndJoinRandomRooms.practice)
        //{
        //    PhotonNetwork.LoadLevel("Hole1");
        //}
        //else if (PhotonNetwork.PlayerList.Length == 2 && CreateAndJoinRandomRooms.versus)
        //{
        //    PhotonNetwork.LoadLevel("Hole1");
        //}
        //else if (PhotonNetwork.PlayerList.Length >= 3 && CreateAndJoinRandomRooms.Tournament)
        //{
        //    PhotonNetwork.LoadLevel("Hole1");
        //}
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
