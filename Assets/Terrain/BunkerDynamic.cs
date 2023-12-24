using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BunkerDynamic : MonoBehaviour
{
    private Rigidbody rb;
    private PhysicMaterial rbphy;
    public GameObject particles;
    public static bool particleEnd = false;
    public static bool bunkerTouch = false;
    public GameObject Bunker;
    public BoxCollider box;
    private bool inBunker = false;
    public float sandHeightThreshold;
    private float lastHeight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>();
        rbphy = GameObject.FindGameObjectWithTag("Ball").GetComponent<SphereCollider>().material;

        lastHeight = rb.transform.position.y;
        box = gameObject.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        float currentHeight = rb.transform.position.y;

        // Check if the ball is currently below the sand height threshold and was not in the bunker before
        if (currentHeight < sandHeightThreshold && !inBunker)
        {
            EnterBunker();
        }
        // If the ball's height is increasing and it's above the threshold, consider it leaving the bunker
        else if (currentHeight >= sandHeightThreshold && inBunker && lastHeight < currentHeight)
        {
            ExitBunker();
        }

        // Update the lastHeight for the next frame
        lastHeight = currentHeight;

        //if (rb.transform.position.y < sandHeightThreshold)
        //{
        //    inBunker = true;
        //    ApplyBunkerPhysics();
        //}
        //else
        //{
        //    inBunker = false;
        //}

        //if (rb.velocity.magnitude == 0)
        //{
        //    particleEnd = true;
        //}
        //else
        //{
        //    particleEnd = false;
        //}

        //if (bunkerTouch == true)
        //{
        //    rb.mass = 1.65f;
        //    rb.angularDrag = 0.05f;
        //    rb.drag = 0.075f;
        //}
        //else
        //{
        //    rb.mass = 1;
        //    rb.angularDrag = 0.05f;
        //    rb.drag = 0.075f;
        //}

    }

    private void EnterBunker()
    {
        inBunker = true;
        ApplyBunkerPhysics();
    }

    private void ExitBunker()
    {
        inBunker = false;
        // Apply regular physics or do whatever is needed when exiting the bunker
    }

    void ApplyBunkerPhysics()
    {
        rb.mass = 1.8f;
        rb.angularDrag = 0.03f;
        rb.drag = 0.05f;
        rbphy.bounciness = 0.1f;
    }


    //private void OnTriggerEnter(Collider other)
    //{

    //    if (other.CompareTag("Ball"))
    //    {
    //        bunkerTouch = true;

    //        if (!particleEnd)
    //        {
    //            Instantiate(particles, rb.transform.position, Quaternion.identity);
    //        }
    //    }
    //    else
    //    {
    //        bunkerTouch = false;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Ball"))
    //    {
    //        bunkerTouch = false;

    //        //box.enabled = true;
    //    }
    //}

}