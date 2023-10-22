using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class YourTurn : MonoBehaviour
{
    public GameObject yourTurnPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Update()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties["turn"] != null)
        {
            if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["turn"])
            {
                yourTurnPanel.SetActive(true);
            }
            else
            {
                yourTurnPanel.SetActive(false);
            }
        }
        if (PhotonNetwork.LocalPlayer.CustomProperties["holeC"] != null)
        {
            if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["holeC"])
            {
                yourTurnPanel.SetActive(false);
            }
        }
    }
}
