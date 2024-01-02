using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using PlayFab;
public class GameEnder : MonoBehaviour
{
    [SerializeField] private int firstHolePar;
    [SerializeField] private int secondHolePar;
    List<int> playerScores =new List<int> { };
    private int score,i;
    public static bool EndGame;
    public static bool EndGamePanelOpen;
    Player player;
    private GameObject ball;
    //Ball ballScript;
    ShotCounter ShotCounter;
    [SerializeField] private TMP_Text scoreDisplayText;  // Drag your Text UI element here in the Inspector
    public GameObject Panel;
    [SerializeField] private Transform scoreDisplayParent;  // Drag the parent object (like a Vertical Layout Group) here
    [SerializeField] private GameObject playerScorePrefab;
    private int lastRank;
    [SerializeField] private int tprize1, tprize2, tprize3, tprize4, vsprize1, vsprize2;
    
    private void Start()
    {
        i = 1;
        PlayerPrefs.GetInt("Score", 0);
        ball = GameObject.FindGameObjectWithTag("Ball");
        //ballScript = ball.GetComponent<Ball>();
        ShotCounter = ball.GetComponent<ShotCounter>();
        Panel.SetActive(false);
        EndGame = true;
        EndGamePanelOpen = false;
    }
    void Update()
    {
        if (Ball.gameEnder)
        {
            Panel.SetActive(true);
            
            CalculateScore();
            UpdateScoreDisplay();
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
        //int lastRank = 0;
        int displayRank = 0;
        
        foreach (Player p in playersSorted)
        {
            int playerScore = p.CustomProperties.ContainsKey("FinalScore") ? (int)p.CustomProperties["FinalScore"] : /*(int)p.CustomProperties["FinalScore"]*/0;

            if (playerScore != lastScore)
            {
                lastRank = displayRank + 1;
            }
            GameObject newScoreDisplay = Instantiate(playerScorePrefab, scoreDisplayParent);
            TMP_Text scoreTextComponent = newScoreDisplay.transform.GetChild(0).GetComponentInChildren<TMP_Text>();
            scoreTextComponent.text = "  " + lastRank + ") " + p.NickName + ": " + playerScore;
            playerScores.Add(playerScore);
            i++;
            lastScore = playerScore;
            displayRank++;
            Debug.Log(lastRank + "for" + p.NickName);
            
        }
    }
    public void MainMenu()
    {
        int totalCoins = PlayerPrefs.GetInt("Coins", 0);
        if (CreateAndJoinRandomRooms.Tournament && CreateAndJoinRooms.Tournament)
        {
            if (lastRank == 1)
            {
                //totalCoins += prize1;
                PlayerPrefs.SetInt("Coins", totalCoins += tprize1);
                PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 1);
                Debug.Log("prize1");
            }
            else if (lastRank == 2)
            {
                if (playerScores[0] == playerScores[1])
                {
                    PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 1);
                }
                PlayerPrefs.SetInt("Coins", totalCoins += tprize2);
                Debug.Log("prize2");
            }
            else if (lastRank == 3)
            {
                if (playerScores[0] == playerScores[2])
                {
                    PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 1);
                }
                PlayerPrefs.SetInt("Coins", totalCoins += tprize3);
                Debug.Log("prize3");
            }
            else if (lastRank == 4)
            {
                if (playerScores[0] == playerScores[3])
                {
                    PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 1);
                }
                PlayerPrefs.SetInt("Coins", totalCoins += tprize4);
                Debug.Log("prize4");
            }
        }

        else if (CreateAndJoinRandomRooms.versus && CreateAndJoinRooms.versus)
        {
            if (lastRank == 1)
            {
                //totalCoins += prize1;
                PlayerPrefs.SetInt("Coins", totalCoins += vsprize1);
                PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 1);
                Debug.Log("prize1");
            }
            else if (lastRank == 2)
            {
                if (playerScores[0] == playerScores[1])
                {
                    PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 1);
                }
                PlayerPrefs.SetInt("Coins", totalCoins += vsprize2);
                Debug.Log("prize2");
            }
        }
        spectCanvasClose = false;

        ShotCounter.ShotCount = 0;
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("MainMenu");
    }
}
