using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hive : MonoBehaviour,IHealth
{
    // Start is called before the first frame update
    [SerializeField] private int defaultHealthPoint=100;
    [SerializeField] private int healthPoint;

    [SerializeField] private TARGET_TYPE targetType;

    public int HealthPoint { get { return healthPoint; } }

    public int TargetType { get { return targetType; } }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
