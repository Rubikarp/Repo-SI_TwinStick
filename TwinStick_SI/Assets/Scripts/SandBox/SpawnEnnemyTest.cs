using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnnemyTest : MonoBehaviour
{
    // Start is called before the first frame update

    

    void Start()
    {

        
        
    }

    [Button("Spawn Ennemy Turret")]
    void spawnEnnemy1()
    {
        List<EnnemyGroup> waves = new List<EnnemyGroup>();
        waves.Add(new EnnemyGroup(AI_TYPE.AI_MELEE_TURRET, 1));
        EnnemyPoolManager.Instance.SpawnEnnemyAtLocation(waves, transform);
    }

    [Button("Spawn Ennemy Generator")]
    void spawnEnnemy3()
    {
        List<EnnemyGroup> waves = new List<EnnemyGroup>();
        waves.Add(new EnnemyGroup(AI_TYPE.AI_MELEE_GENERATOR, 1));
        EnnemyPoolManager.Instance.SpawnEnnemyAtLocation(waves, transform);
    }

    [Button("Spawn Ennemy Wave Melee")]
    void spawnEnnemy2()
    {
        List<EnnemyGroup> waves = new List<EnnemyGroup>();
        waves.Add(new EnnemyGroup(AI_TYPE.AI_MELEE, 5));
        EnnemyPoolManager.Instance.SpawnEnnemyAtLocation(waves, transform);
    }

    [Button("Spawn Ennemy Wave Range")]
    void spawnEnnemy4()
    {
        List<EnnemyWave> waves = new List<EnnemyWave>();
        waves.Add(new EnnemyWave(AI_TYPE.AI_RANGE, 5));
        EnnemyPoolManager.Instance.SpawnEnnemyAtLocation(waves, transform);
    }


    [Button("Spawn Ennemy Wave Explode")]
    void spawnEnnemy5()
    {
        List<EnnemyWave> waves = new List<EnnemyWave>();
        waves.Add(new EnnemyWave(AI_TYPE.AI_EXPLODE, 5));
        EnnemyPoolManager.Instance.SpawnEnnemyAtLocation(waves, transform);
    }







}
