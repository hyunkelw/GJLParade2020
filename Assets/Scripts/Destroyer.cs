using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //if destroy player, restart game
        if(collision.gameObject.GetComponent<Player>())
        {
            redd096.SceneLoader.instance.RestartGame();
            return;
        }

        //else destroy object
        Destroy(collision.gameObject);
    }
}
