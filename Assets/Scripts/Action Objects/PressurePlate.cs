using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] GameObject objectToTrigger = default;
    [SerializeField] float necessaryMass = 60;
    [SerializeField] bool stayPressed = true;
    [SerializeField] bool canPlayer = false;

    float currentMassOnPlatform;
    bool isPressed;

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
        //no with bat
        if (collision.gameObject.GetComponent<Bat>())
            return;

        //check if add rigidbody
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

        if (rb)
        {
            //if can player or is not a player
            if (canPlayer || collision.gameObject.GetComponent<Player>() == null)
            {
                AddMass(rb);
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        //no with bat
        if (collision.gameObject.GetComponent<Bat>())
            return;

        //check if remove rigidbody
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

        if (rb)
        {
            //if can player or is not a player
            if (canPlayer || collision.gameObject.GetComponent<Player>() == null)
            {
                RemoveMass(rb);
            }
        }
    }

    #region private API

    void AddMass(Rigidbody rb)
    {
        //add mass
        currentMassOnPlatform += rb.mass;

        //if reached necessary mass, press
        if(currentMassOnPlatform >= necessaryMass)
        {
            Press();
        }
    }

    void RemoveMass(Rigidbody rb)
    {
        //remove mass
        currentMassOnPlatform -= rb.mass;

        //if no reach necessary mass, release
        if(currentMassOnPlatform < necessaryMass)
        {
            Release();
        }
    }

    void Press()
    {
        //check object to trigger
        if (objectToTrigger == null)
            return;

        //if is already pressed, and must to remain pressed, do nothing
        if (isPressed && stayPressed)
            return;

        isPressed = true;

        //if has IAction, then call Action
        IAction action = objectToTrigger.GetComponent<IAction>();

        if (action != null)
        {
            action.Action();
        }
    }

    void Release()
    {
        //check object to trigger
        if (objectToTrigger == null)
            return;

        //if is already pressed, and must to remain pressed, do nothing
        if (isPressed && stayPressed)
            return;

        isPressed = false;

        //if has IAction, then call reverse action
        IAction action = objectToTrigger.GetComponent<IAction>();

        if (action != null)
        {
            action.ReverseAction();
        }
    }

    #endregion
}