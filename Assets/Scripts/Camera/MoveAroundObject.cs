using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAroundObject : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private float distanceToTarget = 4;
    private Vector3 previousPosition;
    public float heightWhileShooting;
    GameObject passHit;
    Ball ball;
    Ball1 ball1;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Ball").transform;
        ball = target.GetComponent<Ball>();
        ball1 = target.GetComponent<Ball1>();
    }
    private void Update()
    {
        #region camfollow
        if (ball.shooted == false)
        {
            cam.transform.position = new vector3(target.position.x, 1 + target.position.y, target.transform.position.z);
            cam.transform.translate(new vector3(0, 0, -distancetotarget));

            if (ýnput.getmousebuttondown(0))
            {
                previousposition = cam.screentoviewportpoint(ýnput.mouseposition);
            }
            else if (ýnput.getmousebutton(0))
            {
                vector3 newposition = cam.screentoviewportpoint(ýnput.mouseposition);
                //debug.log(newposition+"new");
                //debug.log(previousposition+"previous");
                vector3 direction = previousposition - newposition;
                float rotationaroundyaxis = direction.x * 180;

                cam.transform.position = new vector3(target.position.x, 1 + target.position.y, target.transform.position.z);
                //if(rotationaroundyaxis)
                cam.transform.rotate(new vector3(0, .65f, 0), rotationaroundyaxis / 300, space.world);
                cam.transform.translate(new vector3(0, 0, -distancetotarget));

                //previousposition = newposition;
            }
        }
        else
        {
            cam.transform.position = new vector3(cam.transform.position.x, /*heightwhileshooting*/target.transform.position.y + .397f, cam.transform.position.z);
        }
        #endregion

        //#region CamFollow1
        //if (Ball1.shooted == false)
        //{
        //    cam.transform.position = new Vector3(target.position.x, 1 + target.position.y, target.transform.position.z);
        //    cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        //    }
        //    else if (Input.GetMouseButton(0))
        //    {
        //        Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        //        //Debug.Log(newPosition+"new");
        //        //Debug.Log(previousPosition+"previous");
        //        Vector3 direction = previousPosition - newPosition;
        //        float rotationAroundYAxis = direction.x * 180;

        //        cam.transform.position = new Vector3(target.position.x, 1 + target.position.y, target.transform.position.z);
        //        //if(rotationAroundYAxis)
        //        cam.transform.Rotate(new Vector3(0, .65f, 0), rotationAroundYAxis / 300, Space.World);
        //        cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

        //        //previousPosition = newPosition;
        //    }
        //}
        //else
        //{
        //    cam.transform.position = new Vector3(cam.transform.position.x, /*heightWhileShooting*/target.transform.position.y + .397f, cam.transform.position.z);
        //}
        //#endregion
    }
    void FixedUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        float distance = Vector3.Distance(cam.transform.position, target.transform.position);
        if (Physics.Raycast(cam.transform.position, target.transform.TransformDirection(Vector3.forward), out hit, distance))
        {
            //Debug.Log("Did Hit");
            if (hit.transform.gameObject.tag != "Ground" && hit.transform.gameObject.tag != "Ball" && hit.transform.gameObject.tag != "Hole")
            {
                passHit = hit.transform.gameObject;
                passHit.SetActive(false);
            }

        }
        //if (/*Ball.shooted == true || */target.gameObject.GetComponent<Rigidbody>().velocity != Vector3.zero)
        //{
        //    passHit.SetActive(true);
        //}
        else
        {
            
            //Debug.Log("Did not Hit");
            
        }

    }

}
