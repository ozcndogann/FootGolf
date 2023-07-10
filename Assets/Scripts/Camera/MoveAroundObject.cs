using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAroundObject : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private float distanceToTarget = 4;
    Ball ball;
    private Vector3 previousPosition;
    private void Start()
    {
        ball = target.GetComponent<Ball>();
    }
    private void Update()
    {
        cam.transform.position = new Vector3(target.position.x, 1.3f, target.transform.position.z);
        cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;

            float rotationAroundYAxis = direction.x * 180;
            if (ball.isShooting)
            {
                cam.transform.position = new Vector3(target.position.x, 0, target.transform.position.z);
                Debug.Log("yakinlas");
            }
            else
            {
                cam.transform.position = new Vector3(target.position.x, 1.3f, target.transform.position.z);
            }
            

            cam.transform.Rotate(new Vector3(0, .65f, 0), rotationAroundYAxis, Space.World);

            cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

            previousPosition = newPosition;
        }
    }
}
