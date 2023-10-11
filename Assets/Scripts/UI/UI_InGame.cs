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
    Ball ball;
    public GameObject ReturnPanel;
    public GameObject MainMenuPanel;
    public Sprite OldImage1,OldImage2,OldImage3,OldImage4;
    public Sprite NewImage1, NewImage2, NewImage3, NewImage4;

    public void Start()
    {
        codeText.text = CreateAndJoinRooms.randomCreate;
        if (CreateAndJoinRooms.practice)
        {
            codeText.enabled = false;
        }
        else if (CreateAndJoinRooms.versus)
        {
            codeText.enabled = true;
        }
        else if (CreateAndJoinRooms.Tournament)
        {
            codeText.enabled = true;
        }
    }
    private void Update()
    {
        timeText.text = ((int)Ball.timer).ToString();
        if (CreateAndJoinRandomRooms.versus || CreateAndJoinRooms.versus)
        {
            if (PhotonNetwork.PlayerList.Length < 2)
            {
                //eksik kisi ui
                Debug.Log("eksik kisi");
                ReturnPanel.SetActive(true);
            }
            else
            {
                ReturnPanel.SetActive(false);
            }
        }
        

        if (CreateAndJoinRandomRooms.Tournament || CreateAndJoinRooms.Tournament)
        {
            if (PhotonNetwork.PlayerList.Length < 3)
            {
                //eksik kisi
                Debug.Log("eksik kisi");
                ReturnPanel.SetActive(true);
            }
            else
            {
                ReturnPanel.SetActive(false);
            }
        }
        //if (PhotonNetwork.PlayerList.Length < 2 && CreateAndJoinRandomRooms.versus)
        //{
        //    //eksik kisi ui
        //    Debug.Log("eksik kisi");
        //    ReturnPanel.SetActive(true);
        //    Time.timeScale = 0;
        //}

        //else if (PhotonNetwork.PlayerList.Length >= 3 && CreateAndJoinRandomRooms.Tournament)
        //{
        //    //eksik kisi
        //    Debug.Log("eksik kisi");
        //    ReturnPanel.SetActive(true);
        //    Time.timeScale = 0;
        //}
        //else
        //{
        //    ReturnPanel.SetActive(false);
        //    Time.timeScale = 1;
        //}
       if(Ball.Player1 == true)
        {
            GetComponent<SpriteRenderer>().sprite = NewImage1;

            if(Ball.Player1 == false)
            {
                GetComponent<SpriteRenderer>().sprite = OldImage1;

            }
            
        }
        else if (Ball.Player2 == true)
        {
            GetComponent<SpriteRenderer>().sprite = NewImage2;

            if (Ball.Player2 == false)
            {
                GetComponent<SpriteRenderer>().sprite = OldImage2;

            }

        }
        else if (Ball.Player3 == true)
        {
            GetComponent<SpriteRenderer>().sprite = NewImage3;

            if (Ball.Player3 == false)
            {
                GetComponent<SpriteRenderer>().sprite = OldImage3;

            }

        }
        else if (Ball.Player4 == true)
        {
            GetComponent<SpriteRenderer>().sprite = NewImage4;

            if (Ball.Player4 == false)
            {
                GetComponent<SpriteRenderer>().sprite = OldImage4;

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
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("MainMenu");
    }
}
