using UnityEngine;

public class BuildingExplosion : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Carp"))
        {
            foreach (Transform child in transform)
                child.gameObject.AddComponent<Rigidbody>();
        }
    }

}
