using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carp : MonoBehaviour
{
    [Tooltip("Particles to instantiate on super hit")] [SerializeField] ParticleSystem carpaTrail = default;

    public void SuperHit()
    {
        //active trail
        carpaTrail.Play(true);
    }
}
