using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] Collider area = default;
    [SerializeField] GameObject[] prefabs = default;
    [SerializeField] float delay = 1.5f;

    float time;

    void Update()
    {
        if(Time.time > time)
        {
            time = Time.time + delay;
            CreateObject();
        }
    }

    void CreateObject()
    {
        //random position inside the area
        Vector3 center = area.bounds.center;
        Vector3 halfSize = area.bounds.extents;

        float x = Random.Range(center.x - halfSize.x, center.x + halfSize.x);
        float y = Random.Range(center.y - halfSize.y, center.y + halfSize.y);
        float z = Random.Range(center.z - halfSize.z, center.z + halfSize.z);

        //create object
        GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Length)]);
        obj.transform.position = new Vector3(x, y, z);
        obj.transform.rotation = Random.rotation;

        Destroy(obj, 15);
    }
}
