using UnityEngine;

public class SwipeController : MonoBehaviour
{
    public float sensitivity = 1f; // Adjust this value to control the camera rotation speed
    private Vector2 startPos;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touch.position;
                    break;
                case TouchPhase.Moved:
                    float swipeValue = touch.position.x - startPos.x;
                    transform.Rotate(Vector3.up, swipeValue * sensitivity, Space.World);
                    break;
                default:
                    break;
            }
        }
    }
}
