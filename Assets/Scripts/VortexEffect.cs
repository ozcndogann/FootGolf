using UnityEngine;

public class VortexEffect : MonoBehaviour
{
    public float strength = 10f;
    public float effectRadius = 5f;

    void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, effectRadius);
        foreach (var hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = transform.position - rb.position;
                float distance = direction.magnitude;
                Vector3 force = direction.normalized * (strength / distance);
                rb.AddForce(force, ForceMode.Acceleration);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, effectRadius);
    }
}
