using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
public class UI_GameSelection : MonoBehaviour
{
    bool practice;
    bool versus;
    bool Tournament;
    private TMP_InputField createInput;
    public TMP_InputField joinInput;
    private string characters = "0123456789";
    private string randomCreate;
    public static RoomOptions roomOptions = new RoomOptions();
    public Button practiceBtn;
    public Button VersusBtn;
    public Button TournamentBtn;
    public void Start()
    {
        practice = false;
        versus = false;
        Tournament = false;
    }
    public void Update()
    {
        if (practice)
        {
            practiceBtn.animator.SetBool("Selected",true);
            VersusBtn.animator.SetBool("Selected", false);
            TournamentBtn.animator.SetBool("Selected", false);
        }
        else if (versus)
        {
            VersusBtn.animator.SetBool("Selected", true);
            practiceBtn.animator.SetBool("Selected", false);
            TournamentBtn.animator.SetBool("Selected", false);
        }
        else if (Tournament)
        {
            TournamentBtn.animator.SetBool("Selected", true);
            practiceBtn.animator.SetBool("Selected", false);
            VersusBtn.animator.SetBool("Selected", false);
        }
    }
    public void IsPractice()
    {
        practice = true;
        if (practice == true)
        {
            versus = false;
            Tournament = false;
        }

        
        

    }
    public void IsVersus()
    {
        practice = false;
        versus = true;
        Tournament = false;
    }

    public void IsTournament()
    {
        practice = false;
        versus = false;
        Tournament = true;
    }
    public void GameSelect()
    {
        if (practice)
        {
            SceneManager.LoadScene("Loading");
        }
        if (versus)
        {
            SceneManager.LoadScene("Loading");
        }
        if (Tournament)
        {
            SceneManager.LoadScene("Loading");
        }
    }
    public void CreateRoom()
    {
        roomOptions.IsVisible = false;
        roomOptions.MaxPlayers = 4;
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
   
}
