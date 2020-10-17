using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDestroyer : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        //if hit player, game over
        if(collision.gameObject.GetComponent<Player>())
        {
            GameManager.instance.levelManager.GameOver(false);
            return;
        }

        //if hit carp and is falling by spawn, don't destroy
        Carp carp = collision.gameObject.GetComponent<Carp>();
        if (carp && carp.canDestroy == false)
        {            
            return;
        }

        //destroy object
        Destroy(collision.gameObject);
    }
}
