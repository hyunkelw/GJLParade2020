using UnityEngine;
using UnityEngine.Events;

public class EventOnHit : MonoBehaviour
{
    [Header("Event to set")]
    [SerializeField] UnityEvent response = new UnityEvent();

    void OnCollisionEnter(Collision collision)
    {
        //if hit a bat
        Bat bat = collision.gameObject.GetComponent<Bat>();
        if(bat)
        {
            //if super hit
            if(bat.GetComponent<Rigidbody>().angularVelocity.magnitude > bat.speedThreshold)
            {
                //call event in inspector
                response?.Invoke();
            }
        }
    }

    public void RemoveFreezePosition()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
