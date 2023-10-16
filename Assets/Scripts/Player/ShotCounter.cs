using Photon.Pun;
using UnityEngine;

public class ShotCounter : MonoBehaviourPun
{
    private const string SCORE_KEY = "Score";

    public static int ShotCount
    {
        get
        {
            if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Score"))
            {
                int currentScore = (int)PhotonNetwork.LocalPlayer.CustomProperties["Score"];
                currentScore++;
                PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "Score", currentScore } });
                Debug.Log("Directly Updated Score: " + currentScore);
            }
            else
            {
                PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "Score", 1 } });
                Debug.Log("Directly Set Initial Score to 1");
            }
            return 0;
        }
        set
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { SCORE_KEY, val } });
        }
    }
}
