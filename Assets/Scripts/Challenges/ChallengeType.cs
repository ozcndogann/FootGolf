using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class ChallengeType : MonoBehaviour
{
    public static bool cha1, cha2, cha3, cha4, cha5, cha6, cha7, cha8, cha9, cha10, cha11, cha12, cha13, cha14, cha15, cha16, cha17, cha18,practice;//UI_InGame scriptinde false'lamayý unutma
    public GameObject ChaWin, ChaLose;
    Camera barrierCam;
    void Start()
    {
        #region PlayerPrefs
        PlayerPrefs.GetInt("Cha1", 0);
        PlayerPrefs.GetInt("Cha2", 0);
        PlayerPrefs.GetInt("Cha3", 0);
        PlayerPrefs.GetInt("Cha4", 0);
        PlayerPrefs.GetInt("Cha5", 0);
        PlayerPrefs.GetInt("Cha6", 0);
        PlayerPrefs.GetInt("Cha7", 0);
        PlayerPrefs.GetInt("Cha8", 0);
        PlayerPrefs.GetInt("Cha9", 0);
        PlayerPrefs.GetInt("Cha10", 0);
        PlayerPrefs.GetInt("Cha11", 0);
        PlayerPrefs.GetInt("Cha12", 0);
        PlayerPrefs.GetInt("Cha13", 0);
        PlayerPrefs.GetInt("Cha14", 0);
        PlayerPrefs.GetInt("Cha15", 0);
        PlayerPrefs.GetInt("Cha16", 0);
        PlayerPrefs.GetInt("Cha17", 0);
        PlayerPrefs.GetInt("Cha18", 0);
        #endregion
        barrierCam = GameObject.FindGameObjectWithTag("BarrierCam").GetComponent<Camera>() as Camera;
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            barrierCam.gameObject.SetActive(false);
        }
        
    }

    void Update()
    {
        #region EndChallengeCheck
        if ((cha4 || cha7 || cha10 || cha13))
        {
            //5 vuruþta geçmeli
            if (ShotCounter.ShotCount <= 5)
            {
                if (Ball.challangeCheck)
                {
                    if (cha4)
                    {
                        PlayerPrefs.SetInt("Cha4", 1);
                        barrierCam.gameObject.SetActive(true);
                        ChaWin.SetActive(true);
                    }
                    else if (cha7)
                    {
                        PlayerPrefs.SetInt("Cha7", 1);
                        barrierCam.gameObject.SetActive(true);
                        ChaWin.SetActive(true);
                    }
                    else if (cha10)
                    {
                        PlayerPrefs.SetInt("Cha10", 1);
                        barrierCam.gameObject.SetActive(true);
                        ChaWin.SetActive(true);
                    }
                    else if (cha13)
                    {
                        PlayerPrefs.SetInt("Cha13", 1);
                        barrierCam.gameObject.SetActive(true);
                        ChaWin.SetActive(true);
                    }
                }
               
            }
            else
            {
                Debug.Log("basarisiz");
                barrierCam.gameObject.SetActive(true);
                ChaLose.SetActive(true);
            }
        }
        if (Ball.challangeCheck)
        {
            if (practice)
            {
                barrierCam.gameObject.SetActive(true);
                ChaLose.SetActive(true);
            }
        }
        if ((cha1 || cha5 || cha8 || cha11 || cha14 || cha16))
        {
            //4 vuruþta geçmeli
            if (ShotCounter.ShotCount <= 4)
            {
                if (Ball.challangeCheck)
                {
                    if (cha1)
                    {
                        PlayerPrefs.SetInt("Cha1", 1);
                        barrierCam.gameObject.SetActive(true);
                        ChaWin.SetActive(true);
                    }
                    else if (cha5)
                    {
                        PlayerPrefs.SetInt("Cha5", 1);
                        barrierCam.gameObject.SetActive(true);
                        ChaWin.SetActive(true);
                    }
                    else if (cha8)
                    {
                        PlayerPrefs.SetInt("Cha8", 1);
                        barrierCam.gameObject.SetActive(true);
                        ChaWin.SetActive(true);
                    }
                    else if (cha11)
                    {
                        PlayerPrefs.SetInt("Cha11", 1);
                        barrierCam.gameObject.SetActive(true);
                        ChaWin.SetActive(true);
                    }
                    else if (cha14)
                    {
                        PlayerPrefs.SetInt("Cha14", 1);
                        barrierCam.gameObject.SetActive(true);
                        ChaWin.SetActive(true);
                    }
                    else if (cha16)
                    {
                        PlayerPrefs.SetInt("Cha16", 1);
                        barrierCam.gameObject.SetActive(true);
                        ChaWin.SetActive(true);
                    }
                }
                
            }
            else
            {
                Debug.Log("basarisiz");
                barrierCam.gameObject.SetActive(true);
                ChaLose.SetActive(true);
            }
        }
        if ((cha2 || cha6 || cha9 || cha12 || cha15 || cha17))
        {
            //3 vuruþta geçmeli
            if (ShotCounter.ShotCount <= 3)
            {
                if (Ball.challangeCheck)
                {
                    if (cha2)
                    {
                        PlayerPrefs.SetInt("Cha2", 1);
                        barrierCam.gameObject.SetActive(true);
                        ChaWin.SetActive(true);
                    }
                    else if (cha6)
                    {
                        PlayerPrefs.SetInt("Cha6", 1);
                        barrierCam.gameObject.SetActive(true);
                        ChaWin.SetActive(true);
                    }
                    else if (cha9)
                    {
                        PlayerPrefs.SetInt("Cha9", 1);
                        barrierCam.gameObject.SetActive(true);
                        ChaWin.SetActive(true);
                    }
                    else if (cha12)
                    {
                        PlayerPrefs.SetInt("Cha12", 1);
                        barrierCam.gameObject.SetActive(true);
                        ChaWin.SetActive(true);
                    }
                    else if (cha15)
                    {
                        PlayerPrefs.SetInt("Cha15", 1);
                        barrierCam.gameObject.SetActive(true);
                        ChaWin.SetActive(true);
                    }
                    else if (cha17)
                    {
                        PlayerPrefs.SetInt("Cha17", 1);
                        barrierCam.gameObject.SetActive(true);
                        ChaWin.SetActive(true);
                    }
                }
                
            }
            else
            {
                Debug.Log("basarisiz");
                barrierCam.gameObject.SetActive(true);
                ChaLose.SetActive(true);
            }
        }
        if ((cha3 || cha18))
        {
            //2 vuruþta geçmeli
            if (ShotCounter.ShotCount <= 2)
            {
                if (Ball.challangeCheck)
                {
                    if (cha3)
                    {
                        PlayerPrefs.SetInt("Cha3", 1);
                        barrierCam.gameObject.SetActive(true);
                        ChaWin.SetActive(true);
                    }
                    else if (cha18)
                    {
                        PlayerPrefs.SetInt("Cha18", 1);
                        barrierCam.gameObject.SetActive(true);
                        ChaWin.SetActive(true);
                    }
                }
                
            }
            else
            {
                Debug.Log("basarisiz");
                barrierCam.gameObject.SetActive(true);
                ChaLose.SetActive(true);
            }
        }
        #endregion
    }
    public void Practice()
    {
        practice = true;
    }
    #region ChallangeTypes
    public void Challange1()
    {
        cha1 = true;
    }
    public void Challange2()
    {
        cha2 = true;
    }
    public void Challange3()
    {
        cha3 = true;
    }
    public void Challange4()
    {
        cha4 = true;
    }
    public void Challange5()
    {
        cha5 = true;
    }
    public void Challange6()
    {
        cha6 = true;
    }
    public void Challange7()
    {
        cha7 = true;
    }
    public void Challange8()
    {
        cha8 = true;
    }
    public void Challange9()
    {
        cha9 = true;
    }
    public void Challange10()
    {
        cha10 = true;
    }
    public void Challange11()
    {
        cha11 = true;
    }
    public void Challange12()
    {
        cha12 = true;
    }
    public void Challange13()
    {
        cha13 = true;
    }
    public void Challange14()
    {
        cha14 = true;
    }
    public void Challange15()
    {
        cha15 = true;
    }
    public void Challange16()
    {
        cha16 = true;
    }
    public void Challange17()
    {
        cha17 = true;
    }
    public void Challange18()
    {
        cha18 = true;
    }
    #endregion
}
