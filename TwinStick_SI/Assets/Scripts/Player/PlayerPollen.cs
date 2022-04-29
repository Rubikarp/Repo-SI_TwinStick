using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class PlayerPollen : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private float _pollenAvailable = 10f;
    [SerializeField] private float _pollenStockMax = 100f;
    public float pollenAvailable { get { return _pollenAvailable; } private set { _pollenAvailable = value; } }
    public float pollenMaxStock { get { return _pollenStockMax; } private set { _pollenStockMax = value; } }

    [Header("Events")]
    public UnityEvent onChange;
    public UnityEvent onGain;
    public UnityEvent onLose;

    [SerializeField] BeeManager bees;

    public bool CanConsume(float quantity)
    {
        return pollenAvailable >= quantity;
    }
    public void Consume(float quantity)
    {
        quantity = Mathf.Max(0f, quantity);
        if (quantity > pollenAvailable)
        {
            Debug.LogError("no more pollen available", this);
            return;
        }
        pollenAvailable = Mathf.Clamp(pollenAvailable - quantity, 0, pollenMaxStock);
        onChange.Invoke();
    }
    public void Refill(float quantity)
    {
        quantity = Mathf.Max(0f, quantity);
        pollenAvailable = Mathf.Clamp(pollenAvailable + quantity, 0, pollenMaxStock);
        onChange.Invoke();
    }

    private void Update()
    {
        if (Vector3.Distance(bees.hive.transform.position, transform.position) < 12f)
        {
            if(bees.hive.pollenStock > 0 && _pollenAvailable < _pollenStockMax)
            {
                bees.hive.pollenStock -= Time.deltaTime * 100f;
                pollenAvailable += Time.deltaTime * 100f;
            }
        }
    }

    [Button]
    private void QuickPollenGain()
    {
        Refill(10f);
    }
    [Button]
    private void QuickPollenLoose()
    {
        if (CanConsume(10f))
            Consume(10f);
    }
}
