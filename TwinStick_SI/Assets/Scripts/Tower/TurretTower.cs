using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurretTower : ATower
{

    private GameObject target;
    private BasicHealth bhTarget;

    public int AttackDistance = 10;

    public UnityEvent onFire;



    public override void Action()
    {
        if (CheckTarget())
        {
            anim.SetTrigger("isAttacking");
            onFire.Invoke();
            bhTarget.TakeDamage((int)actionAmount);
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
        return false;
    }

    bool FindNewTarget()
    {
        foreach(GameObject ennemy in EnnemyPoolManager.Instance.GetAllActiveEnnemy())
        {
            if ((ennemy.transform.position - transform.position).magnitude <= AttackDistance)
            {
                target = ennemy;
                bhTarget = target.GetComponent<BasicHealth>();
                return true;
            }
        }
        return false;
    }

}
