using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class PlayerPollen : MonoBehaviour
{
    [Header("Data")]
    [ProgressBar("Pollen", "pollenStockMax", EColor.Yellow)]
    [SerializeField] private float pollenStock = 10f;
    [SerializeField] private float pollenStockMax = 100f;
    public float PollenAvailable { get { return pollenStock; } }
    public float MaxPollenAvailable { get { return pollenStockMax; } }

    [Header("Events")]
    public UnityEvent onChange;
    public UnityEvent onGain;
    public UnityEvent onLose;

    public bool CanConsume(float quantity)
    {
        return pollenStock >= quantity;
    }
    public void ConsumePollen(float quantity)
    {
        quantity = Mathf.Max(0f, quantity);
        if(quantity > pollenStock)
        {
            Debug.LogError("no more pollen available", this);
            return;
        }
        pollenStock = Mathf.Clamp(pollenStock - quantity, 0, pollenStockMax);
    }
    public void LootPollen(float quantity)
    {
        quantity = Mathf.Max(0f, quantity);
        pollenStock = Mathf.Clamp(pollenStock + quantity, 0, pollenStockMax);
    }

    [Button]
    private void QuickPollenGain()
    {
        LootPollen(10f);
    }
    [Button]
    private void QuickPollenLoose()
    {
        if(CanConsume(10f))
        ConsumePollen(10f);
    }
}
