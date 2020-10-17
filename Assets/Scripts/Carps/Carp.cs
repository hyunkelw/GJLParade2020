using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Carp : MonoBehaviour
{
    [Tooltip("Gravity on the fish when spawn")] [SerializeField] float gravity = -3;
    [Tooltip("Particles to instantiate on super hit")] [SerializeField] ParticleSystem[] particlesPrefabs = default;
    [Tooltip("Trails to instantiate on super hit")] [SerializeField] TrailRenderer[] trailsPrefabs = default;
    [Tooltip("Cheater")] [SerializeField] bool isCheatingActive = default;

    ParticleSystem[] particles;
    TrailRenderer[] trails;

    bool alreadyGavePoints;

    Rigidbody rb;
    private Tween jumping;
    private Target targetComponent;


    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        targetComponent = GetComponent<Target>();
    }

    void Start()
    {
        rb.useGravity = false;

        //start falling
        //StartCoroutine(Fall_Coroutine());
    }

    public void Jump(Vector3 position, float jumpPower, int numJumps, float duration)
    {
        jumping = rb.DOJump(position, jumpPower, numJumps, duration);
    }


    void OnCollisionEnter(Collision collision)
    {
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
            if (objective != null)
            {
                //add points on hit
                GameManager.instance.levelManager.AddPoints(objective.PointsOnHit);
                alreadyGavePoints = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //only if not already gave points
        if (alreadyGavePoints == false)
        {
            //if hit objective
            Objective objective = other.GetComponentInParent<Objective>();
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
        InstantiateParticles();

        //instantiate trail
        InstantiateTrail();

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
