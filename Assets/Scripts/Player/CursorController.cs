using UnityEngine;

public class CursorController : MonoBehaviour
{
    public RectTransform barRectTransform; // Assign this in the inspector
    public RectTransform cursorRectTransform; // Assign this in the inspector
    public bool stopMovement = false; // The boolean to control when to stop
    public float minValue = 5f; // The value at the extremes
    public float maxValue = 1f; // The value at the middle
    public static float cursorValue, currentCursorValue;
    private float barWidth;
    [SerializeField]private float moveSpeed; // Adjust speed as needed
    private int direction = 1; // 1 for right, -1 for left
    Ball ball;
    bool getValue;

    void Start()
    {
        // Assuming the bar is aligned along the x-axis and centered
        barWidth = barRectTransform.rect.width;
        cursorRectTransform.anchoredPosition = new Vector2(0, cursorRectTransform.anchoredPosition.y);
        getValue = false;
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();
    }

    void Update()
    {
        //currentCursorValue = cursorRectTransform.anchoredPosition.x;
        //Debug.Log(cursorRectTransform.anchoredPosition.x);
        //if (Mathf.Abs(currentCursorValue) > 120)
        //{
        //    moveSpeed = 400;
        //}
        //else
        //{
        //    moveSpeed = 800;
        //}
        if (!ball.shootedNow && Zoom.changeFovBool)
        {
            // Move the cursor
            cursorRectTransform.anchoredPosition += new Vector2(Time.deltaTime * moveSpeed * direction, 0);

            // Change direction when reaching the ends of the bar
            if (cursorRectTransform.anchoredPosition.x > barWidth / 2 || cursorRectTransform.anchoredPosition.x < -barWidth / 2)
            {
                direction *= -1;
                // Correcting position if it goes beyond bounds
                cursorRectTransform.anchoredPosition = new Vector2(Mathf.Clamp(cursorRectTransform.anchoredPosition.x, -barWidth / 2, barWidth / 2), cursorRectTransform.anchoredPosition.y);
            }

            getValue = true;
        }

        if (ball.shootedNow)
        {
            if (getValue)
            {
                // Calculate and output the float value
                cursorValue = CalculateValue(cursorRectTransform.anchoredPosition.x);
                Debug.Log("Value: " + cursorValue);
                getValue = false;
            }
            // Reset cursor position to the middle of the bar at the start of movement
            cursorRectTransform.anchoredPosition = new Vector2(0, cursorRectTransform.anchoredPosition.y);

        }
    }

    float CalculateValue(float positionX)
    {
        // Normalize position to a 0-1 range where 0 is one extreme and 1 is the other
        float normalizedPos = (positionX + barWidth / 2) / barWidth;

        // Convert normalized position to a value between minValue and maxValue
        return Mathf.Lerp(minValue, maxValue, Mathf.Abs(normalizedPos - 0.5f) * 2);
    }
}