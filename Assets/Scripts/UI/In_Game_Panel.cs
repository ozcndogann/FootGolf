using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;
using Photon.Pun;
public class In_Game_Panel : MonoBehaviour
{
    public GameObject Practice, Versus, Tournament;
    public TMP_Text tournement_player1, tournement_player2, tournement_player3, tournement_player4, vs_player1, vs_player2;
    public TMP_Text tournement_player1_score, tournement_player2_score, tournement_player3_score, tournement_player4_score, vs_player1_score, vs_player2_score;
    ShotCounter ShotCounter;
    private GameObject ball;
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
            int playerScore = 0;

            if (player.CustomProperties.ContainsKey("Score"))
            {
                playerScore = (int)player.CustomProperties["Score"];
            }
            if (player.ActorNumber == 1)
            {
                tournement_player1.text = player.NickName;
                vs_player1.text = player.NickName;
                tournement_player1_score.text = playerScore.ToString();
                vs_player1_score.text = playerScore.ToString();
            }
            else if (player.ActorNumber == 2)
            {
                tournement_player2.text = player.NickName;
                vs_player2.text = player.NickName;
                tournement_player2_score.text = playerScore.ToString();
                vs_player2_score.text = playerScore.ToString();
            }
            else if (player.ActorNumber == 3)
            {
                tournement_player3.text = player.NickName;
                tournement_player3_score.text = playerScore.ToString();
            }
            else if (player.ActorNumber == 4)
            {
                tournement_player4.text = player.NickName;
                tournement_player4_score.text = playerScore.ToString();
            }
        }
        //if (PlayerPrefs.GetInt("MatchType") == 0)
        //{
        //    Practice.SetActive(true);

        //}

        //else if(PlayerPrefs.GetInt("MatchType") == 1)
        //{
        //    Versus.SetActive(true);
        //}
        //else if(PlayerPrefs.GetInt("MatchType") == 2)
        //{
        //    Tournament.SetActive(true);
        }
        //OurVersusText.text = PlayerPrefs.GetString("Username");
        //OurTournamentText.text = PlayerPrefs.GetString("Username");
    }

    

