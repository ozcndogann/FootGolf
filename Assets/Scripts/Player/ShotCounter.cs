using Photon.Pun;
using UnityEngine;

public class ShotCounter : MonoBehaviourPun
{
    private const string SCORE_KEY = "Score";

    public static int ShotCount
    {
        get
        {
            if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey(SCORE_KEY))
            {
                return (int)PhotonNetwork.LocalPlayer.CustomProperties[SCORE_KEY];
            }
            return 0;
        }
        set
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { SCORE_KEY, value } });
        }
    }
}
