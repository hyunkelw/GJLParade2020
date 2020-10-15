using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carp : MonoBehaviour
{
    [Tooltip("Gravity on the fish when spawn")] [SerializeField] float gravity = -3;
    [Tooltip("Particles to instantiate on super hit")] [SerializeField] ParticleSystem[] particlesPrefabs = default;
    [Tooltip("Trails to instantiate on super hit")] [SerializeField] TrailRenderer[] trailsPrefabs = default;

    ParticleSystem[] particles;
    TrailRenderer[] trails;

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
        if (particlesPrefabs != null && particlesPrefabs.Length > 0)
        {
            //if there is no reference, instantiate and save every particle
            if(particles == null || particles.Length <= 0)
            {
                particles = new ParticleSystem[particlesPrefabs.Length];
                for(int i = 0; i < particlesPrefabs.Length; i++)
                {
                    particles[i] = Instantiate(particlesPrefabs[i], transform);
                }
            }

            //restart if hit again
            foreach (ParticleSystem p in particles)
            {
                if (p != null)
                {
                    p.Play(true);
                }
            }
        }

        //instantiate trail
        if (trailsPrefabs != null && trailsPrefabs.Length > 0)
        {
            //if there is no reference, instantiate and save every trail
            if (trails == null || trails.Length <= 0)
            {
                trails = new TrailRenderer[trailsPrefabs.Length];
                for (int i = 0; i < trailsPrefabs.Length; i++)
                {
                    trails[i] = Instantiate(trailsPrefabs[i], transform);
                }
            }
        }
    }
}
