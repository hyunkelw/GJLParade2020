using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carp : MonoBehaviour
{
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

    void OnCollisionEnter(Collision collision)
    {
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

    public void SuperHit()
    {
        //active trail
        Trail.Play(true);
    }
}
