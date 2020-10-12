using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using redd096;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [Tooltip("Speed movement")] [SerializeField] float speed = 5;
    [Tooltip("Jump force")] [SerializeField] float jump = 5;
    [SerializeField] CameraBaseControl cameraBaseControl = default;

    [Header("CheckGround")]
    [SerializeField] Vector3 center = Vector3.zero;
    [SerializeField] Vector3 size = Vector3.one;

    [Header("Bat")]
    [Tooltip("Bat to swing")] [SerializeField] Bat bat = default;
    [Tooltip("When swinging bat, lock the camera?")] public bool onSwingLockCamera = false;

    //check in a box, if hit something other than the player
    bool IsGrounded => Physics.OverlapBox(
        transform.position + center, 
        size / 2, 
        transform.rotation, 
        CreateLayer.LayerAllExcept(new string[] { "Player", "Bat", "Falling Object" }), 
        QueryTriggerInteraction.Ignore)
        .Length > 0;


    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //set default camera
        cameraBaseControl.StartDefault(Camera.main.transform, transform);
    }

    void Update()
    {
        //movement
        Movement(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Jump(Input.GetButtonDown("Jump"));
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

    void OnDrawGizmosSelected()
    {
        //draw check ground
        Gizmos.color = Color.red;

        //matrix to use transform.rotation
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.matrix = rotationMatrix;
        Gizmos.DrawWireCube(center, size);
    }

    #region private API

    void Movement(float inputHorizontal, float inputVertical)
    {
        //get direction by input
        Vector3 direction = Direction.WorldToLocalDirection(new Vector3(inputHorizontal, 0, inputVertical), transform.rotation);

        //set velocity
        Vector3 velocity = direction * speed;
        velocity = Vector3.ClampMagnitude(velocity, speed);
        
        //don't touch Y axis
        velocity.y = rb.velocity.y;
        
        //move rigidbody
        rb.velocity = velocity;
    }

    void Jump(bool inputJump)
    {
        //if press to jump and is grounded, jump (Y axis)
        if (inputJump && IsGrounded)
        {
            //change Y axis velocity
            rb.velocity = new Vector3(rb.velocity.x, jump, rb.velocity.z);
        }
    }

    #endregion
}
