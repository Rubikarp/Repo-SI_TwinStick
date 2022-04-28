using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using NaughtyAttributes;

public enum BEE_STATE
{
    GROUNDED,
    FOLLOWING,
    WORKING,
}

//navAgent.SetDestination();+ Mathf.Rad2Deg % 360
[RequireComponent(typeof(BasicHealth))]
public class Bee : MonoBehaviour
{
    [Header("Move")]
    public bool linkToPlayer = false;
    //
    private NavMeshAgent navAgent;

    [Header("Data")]
    [SerializeField] BasicHealth health;
    public Hive hive;

    [Header("Bullet")]
    public GameObject beeBullet;
    public BulletPoolManager bulletPool;

    void Start()
    {
        health = this.gameObject.GetComponent<BasicHealth>();
    }

    void Update()
    {
        if (!linkToPlayer)
        {

        }
    }

    public void Pew(Vector3 dir)
    {
        bulletPool.Shoot(transform.position, Quaternion.LookRotation(dir, Vector3.up));
    }

    public void Die()
    {
        health.TakeDamage();
        Destroy(gameObject, 0.15f);
    }
}
