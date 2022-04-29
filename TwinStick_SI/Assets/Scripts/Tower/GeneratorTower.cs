using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTower : ATower
{
    public GameObject prefabPollen;
    public float distanceSpawnPollen = 1;

    public int maxGeneratedPollen = 5;
    public int generatedPollen = 0;

    public override void Action()
    {
        float pollentToGenerate = Mathf.Clamp(actionAmount, 0, maxGeneratedPollen - generatedPollen+1);
        generatedPollen += (int)pollentToGenerate;
        for (int i = 0; i < pollentToGenerate; i++)
        {
            GameObject pollen = Instantiate(prefabPollen);
            float x = Random.Range(-1, 1);
            float y = Random.Range(-1, 1);
            pollen.transform.position = transform.position + new Vector3(Mathf.Cos(x), -0.5f, Mathf.Sin(y)) * (distanceSpawnPollen+Random.Range(0,1));


        }
        
    }


}