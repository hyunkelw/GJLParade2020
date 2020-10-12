using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using redd096;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [Tooltip("Speed movement")] [SerializeField] float speed = 5;
    [SerializeField] CameraBaseControl cameraBaseControl = default;

    [Header("Bat")]
    [Tooltip("Bat to swing")] [SerializeField] Bat bat = default;
    [Tooltip("When swinging bat, lock the camera?")] public bool onSwingLockCamera = false;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //set default camera
        cameraBaseControl.StartDefault(Camera.main.transform, transform);
    }

    void Update()
    {
        //do movement
        DoMovement();
    }

    void LateUpdate()
    {
        //make the camera follow the player
        cameraBaseControl.UpdateCameraPosition();

        //if not swinging the bat or there is no lock, rotate the camera
        if (bat.SwingingBat == false || onSwingLockCamera == false)
        {
            cameraBaseControl.UpdateRotation(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
    }

    #region private API

    void DoMovement()
    {
        //get direction by input
        Vector3 direction = Direction.WorldToLocalDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")), transform.rotation);

        //set velocity
        Vector3 velocity = direction * speed;
        velocity = Vector3.ClampMagnitude(velocity, speed);

        //move rigidbody
        rb.velocity = velocity;
    }

    #endregion
}
