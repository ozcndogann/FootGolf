using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAroundHole : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private float distanceToTarget = 4;
    private Vector3 previousPosition;
    public float heightWhileShooting;

    private void Start()
    {
    }
    private void Update()
    {
        #region CamFollow
        //if (Ball.shooted == false)
        {
            cam.transform.position = new Vector3(target.position.x, 2.5f + target.position.y, target.transform.position.z);
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

                cam.transform.position = new Vector3(target.position.x, 2.5f + target.position.y, target.transform.position.z);
                //if(rotationAroundYAxis)
                cam.transform.Rotate(new Vector3(0, .65f, 0), rotationAroundYAxis / 60, Space.World);
                cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

                //previousPosition = newPosition;
            }
        }
        //else
        //{
        //    cam.transform.position = new Vector3(cam.transform.position.x, /*heightWhileShooting*/target.transform.position.y + 2.5f, cam.transform.position.z);
        //}
        #endregion
    }
    

}
