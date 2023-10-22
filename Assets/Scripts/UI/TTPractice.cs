using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TTPractice : MonoBehaviour
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

        if (PlayerPrefs.GetInt("isShown") == 0)
        {
            Tutorial();
        }
    }

    void Update()
    {
        if (Zoom.changeFovBool == true)
        {
            if (PlayerPrefs.GetInt("isShown") == 0)
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

    public void Tutorial()
    {
        TT1.SetActive(true);
    }

    public void TT1Close()
    {
        TT1.SetActive(false);
    }

    public void TTZoomClose()
    {
        TTZoom.SetActive(false);
    }

}
