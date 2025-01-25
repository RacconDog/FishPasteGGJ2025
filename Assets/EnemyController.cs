using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform viewCone;

    // enum EnemyState
    // {
    //     Idle,
    //     Chase
    // }

    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
        DebugStuff();
        RaycastHit2D playerCheck;
        playerCheck = Physics2D.Raycast(transform.position, player.transform.position - transform.position, math.INFINITY);

        float playerDist = Vector2.Distance(player.position, this.transform.position);

        // print(viewCone.lossyScale.x / 2 + " ||| " + playerDist + " ||| " + playerCheck + " ||| " + playerCheck.transform.name);
    
        if (playerCheck && playerCheck.transform.name == "Player" && playerDist <= viewCone.lossyScale.x / 2)
        {
            print("Chasing");
        }
    }

    void DebugStuff()
    {
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
        if (viewCone.lossyScale.x != viewCone.lossyScale.y) {Debug.LogError("WESTON |||| the viewcone isn't symetrical");}
    }
}
