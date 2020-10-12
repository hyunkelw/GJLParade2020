using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using redd096;

public class Bat : MonoBehaviour
{
    [Header("Important")]
    public float rotationSpeed = 10;
    [Tooltip("Offset from camera position")] [SerializeField] Vector3 offset = Vector3.zero;

    [Header("Limit Rotation Bat")]
    [Tooltip("Y axis to negative")] [SerializeField] float left = -75;
    [Tooltip("Y axis to positive")] [SerializeField] float right = 75;
    [Tooltip("X axis to negative")] [SerializeField] float up = -50;
    [Tooltip("X axis to positive")] [SerializeField] float down = 30;

    [Header("Super Hit")]
    [Tooltip("Speed to reach for super hit")] [SerializeField] float speedThreshold = 20;
    [Tooltip("Particles to instantiate on super hit")] [SerializeField] ParticleSystem batParticles = default;

    public bool SwingingBat { get; private set; }
    

    Camera cam;
    Rigidbody rb;

    Quaternion inputRotation = Quaternion.identity;
    Pooling<ParticleSystem> poolingParticles = new Pooling<ParticleSystem>();

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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("POTENZA MAZZATA: " + rb.angularVelocity.magnitude);

        //if hitting with a lot of speed
        if(rb.angularVelocity.magnitude > speedThreshold)
        {
            SuperHit(collision);
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

    void OldSetInputRotation()
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
        inputRotation = rotation;// Quaternion.Lerp(inputRotation, rotation, Time.fixedDeltaTime * speedRotation);
    }

    void SetInputRotation()
    {
        //get input rotation on X and Y axis
        Vector3 input = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        input *= rotationSpeed;

        //add to current rotation, with clamp
        Vector3 euler = inputRotation.eulerAngles + input;
        euler.x = Angle.NegativeAngle(euler.x, up, down);
        euler.y = Angle.NegativeAngle(euler.y, left, right);

        Quaternion rotation = Quaternion.Euler(euler);

        //set input rotation
        inputRotation = rotation;
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

    void SuperHit(Collision collision)
    {
        //pooling particles on the bat
        ParticleSystem go = poolingParticles.Instantiate(batParticles, collision.GetContact(0).point, Quaternion.identity);
        var cameraShake = cam.GetComponent<CameraShaker>();
        if (cameraShake.isActiveAndEnabled)
        {
            cameraShake.Shake();
        }
        
        go.Play(true);

        //if hit a carp
        Carp carp = collision.gameObject.GetComponent<Carp>();
        if (carp != null)
        {
            carp.SuperHit();
        }
    }

    #endregion
}
