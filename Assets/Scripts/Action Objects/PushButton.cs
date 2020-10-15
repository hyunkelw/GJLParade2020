using UnityEngine;

public class PushButton : MonoBehaviour
{
    [SerializeField] GameObject objectToTrigger = default;

    void OnDrawGizmosSelected()
    {
        //draw line to object to trigger
        if (objectToTrigger)
        {
            Gizmos.color = Color.cyan;

            Gizmos.DrawLine(transform.position, objectToTrigger.transform.position);
            Gizmos.DrawWireSphere(objectToTrigger.transform.position, 0.5f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //if hitted by a carp or super hit by bat
        if(collision.gameObject.GetComponent<Carp>() ||
            collision.gameObject.GetComponent<Bat>() && collision.rigidbody.angularVelocity.magnitude > collision.gameObject.GetComponent<Bat>().speedThreshold)
        {
            Press();
        }
    }

    void Press()
    {
        //check object to trigger
        if (objectToTrigger == null) 
            return;

        //if has IAction, then call Action
        IAction action = objectToTrigger.GetComponent<IAction>();

        if (action != null)
            action.Action();
    }
}
