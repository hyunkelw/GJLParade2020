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
                Player player = GameManager.instance.player;

                //if player in radius
                if (player && Vector3.Distance(player.transform.position, transform.position) <= radius)
                {
                    //get velocity only X and Z axis
                    Vector3 velocity = player.transform.position - transform.position;
                    velocity.y = 0;
                    velocity = velocity.normalized * force;

                    //velocity on Y axis
                    Vector3 upVelocity = Vector3.up * upwardsModifier;

                    //explosion
                    player.GetComponent<Rigidbody>().AddForce(velocity + upVelocity, ForceMode.VelocityChange);
                    alreadyExploded = true;
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        //draw explosion
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
