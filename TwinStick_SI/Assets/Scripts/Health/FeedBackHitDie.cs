using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBackHitDie : MonoBehaviour
{

    public ParticleSystem[] listOfParticles;
    public float hitSize = .5f;
    public float dieSize = 1.5f;

    
    [Button]
    public void Hit()
    {
        ActivateParticle(hitSize);
    }

    [Button]
    public void Die()
    {
        ActivateParticle(dieSize);
    }

    public void ActivateParticle(float size)
    {

        foreach (ParticleSystem item in listOfParticles)
        {
            item.Play();
        }

    }


}
