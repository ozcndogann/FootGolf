using UnityEngine;

public class CursorController : MonoBehaviour
{
    public RectTransform barRectTransform; // Assign this in the inspector
    public RectTransform cursorRectTransform; // Assign this in the inspector
    public bool stopMovement = false; // The boolean to control when to stop
    public float minValue = 5f; // The value at the extremes
    public float maxValue = 1f; // The value at the middle

    private float barWidth;
    private float moveSpeed = 100f; // Adjust speed as needed
    private int direction = 1; // 1 for right, -1 for left
    Ball ball;

    void Start()
    {
        // Assuming the bar is aligned along the x-axis and centered
        barWidth = barRectTransform.rect.width;
        cursorRectTransform.anchoredPosition = new Vector2(0, cursorRectTransform.anchoredPosition.y);

        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();
    }

    void Update()
    {
        if (!ball.shotClicked)
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
        }
        else
        {
            // Calculate and output the float value
            float value = CalculateValue(cursorRectTransform.anchoredPosition.x);
            Debug.Log("Value: " + value);
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