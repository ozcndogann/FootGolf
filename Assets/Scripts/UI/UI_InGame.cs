using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
public class UI_InGame : MonoBehaviour
{
    [SerializeField] public TMP_Text codeText;
    [SerializeField] public TMP_Text timeText;
    [SerializeField] public TMP_Text pingText;
    public GameObject PingPanel;
    public GameObject ReturnPanel;
    public GameObject MainMenuPanel;
    public Image OldImage1,OldImage2,OldImage3,OldImage4;
    public Sprite NewImage1, NewImage2, NewImage3, NewImage4;
    public GameObject CodeSprite;
    
    
   
    public Sprite OldSprite1, OldSprite2, oldsprite3, oldsprite4;

    private GameObject ball;
    public void Start()
    {
        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("roomCode", out object roomCodeValue))
        {
            codeText.text = roomCodeValue.ToString();
        }
        //if (CreateAndJoinRooms.practice)
        //{
        //    codeText.enabled = false;
        //}
        else if (CreateAndJoinRooms.versus)
        {
            codeText.enabled = true;
        }
        else if (CreateAndJoinRooms.Tournament)
        {
            codeText.enabled = true;
        }
        ball = GameObject.FindGameObjectWithTag("Ball");

        if (GoFriendLobby.RandomGame)
        {
            CodeSprite.SetActive(false);
        }
        else
        {
            //CodeSprite.SetActive(true);
        }
    }
    private void Update()
    {
        
        
        timeText.text = ((int)Ball.timer).ToString();
        pingText.text = PhotonNetwork.GetPing().ToString() + " ms";
        if (PhotonNetwork.GetPing() > 1000 || PhotonNetwork.IsConnected==false)
        {
            PingPanel.SetActive(true);
        }
        if (CreateAndJoinRandomRooms.versus || CreateAndJoinRooms.versus)
        {
            //IsVersus.gameObject.SetActive(true);
            if (PhotonNetwork.PlayerList.Length < 2)
            {
                //eksik kisi ui
                Debug.Log("eksik kisi");
                ReturnPanel.SetActive(true);
                PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", false } });
            }
            else
            {
                ReturnPanel.SetActive(false);
            }
        }
        

        if (CreateAndJoinRandomRooms.Tournament || CreateAndJoinRooms.Tournament)
        {
            //IsTournament.gameObject.SetActive(true);
            if (PhotonNetwork.PlayerList.Length < 4)
            {
                //eksik kisi
                Debug.Log("eksik kisi");
                ReturnPanel.SetActive(true);
                PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", false } });
            }
            else
            {
                ReturnPanel.SetActive(false);
            }
           
        }
        if (CreateAndJoinRandomRooms.versus || CreateAndJoinRooms.versus || CreateAndJoinRandomRooms.Tournament || CreateAndJoinRooms.Tournament)
        {
            
            if (Ball.Player1 == true)
            {
                OldImage1.sprite = NewImage1;
                OldImage2.sprite = OldSprite2;
                if (CreateAndJoinRandomRooms.Tournament || CreateAndJoinRooms.Tournament)
                {
                    OldImage3.sprite = oldsprite3;
                    OldImage4.sprite = oldsprite4;
                }



            }
            else if (Ball.Player2 == true)
            {
                OldImage2.sprite = NewImage2;
                OldImage1.sprite = OldSprite1;
                if (CreateAndJoinRandomRooms.Tournament || CreateAndJoinRooms.Tournament)
                {
                    OldImage3.sprite = oldsprite3;
                    OldImage4.sprite = oldsprite4;
                }




            }
            else if (Ball.Player3 == true)
            {
                OldImage3.sprite = NewImage3;
                OldImage4.sprite = oldsprite4;
                if (CreateAndJoinRandomRooms.Tournament || CreateAndJoinRooms.Tournament)
                {
                    OldImage2.sprite = OldSprite2;
                    OldImage1.sprite = OldSprite1;
                }





            }
            else if (Ball.Player4 == true)
            {
                OldImage4.sprite = NewImage4;
                OldImage1.sprite = OldSprite1;
                if (CreateAndJoinRandomRooms.Tournament || CreateAndJoinRooms.Tournament)
                {
                    OldImage2.sprite = OldSprite2;
                    OldImage3.sprite = oldsprite3;
                }


            }
        }
       
        


    }
    public void OpenPanel()
    {
        MainMenuPanel.SetActive(true);
    }
    public void ClosePanel()
    {
        MainMenuPanel.SetActive(false);
    }
    public void MainMenu()
    {
        ShotCounter.ShotCount = 0;
        #region chaBools
        ChallengeType.cha1 = false;
        ChallengeType.cha2 = false;
        ChallengeType.cha3 = false;
        ChallengeType.cha4 = false;
        ChallengeType.cha5 = false;
        ChallengeType.cha6 = false;
        ChallengeType.cha7 = false;
        ChallengeType.cha8 = false;
        ChallengeType.cha9 = false;
        ChallengeType.cha10 = false;
        ChallengeType.cha11 = false;
        ChallengeType.cha12 = false;
        ChallengeType.cha13 = false;
        ChallengeType.cha14 = false;
        ChallengeType.cha15 = false;
        ChallengeType.cha16 = false;
        ChallengeType.cha17 = false;
        ChallengeType.cha18 = false;
        #endregion
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("MainMenu");
    }
}
