using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

[Serializable]
public class GameWave
{
    public string name;
    [Space(10)]
    [Expandable] public EnemyWave ennemies;
    public GameObject spawnPoint;
    public float cooldownBeforeNextWave;

    public GameWave(EnemyWave ennemies = null, GameObject spawnPoint = null, float cooldownBeforeNextWave = 5f)
    {
        this.name = "_";
        this.ennemies = ennemies;
        this.spawnPoint = spawnPoint;
        this.cooldownBeforeNextWave = cooldownBeforeNextWave;
    }
}
public class WaveManager : MonoBehaviour
{
    public float delayBeforeFirstWave = 0f;
    [Space(10)]
    public int waveNbr;
    public UnityEvent onNewWave;
    [SerializeField] GameWave currentWave;
    public List<GameWave> gameWaves = new List<GameWave>();

    public EnnemyPoolManager ennemyManager;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(delayBeforeFirstWave);

        while (waveNbr < gameWaves.Count)
        {
            Debug.LogWarning("new wave");
            currentWave = gameWaves[waveNbr];
            ennemyManager.SpawnEnnemyAtLocation(currentWave.ennemies, currentWave.spawnPoint.transform);
            waveNbr++;
            onNewWave?.Invoke();
            yield return new WaitForSeconds(currentWave.cooldownBeforeNextWave);
        }

        yield return null;
    }
}
