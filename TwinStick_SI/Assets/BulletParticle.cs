using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParticle : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("particle hit");
    }
}
