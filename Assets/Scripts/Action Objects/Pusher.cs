using UnityEngine;

public class Pusher : MonoBehaviour
{
    [SerializeField] Vector3 force = Vector3.forward;
    [SerializeField] ForceMode mode = ForceMode.VelocityChange;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawLine(transform.position, transform.position + force);
    }

    void OnCollisionEnter(Collision collision)
    {
        //push if hit rigidbody
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.AddForce(force, mode);
        }
    }
}
