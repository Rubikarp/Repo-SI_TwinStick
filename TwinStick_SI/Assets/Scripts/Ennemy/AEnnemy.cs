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

    public int speed = 3;
    public int attackDistance = 10;
    public GameObject target;
    bool isPriorityTarget=false;
    protected NavMeshAgent navAgent;

    protected Vector3 attackPosition;

    // Start is called before the first frame update
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
                navAgent.isStopped = true;
                break;
            case AI_STATE.AI_STATE_IN_POOL:
                // GameObject is inactive
                break;
            case AI_STATE.AI_STATE_IDLE:
                // doing Nothing ( Waiting for a new target maybe ? )
                navAgent.isStopped = true;
                break;
            case AI_STATE.AI_STATE_REACH_TARGET:
                AI_STATE_REACH_TARGET();
                RotationBahevior();
                break;
            case AI_STATE.AI_STATE_ATTACK_TARGET:
                if (target == null) // Target is Dead
                {
                    currentState = AI_STATE.AI_STATE_IDLE;
                    FindNewTarget();
                }
                else
                {
                    // Stop Moving And Shoot Target ( Set Agent Speed to 0 ? )
                    AI_STATE_ATTACK_TARGET();
                }
                RotationBahevior();
                break;
            case AI_STATE.AI_STATE_DIE:
                // Die
                break;
            default:
                
                break;
        }
        Debug.DrawLine(navAgent.destination, navAgent.destination + Vector3.up * 3, Color.red);

        // 
    }

    #region Find Target Bahavior
    [Button]
    public void FindNewTarget()
    {
        isPriorityTarget = false;
        target = (GameObject) GameObject.FindObjectOfType<Hive>().gameObject;
        currentState = AI_STATE.AI_STATE_REACH_TARGET;
        
    }
    #endregion

    #region State Behavior
    // Override function if you have to change a bahavior

    public virtual void AI_STATE_IDLE()
    {
        // Try to find a target
        FindNewTarget();
        navAgent.isStopped = true;
    }

    public virtual void AI_STATE_REACH_TARGET()
    {

        if (target == null)
        {
            // Go to idle since there is no target
            currentState = AI_STATE.AI_STATE_IDLE;
        }
        else
        {
            if ((transform.position - target.transform.position).magnitude <= 30 )
            {
                // Change to Attack state if near target
                currentState = AI_STATE.AI_STATE_ATTACK_TARGET;


                Debug.DrawRay(target.transform.position, target.transform.forward * 100, Color.red, 10);
                Debug.DrawRay(target.transform.position, (transform.position - target.transform.position ).normalized * 100, Color.blue, 10);

                float angle = Vector3.Angle(target.transform.forward, (transform.position - target.transform.position).normalized);
                
                angle = Random.Range(angle - 20, angle + 20);
                Debug.Log(angle);
                attackPosition = target.transform.position + (new Vector3(Mathf.Sin(angle*Mathf.Deg2Rad), navAgent.destination.y, Mathf.Cos(angle * Mathf.Deg2Rad)) * attackDistance);
                navAgent.destination = attackPosition;



                /*float angle = Random.Range(anglebase - 5, anglebase + 5);

                

                Debug.Log(anglebase + " | " + angle);
                
                */


            }
            else
            {
                // Move To Target ( Change Agent destination )
                navAgent.isStopped = false;
                navAgent.destination = target.transform.position;
            }
            
        }
    }

    IEnumerator DrawAngle()
    {

            
            yield return new WaitForEndOfFrame();

    }

    public virtual void AI_STATE_ATTACK_TARGET()
    {
        if ((transform.position - target.transform.position).magnitude < 2)
        {
            navAgent.isStopped = true;
        }
        else
        {
            navAgent.isStopped = false;
        }

    }

    // Override Attack for each ennemy type
    public abstract void Attack();

    public virtual void AI_STATE_DIE()
    {
        // Override for special ennemy that pop up Turret or Generator
    }

    #endregion

    #region Detection Behavior
    void OnTriggerEnter(Collider collision)
    {
        GameObject col = collision.gameObject;
        
        IHealth targetable = col.GetComponent<IHealth>();
        if (targetable!=null)
        {
            if(targetable.TargetType == TARGET_TYPE.TARGET_TYPE_BUILDING && !isPriorityTarget)
            {
                target = col;
                isPriorityTarget = true;
            }
            if(targetable.TargetType == TARGET_TYPE.TARGET_TYPE_PLAYER && !isPriorityTarget)
            {
                target = col;
                isPriorityTarget = true;
            }
        }
    }
    #endregion


    #region Rotation Bahevior
    void RotationBahevior()
    {
        
    }

    #endregion



}
