using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using redd096;

public class Bat : MonoBehaviour
{
    [Header("Important")]
    [SerializeField] float speedRotation = 5;
    [SerializeField] Vector3 offset = Vector3.zero;

    [Header("Limit Bat")]
    [Tooltip("Y axis to negative")] [SerializeField] float left = -75;
    [Tooltip("Y axis to positive")] [SerializeField] float right = 75;
    [Tooltip("X axis to negative")] [SerializeField] float up = -50;
    [Tooltip("X axis to positive")] [SerializeField] float down = 30;

    Camera cam;
    Rigidbody rb;

    Quaternion lastBatRotation = Quaternion.identity;

    public bool SwingingBat { get; private set; }

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //on click
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            //if hit the bat, swinging true
            //if (CheckClickOnBat())
            {
                SwingingBat = true;

                //show cursors
                Utility.LockMouse(CursorLockMode.Confined);
            }
        }
        //on release
        else if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            //swinging false
            SwingingBat = false;

            //hide cursors
            Utility.LockMouse(CursorLockMode.Locked);
        }
    }

    void FixedUpdate()
    {
        //rotate bat
        if (SwingingBat)
        {
            RotateBat();
        }

        //follow cam position and rotation
        FollowCamPosition();
        FollowCamRotation();
    }

    #region private API

    bool CheckClickOnBat()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        int layer = CreateLayer.LayerOnly("Bat");

        //return if hit the bat
        return Physics.Raycast(ray, 100, layer, QueryTriggerInteraction.Collide);
    }

    void RotateBat()
    {
        //get target rotation (mouse position)
        Quaternion targetRotation = GetTargetRotation();

        //lerp rotation
        Quaternion rotation = Quaternion.Lerp(rb.rotation, targetRotation, Time.fixedDeltaTime * speedRotation);

        //move bat
        rb.MoveRotation(rotation);
        lastBatRotation = rotation;
    }

    Quaternion GetTargetRotation()
    {
        //mouse position to viewport (from 0,0 to 1,1)
        Vector3 viewportPosition = cam.ScreenToViewportPoint(Input.mousePosition);

        //from left to right, from down to up
        float leftRight = Mathf.Lerp(left, right, viewportPosition.x);
        float upDown = Mathf.Lerp(down, up, viewportPosition.y);

        //rotation on X and Y axis
        Vector3 euler = new Vector3(upDown, leftRight, 0);
        return Quaternion.Euler(euler);
    }

    void FollowCamPosition()
    {
        //player position + offset
        Vector3 targetPosition = cam.transform.position + Direction.WorldToLocalDirection(offset, cam.transform.rotation);

        //move bat
        rb.MovePosition(targetPosition);
    }

    void FollowCamRotation()
    {
        //rotate bat
        rb.MoveRotation(cam.transform.rotation * lastBatRotation);
    }

    #endregion
}
