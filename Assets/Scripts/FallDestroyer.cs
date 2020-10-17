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

        //else destroy object
        Destroy(collision.gameObject);
    }
}
