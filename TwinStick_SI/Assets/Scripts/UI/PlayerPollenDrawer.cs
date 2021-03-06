using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPollenDrawer : MonoBehaviour
{
    public PlayerPollen player;

    [SerializeField] Slider pollenGauge;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        pollenGauge.maxValue = player.pollenMaxStock;
        UpdateScore();
    }

    public void UpdateScore()
    {
        pollenGauge.value = player.pollenAvailable;
    }
}
