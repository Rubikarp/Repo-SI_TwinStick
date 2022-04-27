using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BEE_STATE
{
    GROUNDED,
    FOLLOWING,
    WORKING,
}

public class Bee : MonoBehaviour
{

    private NavMeshAgent navAgent;
    private Hive hive;

    void Start()
    {
        
    }

    void Update()
    {
        //navAgent.SetDestination();
        
    }
}
