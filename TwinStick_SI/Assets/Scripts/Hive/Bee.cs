using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.AI;

public enum BEE_STATE
{
    GROUNDED,
    FOLLOWING,
    WORKING,
}

//navAgent.SetDestination();+ Mathf.Rad2Deg % 360
public class Bee : MonoBehaviour
{
    public float turnSpeed;
    public GameObject player;
    public float distanceToPlayer;
    private NavMeshAgent navAgent;
    private Hive hive;
    [HideInInspector] public float turnAngle = 0;
    public GameObject beeBullet;
    public BulletPoolManager bulletPool;

    void Start()
    {
        
    }

    void Update()
    {
        SpinArround();
    }

    public void SpinArround()
    {
        turnAngle += Time.deltaTime * turnSpeed;
        turnAngle %= 360f;
        transform.position = player.transform.position + new Vector3(Mathf.Cos(turnAngle * Mathf.Deg2Rad), 0, Mathf.Sin(turnAngle * Mathf.Deg2Rad)) * distanceToPlayer;
    }

    public void Pew(Vector3 dir)
    {
        bulletPool.Shoot(transform.position, Quaternion.LookRotation(dir, Vector3.up));
    }
}
