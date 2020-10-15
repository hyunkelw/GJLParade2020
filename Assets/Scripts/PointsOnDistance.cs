using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsOnDistance : MonoBehaviour
{
    [SerializeField] int scoreOnMeters = 1;

    Vector3 previousPosition;
    Coroutine addPoints_Coroutine;

    void OnCollisionEnter(Collision collision)
    {
        //if hit by bat
        Bat bat = collision.gameObject.GetComponentInParent<Bat>();
        if(bat)
        {
            //save start position
            previousPosition = transform.position;

            //and start coroutine
            if (addPoints_Coroutine != null)
                StopCoroutine(addPoints_Coroutine);

            addPoints_Coroutine = StartCoroutine(AddPoints_Coroutine());
        }
        //if not hit bat
        else
        {
            //stop coroutine because reached the destination
            if (addPoints_Coroutine != null)
                StopCoroutine(addPoints_Coroutine);
        }
    }

    IEnumerator AddPoints_Coroutine()
    {
        while(true)
        {
            //check distance from previous position
            int distance = Mathf.RoundToInt( Vector3.Distance(previousPosition, transform.position) );

            //add points
            GameManager.instance.levelManager.AddPoints(scoreOnMeters * distance);

            //save new position
            previousPosition = transform.position;

            yield return null;
        }
    }
}
