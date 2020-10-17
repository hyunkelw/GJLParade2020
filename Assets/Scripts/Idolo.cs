using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idolo : MonoBehaviour
{
    static List<Idolo> idolosInScene = new List<Idolo>();

    void Start()
    {
        //add to the list
        idolosInScene.Add(this);
    }

    void OnCollisionEnter(Collision collision)
    {
        //if hit by a carp, remove from the list
        if(idolosInScene.Contains(this) && collision.gameObject.GetComponent<Carp>())
        {
            idolosInScene.Remove(this);

            //and try activate boss
            TryActivateBoss();
        }
    }

    void TryActivateBoss()
    {
        //if there are no more idolos in scene
        if(idolosInScene == null || idolosInScene.Count <= 0)
        {
            //activate boss
            GameManager.instance.levelManager.GetComponent<SpawnBoss>().Spawn();
        }
    }
}
