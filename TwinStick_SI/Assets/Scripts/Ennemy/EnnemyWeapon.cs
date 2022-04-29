using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnnemyWeapon : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] Transform bulletContainer;
    [Space(5)]
    [SerializeField] GameObject shoot;


    public void Shoot(Vector3 direction)
    {
        Instantiate(shoot, transform.position, Quaternion.LookRotation(direction, Vector3.up), bulletContainer);
    }
}
