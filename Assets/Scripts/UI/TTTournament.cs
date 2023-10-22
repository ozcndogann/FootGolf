using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TTTournament : MonoBehaviour
{
    public GameObject TT1;
    public GameObject TTZoom;
    //public GameObject TTNetwork;
    Camera cam;
    Zoom zoom;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
        zoom = cam.GetComponent<Zoom>();

        //if (PlayerPrefs.GetInt("isShown") == 0)
        //{
        //    Tutorial();
        //}
    }

    public void Update()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties["turn"] != null)
        {
            if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["turn"])
            {
                if (PlayerPrefs.GetInt("isShown") == 0)
                {
                    TT1.SetActive(true);
                    if (Zoom.changeFovBool == true)
                    {
                        TTZoom.SetActive(true);
                        TT1.SetActive(false);
                        if (Input.GetMouseButtonDown(0) == true)
                        {
                            TTZoom.SetActive(false);
                        }
                        PlayerPrefs.SetInt("isShown", 1);
                    }
                }

            }
        }
    }

    //public void Tutorial()
    //{
    //    if (PhotonNetwork.LocalPlayer.CustomProperties["turn"] != null)
    //    {
    //        if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["turn"])
    //        {
    //            TT1.SetActive(true);
    //        }
    //    }
    //    else
    //    {
    //        return;
    //    }

    //}

    public void TT1Close()
    {
        TT1.SetActive(false);
    }

    public void TTZoomClose()
    {
        TTZoom.SetActive(false);
    }

}