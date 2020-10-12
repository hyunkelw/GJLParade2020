using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carp : MonoBehaviour
{
    [Tooltip("Gravity on the fish when spawn")] [SerializeField] float gravity = -3;
    [Tooltip("Particles to instantiate on super hit")] [SerializeField] ParticleSystem carpParticle = default;
    [Tooltip("Trails to instantiate on super hit")] [SerializeField] TrailRenderer carpTrail = default;

    ParticleSystem particle;
    TrailRenderer trail;

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
        //instantiate particles
        if (carpParticle != null)
        {
            if(particle == null)
                particle = Instantiate(carpParticle, transform);

            //restart if hit again
            particle.Play(true);
        }

        //instantiate trail
        if (carpTrail != null)
        {
            if(trail == null)
                trail = Instantiate(carpTrail, transform);
        }
    }
}
