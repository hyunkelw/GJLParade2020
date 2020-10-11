using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using redd096;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float speed = 5;
    [SerializeField] CameraBaseControl cameraBaseControl = default;

    [Header("Bat")]
    [SerializeField] Bat bat = default;

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

        //if not swinging the bat, rotate the camera
        if (bat.SwingingBat == false)
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
