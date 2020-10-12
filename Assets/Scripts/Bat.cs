﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using redd096;

public class Bat : MonoBehaviour
{
    [Header("Important")]
    [Tooltip("Speed lerp rotation")] [SerializeField] float speedRotation = 5;
    [Tooltip("Offset from camera position")] [SerializeField] Vector3 offset = Vector3.zero;

    [Header("Limit Rotation Bat")]
    [Tooltip("Y axis to negative")] [SerializeField] float left = -75;
    [Tooltip("Y axis to positive")] [SerializeField] float right = 75;
    [Tooltip("X axis to negative")] [SerializeField] float up = -50;
    [Tooltip("X axis to positive")] [SerializeField] float down = 30;

    public bool SwingingBat { get; private set; }

    Camera cam;
    Rigidbody rb;

    Quaternion inputRotation = Quaternion.identity;

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //if pause, stop controlling bat
        if(Time.timeScale <= 0)
        {
            SwingingBat = false;
            return;
        }

        //on click
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            //if hit the bat, swinging true
            //if (CheckClickOnBat())
            {
                SwingingBat = true;

                //unlock cursor
                Utility.LockMouse(CursorLockMode.Confined);
            }
        }
        //on release
        else if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            //swinging false
            SwingingBat = false;

            //lock cursor
            Utility.LockMouse(CursorLockMode.Locked);
        }
    }

    void LateUpdate()
    {
        //set input rotation
        if (SwingingBat)
        {
            SetInputRotation();
        }
    }

    void FixedUpdate()
    {
        //follow cam position
        FollowCamPosition();

        //rotate bat
        RotateBat();
    }

    #region private API

    bool CheckClickOnBat()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        int layer = CreateLayer.LayerOnly("Bat");

        //return if hit the bat
        return Physics.Raycast(ray, 100, layer, QueryTriggerInteraction.Collide);
    }

    void SetInputRotation()
    {
        //mouse position to viewport (from 0,0 to 1,1)
        Vector3 viewportPosition = cam.ScreenToViewportPoint(Input.mousePosition);

        //from left to right, from down to up
        float leftRight = Mathf.Lerp(left, right, viewportPosition.x);
        float upDown = Mathf.Lerp(down, up, viewportPosition.y);

        //rotation on X and Y axis
        Vector3 euler = new Vector3(upDown, leftRight, 0);
        Quaternion rotation = Quaternion.Euler(euler);

        //lerp input rotation
        inputRotation = Quaternion.Lerp(inputRotation, rotation, Time.fixedDeltaTime * speedRotation);
    }

    void FollowCamPosition()
    {
        //cam position + offset
        Vector3 targetPosition = cam.transform.position + Direction.WorldToLocalDirection(offset, cam.transform.rotation);

        //move bat
        rb.MovePosition(targetPosition);
    }

    void RotateBat()
    {
        //camera rotation + input rotation
        rb.MoveRotation(cam.transform.rotation * inputRotation);
    }

    #endregion
}
