using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CarpStruct
{
    public Carp carpPrefab;
    [Range(0, 100)] public int percentage;
}

public class FallManager : MonoBehaviour
{
    [Tooltip("Area spawn objects")] [SerializeField] List<Collider> areas = new List<Collider>();
    [Tooltip("Prefabs to instantiate")] [SerializeField] CarpStruct[] carps = default;
    [Tooltip("Delay between spawn")] [SerializeField] float delay = 1.5f;
    [Tooltip("Time to destroy objects")] [SerializeField] float timeToDestroy = 15;

    float time;

    public bool IsSpawning = false;

    void Update()
    {
        //only if IsSpawning is active
        if (!IsSpawning) { return; }

        //Create objects with delay
        if (Time.time > time)
        {
            time = Time.time + delay;
            CreateObject();
        }
    }

    void CreateObject()
    {
        if (areas == null || areas.Count <= 0)
            return;

        //select one area
        Collider area = areas[Random.Range(0, areas.Count)];

        //random position inside the area
        Vector3 center = area.bounds.center;
        Vector3 halfSize = area.bounds.extents;

        float x = Random.Range(center.x - halfSize.x, center.x + halfSize.x);
        float y = Random.Range(center.y - halfSize.y, center.y + halfSize.y);
        float z = Random.Range(center.z - halfSize.z, center.z + halfSize.z);
        Vector3 randomPosition = new Vector3(x, y, z);

        //find random prefab based on percentage
        int random = Random.Range(0, 100);
        int percentage = 0;

        for(int i = 0; i < carps.Length; i++)
        {
            percentage += carps[i].percentage;

            //if is this one, instantiate prefab in the random position
            if (random < percentage)
            {
                InstantiateCarp(carps[i].carpPrefab, randomPosition);
                return;
            }
        }
    }

    void InstantiateCarp(Carp carpPrefab, Vector3 randomPosition)
    {
        //instantiate carp
        Carp carp = Instantiate(carpPrefab, randomPosition, Random.rotation);

        //destroy after few seconds
        Destroy(carp.gameObject, timeToDestroy);
    }

    public void AddArea(Collider col)
    {
        areas.Add(col);
    }

    public void RemoveArea(Collider col)
    {
        if(areas.Contains(col))
            areas.Remove(col);
    }
}
