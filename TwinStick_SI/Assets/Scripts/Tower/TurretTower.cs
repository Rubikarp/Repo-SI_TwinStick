using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurretTower : ATower
{

    public GameObject target;
    private BasicHealth bhTarget;

    public int AttackDistance = 10;

    public UnityEvent onFire;



    public override void Action()
    {
        if (CheckTarget())
        {
            if (Vector3.Distance(target.transform.position, transform.position) <= AttackDistance)
            {
                anim.SetTrigger("isAttacking");
                onFire.Invoke();
                bhTarget.TakeDamage((int)actionAmount);
                timeElapsed = 0;
            }
        }
    }

    public override void Orient()
    {
        if(target!=null && target.activeSelf)
        {
            Vector3 toTarget = target.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(toTarget.normalized, Vector3.up), Time.deltaTime * 6f);
            
            Debug.DrawRay(transform.position, toTarget, Color.cyan);
        }
    }


    bool CheckTarget()
    {
        if(target==null || !target.activeSelf)
        {
            if (FindNewTarget())
            {
                return true;
            }
        }
        else
        {
            return true;
        }
        return false;
    }

    bool FindNewTarget()
    {
        GameObject closest = null;
        float closestDist = AttackDistance;
        foreach(GameObject ennemy in EnnemyPoolManager.Instance.GetAllActiveEnnemy())
        {
            float distance = (Vector3.Distance(ennemy.transform.position, transform.position));
            if ((distance <= AttackDistance) && (closestDist>=distance) )
            {
                closest = ennemy;
                
            }
        }
        if (closest != null)
        {
            target = closest;
            bhTarget = target.GetComponent<BasicHealth>();
            return true;
        }
        return false;
    }

}
