using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicHealth : MonoBehaviour, IHealth
{
    [Header("Data")]
    [SerializeField] private int defaultHealth = 10;
    [SerializeField] private int healthPoint;
    [SerializeField] private TARGET_TYPE targetType = TARGET_TYPE.TARGET_TYPE_ENNEMY;
    public int HealthPoint { get { return healthPoint; } }
    public TARGET_TYPE TargetType { get { return targetType; } }
    [Header("Events")]
    public UnityEvent onHit;
    public UnityEvent onDeath;

    [Header("Parameter")]
    [SerializeField] float deathDelay = 0.2f;

    public void Initialise()
    {
        healthPoint = defaultHealth;
    }

    public void TakeDamage(int damage = 1)
    {
        //Damage can't be negative
        damage = Mathf.Max(0, damage);

        //Get Hit
        healthPoint -= damage;
        onHit?.Invoke();

        //Check Death
        if (healthPoint <= 0)
        {
            Invoke("Dying", deathDelay);
        }
    }

    public void Dying()
    {
        onDeath?.Invoke();
    }
}

