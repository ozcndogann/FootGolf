using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelebrityController : MonoBehaviour
{
    Animator animController;
    int rastgele;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        animController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(timer);
        timer += Time.deltaTime;
        if (Ball.firstfinishCheck == true)
        {
            animController.SetBool("isVictory", true);
            rastgele = Random.Range(1, 5);
            animController.SetInteger("celebrityNumber", rastgele);
            if (timer >= 5)
            {
                Ball.firstfinishCheck = false;
            }
        }
        else
        {
            Debug.Log("hi");
            animController.SetBool("isVictory", false);
            animController.SetInteger("celebrityNumber", 0);
            timer = 0;
        }
    }
}
