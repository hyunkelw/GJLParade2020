using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Boss")]
    [SerializeField] float health = 100;
    [SerializeField] float damageByCarp = 10;
    [SerializeField] float pushOnDie = 100;

    [Header("Blink")]
    [SerializeField] private int blinkTimes = default;
    [SerializeField] float timeBlink = default;
    [SerializeField] Material blinkMaterial = default;

    [SerializeField] GameObject nukeParticles = default;
    [SerializeField] Transform nukePosition = default;

    Coroutine blink_Coroutine;
    

    void Awake()
    {
        //lock rigidbody
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    void OnCollisionEnter(Collision collision)
    {
        //if hit by a carp
        if(collision.gameObject.GetComponent<Carp>())
        {
            GetDamage();
        }
    }

    void GetDamage()
    {
        //get damage
        health -= damageByCarp;

        //blink if not already blinking
        if (blink_Coroutine == null)
            blink_Coroutine = StartCoroutine(Blink_Coroutine());

        //die
        if (health <= 0)
        {
            Die();
        }
    }

    IEnumerator Blink_Coroutine()
    {
        Renderer r = GetComponentInChildren<Renderer>();
        Material originalMat = r.material;

        for (int i = 0; i < blinkTimes; i++)
        {
            //change material

            r.material = blinkMaterial;

            //wait
            yield return new WaitForSeconds(timeBlink);

            //back to original material
            r.material = originalMat;
        }

        blink_Coroutine = null;
    }

    void Die()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        //unlock rigidbody and push
        rb.constraints = RigidbodyConstraints.None;
        rb.AddForce(-transform.forward * pushOnDie, ForceMode.VelocityChange);
        Instantiate(nukeParticles, nukePosition.position, Quaternion.identity);
        Destroy(gameObject, 3f);
        //save to unlock bat
        PlayerPrefs.SetInt("Boss Killed", 1);
    }
}
