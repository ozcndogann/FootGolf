using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameSelection : MonoBehaviour
{
    bool practice;
    bool versus;
    bool Tournament;
    public void Start()
    {
      practice = false;
        versus = false;
        Tournament = false;
    }
    public void IsPractice()
    {
        practice = true;
        versus = false;
        Tournament = false;

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
}
