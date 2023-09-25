using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnder : MonoBehaviour
{
    

    void Update()
    {
        if (Ball.gameEnder)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
