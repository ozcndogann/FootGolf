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
    public static bool EndGamePanelOpen;
    Player player;
    private GameObject ball;
    //Ball ballScript;
    ShotCounter ShotCounter;
    [SerializeField] private TMP_Text scoreDisplayText;  // Drag your Text UI element here in the Inspector
    public GameObject Panel;
    [SerializeField] private Transform scoreDisplayParent, scoreDisplayParent1, scoreDisplayParent2, scoreDisplayParent3;  // Drag the parent object (like a Vertical Layout Group) here
    [SerializeField] private GameObject playerScorePrefab;
    private bool hasProcessed = false, hasProcessed1 = false, hasProcessed2 = false, hasProcessed3 = false;
    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        //ballScript = ball.GetComponent<Ball>();
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
            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", false } });
        }
    }
    private void CalculateScore()
    {
        score = ShotCounter.ShotCount - (firstHolePar + secondHolePar);

        // Set the score as a custom property
        ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();
        customProperties["FinalScore"] = score;
        PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties); 
        EndGamePanelOpen = true;
    }
    private void UpdateScoreDisplay()
    {
        // Clear existing displays.
        foreach (Transform child in scoreDisplayParent)
        {
            Destroy(child.gameObject);
        }

        // Sort players based on scores
        List<Player> playersSorted = new List<Player>(PhotonNetwork.PlayerList);
        playersSorted.Sort((player1, player2) =>
        {
            int score1 = player1.CustomProperties.ContainsKey("FinalScore") ? (int)player1.CustomProperties["FinalScore"] : int.MaxValue;
            int score2 = player2.CustomProperties.ContainsKey("FinalScore") ? (int)player2.CustomProperties["FinalScore"] : int.MaxValue;
            return score1.CompareTo(score2);
        });

        // Display players with rank
        int lastScore = int.MinValue;
        int lastRank = 0;
        int displayRank = 0;
        
        foreach (Player p in playersSorted)
        {
            int playerScore = p.CustomProperties.ContainsKey("FinalScore") ? (int)p.CustomProperties["FinalScore"] : 0;

            if (playerScore != lastScore)
            {
                lastRank = displayRank + 1;
            }
            if (lastRank == 1 && !hasProcessed)
            {
                GameObject newScoreDisplay = Instantiate(playerScorePrefab, scoreDisplayParent);
                TMP_Text scoreTextComponent = newScoreDisplay.transform.GetChild(0).GetComponentInChildren<TMP_Text>();
                scoreTextComponent.text = lastRank + ") " + p.NickName + ": " + playerScore;
                hasProcessed = true;
            }
            if (lastRank == 2 && !hasProcessed1)
            {
                GameObject newScoreDisplay = Instantiate(playerScorePrefab, scoreDisplayParent1);
                TMP_Text scoreTextComponent = newScoreDisplay.transform.GetChild(0).GetComponentInChildren<TMP_Text>();
                scoreTextComponent.text = lastRank + ") " + p.NickName + ": " + playerScore;
                hasProcessed1 = true;
            }
            if (lastRank == 3 && !hasProcessed2)
            {
                GameObject newScoreDisplay = Instantiate(playerScorePrefab, scoreDisplayParent2);
                TMP_Text scoreTextComponent = newScoreDisplay.transform.GetChild(0).GetComponentInChildren<TMP_Text>();
                scoreTextComponent.text = lastRank + ") " + p.NickName + ": " + playerScore;
                hasProcessed2 = true;
            }
            if (lastRank == 4 && !hasProcessed3)
            {
                GameObject newScoreDisplay = Instantiate(playerScorePrefab, scoreDisplayParent3);
                TMP_Text scoreTextComponent = newScoreDisplay.transform.GetChild(0).GetComponentInChildren<TMP_Text>();
                scoreTextComponent.text = lastRank + ") " + p.NickName + ": " + playerScore;
                hasProcessed3 = true;
            }


            Debug.Log(lastRank + "for" + p.NickName);

            lastScore = playerScore;
            displayRank++;
        }
    }
}
