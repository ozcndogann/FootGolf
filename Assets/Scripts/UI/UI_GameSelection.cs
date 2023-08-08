using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameSelection : MonoBehaviour
{
    bool practice;
    bool versus;
    bool Tournament;
    CreateAndJoinRooms CreateAndJoinRooms;
    [SerializeField] GameObject createJoinRoom; 
    public void Start()
    {
      practice = false;
        versus = false;
        Tournament = false;
        CreateAndJoinRooms = createJoinRoom.GetComponent<CreateAndJoinRooms>();
    }
    public void IsPractice()
    {
        practice = true;
        versus = false;
        Tournament = false;
        CreateAndJoinRooms.roomOptions.MaxPlayers = 1;

    }
    public void IsVersus()
    {
        practice = false;
        versus = true;
        Tournament = false;
        CreateAndJoinRooms.roomOptions.MaxPlayers = 2;
    }

    public void IsTournament()
    {
        practice = false;
        versus = false;
        Tournament = true;
        CreateAndJoinRooms.roomOptions.MaxPlayers = 2;
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
