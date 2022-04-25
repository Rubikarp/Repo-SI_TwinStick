using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnnemyOnDie
{
    public void OnDie();
}

public enum AI_STATE
{
    AI_STATE_SPAWNING,
    AI_STATE_IN_POOL,
    AI_STATE_IDLE,
    AI_STATE_REACH_TARGET,
    AI_STATE_ATTACK_TARGET,
    AI_STATE_DIE,
}


public abstract class AEnnemy : MonoBehaviour
{
    public AI_STATE currentState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
