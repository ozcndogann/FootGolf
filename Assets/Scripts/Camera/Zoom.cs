using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    //public variables paremeters for Unity inpector inputs
    public float initialFOV;
    public float zoomInFOV;
    public float smooth;
    public Vector3 BallZoom;
    Ball ball;
    Ball1 ball1;
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
        ballObj = GameObject.FindGameObjectWithTag("Ball");
        ball = ballObj.GetComponent<Ball>();
        ball.enabled = true;
        moveAroundObject = cam.GetComponent<MoveAroundObject>();
        changeFovBool = false;
        //ball1 = ballObj.GetComponent<Ball1>();
        //ball1.enabled = true;
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
        else
        {
            Camera.main.fieldOfView = initialFOV;
        }
        
    }

    //function to zoom in the FOV
    public void ChangeFOV()
    {
       // gameObject.transform.position = new Vector3(ball.lineX, transform.position.y,ball.lineZ);
        //gameObject.transform.rotation = ball.CamRot.rotation;
        //gameObject.transform.position = new Vector3(ball.lineX, gameObject.transform.position.y, gameObject.transform.position.z);
        //check that current FOV is different than Zoomed
        if (currentFOV != zoomInFOV)
        {
            //burada cam pos topun arkasýna alarak deðiþtir!!!!
            
            if (currentFOV > zoomInFOV)//check if current FOV is grater than the Zoomed in FOV input and increment the FOV smoothly
            {
                Camera.main.fieldOfView += (-smooth * Time.deltaTime);
                ballObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
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
