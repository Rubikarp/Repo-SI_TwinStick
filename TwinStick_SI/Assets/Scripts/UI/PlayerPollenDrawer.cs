using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPollenDrawer : MonoBehaviour
{
    public PlayerPollen player;
    public HiveDrawer hive;

    [SerializeField] Slider pollenGauge;
    [SerializeField] TextMeshProUGUI beeCounter;


    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        pollenGauge.maxValue = player.pollenMaxStock;
    }

    public void Update()
    {
        beeCounter.text = hive.hive.allBees.Count.ToString();
        pollenGauge.value = player.pollenAvailable;
    }
}
