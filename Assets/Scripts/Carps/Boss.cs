using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    //deve spawnare fuori mappa e avvicinarsi per affacciarsi al cono
    //il rigidbody può essere rimosso o reso kinematic o lockare posizioni e rotazioni
    //deve buttare fuori carpe esplosive (rimuovi le aree che ci sono ora in FallManager e aggiungerne una che si trova nella bocca della carpa?)
    //quando muore deve resettare il rigidbody per tornare normale e aggiungere un AddForce per spararlo via
    //per sbloccare la mazza deve fare PlayerPrefs.SetInt("Boss Killed", 1);
}
