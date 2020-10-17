using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EventOnHit : MonoBehaviour
{
    [Header("Event to set")]
    [SerializeField] UnityEvent response = new UnityEvent();

    bool canHit = true;
    Coroutine resetHit_Coroutine;

    void OnCollisionEnter(Collision collision)
    {
        //if hit a bat
        Bat bat = collision.gameObject.GetComponent<Bat>();
        if(bat && canHit)
        {
            //if super hit - or is broken bat and is on no player (so pushed)
            if(bat.GetComponent<Rigidbody>().angularVelocity.magnitude > bat.speedThreshold || bat is BatBroken && bat.GetComponent<BatBroken>().isOnPlayer == false)
            {
                canHit = false;

                //call event in inspector
                response?.Invoke();
            }
        }
    }

    public void RemoveRigidbodyConstraints()
    {
        //remove freeze position and stop event subscriber
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }

    public void ResetHitAfterTime(float timeToWait)
    {
        //reset hit after few seconds
        if (resetHit_Coroutine == null)
            resetHit_Coroutine = StartCoroutine(ResetHit_Coroutine(timeToWait));
    }

    IEnumerator ResetHit_Coroutine(float timeToWait)
    {
        //wait
        yield return new WaitForSeconds(timeToWait);

        //reset hit
        canHit = true;

        resetHit_Coroutine = null;
    }
}
