using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class GameEnder : MonoBehaviour
{
    [SerializeField] private int firstHolePar;
    [SerializeField] private int secondHolePar;
    private int score;
    public static bool EndGame;

    private void Start()
    {
        EndGame = true;
    }
    void Update()
    {
        if (Ball.gameEnder)
        {
            CalculateScore();
            Debug.Log("Score: " + score);
            //ShotCounter.ShotCount = 0;
            //PhotonNetwork.LeaveRoom();
            //SceneManager.LoadScene("MainMenu");
        }
    }
    private void CalculateScore()
    {
        score =  ShotCounter.ShotCount - (firstHolePar + secondHolePar);
    }
}
