using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnnemy : AEnnemy
{

    public override void Attack()
    {

        if (target.activeSelf)
        {
            anim.SetBool("isAttacking", true);
            bh.TakeDamage(attackDamage);
            StartCoroutine(StopAttack(1.08f));
            
        }
        
    }

    IEnumerator StopAttack(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        anim.SetBool("isAttacking", false);
    }



}
