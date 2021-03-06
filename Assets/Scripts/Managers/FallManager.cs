﻿
using System;
using UnityEngine;

[System.Serializable]
public struct CarpStruct
{
    public Carp carpPrefab;
    [Range(0, 100)] public int percentage;
}

public class FallManager : MonoBehaviour
{
    [Tooltip("Area spawn objects")]
    [SerializeField] private Collider[] areas = default;
    [Tooltip("Prefabs to instantiate")]
    [SerializeField] private FallManagerConfig_SO config = default;
    //[SerializeField] private CarpStruct[] carps = default;
    [Tooltip("Delay between spawn")]
    [SerializeField] private float delay = 1.5f;
    [Header("Jump")]
    [SerializeField] private bool jump = false;
    [SerializeField] [Range(0f, 10f)] private float minJumpPower = 0;
    [SerializeField] [Range(0f, 10f)] private float maxJumpPower = 0;
    [SerializeField] [Range(0f, 10f)] private float minDuration = 0;
    [SerializeField] [Range(0f, 10f)] private float maxDuration = 0;

    float time;


    public bool IsSpawning { get; set; } = false;
    public Collider[] Areas { get => areas; set => areas = value; }

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
        //select one area
        Collider area = areas[UnityEngine.Random.Range(0, areas.Length)];

        //random position inside the area
        Vector3 center = area.bounds.center;
        Vector3 halfSize = area.bounds.extents;

        float x = UnityEngine.Random.Range(center.x - halfSize.x, center.x + halfSize.x);
        float y = UnityEngine.Random.Range(center.y - halfSize.y, center.y + halfSize.y);
        float z = UnityEngine.Random.Range(center.z - halfSize.z, center.z + halfSize.z);
        Vector3 randomPosition = new Vector3(x, y, z);

        //find random prefab based on percentage
        int random = UnityEngine.Random.Range(0, 100);
        int percentage = 0;

        for (int i = 0; i < config.carps.Length; i++)
        {
            percentage += config.carps[i].percentage;

            //if is this one, instantiate prefab in the random position
            if (random < percentage)
            {
                InstantiateCarp(config.carps[i].carpPrefab, randomPosition);
                return;
            }
        }
    }

    public void SetConfig(FallManagerConfig_SO bossConfig)
    {
        config = bossConfig;
    }

    void InstantiateCarp(Carp carpPrefab, Vector3 randomPosition)
    {
        //instantiate carp
        Carp carp = Instantiate(carpPrefab, randomPosition, UnityEngine.Random.rotation);
        CheckIsMagnifyingBat(carp);
        if (jump)
        {
            var jumpPower = UnityEngine.Random.Range(minJumpPower, maxJumpPower);
            var duration = UnityEngine.Random.Range(minDuration, maxDuration);
            carp.Jump(GameManager.instance.player.transform.position, jumpPower, 1, duration);
            //carp.GetComponent<Rigidbody>().DOJump(GameManager.instance.player.transform.position, jumpPower, 1, duration);
        }
    }

    public void SetSpawnValues(float minJumpPower, float maxJumpPower, float minDuration, float maxDuration)
    {
        this.minJumpPower = minJumpPower;
        this.maxJumpPower = maxJumpPower;
        this.minDuration = minDuration;
        this.maxDuration = maxDuration;
    }

    void CheckIsMagnifyingBat(Carp carp)
    {
        //if is magnifying bat, apply multiplier
        if (GameManager.instance.levelManager.batMagnifyingEquipped)
            carp.transform.localScale *= GameManager.instance.levelManager.SizeMultiplier;
    }
}
