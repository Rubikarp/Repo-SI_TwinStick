using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BasicHealth))]
public class Hive : MonoBehaviour
{
    [SerializeField] private BasicHealth health;
    private void Reset()
    {
        health = this.gameObject.GetComponent<BasicHealth>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }    
}
