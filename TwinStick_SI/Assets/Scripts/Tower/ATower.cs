using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TOWER_TYPE
{
    TOWER_TYPE_TURRET,
    TOWER_TYPE_GENERATOR,

}


public class ATower : MonoBehaviour
{
    public TOWER_TYPE tower_type;

    public float actionSpeed = 1;
    public float actionAmount = 2;


    protected float timeElapsed = 0;



    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= actionSpeed)
        {
            Action();
            timeElapsed = 0;
        }

    }


    public virtual void Action()
    {

    }

    public virtual void OnHit()
    {
        // Ovverride for specific effect for each tower or go 1 for all
    }

    public virtual void Die()
    {
        TowerPoolManager.Instance.RemoveTower(this);
    }



}
