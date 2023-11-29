using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwipe : MonoBehaviour
{
    private Touch startSwipe;
    private float dragAmount;
    public Transform objectSpin;

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
        {
            startSwipe = Input.GetTouch(0);
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) 
        {
            dragAmount = Mathf.Clamp((startSwipe.position.x - Input.GetTouch(0).position.x) / 140f, -4f, 4f);
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).position.y < 1670f && Input.GetTouch(0).position.y > 750f) 
        {
            objectSpin.Rotate(0f,dragAmount ,0f);
        }
    }
    public void resetMove() 
    {
        objectSpin.rotation = Quaternion.Euler(0f, 180f, 0f);
    }
}
