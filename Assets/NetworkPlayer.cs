using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NetworkPlayer : MonoBehaviourPun, IPunObservable
{
    private Rigidbody _rigidbody;

    private Vector3 _netPosition;
    private Quaternion _netRotation;
    private Vector3 _previousPos;

    public bool teleportIfFar;
    public float teleportIfFarDistance;

    [Header("Lerping [Experimental")]
    public float smoothPos = 5.0f;
    public float smoothRot = 5.0f;
    const float someSmallThreshold = 0.75f;// bunu 0.75 de yapabiliriz topun durdu�u �ey falan
    private void Awake()
    {
        PhotonNetwork.SendRate = 40;//30du
        PhotonNetwork.SerializationRate = 20;//10du

        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //if (stream.IsWriting)
        //{
        //    stream.SendNext(_rigidbody.position);
        //    stream.SendNext(_rigidbody.rotation);
        //    stream.SendNext(_rigidbody.velocity);
        //}
        if (stream.IsWriting)
        {
            if (_rigidbody.velocity.magnitude > someSmallThreshold)
            {
                stream.SendNext(_rigidbody.position);
                stream.SendNext(_rigidbody.rotation);
                stream.SendNext(_rigidbody.velocity);
            }
        }

        else
        {
            _netPosition = (Vector3)stream.ReceiveNext();
            _netRotation = (Quaternion)stream.ReceiveNext();
            _rigidbody.velocity = (Vector3)stream.ReceiveNext();

            float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
            _netPosition += (_rigidbody.velocity * lag);
        }
    }

    void FixedUpdate()
    {
        if (photonView.IsMine) return;
        _rigidbody.MovePosition(Vector3.Lerp(_rigidbody.position, _netPosition, smoothPos * Time.fixedDeltaTime));
        _rigidbody.MoveRotation(Quaternion.Lerp(_rigidbody.rotation, _netRotation, smoothRot * Time.fixedDeltaTime));
        //_rigidbody.position = Vector3.Lerp(_rigidbody.position, _netPosition, smoothPos * Time.fixedDeltaTime);
        //_rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation, _netRotation, smoothRot * Time.fixedDeltaTime);

        if(Vector3.Distance(_rigidbody.position, _netPosition) > teleportIfFarDistance)
        {
            _rigidbody.position = _netPosition;
        }
    }
}