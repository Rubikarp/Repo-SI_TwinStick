using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [Header("Data")]
    [SerializeField] private int healthPoint;
    public bool HaveBee { get; }
    public int HealthPoint { get { return healthPoint; } }

    [Header("Events")]
    public UnityEvent onHit;
    public UnityEvent onHeal;
    public UnityEvent onDeath;

    [Header("Parameter")]
    [SerializeField] float deathDelay = 0.2f;

    public void Initialise()
    {
        healthPoint = 1;
    }

    public void CheckForBee()
    {
        if(healthPoint < 2 /* && BeeCounter > 0*/)
        {
            healthPoint = 2;
            onHeal?.Invoke();
        }
    }

    public void TakeDamage(int damage = 1)
    {
        if (HaveBee)
        {
            healthPoint = 1;
            onHit?.Invoke();
        }
        else
        {
            healthPoint = 0;
            onHit?.Invoke();
            Invoke("Dying", deathDelay);
        }
    }

    public void Dying()
    {
        onDeath?.Invoke();
    }
}

