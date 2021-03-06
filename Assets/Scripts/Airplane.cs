﻿using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Airplane : MonoBehaviour
{
    [SerializeField] List<Transform> patrolPoints = new List<Transform>();
    [SerializeField] private float travelSpeed = 2f;
    [SerializeField] private GameObject ExplosionVFX = default;
    private Rigidbody rb;
    private Tween patrol;
    Vector3[] path;
    [SerializeField] AudioClip aereoCade = default;
    [SerializeField] float volume = 0.5f;


    private void OnEnable()
    {

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        path = new Vector3[patrolPoints.Count];
        for (int i = 0; i < patrolPoints.Count; i++)
        {
            path[i] = patrolPoints[i].position;
        }
        //path[patrolPoints.Count + 1] = transform.position;


    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            StartPath();
        }
    }
#endif

    public void StartPath()
    {
        patrol = rb.DOPath(path, travelSpeed, PathType.CatmullRom)
                   .SetEase(Ease.Linear)
                   .SetSpeedBased()
                   .SetLookAt(0.01f)
                   .SetLoops(-1, LoopType.Incremental);
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.useGravity = true;
        patrol.Kill();

        redd096.SoundManager.StartMusic(GetComponent<AudioSource>(), aereoCade, volume, false);

        Instantiate(ExplosionVFX, transform.position, Quaternion.identity);
        Destroy(gameObject, 10f);

        //save objective
        PlayerPrefs.SetInt("Airplane Destroyed", 1);
    }
}
