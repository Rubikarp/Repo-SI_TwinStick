using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public enum AI_STATE
{
    AI_STATE_SPAWNING,
    AI_STATE_IN_POOL,
    AI_STATE_IDLE,
    AI_STATE_REACH_TARGET,
    AI_STATE_ATTACK_TARGET,
    AI_STATE_DIE,
}

public enum AI_TYPE
{
    AI_MELEE,
    AI_MELEE_TURRET,
    AI_MELEE_GENERATOR,
    AI_RANGE,
    AI_RANGE_TURRET,
    AI_RANGE_GENERATOR,
}


public abstract class AEnnemy : MonoBehaviour
{
    public AI_STATE currentState;
    public AI_TYPE typeOfEnnemy;

    public GameObject target;
    private NavMeshAgent navAgent;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        switch (currentState)
        {
            case AI_STATE.AI_STATE_SPAWNING:
                // GameObject is currently Spwaning
                break;
            case AI_STATE.AI_STATE_IN_POOL:
                // GameObject is inactive
                break;
            case AI_STATE.AI_STATE_IDLE:
                // doing Nothing ( Waiting for a new target maybe ? )
                break;
            case AI_STATE.AI_STATE_REACH_TARGET:
                if (target == null)
                {
                    currentState = AI_STATE.AI_STATE_IDLE;
                    FindNewTarget();
                }
                else
                {
                    // Move To Target ( Change Agent destination )
                    navAgent.destination = target.transform.position;
                }
                break;
            case AI_STATE.AI_STATE_ATTACK_TARGET:
                if (target == null)
                {
                    currentState = AI_STATE.AI_STATE_IDLE;
                    FindNewTarget();
                }
                else
                {
                    // Stop Moving And Shoot Target ( Set Agent Speed to 0 ? )
                }
                break;
            case AI_STATE.AI_STATE_DIE:
                // Die
                break;
            default:
                break;
        }
    }
    [Button]
    public void FindNewTarget()
    {
        target = (GameObject) GameObject.FindObjectOfType<Hive>().gameObject;
        currentState = AI_STATE.AI_STATE_REACH_TARGET;
        
    }







}
