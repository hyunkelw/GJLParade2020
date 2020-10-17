using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    void Awake()
    {
        //il rigidbody può essere rimosso o reso kinematic o lockare posizioni e rotazioni
    }

    //quando viene colpito deve lampeggiare di rosso

    //quando muore deve resettare il rigidbody per tornare normale e aggiungere un AddForce per spararlo via
    //per sbloccare la mazza deve fare PlayerPrefs.SetInt("Boss Killed", 1);
}
