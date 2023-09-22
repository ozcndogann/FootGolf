using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    private Rigidbody ballRigidbody;
    public float teleportDistance = 2.0f; // teleport noktasýnýn duvardan uzaklýðýný belirliyoruz (hala iþe yaramasýný saðlayamadým) 
    public LayerMask teleportLayerMask; // teleport noktasý için zemin layerini seçmek
    public GameObject teleportDestination; // teleport destination diye bir empty gameobjectimiz var

    private void Start()
    {
        ballRigidbody = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>();
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            // teleport destinasyon objesinin collider boundlarý
            Bounds destinationBounds = teleportDestination.GetComponent<Collider>().bounds;

            // destinasyon objesi ile duvar trigger noktasý arasý en yakýn nokta
            Vector3 closestPoint = destinationBounds.ClosestPoint(other.gameObject.transform.position);

            // teleport yönünü duvar'dan closestpoint'e doðru çevirme
            Vector3 teleportDirection = transform.position - closestPoint;
            teleportDirection.y = 0; // dikey yönü almýyoruz, y sini sonra ayarlýcaz zemine göre
            teleportDirection.Normalize();

            // destinasyon objesinde sectigimiz closest noktaya göre teleport noktasý belirleme
            Vector3 newPosition = closestPoint + teleportDirection * teleportDistance;

            // toptan aþaðý ray atýyoruz zemini bulmak için
            Ray ray = new Ray(newPosition + Vector3.up * 10, Vector3.down); // bi týk altýndan baþlatýyoruz topun kendisini algýlamasýn diye
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, teleportLayerMask))
            {
                newPosition.y = hit.point.y + 0.2f; // teleport noktasýnýn y'sini zemin y'sine göre ayarlýyoruz (+0.2f zeminin içine ýþýnlanmasýn diye)
            }

            IEnumerator WFSOOB()
            {
                yield return new WaitForSeconds(1.95f);

                ballRigidbody.velocity = Vector3.zero; // harekete devam etmesin diye hýzý sýfýrlýyoruz
                other.transform.position = newPosition; // topu teleport ediyoruz o noktaya
            }

            StartCoroutine(WFSOOB());

        }
    }
}
