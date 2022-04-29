using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParticle : MonoBehaviour
{
    public int damagePerBullet = 1;

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out BasicHealth basicHealth))
        {
            basicHealth.TakeDamage(damagePerBullet);
        }
    }
}
