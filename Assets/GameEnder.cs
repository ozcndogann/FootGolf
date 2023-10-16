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
    ShotCounter ShotCounter;
    [SerializeField] private TMP_Text scoreDisplayText;  // Drag your Text UI element here in the Inspector
    public GameObject Panel;
    [SerializeField] private Transform scoreDisplayParent;  // Drag the parent object (like a Vertical Layout Group) here
    [SerializeField] private GameObject playerScorePrefab;

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
        PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties); EndGamePanelOpen = true;
    }
    private void UpdateScoreDisplay()
    {
        // First, clear any existing score displays.
        foreach (Transform child in scoreDisplayParent)
        {
            Destroy(child.gameObject);
        }

        // For each player, create a new score display.
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            int playerScore = 0;
            if (p.CustomProperties.ContainsKey("FinalScore"))
            {
                playerScore = (int)p.CustomProperties["FinalScore"];
            }

            // Instantiate a new score display for this player.
            GameObject newScoreDisplay = Instantiate(playerScorePrefab, scoreDisplayParent);
            TMP_Text scoreTextComponent = newScoreDisplay.GetComponentInChildren<TMP_Text>();
            scoreTextComponent.text = p.NickName + ": " + playerScore;
        }
    }
    //private void UpdateScoreDisplay()
    //{
    //    string scoreText = "";
    //    foreach (Player p in PhotonNetwork.PlayerList)
    //    {
    //        int playerScore = 0;
    //        if (p.CustomProperties.ContainsKey("FinalScore"))
    //        {
    //            playerScore = (int)p.CustomProperties["FinalScore"];
    //        }
    //        scoreText += p.NickName + ": " + playerScore + "\n";
    //    }
    //    scoreDisplayText.text = scoreText;

    //}
}
