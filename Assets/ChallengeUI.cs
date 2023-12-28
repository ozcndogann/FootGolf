using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeUI : MonoBehaviour
{
    [SerializeField] private GameObject tryme1, tryme2, tryme3, tryme4, tryme5, tryme6, tryme7, tryme8, tryme9, tryme10, tryme11, tryme12, tryme13, tryme14, tryme15, tryme16, tryme17, tryme18;

    void Start()
    {
        if (PlayerPrefs.GetInt("Cha1") == 1)
        {
            tryme1.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha2") == 1)
        {
            tryme2.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha3") == 1)
        {
            tryme3.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha4") == 1)
        {
            tryme4.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha5") == 1)
        {
            tryme5.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha6") == 1)
        {
            tryme6.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha7") == 1)
        {
            tryme7.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha8") == 1)
        {
            tryme8.SetActive(false);

        }
        if (PlayerPrefs.GetInt("Cha9") == 1)
        {
            tryme9.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha10") == 1)
        {
            tryme10.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha11") == 1)
        {
            tryme11.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha12") == 1)
        {
            tryme12.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha13") == 1)
        {
            tryme13.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha14") == 1)
        {
            tryme14.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha15") == 1)
        {
            tryme15.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha16") == 1)
        {
            tryme16.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha17") == 1)
        {
            tryme17.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha18") == 1)
        {
            tryme18.SetActive(false);
        }
    }
}
