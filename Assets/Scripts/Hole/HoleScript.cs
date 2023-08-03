using UnityEngine;

public class HoleScript : MonoBehaviour
{
    public GameObject ball;
    public Rigidbody rb;
    public static bool holeC;
    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        rb = ball.GetComponent<Rigidbody>();
        holeC = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            holeC = true;
            Debug.Log("girdi");
        }

    }

    private void Update()
    {
        //if (holeC == true)
        //{
        //    rb.AddForce(0, -10, 0, ForceMode.Impulse);
        //}
    }
}
