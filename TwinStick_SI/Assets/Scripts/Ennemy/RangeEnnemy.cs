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
            
            anim.SetBool("isAttacking", true);
            
            
            StartCoroutine(StopAttack(1f));
        }
    }

    IEnumerator StopAttack(float seconds)
    {
        yield return new WaitForSeconds(0.5f);
        ew.Shoot((target.transform.position - transform.position).normalized);
        yield return new WaitForSeconds(seconds);
        anim.SetBool("isAttacking", false);
    }
}
