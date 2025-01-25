using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public Transform player;

    [SerializeField] Transform[] patrolPos;
    [SerializeField] float patrolSpeed;
    [SerializeField] float patrolThreshold;

    [SerializeField] float chaseSpeed;

    [HideInInspector] public bool isInVisionCone;

    [SerializeField] LayerMask raycastLayerMask;
    [HideInInspector] public Transform rotTarget;

    enum EnemyState
    {
        Patrolling,
        Chase
    }

    int patrolIndex = 0;
    EnemyState curState = EnemyState.Patrolling;

    void Awake() 
    {
        rotTarget = patrolPos[0];
    }

    void Update()
    {
        DebugStuff();

        if (patrolIndex > patrolPos.Length - 1) 
            {patrolIndex = 0;}

        if(curState == EnemyState.Patrolling)
        {
            transform.position += -(transform.position - patrolPos[patrolIndex].position).normalized * patrolSpeed * Time.deltaTime;

            rotTarget = patrolPos[patrolIndex];
            if (Vector2.Distance(transform.position, patrolPos[patrolIndex].position) <= patrolThreshold)
            {
                patrolIndex += 1;
            }
        }
        
        RaycastHit2D playerCheck;
        playerCheck = Physics2D.Raycast(transform.position, player.transform.position - transform.position, math.INFINITY, raycastLayerMask);

        float playerDist = Vector2.Distance(player.position, this.transform.position);
    
        if (playerCheck && playerCheck.transform.gameObject == player.gameObject && isInVisionCone)
        {
            curState = EnemyState.Chase;
        }

        if (curState == EnemyState.Chase)
        {
            rotTarget = player;
            transform.position += -(transform.position - player.transform.position).normalized * chaseSpeed * Time.deltaTime;
        }
    }

    void DebugStuff()
    {
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
        // if (viewCone.lossyScale.x != viewCone.lossyScale.y) 
        //     {Debug.LogError("WESTON |||| the viewcone isn't symetrical");}
    }
    

}
