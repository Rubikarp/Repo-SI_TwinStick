using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(BasicHealth))]
public class Hive : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private BasicHealth health;
    
    [Header("Data")]
    [SerializeField] private float pollenStock = 500f;
    [SerializeField] private float pollenStockMax = 1000f;

    private void Reset()
    {
        health = this.gameObject.GetComponent<BasicHealth>();
    }

    public void GetBee()
    {

    }
}
