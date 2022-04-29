using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[SerializeField]
public enum AI_STATE
{
    AI_STATE_SPAWNING,
    AI_STATE_IN_POOL,
    AI_STATE_IDLE,
    AI_STATE_REACH_TARGET,
    AI_STATE_ATTACK_TARGET,
    AI_STATE_DIE,
}

[SerializeField]
public enum AI_TYPE
{
    AI_MELEE,
    AI_MELEE_TURRET,
    AI_MELEE_GENERATOR,
    AI_RANGE,
    AI_RANGE_TURRET,
    AI_RANGE_GENERATOR,
    AI_EXPLODE,
    AI_EXPLODE_TURRET,
    AI_EXPLODE_GENERATOR,
}


public abstract class AEnnemy : MonoBehaviour
{
    public AI_STATE currentState;
    public AI_TYPE typeOfEnnemy;

    public int attackDistance = 10;
    public int attackDamage = 1;
    public GameObject target;
    bool isPriorityTarget=false;
    [SerializeField]
    protected NavMeshAgent navAgent;
    [SerializeField] 
    protected Rigidbody rigidBody;
    [SerializeField]
    protected BoxCollider boxCol;
    [SerializeField]
    protected Animator anim;

    protected Vector3 attackPosition;
    [SerializeField][ReadOnly]
    private float lastAttack=0;
    [SerializeField][ReadOnly]
    private float attackSpeed=2;
    [SerializeField][ReadOnly]
    private float distanceFromEnnemy;

    protected BasicHealth bh;

    // Start is called before the first frame update
    void Start()
    {
        //navAgent = GetComponent<NavMeshAgent>();
        
    }

    void Update()
    {
        //Look at Ennemy

        if (target != null && target.activeSelf)
        {
            Vector3 toTarget = target.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp( transform.rotation,Quaternion.LookRotation(toTarget.normalized, Vector3.up),Time.deltaTime * 2f);
            Debug.DrawRay(transform.position, toTarget, Color.cyan);
        }

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
                AI_STATE_IDLE();
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

                AI_STATE_DIE();
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
        Debug.Log(gameObject.name+" | Target Found : " + target.name);
        GameObject closestTower=null;
        float closestDistance = 0;
        foreach (GameObject tower in TowerPoolManager.Instance.GetAllActiveTower())
        {
            float distance = Vector3.Distance(tower.transform.position, transform.position);
            if (distance < (boxCol.size.x)/2)
            {
                if((closestTower==null || !closestTower.activeSelf) || distance < closestDistance)
                {
                    closestTower = tower;
                    closestDistance = distance;
                }
            }
        }

        if (closestTower != null)
        {
            target = closestTower;
        }


        currentState = AI_STATE.AI_STATE_REACH_TARGET;
        bh = target.GetComponent<BasicHealth>();
        Debug.Log(gameObject.name + " | Target Found : " + target.name);


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
                
                angle = Random.Range(angle - 90, angle + 90);
                Debug.Log(angle);
                if (typeOfEnnemy == AI_TYPE.AI_RANGE)
                {
                   // attackDistance = Random.Range(attackDistance - 3, attackDistance+3);
                }
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

    public virtual void AI_STATE_ATTACK_TARGET()
    {
        lastAttack += Time.deltaTime;
        if (target.activeSelf)
        {
            distanceFromEnnemy = (transform.position - target.transform.position).magnitude;
            if (distanceFromEnnemy < attackDistance+1 || (transform.position - navAgent.destination).magnitude<1)
            {
                navAgent.isStopped = true;
                rigidBody.velocity = Vector3.zero;
                
                if (lastAttack >= attackSpeed)
                {
                    lastAttack = 0;
                    Attack();
                }
            }
            else
            {
                navAgent.isStopped = false;
            }
        }
        else
        {
            currentState = AI_STATE.AI_STATE_IDLE;
        }

    }

    // Override Attack for each ennemy type
    public abstract void Attack();

    public virtual void AI_STATE_DIE()
    {
        // Override for special ennemy that pop up Turret or Generator
        navAgent.isStopped = false;
        rigidBody.velocity = Vector3.zero;
        EnnemyPoolManager.Instance.RemoveEnnemy(this);
    }

    #endregion

    #region Detection Behavior
    void OnTriggerEnter(Collider collision)
    {
        GameObject col = collision.gameObject;
        
        BasicHealth targetable = col.GetComponent<BasicHealth>();
        if (targetable!=null)
        {
            Debug.Log(gameObject.name + " | " + targetable.TargetType);
            if (targetable.TargetType == TARGET_TYPE.TARGET_TYPE_BUILDING && !isPriorityTarget)
            {
                target = col;
                bh = targetable;
                isPriorityTarget = true;
                Debug.Log(gameObject.name + " | Target Found : " + target.name);
            }
            if(targetable.TargetType == TARGET_TYPE.TARGET_TYPE_PLAYER && !isPriorityTarget)
            {
                target = col;
                bh = targetable;
                isPriorityTarget = true;
                Debug.Log(gameObject.name + " | Target Found : " + target.name);
            }
        }

        

    }
    #endregion


    #region Rotation Bahevior
    void RotationBahevior()
    {
        
    }

    #endregion



    #region Feedback 

    public void OnHit()
    {

    }

    public void OnDie()
    {
        currentState = AI_STATE.AI_STATE_DIE;
        
    }

    #endregion


}
