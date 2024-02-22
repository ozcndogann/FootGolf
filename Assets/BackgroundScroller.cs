using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed;
    public GameObject Karo;
    bool up;
    public float upperBound;
    public float lowerBound;
    private void Start()
    {
    }
    void Update()
    {
        if (Karo.transform.position.y <= lowerBound/*-894.1125f*/)
        {
            up = true;
        }
        if(Karo.transform.position.y >= upperBound/*3459.75f*/)
        {
            up = false;
        }
        if (up)
        {
            Karo.transform.position += new Vector3(0, scrollSpeed * Time.deltaTime, 0);
        }
        else
        {
            Karo.transform.position -= new Vector3(0, scrollSpeed * Time.deltaTime, 0);
        }
    }
}
