using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    [Header("Explosion")]
    [Tooltip("Radius of explosion")] [SerializeField] float radius = 6;
    [Tooltip("Explosion Force on X and Z axis")] [SerializeField] float force = 5;
    [Tooltip("Explosion Force on Y axis to lift up Player")] [SerializeField] float upwardsModifier = 1;

    [Header("Designer :mumble mumble:")]
    [Tooltip("Explode when hit a carp or only on the ground?")] [SerializeField] bool explodeOnCarpHit = false;

    [Header("Particles")]
    [SerializeField] GameObject particles = default;
    [SerializeField] bool isOnlyOnSuperHit = false;

    [HideInInspector] public bool superHit;

    bool alreadyExploded;

    void OnCollisionEnter(Collision collision)
    {
        //do only one time
        if (alreadyExploded)
            return;

        //if is not bat or player
        if(collision.gameObject.GetComponent<Bat>() == false && collision.gameObject.GetComponent<Player>() == false)
        {
            //if can explode on carp, or if not hit a carp (hit ground)
            if (explodeOnCarpHit || collision.gameObject.GetComponent<Carp>() == false)
            {
                //explosion
                Explode();
            }

            superHit = false;
        }
    }

    void OnDrawGizmos()
    {
        //draw explosion
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Explode()
    {
        //set already exploded
        alreadyExploded = true;

        List<Rigidbody> pushedRigidbodies = new List<Rigidbody>();
        int layer = redd096.CreateLayer.LayerOnly(new string[] { "Player" });//, "Falling Object" });

        //overlap
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layer);

        //foreach collider in the radius
        foreach(Collider col in colliders)
        {
            //if has a rigidbody and is not in the list (not already pushed)
            Rigidbody rb = col.GetComponentInParent<Rigidbody>();
            if(rb && pushedRigidbodies.Contains(rb) == false)
            {
                //add to list
                pushedRigidbodies.Add(rb);

                //push it
                PushRigidbody(rb);
            }
        }

        //instantiate particles
        ExplosionParticles();
    }

    void PushRigidbody(Rigidbody rb)
    {
        //get velocity only X and Z axis
        Vector3 velocity = rb.transform.position - transform.position;
        velocity.y = 0;
        velocity = velocity.normalized * force;

        //velocity on Y axis
        Vector3 upVelocity = Vector3.up * upwardsModifier;

        //push
        rb.AddForce(velocity + upVelocity, ForceMode.Impulse);
    }

    void ExplosionParticles()
    {
        //if is a super hit, or can instantiate particles always
        if (superHit || isOnlyOnSuperHit == false)
        {
            //if there is, instantiate particles at this position
            if (particles != null)
                Instantiate(particles, transform.position, Quaternion.identity);
        }
    }
}
