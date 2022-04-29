using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeEnnemy : AEnnemy
{
    public UnityEvent onAttack;


    public override void Attack()
    {

        if (target.activeSelf)
        {
            anim.SetBool("isAttacking", true);
            onAttack.Invoke();
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
