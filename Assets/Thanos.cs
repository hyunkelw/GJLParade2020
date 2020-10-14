using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thanos : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        //if hit objective
        var shrinkable = collision.gameObject.GetComponent<Shrinker>();
        if (shrinkable != null)
        {
            shrinkable.Shrink();
        }

    }
}
