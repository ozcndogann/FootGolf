using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class AnimationFootballer : MonoBehaviour
{
    public static bool footballerTeleport;
    public static bool AcceptShoot;
    public bool ObjectMirrorBool,ObjectMirrorReverse;
    Rigidbody rb;
    [SerializeField] private float stopVelocity;
    [SerializeField] private float ace;
    Camera cam;
    PhotonView view;
    Ball ballScript;
    public GameObject Ronaldinho, Messi,Alexia,Esmer,hanımKız,Male;
    Animator footballerAnimator, trivelaAnimator;
    GameObject OurFootballer, TrivelaFootballer;
    public float waitForShootTimer, waitForShootTriTimer, whichAnim;
    Vector3 distanceP, distanceT;
    public LayerMask ground;
    Ray rayNorm;
    Ray rayTri;
    public static bool waitForShoot, waitForShootTri;
    public static bool LastTouch,FirstTouch;
    public static bool lineRendererOn;
    public static bool lineRendererController;
    public Vector3 mousePos,mouseStartPos,mouseLastPos;
    // Start is called before the first frame update
    void Start()
    {
        LastTouch = false;
        FirstTouch = false;
        ObjectMirrorBool = false;
        ObjectMirrorReverse = false;
        AcceptShoot = false;
        rb = GetComponent<Rigidbody>();
        cam =GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
        PlayerPrefs.GetInt("FootballerChooser", 0);
        PlayerPrefs.SetInt("FootballerChooser", 5);
        waitForShoot = false;
        waitForShootTri = false;
        footballerTeleport = false;
        waitForShootTimer = 0;
        waitForShootTriTimer = 0;
        whichAnim = 0;
        lineRendererController = false;
        lineRendererOn = false;
        view = GetComponent<PhotonView>();
        ballScript = GetComponent<Ball>();
        if (view.IsMine)
        {

            if (PlayerPrefs.GetInt("FootballerChooser") == 1)
            {
                OurFootballer = PhotonNetwork.Instantiate(Ronaldinho.name, new Vector3(transform.position.x + 2.6f, transform.position.y - 0.3f, transform.position.z + 1.6f), Quaternion.identity);
                TrivelaFootballer = PhotonNetwork.Instantiate(Ronaldinho.name, new Vector3(transform.position.x + 1.5f, transform.position.y - 0.3f, transform.position.z + 1.7f), Quaternion.identity);
                distanceP = transform.position - OurFootballer.transform.position;
                distanceT = transform.position - TrivelaFootballer.transform.position;
                OurFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
                TrivelaFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
                footballerAnimator = OurFootballer.GetComponent<Animator>();
                trivelaAnimator = TrivelaFootballer.GetComponent<Animator>();
                //OurFootballer.SetActive(false);
                view.RPC("HideOurFootballer", RpcTarget.All, OurFootballer.GetComponent<PhotonView>().ViewID.ToString());
                //en ba�ta millet �ok yak�nken futbolcular �ne ge�mesin diye
                view.RPC("HideOurFootballer", RpcTarget.All, TrivelaFootballer.GetComponent<PhotonView>().ViewID.ToString());
            }
            else if (PlayerPrefs.GetInt("FootballerChooser") == 0)
            {
                OurFootballer = PhotonNetwork.Instantiate(Messi.name, new Vector3(transform.position.x + 2.6f, transform.position.y - 0.3f, transform.position.z + 1.6f), Quaternion.identity);
                TrivelaFootballer = PhotonNetwork.Instantiate(Messi.name, new Vector3(transform.position.x + 1.5f, transform.position.y - 0.3f, transform.position.z + 1.7f), Quaternion.identity);
                distanceP = transform.position - OurFootballer.transform.position;
                distanceT = transform.position - TrivelaFootballer.transform.position;
                OurFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
                TrivelaFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
                footballerAnimator = OurFootballer.GetComponent<Animator>();
                trivelaAnimator = TrivelaFootballer.GetComponent<Animator>();
                //OurFootballer.SetActive(false);
                view.RPC("HideOurFootballer", RpcTarget.All, OurFootballer.GetComponent<PhotonView>().ViewID.ToString());
                //en ba�ta millet �ok yak�nken futbolcular �ne ge�mesin diye
                view.RPC("HideOurFootballer", RpcTarget.All, TrivelaFootballer.GetComponent<PhotonView>().ViewID.ToString());
            }
            else if (PlayerPrefs.GetInt("FootballerChooser") == 2)
            {
                OurFootballer = PhotonNetwork.Instantiate(Alexia.name, new Vector3(transform.position.x + 2.6f, transform.position.y - 0.3f, transform.position.z + 1.6f), Quaternion.identity);
                TrivelaFootballer = PhotonNetwork.Instantiate(Alexia.name, new Vector3(transform.position.x + 1.5f, transform.position.y - 0.3f, transform.position.z + 1.7f), Quaternion.identity);
                distanceP = transform.position - OurFootballer.transform.position;
                distanceT = transform.position - TrivelaFootballer.transform.position;
                OurFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
                TrivelaFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
                footballerAnimator = OurFootballer.GetComponent<Animator>();
                trivelaAnimator = TrivelaFootballer.GetComponent<Animator>();
                //OurFootballer.SetActive(false);
                view.RPC("HideOurFootballer", RpcTarget.All, OurFootballer.GetComponent<PhotonView>().ViewID.ToString());
                //en ba�ta millet �ok yak�nken futbolcular �ne ge�mesin diye
                view.RPC("HideOurFootballer", RpcTarget.All, TrivelaFootballer.GetComponent<PhotonView>().ViewID.ToString());
            }
            else if (PlayerPrefs.GetInt("FootballerChooser") == 3)
            {
                OurFootballer = PhotonNetwork.Instantiate(Esmer.name, new Vector3(transform.position.x + 2.6f, transform.position.y - 0.3f, transform.position.z + 1.6f), Quaternion.identity);
                TrivelaFootballer = PhotonNetwork.Instantiate(Esmer.name, new Vector3(transform.position.x + 1.5f, transform.position.y - 0.3f, transform.position.z + 1.7f), Quaternion.identity);
                distanceP = transform.position - OurFootballer.transform.position;
                distanceT = transform.position - TrivelaFootballer.transform.position;
                OurFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
                TrivelaFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
                footballerAnimator = OurFootballer.GetComponent<Animator>();
                trivelaAnimator = TrivelaFootballer.GetComponent<Animator>();
                //OurFootballer.SetActive(false);
                view.RPC("HideOurFootballer", RpcTarget.All, OurFootballer.GetComponent<PhotonView>().ViewID.ToString());
                //en ba�ta millet �ok yak�nken futbolcular �ne ge�mesin diye
                view.RPC("HideOurFootballer", RpcTarget.All, TrivelaFootballer.GetComponent<PhotonView>().ViewID.ToString());
            }
            else if (PlayerPrefs.GetInt("FootballerChooser") == 4)
            {
                OurFootballer = PhotonNetwork.Instantiate(hanımKız.name, new Vector3(transform.position.x + 2.6f, transform.position.y - 0.3f, transform.position.z + 1.6f), Quaternion.identity);
                TrivelaFootballer = PhotonNetwork.Instantiate(hanımKız.name, new Vector3(transform.position.x + 1.5f, transform.position.y - 0.3f, transform.position.z + 1.7f), Quaternion.identity);
                distanceP = transform.position - OurFootballer.transform.position;
                distanceT = transform.position - TrivelaFootballer.transform.position;
                OurFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
                TrivelaFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
                footballerAnimator = OurFootballer.GetComponent<Animator>();
                trivelaAnimator = TrivelaFootballer.GetComponent<Animator>();
                //OurFootballer.SetActive(false);
                view.RPC("HideOurFootballer", RpcTarget.All, OurFootballer.GetComponent<PhotonView>().ViewID.ToString());
                //en ba�ta millet �ok yak�nken futbolcular �ne ge�mesin diye
                view.RPC("HideOurFootballer", RpcTarget.All, TrivelaFootballer.GetComponent<PhotonView>().ViewID.ToString());
            }
            else if (PlayerPrefs.GetInt("FootballerChooser") == 5)
            {
                OurFootballer = PhotonNetwork.Instantiate(Male.name, new Vector3(transform.position.x + 2.6f, transform.position.y - 0.3f, transform.position.z + 1.6f), Quaternion.identity);
                TrivelaFootballer = PhotonNetwork.Instantiate(Male.name, new Vector3(transform.position.x + 1.5f, transform.position.y - 0.3f, transform.position.z + 1.7f), Quaternion.identity);
                distanceP = transform.position - OurFootballer.transform.position;
                distanceT = transform.position - TrivelaFootballer.transform.position;
                OurFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
                TrivelaFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
                footballerAnimator = OurFootballer.GetComponent<Animator>();
                trivelaAnimator = TrivelaFootballer.GetComponent<Animator>();
                //OurFootballer.SetActive(false);
                view.RPC("HideOurFootballer", RpcTarget.All, OurFootballer.GetComponent<PhotonView>().ViewID.ToString());
                //en ba�ta millet �ok yak�nken futbolcular �ne ge�mesin diye
                view.RPC("HideOurFootballer", RpcTarget.All, TrivelaFootballer.GetComponent<PhotonView>().ViewID.ToString());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(mouseStartPos != null && mouseLastPos !=null && ObjectMirrorBool==false)
        //{
        //    if (mouseLastPos.y>mouseStartPos.y && LastTouch==true)
        //    {
        //        MirrorTheObject();
        //    }
        //}
        if (OurFootballer != null)
        {
            rayNorm = new Ray(OurFootballer.transform.position + Vector3.up * 10, Vector3.down); // bi t�k alt�ndan ba�lat�yoruz topun kendisini alg�lamas�n diye
        }
        if (TrivelaFootballer != null)
        {
            rayTri = new Ray(TrivelaFootballer.transform.position + Vector3.up * 10, Vector3.down); // bi t�k alt�ndan ba�lat�yoruz topun kendisini alg�lamas�n diye
        }
        RaycastHit hit;

        if (Physics.Raycast(rayNorm, out hit, Mathf.Infinity, ground))
        {
            OurFootballer.transform.position = new Vector3(OurFootballer.transform.position.x, hit.point.y - 0.15f, OurFootballer.transform.position.z);
        }
        if (Physics.Raycast(rayTri, out hit, Mathf.Infinity, ground))
        {
            TrivelaFootballer.transform.position = new Vector3(TrivelaFootballer.transform.position.x, hit.point.y - 0.15f, TrivelaFootballer.transform.position.z);
        }
        if (!Input.GetMouseButton(0))
        {
            if (OurFootballer != null)
            {
                footballerAnimator.SetBool("rightWalk", false);
                footballerAnimator.SetBool("leftWalk", false);
            }

            if (TrivelaFootballer != null)
            {
                trivelaAnimator.SetBool("rightWalk", false);
                trivelaAnimator.SetBool("leftWalk", false);
            }
        }
        else
        {
            if (MoveAroundObject.rotationaroundyaxis < 0)
            {
                if (OurFootballer != null)
                {
                    footballerAnimator.SetBool("rightWalk", true);
                    footballerAnimator.SetBool("leftWalk", false);
                }
                if (TrivelaFootballer != null)
                {
                    trivelaAnimator.SetBool("rightWalk", true);
                    trivelaAnimator.SetBool("leftWalk", false);
                }
            }
            else if (MoveAroundObject.rotationaroundyaxis > 0)
            {
                if (OurFootballer != null)
                {
                    footballerAnimator.SetBool("rightWalk", false);
                    footballerAnimator.SetBool("leftWalk", true);
                }
                if (TrivelaFootballer != null)
                {
                    trivelaAnimator.SetBool("rightWalk", false);
                    trivelaAnimator.SetBool("leftWalk", true);
                }
            }
        }
        Scene scene = SceneManager.GetActiveScene();
        if (ObjectMirrorBool == false)
        {
            if (scene.name == "Hole1")
            {
                if (TrivelaFootballer != null)
                {
                    TrivelaFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
                }
                if (OurFootballer != null)
                {
                    OurFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
                }
            }
            else if (scene.name == "Hole2" || scene.name == "Hole2Rainy")
            {
                if (TrivelaFootballer != null)
                {
                    TrivelaFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y + 30, 0);
                }
                if (OurFootballer != null)
                {
                    OurFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y + 30, 0);
                }
            }
            else if (scene.name == "Hole3" || scene.name == "Hole3Rainy")
            {
                if (TrivelaFootballer != null)
                {
                    TrivelaFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y + 65, 0);
                }
                if (OurFootballer != null)
                {
                    OurFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y + 65, 0);
                }
            }
            else if (scene.name == "Hole4" || scene.name == "Hole4Rainy")
            {
                if (TrivelaFootballer != null)
                {
                    TrivelaFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - ace, 0);
                }
                if (OurFootballer != null)
                {
                    OurFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - ace, 0);
                }
            }
        }
        if (view.IsMine)
        {
            if (waitForShoot == true)
            {
                waitForShootTimer += Time.deltaTime;
                Zoom.changeFovBool = false;
            }
            if (waitForShootTri == true)
            {
                waitForShootTriTimer += Time.deltaTime;
                Zoom.changeFovBool = false;
            }
            if (waitForShootTimer >= 0.9f)
            {
                //OurFootballer.SetActive(false);
                waitForShoot = false;
                waitForShootTimer = 0;
                footballerAnimator.SetBool("penaltyKick", false);
                trivelaAnimator.SetBool("trivela", false);
                AcceptShoot = true;
            }
            if (waitForShootTriTimer >= 0.65f)
            {
                //OurFootballer.SetActive(false);
                waitForShootTri = false;
                waitForShootTriTimer = 0;
                footballerAnimator.SetBool("penaltyKick", false);
                trivelaAnimator.SetBool("trivela", false);
                AcceptShoot = true;

            }
            if (Ball.shooted == false && rb.velocity.magnitude < stopVelocity && Input.GetMouseButton(0))
            {
                if (lineRendererOn == false)
                {
                    OurFootballer.transform.RotateAround(transform.position, Vector3.up, MoveAroundObject.rotationaroundyaxis / 60);
                    TrivelaFootballer.transform.RotateAround(transform.position, Vector3.up, MoveAroundObject.rotationaroundyaxis / 60);
                }
                else
                {
                    OurFootballer.transform.RotateAround(transform.position, Vector3.up, MoveAroundObject.rotationaroundyaxis / 300);
                    TrivelaFootballer.transform.RotateAround(transform.position, Vector3.up, MoveAroundObject.rotationaroundyaxis / 300);
                }

            }
            if (rb.velocity.magnitude < stopVelocity && footballerTeleport == false)
            {
                distanceP.y = 0.3f;
                distanceT.y = 0.3f;
                OurFootballer.transform.position = transform.position - distanceP;
                TrivelaFootballer.transform.position = transform.position - distanceT;
                //if (ObjectMirrorReverse == true)
                //{
                //    Debug.Log("calistim");
                //    MirrorTheObjectReverse();
                //}
                //ObjectMirrorBool = false;
                if (ObjectMirrorBool == true)
                {
                    MirrorTheObjectReverse();
                    ObjectMirrorBool = false;
                }
                LastTouch = false;
                FirstTouch = false;
                OurFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
                TrivelaFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
                footballerTeleport = true;
            }
            else if (rb.velocity.magnitude > stopVelocity)
            {
                footballerTeleport = false;
            }
        }
        
    }
    void MirrorTheObject()
    {
        // Calculate mirrored position
        Vector3 directionToReferencePoint = OurFootballer.transform.position - transform.position;
        Vector3 mirroredPosition = transform.position - directionToReferencePoint;
        Vector3 directionToReferencePointTri = TrivelaFootballer.transform.position - transform.position;
        Vector3 mirroredPositionTri = transform.position - directionToReferencePointTri;
        //ObjectMirrorBool = true;
        //ObjectMirrorReverse = true;
        // Set the mirrored object's position
        OurFootballer.transform.position = mirroredPosition;
        TrivelaFootballer.transform.position = mirroredPositionTri;

        // Calculate mirrored rotation
        OurFootballer.transform.rotation = Quaternion.Euler(0, (cam.transform.rotation.eulerAngles.y - 320)-180, 0);
        TrivelaFootballer.transform.rotation = Quaternion.Euler(0, (cam.transform.rotation.eulerAngles.y - 320) - 180, 0);

    }
    void MirrorTheObjectReverse()
    {
        // Calculate mirrored position
        Vector3 directionToReferencePoint = OurFootballer.transform.position - transform.position;
        Vector3 mirroredPosition = transform.position - directionToReferencePoint;
        Vector3 directionToReferencePointTri = TrivelaFootballer.transform.position - transform.position;
        Vector3 mirroredPositionTri = transform.position - directionToReferencePointTri;
        // Set the mirrored object's position
        OurFootballer.transform.position = mirroredPosition;
        TrivelaFootballer.transform.position = mirroredPositionTri;
        //ObjectMirrorReverse = false;
        //ObjectMirrorBool = false;

    }
    [PunRPC]
    void HideOurFootballer(string footballerPhotonViewId)
    {
        PhotonView targetFootballer = PhotonView.Find(int.Parse(footballerPhotonViewId));
        if (targetFootballer != null)
        {
            targetFootballer.gameObject.SetActive(false);
            //targetFootballer.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    [PunRPC]
    void ShowOurFootballer(string footballerPhotonViewId)
    {
        PhotonView targetFootballer = PhotonView.Find(int.Parse(footballerPhotonViewId));
        if (targetFootballer != null)
        {
            targetFootballer.gameObject.SetActive(true);
            //targetFootballer.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
    private void OnMouseUp()
    {
        if (LastTouch == false)
        {
            mouseLastPos = Input.mousePosition;
            //Debug.Log(mouseLastPos + " Last");
            LastTouch = true;
        }
    }
    private void OnMouseDown()
    {
        if (FirstTouch == false)
        {
            mouseStartPos = Input.mousePosition;
            mouseStartPos = Input.mousePosition;
            FirstTouch = true;
        }
        
        //Debug.Log(mouseStartPos + " Start");
        if (Ball.isIdle)
        {
            Ball.isAiming = true;
        }
        if (Ball.shooted == true)
        {
            //if(FirstTouch == false)
            //{
            //    mouseStartPos = Input.mousePosition;
            //    Debug.Log(mouseStartPos + " Start");
            //    FirstTouch = true;
            //}
            if (Input.GetMouseButtonDown(0) && Ball.shootCloser == false)
            {
                if (mouseLastPos.y > mouseStartPos.y)
                {
                    MirrorTheObject();
                    ObjectMirrorBool = true;
                }
                mousePos = Input.mousePosition;
                mouseStartPos = Vector3.zero;
                mouseLastPos = Vector3.zero;
                //Debug.Log(mousePos + " Sonuc");
                if (mousePos.x > Screen.width / 2)
                {
                    view.RPC("ShowOurFootballer", RpcTarget.All, OurFootballer.GetComponent<PhotonView>().ViewID.ToString());
                    view.RPC("HideOurFootballer", RpcTarget.All, TrivelaFootballer.GetComponent<PhotonView>().ViewID.ToString());
                    //OurFootballer.SetActive(true);
                    //TrivelaFootballer.SetActive(false);
                    footballerAnimator.SetBool("penaltyKick", true);
                    distanceP = transform.position - OurFootballer.transform.position;
                    distanceT = transform.position - TrivelaFootballer.transform.position;
                    waitForShoot = true;
                }
                else
                {
                    view.RPC("ShowOurFootballer", RpcTarget.All, TrivelaFootballer.GetComponent<PhotonView>().ViewID.ToString());
                    view.RPC("HideOurFootballer", RpcTarget.All, OurFootballer.GetComponent<PhotonView>().ViewID.ToString());
                    //TrivelaFootballer.SetActive(true);
                    //OurFootballer.SetActive(false);
                    trivelaAnimator.SetBool("trivela", true);
                    distanceT = transform.position - TrivelaFootballer.transform.position;
                    distanceP = transform.position - OurFootballer.transform.position;
                    waitForShootTri = true;

                }
            }

        }

    }
}
