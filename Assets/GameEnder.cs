using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class GameEnder : MonoBehaviour
{
    [SerializeField] private int firstHolePar;
    [SerializeField] private int secondHolePar;
    private int score;
    public static bool EndGame;
    Player player;
    private GameObject ball;
    ShotCounter ShotCounter;
    [SerializeField] private TMP_Text scoreDisplayText;  // Drag your Text UI element here in the Inspector
    public GameObject Panel;
    
    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        ShotCounter = ball.GetComponent<ShotCounter>();
        Panel.SetActive(false);
        EndGame = true;
    }
    void Update()
    {
        if (Ball.gameEnder)
        {
            CalculateScore();
            UpdateScoreDisplay();
            Panel.SetActive(true);
            //ShotCounter.ShotCount = 0;
            //PhotonNetwork.LeaveRoom();
            //SceneManager.LoadScene("MainMenu");
        }
    }
    private void CalculateScore()
    {
        score = ShotCounter.ShotCount - (firstHolePar + secondHolePar);

        // Set the score as a custom property
        ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();
        customProperties["FinalScore"] = score;
        PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);
    }
    private void UpdateScoreDisplay()
    {
        string scoreText = "";
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            int playerScore = 0;
            if (p.CustomProperties.ContainsKey("FinalScore"))
            {
                playerScore = (int)p.CustomProperties["FinalScore"];
            }
            scoreText += p.ActorNumber + " Score: " + playerScore + "\n";
        }
        scoreDisplayText.text = scoreText;
    }
}
