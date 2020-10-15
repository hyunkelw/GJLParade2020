using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IAction
{
    [SerializeField] float timeToOpen = 2;
    [SerializeField] Vector3 direction = Vector3.up;
    [SerializeField] float distance = 1;

    Coroutine open_Coroutine;
    bool isOpen;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        //open or close
        Vector3 dir = isOpen ? -direction : direction;

        //matrix to use transform.rotation
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.matrix = rotationMatrix;

        Gizmos.DrawLine(Vector3.zero, redd096.Vect.DivideVector3(dir * distance, transform.lossyScale));
        Gizmos.DrawWireCube(redd096.Vect.DivideVector3(dir * distance, transform.lossyScale), Vector3.one);
    }

    IEnumerator Open_Coroutine(bool openDoor)
    {
        float delta = 0;
        Vector3 startPosition = transform.localPosition;

        //open or close
        Vector3 dir = openDoor ? direction : -direction;
        dir = redd096.Direction.WorldToLocalDirection(dir, transform.rotation);

        //if is open, come back to close, else open
        Vector3 endPosition = transform.localPosition + dir * distance;

        //move animation
        while(delta < 1)
        {
            delta += Time.deltaTime / timeToOpen;

            transform.localPosition = Vector3.Lerp(startPosition, endPosition, delta);
            yield return null;
        }

        open_Coroutine = null;
    }

    public void Action()
    {
        //start only if not moving and the door is closed
        if (open_Coroutine == null && isOpen == false)
        {
            isOpen = true;
            open_Coroutine = StartCoroutine(Open_Coroutine(true));
        }
    }

    public void ReverseAction()
    {
        //start only if not moving and the door is open
        if (open_Coroutine == null && isOpen)
        {
            isOpen = false;
            open_Coroutine = StartCoroutine(Open_Coroutine(false));
        }
    }
}
