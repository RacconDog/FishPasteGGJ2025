using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform viewCone;

    [SerializeField] Transform[] patrolPos;
    [SerializeField] float patrolSpeed;
    [SerializeField] float patrolThreshold;

    enum EnemyState
    {
        Patrolling,
        Chase
    }

    int patrolIndex = 0;
    EnemyState curState = EnemyState.Patrolling;

    // Update is called once per frame
    void Update()
    {
        DebugStuff();
        // print(patrolIndex + "          " + Vector2.Distance(transform.position, patrolPos[patrolIndex].position));    

        if (patrolIndex > patrolPos.Length - 1) 
            {patrolIndex = 0;}

        if(curState == EnemyState.Patrolling)
        {
            transform.position += -(transform.position - patrolPos[patrolIndex].position).normalized * patrolSpeed * Time.deltaTime;

            if (Vector2.Distance(transform.position, patrolPos[patrolIndex].position) <= patrolThreshold)
            {
                patrolIndex += 1;
            }
        }
        
        RaycastHit2D playerCheck;
        playerCheck = Physics2D.Raycast(transform.position, player.transform.position - transform.position, math.INFINITY);

        float playerDist = Vector2.Distance(player.position, this.transform.position);
    
        if (playerCheck && playerCheck.transform.gameObject == player.gameObject && playerDist <= viewCone.lossyScale.x / 2)
        {
            print("Chasing");
        }
    }

    void DebugStuff()
    {
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
        if (viewCone.lossyScale.x != viewCone.lossyScale.y) 
            {Debug.LogError("WESTON |||| the viewcone isn't symetrical");}
    }
}
