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

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Ball").transform;
    }
    private void Update()
    {
        #region CamFollow
        if (Ball.shooted == false)
        {
            cam.transform.position = new Vector3(target.position.x, 1 + target.position.y, target.transform.position.z);
            cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

            if (Input.GetMouseButtonDown(0))
            {
                previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
                //Debug.Log(newPosition+"new");
                //Debug.Log(previousPosition+"previous");
                Vector3 direction = previousPosition - newPosition;

                float rotationAroundYAxis = direction.x * 180;

                cam.transform.position = new Vector3(target.position.x, 1 + target.position.y, target.transform.position.z);
                //if(rotationAroundYAxis)
                cam.transform.Rotate(new Vector3(0, .65f, 0), rotationAroundYAxis, Space.World);
                cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

                previousPosition = newPosition;
            }
        }
        else
        {
            cam.transform.position = new Vector3(cam.transform.position.x, /*heightWhileShooting*/target.transform.position.y+.397f, cam.transform.position.z);
        }
        #endregion
    }
    void FixedUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        float distance = Vector3.Distance(cam.transform.position, target.transform.position);
        if (Physics.Raycast(cam.transform.position, target.transform.TransformDirection(Vector3.forward), out hit, distance))
        {
            //Debug.Log("Did Hit");
            if (hit.transform.gameObject.tag != "Ground" && hit.transform.gameObject.tag != "Ball")
            {
                passHit = hit.transform.gameObject;
                passHit.gameObject.SetActive(false);
            }

        }
        else
        {
            if(Ball.shooted == true || target.gameObject.GetComponent<Rigidbody>().velocity != Vector3.zero)
            {
                passHit.gameObject.SetActive(true);
            }
            //Debug.Log("Did not Hit");
            
        }

    }

}
