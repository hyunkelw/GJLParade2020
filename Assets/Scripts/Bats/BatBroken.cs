using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBroken : Bat
{
    [Header("Bat Broken")]
    [SerializeField] float throwForce = 100;
    [SerializeField] float timeBeforeGetNewBat = 1;

    bool isOnPlayer = true;

    protected override void Update()
    {
        //instead of start swing, on click push the bat
        if (Input.GetKeyDown(KeyCode.Mouse0) && isOnPlayer)
        {
            PushBat();

            //and give to player new bat
            GameManager.instance.player.GetNewBat(timeBeforeGetNewBat);

            //deactive, so can't push again
            isOnPlayer = false;
        }
    }

    protected override void FixedUpdate()
    {
        //follow camera only if is on player
        if (isOnPlayer)
        {
            base.FixedUpdate();
        }
    }

    void PushBat()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        //remove rigidbody
        rb.isKinematic = false;

        //and push
        rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
    }
}
