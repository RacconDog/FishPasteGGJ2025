using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyController : MonoBehaviour
{
    public float bubbleDistance = 10f;
    [SerializeField] public Vector3 mostRecentBubble;
    [HideInInspector] public Vector3 mostRecentBubbleDetectable;

    [SerializeField] public Transform player;

    [SerializeField] Transform[] patrolPos;
    [SerializeField] float patrolSpeed;
    [SerializeField] float patrolThreshold;

    [SerializeField] float chaseSpeed;

    [HideInInspector] public bool isInVisionCone;

    [SerializeField] LayerMask raycastLayerMask;
    [HideInInspector] public Transform rotTarget;

    [SerializeField] GameObject qPrefab;
    enum EnemyState
    {
        Patrolling,
        Chase,
        InvistagateBubble
    }

    int patrolIndex = 0;
    [SerializeField] EnemyState curState = EnemyState.Patrolling;

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
            if (mostRecentBubble != new Vector3(0,0,0) && Vector2.Distance(mostRecentBubble, transform.position) < bubbleDistance)
            {
                curState = EnemyState.InvistagateBubble;
                mostRecentBubbleDetectable = mostRecentBubble;
                Instantiate(qPrefab, transform.position, Quaternion.identity).GetComponent<QuestionMark>().follow = this.gameObject;
                mostRecentBubble = Vector3.zero;
            }

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
        if (curState == EnemyState.InvistagateBubble)
        {
            
            rotTarget = player;
            transform.position += -(transform.position - mostRecentBubbleDetectable).normalized * chaseSpeed * Time.deltaTime;
        }
    }

    void DebugStuff()
    {
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
    }
}
