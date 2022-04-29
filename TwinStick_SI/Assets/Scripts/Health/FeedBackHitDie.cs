using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

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
            MainModule mm = item.main;
            mm.startSize = size;
            item.Stop();
            item.Play();
        }

    }


}
