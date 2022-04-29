using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParticle : MonoBehaviour
{
    public int damagePerBullet = 1;

    private void OnParticleCollision(GameObject other)
    {
        GameObject col = other.gameObject;

        BasicHealth bh = col.GetComponent<BasicHealth>();

        if (bh != null)
        {
            if (bh.TargetType == TARGET_TYPE.TARGET_TYPE_ENNEMY)
            {
                Debug.Log("Dealing Player Damage to " + col.name);
                bh.TakeDamage(damagePerBullet);

            }

        }
    }
}
