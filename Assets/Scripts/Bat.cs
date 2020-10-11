using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    [SerializeField] float speedRotation = 5;

    [Header("Limit Bat")]
    [Tooltip("Y axis to negative")] [SerializeField] float left = -75;
    [Tooltip("Y axis to positive")] [SerializeField] float right = 75;
    [Tooltip("X axis to negative")] [SerializeField] float up = -50;
    [Tooltip("X axis to positive")] [SerializeField] float down = 30;

    Camera cam;
    Rigidbody rb;

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //rotate bat on mouse position
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //get target rotation (mouse position)
            Quaternion targetRotation = GetTargetRotation();

            //lerp rigidbody rotation
            Quaternion rotation = Quaternion.Lerp(rb.rotation, targetRotation, Time.fixedDeltaTime * speedRotation);
            rb.MoveRotation(rotation);
        }
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
}
