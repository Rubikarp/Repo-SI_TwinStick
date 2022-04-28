using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTower : ATower
{

    private GameObject target;
    private BasicHealth bhTarget;

    public int AttackDistance = 6;

    public override void Action()
    {
        if (CheckTarget())
        {
            bhTarget.TakeDamage((int)actionAmount);
        }
    }

    bool CheckTarget()
    {
        if(target!=null || !target.activeSelf)
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
