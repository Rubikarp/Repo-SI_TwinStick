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




    public virtual void Die()
    {
        TowerPoolManager.Instance.RemoveTower(this);
    }



}
