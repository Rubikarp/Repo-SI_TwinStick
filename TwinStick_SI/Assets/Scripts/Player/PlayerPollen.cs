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
    public float pollenAvailable { get { return pollenStock; } }

    [Header("Events")]
    public UnityEvent onChange;
    public UnityEvent onGain;
    public UnityEvent onLose;

    public bool CanConsumePollent(float quantity)
    {
        return pollenAvailable >= quantity;
    }
    public void ConsumePollen(float quantity)
    {
        quantity = Mathf.Max(0f, quantity);
        if(quantity > pollenAvailable)
        {
            Debug.LogError("impossible " + quantity + " is less than " + pollenAvailable,this);
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
        if(CanConsumePollent(10f))
        ConsumePollen(10f);
    }
}
