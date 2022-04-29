using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Test());
        
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(0.1f);
        TowerPoolManager.Instance.SpawnTowerAtLocation(TOWER_TYPE.TOWER_TYPE_GENERATOR, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
