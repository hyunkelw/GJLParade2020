using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallManager : MonoBehaviour
{
    [Tooltip("Area spawn objects")] [SerializeField] Collider[] areas = default;
    [Tooltip("Prefabs to instantiate")] [SerializeField] GameObject[] prefabs = default;
    [Tooltip("Delay between spawn")] [SerializeField] float delay = 1.5f;
    [Tooltip("Time to destroy objects")] [SerializeField] float timeToDestroy = 15;

    float time;

    public bool IsSpawning { get; set; } = false;

    void Update()
    {
        if (!IsSpawning) { return; }

        if (Time.time > time)
        {
            time = Time.time + delay;
            CreateObject();
        }
    }

    void CreateObject()
    {
        //select one area
        Collider area = areas[Random.Range(0, areas.Length)];

        //random position inside the area
        Vector3 center = area.bounds.center;
        Vector3 halfSize = area.bounds.extents;

        float x = Random.Range(center.x - halfSize.x, center.x + halfSize.x);
        float y = Random.Range(center.y - halfSize.y, center.y + halfSize.y);
        float z = Random.Range(center.z - halfSize.z, center.z + halfSize.z);

        //create object and set position and random rotation
        GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Length)]);
        obj.transform.position = new Vector3(x, y, z);
        obj.transform.rotation = Random.rotation;

        //destroy after few seconds
        Destroy(obj, timeToDestroy);
    }
}
