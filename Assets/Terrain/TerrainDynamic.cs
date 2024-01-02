using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDynamic : MonoBehaviour
{
    private Rigidbody rb;
    private PhysicMaterial rbphy;
    private PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        view = GameObject.FindGameObjectWithTag("Ball").GetComponent<PhotonView>();
        rb = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>();
        rbphy = GameObject.FindGameObjectWithTag("Ball").GetComponent<SphereCollider>().material;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball")
        {
            if (view.IsMine)
            {
                rb.mass = 1f;
                rb.angularDrag = 0.05f;
                rb.drag = 0.075f;
                rbphy.bounciness = 0.7f;
            }
        }
    }
}
