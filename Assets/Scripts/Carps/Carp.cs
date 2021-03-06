﻿using DG.Tweening;
using UnityEngine;

public class Carp : MonoBehaviour
{
    [Tooltip("Particles to instantiate on super hit")] [SerializeField] ParticleSystem[] particlesPrefabs = default;
    [Tooltip("Trails to instantiate on super hit")] [SerializeField] TrailRenderer[] trailsPrefabs = default;
    [Tooltip("Cheater")] [SerializeField] bool isCheatingActive = default;
    [Tooltip("Sound")] [SerializeField] AudioClip whistle = default;

    ParticleSystem[] particles;
    TrailRenderer[] trails;
    AudioSource carpSound;

    bool alreadyGavePoints;

    Rigidbody rb;
    private Tween jumping;
    private Target targetComponent;


    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        targetComponent = GetComponent<Target>();
        carpSound = GetComponent<AudioSource>();
    }

    void Start()
    {
        rb.useGravity = false;
    }

    public void Jump(Vector3 position, float jumpPower, int numJumps, float duration)
    {
        jumping = rb.DOJump(position, jumpPower, numJumps, duration);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!carpSound.isPlaying)
        {
            carpSound.Play();
        }
        //if still falling by coroutine, stop and use gravity
        if (rb.useGravity == false)
        { 
            rb.useGravity = true;
            jumping.Kill();
            if (isCheatingActive)
            {
                rb.useGravity = false;
                rb.DOMove(FindObjectOfType<Airplane>().transform.position, 2f).OnComplete(()=> rb.useGravity=true);
            }
        }
        if (targetComponent != null)
        {
            targetComponent.enabled = false;
        }
        

        //only if not already gave points
        if (alreadyGavePoints == false)
        {
            //if hit objective
            Objective objective = collision.gameObject.GetComponent<Objective>();
            if (objective != null && !objective.actsOnTrigger)
            {
                //add points on hit
                GameManager.instance.levelManager.AddPoints(objective.PointsOnHit);
                alreadyGavePoints = true;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        //only if not already gave points
        if (alreadyGavePoints == false)
        {
            //if hit objective
            Objective objective = other.GetComponentInParent<Objective>();
            if (objective != null && objective.actsOnTrigger)
            {
                //add points on hit
                GameManager.instance.levelManager.AddPoints(objective.PointsOnHit);
                alreadyGavePoints = true;
            }
        }
    }

    public void SuperHit()
    {
        //instantiate particles
        InstantiateParticles();

        //instantiate trail
        InstantiateTrail();

        AudioSource.PlayClipAtPoint(whistle, transform.position, .1f);
        

        //if has explosive, set super hit
        Explosive explosive = GetComponent<Explosive>();
        if (explosive)
        {
            explosive.superHit = true;
        }
    }

    #region super hit

    void InstantiateParticles()
    {
        //instantiate particles
        if (particlesPrefabs != null && particlesPrefabs.Length > 0)
        {
            //if there is no reference, instantiate and save every particle
            if (particles == null || particles.Length <= 0)
            {
                particles = new ParticleSystem[particlesPrefabs.Length];
                for (int i = 0; i < particlesPrefabs.Length; i++)
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
    }

    void InstantiateTrail()
    {
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

    #endregion
}
