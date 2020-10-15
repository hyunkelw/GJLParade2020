using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Patrol")]
    [SerializeField] Transform[] patrolPoints = default;
    [SerializeField] float patrolSpeed = 2;

    [Header("Look for Player")]
    [SerializeField] Vector3 center = default;
    [SerializeField] Vector3 size = default;

    [Header("Follow Player")]
    [SerializeField] float speed = 10;

    int patrolIndex;
    Coroutine patrol_Coroutine;
    Coroutine lookForEnemy_Coroutine;
    bool playerFound;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //start movement
        patrol_Coroutine = StartCoroutine(Patrol_Coroutine());

        //start look for player
        lookForEnemy_Coroutine = StartCoroutine(LookForEnemy_Coroutine());
    }

    void OnDrawGizmosSelected()
    {
        //show patrol
        Gizmos.color = Color.cyan;

        for(int i = 0; i < patrolPoints.Length; i++)
        {
            int nextPatrol = i < patrolPoints.Length - 1 ? i + 1 : 0;
            Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[nextPatrol].position);
        }

        //show vision
        Gizmos.color = Color.red;

        //matrix to use transform.rotation
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.matrix = rotationMatrix;
        Gizmos.DrawWireCube(center, size);
    }

    void FoundPlayer()
    {
        //stop patrol coroutines
        if(patrol_Coroutine != null)
            StopCoroutine(patrol_Coroutine);

        if (lookForEnemy_Coroutine != null)
            StopCoroutine(lookForEnemy_Coroutine);

        //start attack player
        playerFound = true;
        StartCoroutine(FollowPlayer_Coroutine());
    }

    #region patrol

    void NextPatrol()
    {
        //go to next patrol point
        patrolIndex++;

        //if reached limit, come back to first point
        if (patrolIndex >= patrolPoints.Length)
            patrolIndex = 0;
    }

    IEnumerator Patrol_Coroutine()
    {
        if (patrolPoints == null || patrolPoints.Length <= 0)
            yield break;

        //set vars
        Vector3 startPosition = transform.position;
        Vector3 endPosition = patrolPoints[patrolIndex].position;
        endPosition.y = startPosition.y;    //not on y axis

        float distance = Vector3.Distance(endPosition, startPosition);
        float currentPosition = 0;

        //movement
        float delta = 0;
        while(delta < 1)
        {
            currentPosition += Time.deltaTime * patrolSpeed;
            delta = currentPosition / distance;

            //move rigidbody
            Vector3 newPosition = Vector3.Lerp(startPosition, endPosition, delta);
            rb.position = newPosition;

            //look at patrol point
            transform.LookAt(endPosition);

            yield return null;
        }

        //move to next patrol point
        NextPatrol();
        patrol_Coroutine = StartCoroutine(Patrol_Coroutine());
    }

    IEnumerator LookForEnemy_Coroutine()
    {
        while(playerFound == false)
        {
            //check in a box, if hit player
            Collider[] colliders = Physics.OverlapBox(
                transform.position + center,
                size /2,
                transform.rotation,
                redd096.CreateLayer.LayerOnly("Player"),
                QueryTriggerInteraction.Ignore);

            //found player
            if(colliders.Length > 0)
            {
                FoundPlayer();
            }

            yield return null;
        }
    }

    #endregion

    #region follow player

    IEnumerator FollowPlayer_Coroutine()
    {
        if (GameManager.instance.player)
        {
            Transform player = GameManager.instance.player.transform;

            //get direction from enemy to player
            Vector3 direction = (player.position - transform.position).normalized;
            Vector3 velocity = direction * speed;

            //clamp
            velocity = Vector3.ClampMagnitude(velocity, speed);

            //move rigidbody
            rb.AddForce(velocity, ForceMode.Acceleration);

            //look at player (no y axis)
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

            yield return null;
        }
    }

    #endregion
}
