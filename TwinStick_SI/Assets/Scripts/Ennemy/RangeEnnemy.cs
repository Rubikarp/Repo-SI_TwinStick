using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnnemy : AEnnemy
{
    public override void Attack()
    {
        if (target.activeSelf)
        {
            bh.TakeDamage(attackDamage);
        }
    }
}
