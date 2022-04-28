using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnnemyTest : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public List<EnnemyWave> waves;

    void Start()
    {

        waves = new List<EnnemyWave>();
        waves.Add(new EnnemyWave(AI_TYPE.AI_MELEE_TURRET, 1));
        
    }

    [Button]
    void StartWaves()
    {
        EnnemyPoolManager.Instance.SpawnEnnemyAtLocation(waves, transform);
    }



}
