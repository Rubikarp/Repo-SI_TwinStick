using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [Header("Data")]
    [SerializeField] private int healthPoint;
    [SerializeField] private TARGET_TYPE targetType = TARGET_TYPE.TARGET_TYPE_PLAYER;
    public bool HaveBee { get { return beeManger.playersBees.Count > 0; } }
    public int HealthPoint { get { return healthPoint; } }
    public TARGET_TYPE TargetType { get { return targetType; } }

    [Header("Events")]
    public UnityEvent onHit;
    public UnityEvent onHeal;
    public UnityEvent onDeath;

    [Header("Component")]
    public BeeManager beeManger;

    [Header("Parameter")]
    [SerializeField] float deathDelay = 0.2f;

    public void Start()
    {
        healthPoint = 1;
    }

    public void CheckForBee()
    {
        if(healthPoint < 2)
        {
            healthPoint = 2;
            onHeal?.Invoke();
        }
    }

    [NaughtyAttributes.Button]
    public void TakeDamage(int damage = 1)
    {
        if (HaveBee)
        {
            healthPoint = 1;
            beeManger.BeeShield();
            onHit?.Invoke();
        }
        else
        {
            healthPoint = 0;
            Invoke("Dying", deathDelay);
        }
    }

    public void Dying()
    {
        onDeath?.Invoke();
    }
}

