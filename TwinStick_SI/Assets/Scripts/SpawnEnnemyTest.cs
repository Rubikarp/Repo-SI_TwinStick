using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnnemyTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Test());
        
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(1);
        List<EnnemyWave> waves = new List<EnnemyWave>();
        EnnemyWave wave1 = new EnnemyWave();
        wave1.typeOfEnnemy = AI_TYPE.AI_MELEE;
        wave1.numberSpawn = 3;
        waves.Add(wave1);
        EnnemyWave wave2 = new EnnemyWave();
        wave2.typeOfEnnemy = AI_TYPE.AI_RANGE;
        wave2.numberSpawn = 2;
        waves.Add(wave2);
        EnnemyPoolManager.Instance.SpawnEnnemyAtLocation(waves, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
