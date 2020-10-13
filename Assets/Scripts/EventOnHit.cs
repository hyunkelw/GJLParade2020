using UnityEngine;

public class EventOnHit : redd096.EventSubscriber
{
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
                InvokeEvent();

                //remove freeze position and stop event subscriber
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                enabled = false;
            }
        }
    }
}
