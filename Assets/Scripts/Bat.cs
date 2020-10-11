using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    [Header("Rotate Bat")]
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

    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0) == false)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = transform.position.z;
            Vector3 worldPosition = cam.ScreenToWorldPoint(mousePosition);

            Debug.Log(mousePosition + " -- " + worldPosition);

            //move position
            Vector3 position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
            //rb.MovePosition(position);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //mouse position to viewport (from 0,0 to 1,1)
            Vector3 viewportPosition = cam.ScreenToViewportPoint(Input.mousePosition);

            //from left to right, from down to up
            float leftRight = Mathf.Lerp(left, right, viewportPosition.x);
            float upDown = Mathf.Lerp(down, up, viewportPosition.y);

            //rotate X and Y axis
            Vector3 euler = new Vector3(upDown, leftRight, 0);
            //rb.rotation = Quaternion.Euler(euler);
            rb.MoveRotation(Quaternion.Euler(euler));
            //transform.eulerAngles = new Vector3(upDown, leftRight, 0);
        }
    }
}
