using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class In_Game_Panel : MonoBehaviour
{
    public GameObject Practice, Versus, Tournament;
    public TMP_Text OurVersusText, OurTournamentText;

    // Start is called before the first frame update
    void Start()
    {
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
        OurVersusText.text = PlayerPrefs.GetString("Username");
        OurTournamentText.text = PlayerPrefs.GetString("Username");

    }

    
}
