using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using redd096;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [Tooltip("Speed movement")] [SerializeField] float speed = 5;
    [Tooltip("Jump force")] [SerializeField] float jump = 5;
    [SerializeField] float fallMultiplier = 3;
    [Tooltip("Time to wait before apply fall multiplier")] [SerializeField] float delayFallMutliplier = 0.5f;

    public CameraBaseControl cameraBaseControl = default;

    [Header("CheckGround")]
    [SerializeField] Vector3 center = Vector3.zero;
    [SerializeField] float radius = 2;

    [Header("Bat (used by options menu)")]
    public Bat[] batsToSwing;

    //check in a sphere, if hit something other than player and bat
    bool IsGrounded => Physics.OverlapSphere(
        transform.position + center,
        radius,
        CreateLayer.LayerAllExcept(new string[] { "Player", "Bat" }),
        QueryTriggerInteraction.Collide)
        .Length > 0;

    Rigidbody rb;
    Coroutine applyFallMultiplier_Coroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //set default camera
        cameraBaseControl.StartDefault(Camera.main.transform, transform);
    }

    void Update()
    {
        //jump
        Jump(Input.GetButtonDown("Jump"));
    }

    void FixedUpdate()
    {
        //move
        Movement(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        MultiplierGravity();
    }

    void LateUpdate()
    {
        //make the camera follow the player
        cameraBaseControl.UpdateCameraPosition();

#if UNITY_ANDROID

#else

        //rotate the camera
        cameraBaseControl.UpdateRotation(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

#endif
    }

    void OnDrawGizmosSelected()
    {
        //draw check ground
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + center, radius);

        ////matrix to use transform.rotation
        //Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        //Gizmos.matrix = rotationMatrix;
        //Gizmos.DrawWireCube(center, size);
    }

    #region private API

    void Movement(float inputHorizontal, float inputVertical)
    {
        //get velocity by input
        Vector3 direction = Direction.WorldToLocalDirection(new Vector3(inputHorizontal, 0, inputVertical), transform.rotation);
        Vector3 velocity = direction * speed;

        velocity = Vector3.ClampMagnitude(velocity, speed);

        //move rigidbody
        rb.AddForce(velocity, ForceMode.Acceleration);
    }

    void Jump(bool inputJump)
    {
        //if press to jump and is grounded, jump (Y axis)
        if (inputJump && IsGrounded)
        {
            //change Y axis velocity
            rb.AddForce(transform.up * jump, ForceMode.VelocityChange);
        }
    }

    void MultiplierGravity()
    {
        //if going down
        if (applyFallMultiplier_Coroutine == null && rb.velocity.y < 0)
        {
            //start coroutine
            applyFallMultiplier_Coroutine = StartCoroutine(ApplyMultiplierGravity_Coroutine());
        }
    }

    IEnumerator ApplyMultiplierGravity_Coroutine()
    {
        //wait
        yield return new WaitForSeconds(delayFallMutliplier);

        //apply fall multiplier while is going down
        while (rb.velocity.y < 0)
        {
            //apply fall multiplier
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

        applyFallMultiplier_Coroutine = null;
    }

    #endregion
}
