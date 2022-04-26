using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AI_STATE
{
    AI_STATE_SPAWNING,
    AI_STATE_IN_POOL,
    AI_STATE_IDLE,
    AI_STATE_REACH_TARGET,
    AI_STATE_ATTACK_TARGET,
    AI_STATE_DIE,
}

public enum AI_TYPE
{
    AI_MELEE,
    AI_MELEE_TURRET,
    AI_MELEE_GENERATOR,
    AI_RANGE,
    AI_RANGE_TURRET,
    AI_RANGE_GENERATOR,
}


public abstract class AEnnemy : MonoBehaviour
{
    public AI_STATE currentState;
    public AI_TYPE typeOfEnnemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FindNewTarget()
    {
    }




}
