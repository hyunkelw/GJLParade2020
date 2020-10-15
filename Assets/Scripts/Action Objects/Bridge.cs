using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour, IAction
{
    [SerializeField] float timeToOpen = 2;
    [SerializeField] Vector3 axisToRotate = Vector3.right;
    [SerializeField] float angle = 60;

    Coroutine open_Coroutine;
    bool isOpen;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        //open or close
        Vector3 dir = isOpen ? -axisToRotate : axisToRotate;

        //matrix to use transform.rotation
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.localRotation * Quaternion.Euler(dir * angle), transform.lossyScale);
        Gizmos.matrix = rotationMatrix;

        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }

    IEnumerator Open_Coroutine(bool openBridge)
    {
        float delta = 0;
        Vector3 startPosition = transform.localEulerAngles;

        //open or close
        Vector3 dir = openBridge ? -axisToRotate : axisToRotate;

        //if is open, come back to close, else open
        Vector3 endPosition = transform.localEulerAngles - dir * angle;

        //rotate animation
        while (delta < 1)
        {
            delta += Time.deltaTime / timeToOpen;

            transform.localEulerAngles = Vector3.Lerp(startPosition, endPosition, delta);
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
