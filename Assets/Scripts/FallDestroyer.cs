using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDestroyer : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        //if hit player, game over
        Player player = collision.gameObject.GetComponent<Player>();
        if(player)
        {
            GameManager.instance.levelManager.GameOver(false);
        }
        //else destroy object
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
