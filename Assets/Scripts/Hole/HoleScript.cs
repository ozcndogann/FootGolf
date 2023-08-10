using UnityEngine;
using Photon.Pun;
public class HoleScript : MonoBehaviour
{
    public GameObject ball;
    public Rigidbody rb;
    public static bool holeC;
    Camera cam1;
    Camera cam2;
    PhotonView view;
    private void Start()
    {
        holeC = false;
        cam1 = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
        cam2 = GameObject.FindGameObjectWithTag("AfterCamera").GetComponent<Camera>() as Camera;
        view = GameObject.FindGameObjectWithTag("Ball").GetComponent<PhotonView>() as PhotonView;
        view = ball.GetComponent<PhotonView>();
        cam1.enabled = (true);
        cam2.enabled = (false);
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Ball"))
        {
            if (view.IsMine)
            {
                holeC = true;
                cam1.enabled = (false);
                cam2.enabled = (true);
                Debug.Log("girdi");
            }

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
