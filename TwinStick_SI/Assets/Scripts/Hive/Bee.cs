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
    WAITING,
    WORKING,
}

//navAgent.SetDestination();
[RequireComponent(typeof(BasicHealth))]
public class Bee : MonoBehaviour
{
    [HideInInspector ]public NavMeshAgent navAgent;
    private BEE_STATE _state = BEE_STATE.FOLLOWING;
    public Animator animator;
    [HideInInspector] public bool hasPollen; public Pollen pollenCaried;
    public BEE_STATE state 
    { 
        get { return _state; }
        set
        {
            _state = value;
            switch (value)
            {
                case BEE_STATE.GROUNDED:
                    animator.SetBool("death", true);
                    break;
                case BEE_STATE.FOLLOWING:
                    animator.SetBool("idle", true);
                    break;
                case BEE_STATE.WAITING:
                    animator.SetBool("idle", true);
                    navAgent.SetDestination(Hive.Instance.transform.position);
                    break;
                case BEE_STATE.WORKING:
                    animator.SetBool("idle", true);
                    break;
                default:
                    break;
            }
        } }

    [Header("Data")]
    [SerializeField] BasicHealth health;
    public Hive hive;

    [Header("Bullet")]
    public GameObject beeBullet;
    public BulletPoolManager bulletPool;



    void Start()
    {
        health = this.gameObject.GetComponent<BasicHealth>();
        navAgent = GetComponent<NavMeshAgent>();
    }



    public void Pew(Vector3 dir)
    {
        animator.SetTrigger("Attack");
        bulletPool.Shoot(transform.position, Quaternion.LookRotation(dir, Vector3.up));
    }

    public void Die()
    {
        
        health.TakeDamage();
        Destroy(gameObject, 0.15f);
    }
}
