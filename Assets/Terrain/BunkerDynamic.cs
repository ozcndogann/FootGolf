using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunkerDynamic : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject particles;
    public static bool particleEnd = false;
    public GameObject Bunker;
    // Start is called before the first frame update
    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>();
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
        Bunker.gameObject.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            rb.mass = 1.45f;
            rb.angularDrag = 0.0725f;
            rb.drag = 0.10875f;

            if(particleEnd == false)
            {
                GameObject Particle = particles;
                Instantiate(Particle, new Vector3(rb.transform.position.x, rb.transform.position.y, rb.transform.position.z), Quaternion.identity);
            }

            //Destroy(Particle, 0.37f);
        }
    }
}