using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BunkerDynamic : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject particles;
    public static bool particleEnd = false;
    public static bool bunkerTouch = false;
    public GameObject Bunker;
    public BoxCollider box;

    // Start is called before the first frame update
    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>();
        box = gameObject.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (rb.velocity.magnitude == 0)
        {
            particleEnd = true;
        }
        else
        {
            particleEnd = false;
        }

        if (bunkerTouch == true)
        {
            rb.mass = 1.65f;
            rb.angularDrag = 0.05f;
            rb.drag = 0.075f;
        }
        else
        {
            rb.mass = 1;
            rb.angularDrag = 0.05f;
            rb.drag = 0.075f;
        }

        Bunker.gameObject.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Ball"))
        {
            bunkerTouch = true;

            if (!particleEnd)
            {
                Instantiate(particles, rb.transform.position, Quaternion.identity);
            }
        }
        else
        {
            bunkerTouch = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            bunkerTouch = false;
        }
    }
}