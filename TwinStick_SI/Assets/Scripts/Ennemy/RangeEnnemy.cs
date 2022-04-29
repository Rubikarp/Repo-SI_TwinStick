using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnnemy : AEnnemy
{

    EnnemyWeapon ew;


    private void Start()
    {
        ew = GetComponent<EnnemyWeapon>();
    }


    public override void Attack()
    {
        if (target.activeSelf)
        {
            //bh.TakeDamage(attackDamage);
            ew.Shoot((target.transform.position - transform.position).normalized);
        }
    }
}
