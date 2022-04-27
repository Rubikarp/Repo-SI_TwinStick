using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnnemy : AEnnemy
{

    public override void Attack()
    {

        if (target.activeSelf)
        {
            BasicHealth bh = target.GetComponent<BasicHealth>();
            bh.TakeDamage();
        }
        
    }

}
