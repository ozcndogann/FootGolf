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

    // Start is called before the first frame update
    void Start()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            //if (player != localPlayer)
            //{
            //    otherPlayer = player;
            //    break;
            //}
            if (player.ActorNumber == 1)
            {
                tournement_player1.text = player.NickName;
                vs_player1.text = player.NickName;
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
        if (PlayerPrefs.GetInt("MatchType") == 0)
        {
            Practice.SetActive(true);

        }

        else if(PlayerPrefs.GetInt("MatchType") == 1)
        {
            Versus.SetActive(true);
        }
        else if(PlayerPrefs.GetInt("MatchType") == 2)
        {
            Tournament.SetActive(true);
        }
        //OurVersusText.text = PlayerPrefs.GetString("Username");
        //OurTournamentText.text = PlayerPrefs.GetString("Username");
    }

    
}
