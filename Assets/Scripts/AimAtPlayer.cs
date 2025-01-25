using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtPlayer : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float timeToReachTarget = .2f;

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        target = GetComponent<EnemyController>().rotTarget;
    }

    void Update()
    {
        target = GetComponent<EnemyController>().rotTarget;
        
        Vector3 targetRot = target.position - transform.position;

        transform.up = Vector3.SmoothDamp(
            transform.up, targetRot, ref velocity, timeToReachTarget);
    }
} 