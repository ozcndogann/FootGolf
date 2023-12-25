using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeUI : MonoBehaviour
{
    [SerializeField] private GameObject do1, do2;

    void Start()
    {
        if (PlayerPrefs.GetInt("Cha1") == 1)
        {
            do1.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha2") == 1)
        {
            do2.SetActive(false);
        }
    }
}
