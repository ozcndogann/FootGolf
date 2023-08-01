using UnityEngine;

public class HoleScript : MonoBehaviour
{
    // The unique tag assigned to the hole
    private const string HoleTag = "Hole";
    public Animator anim;
    // Called when a GameObject with a Rigidbody enters the hole's trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(HoleTag))
        {
            anim.Play("BallHole");
            Debug.Log("Ball entered the hole!");
        }
    }
}
