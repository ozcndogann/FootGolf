using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class In_Game_Panel : MonoBehaviour
{
    public GameObject Practice, Versus, Tournament;
    public TMP_Text tournement_player1, tournement_player2, tournement_player3, tournement_player4, vs_player1, vs_player2, practice_p1;
    public TMP_Text tournement_player1_score, tournement_player2_score, tournement_player3_score, tournement_player4_score, vs_player1_score, vs_player2_score, practice_p1_score;
    ShotCounter ShotCounter;
    private GameObject ball;
     int playerScore;
    bool dene;
    void Start()
    {
        if (CreateAndJoinRandomRooms.versus || CreateAndJoinRooms.versus)
        {
            Versus.SetActive(true);
        }
        if (CreateAndJoinRandomRooms.practice || CreateAndJoinRooms.practice)
        {
            Practice.SetActive(true);
        }
        if (CreateAndJoinRandomRooms.Tournament || CreateAndJoinRooms.Tournament)
        {
            Tournament.SetActive(true);
        }
        ball = GameObject.FindGameObjectWithTag("Ball");
        ShotCounter = ball.GetComponent<ShotCounter>();
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.ActorNumber == 1)
            {
                tournement_player1.text = player.NickName;
                vs_player1.text = player.NickName;
                practice_p1.text = player.NickName;
            }
            else if (player.ActorNumber == 2)
            {
                tournement_player2.text = player.NickName;
                vs_player2.text = player.NickName;
            }
            else if (player.ActorNumber == 3)
            {
                tournement_player3.text = player.NickName;
            }
            else if (player.ActorNumber == 4)
            {
                tournement_player4.text = player.NickName;
            }
        }
    }   
    private void Update()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {

            if (player.CustomProperties.ContainsKey("Score"))
            {
                playerScore = (int)player.CustomProperties["Score"];
            }
            if (player.ActorNumber == 1)
            {
                tournement_player1_score.text = playerScore.ToString();
                vs_player1_score.text = playerScore.ToString();
                practice_p1_score.text = playerScore.ToString();
            }
            else if (player.ActorNumber == 2)
            {
                tournement_player2_score.text = playerScore.ToString();
                vs_player2_score.text = playerScore.ToString();
            }
            else if (player.ActorNumber == 3)
            {
                tournement_player3_score.text = playerScore.ToString();
            }
            else if (player.ActorNumber == 4)
            {
                tournement_player4_score.text = playerScore.ToString();
            }
        }

    }
    public void MainMenu()
    {
        ShotCounter.ShotCount = 0;
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("MainMenu");
    }
}

    

