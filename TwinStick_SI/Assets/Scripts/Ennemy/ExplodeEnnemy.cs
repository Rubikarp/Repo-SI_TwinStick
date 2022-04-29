using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeEnnemy : AEnnemy
{

    public float explosionRange=2;

    public override void Attack()
    {


        BasicHealth[] listOfTarget = GameObject.FindObjectsOfType<BasicHealth>();

        foreach(BasicHealth tbh in listOfTarget)
        {
            if(tbh.TargetType == TARGET_TYPE.TARGET_TYPE_HIVE || tbh.TargetType == TARGET_TYPE.TARGET_TYPE_PLAYER || tbh.TargetType == TARGET_TYPE.TARGET_TYPE_BUILDING  )
            {
                if (Vector3.Distance(tbh.transform.position,transform.position)<explosionRange)
                {
                    tbh.TakeDamage(attackDamage);
                }
            }
        }


    }
}
