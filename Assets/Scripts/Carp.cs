using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carp : MonoBehaviour
{
    [Tooltip("Gravity on the fish when spawn")] [SerializeField] float gravity = -3;
    [Tooltip("Trail to instantiate on super hit")] [SerializeField] ParticleSystem carpaTrail = default;

    ParticleSystem trail = null;
    ParticleSystem Trail { 
        get 
        {
            //if null, instantiate
            if (trail == null)
                trail = Instantiate(carpaTrail, transform);
            
            return trail;
        } }

    bool alreadyGavePoints;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        //start falling
        StartCoroutine(Fall_Coroutine());
    }

    void OnCollisionEnter(Collision collision)
    {
        //if still falling by coroutine, stop and use gravity
        if (rb.useGravity == false)
            rb.useGravity = true;

        //only if not already gave points
        if (alreadyGavePoints == false)
        {
            //if hit objective
            Objective objective = collision.gameObject.GetComponent<Objective>();
            if (objective != null)
            {
                //add points on hit
                GameManager.instance.levelManager.AddPoints(objective.PointsOnHit);
                alreadyGavePoints = true;
            }
        }
    }

    IEnumerator Fall_Coroutine()
    {
        //move by script instead of gravity
        while(rb.useGravity == false)
        {
            rb.velocity = Vector3.up * gravity;
            yield return null;
        }
    }

    public void SuperHit()
    {
        //active trail
        Trail.Play(true);
    }
}
