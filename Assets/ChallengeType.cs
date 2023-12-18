using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChallengeType : MonoBehaviour
{
    public static bool cha1, cha2;
    void Start()
    {
        PlayerPrefs.GetInt("Cha1", 0);
    }

    void Update()
    {
        if (cha1 && Ball.challangeCheck)
        {
            //hole1 ve 3 vuruþta geçmeli
            if (ShotCounter.ShotCount <=3)
            {
                PlayerPrefs.SetInt("Cha1", 1);
                Debug.Log("basari" + PlayerPrefs.GetInt("Cha1"));
            }
            else
            {
                Debug.Log("basarisiz");
            }
        }
    }
    public void Challange1()
    {
        cha1 = true;
    }
    public void Challange2()
    {
        cha2 = true;
    }
}
