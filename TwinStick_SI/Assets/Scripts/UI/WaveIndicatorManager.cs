using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveIndicatorManager : MonoBehaviour
{
    public Transform player;
    public WaveManager waveManger;

    public BorderIndicator[] indicators;

    private void Start()
    {
        foreach (var item in indicators)
        {
            item.player = player;
        }
        indicators[0].target = waveManger.gameWaves[waveManger.waveNbr].spawnPoint.transform;
        indicators[1].target = waveManger.gameWaves[waveManger.waveNbr + 1].spawnPoint.transform;
    }

    public void RefreshIndicator()
    {
        indicators[0].target = waveManger.gameWaves[waveManger.waveNbr].spawnPoint.transform;
        indicators[1].target = waveManger.gameWaves[waveManger.waveNbr+1].spawnPoint.transform;
    }
}
