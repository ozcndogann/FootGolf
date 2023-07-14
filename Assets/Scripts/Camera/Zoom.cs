using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    //public variables paremeters for Unity inpector inputs
    public float initialFOV;
    public float zoomInFOV;
    public float smooth;
    Ball ball;
    MoveAroundObject moveAroundObject;
    [SerializeField] private GameObject ballObj;
    [SerializeField] private Camera cam;
    private float currentFOV;
    public static bool changeFovBool;

    // Use this for initialization
    void Start()
    {
        //set initial FOV at start
        Camera.main.fieldOfView = initialFOV;
        ball = ballObj.GetComponent<Ball>();
        moveAroundObject = cam.GetComponent<MoveAroundObject>();
        changeFovBool = false;
    }

    // Update is called once per frame
    void Update()
    {
        //store current field of view value in variable
        currentFOV = Camera.main.fieldOfView;
        if (changeFovBool == true)
        {
            ChangeFOV();
        }
        
    }

    //function to zoom in the FOV
    public void ChangeFOV()
    {
        //check that current FOV is different than Zoomed
        if (currentFOV != zoomInFOV)
        {
            //check if current FOV is grater than the Zoomed in FOV input and increment the FOV smoothly
            if (currentFOV > zoomInFOV)
            {
                Camera.main.fieldOfView += (-smooth * Time.deltaTime);
            }
            else
            {
                //then current FOV gets to the same or smaller value than the Zoomed in input
                //set FOV as the Zoomed in input
                if (currentFOV <= zoomInFOV)
                {
                    Camera.main.fieldOfView = zoomInFOV;
                }
            }
        }
    }
}
