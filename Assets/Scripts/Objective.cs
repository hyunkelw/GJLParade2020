using System.Collections;
using UnityEngine;

public class Objective : MonoBehaviour
{

    [Header("Blink")]
    [SerializeField] private int blinkTimes = 3;
    [SerializeField] float timeBlink = .1f;
    [SerializeField] Material blinkMaterial = default;

    public int PointsOnHit = 10;
    public bool actsOnTrigger = false;
    
    Coroutine blink_Coroutine;

    void OnCollisionEnter(Collision collision)
    {
        //if hit by a carp
        if (collision.gameObject.GetComponent<Carp>())
        {
            //blink if not already blinking
            if (blink_Coroutine == null)
                blink_Coroutine = StartCoroutine(Blink_Coroutine());

        }
    }

        IEnumerator Blink_Coroutine()
    {
        Renderer r = GetComponentInChildren<Renderer>();
        Material originalMat = r.material;

        for (int i = 0; i < blinkTimes; i++)
        {
            //change material

            r.material = blinkMaterial;

            //wait
            yield return new WaitForSeconds(timeBlink);

            //back to original material
            r.material = originalMat;
        }

        blink_Coroutine = null;
    }
}
