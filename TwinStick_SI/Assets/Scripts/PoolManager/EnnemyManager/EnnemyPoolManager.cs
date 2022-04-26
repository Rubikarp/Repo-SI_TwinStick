using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public struct EnnemyPercent
{
    public UnityEngine.Object ennemyPrefab;

    public int spawnForce;
}


public struct EnnemyWave
{
    public AI_TYPE typeOfEnnemy;
    public int numberSpawn;
}




public class EnnemyPoolManager : Singleton<EnnemyPoolManager>
{
    #region Inspector Variables
    [Header("EnnemyType")]
    [SerializeField]
    public List<EnnemyPercent> listOfEnnemyType;
    public int numberMultipier = 10;
    #endregion

    #region Internal Variables
    [HideInInspector]
    public List<GameObject> listOfCreatedEnnemy;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Generate a bunch of Ennemy based of listOfEnnemyType, We will multiply numberMultiplier by theur respective spawnForce to have a large amount of created ennemy
        foreach (EnnemyPercent typeOfEnnemy in listOfEnnemyType){
            CreateEnnemy(typeOfEnnemy.ennemyPrefab, typeOfEnnemy.spawnForce*numberMultipier);
        }
        

    }

    bool CreateEnnemy(UnityEngine.Object type,int nb)
    {
        for (int i = 0; i < nb; i++)
        {
            GameObject ennemyCreated = (GameObject)Instantiate(type, new Vector3(-500, -500, -500), Quaternion.identity);
            ennemyCreated.name = "Ennemy #" + (listOfCreatedEnnemy.Count);
            ennemyCreated.transform.SetParent(gameObject.transform);
            ennemyCreated.GetComponent<AEnnemy>().currentState = AI_STATE.AI_STATE_IN_POOL;
            ennemyCreated.SetActive(false);
            listOfCreatedEnnemy.Add(ennemyCreated);
        }
        return true;
    }

    public bool SpawnEnnemyAtLocation(List<EnnemyWave> waves,Transform parent)
    {
        Debug.Log("Start Spwaning");
        List<GameObject> ennemyToSpawn = new List<GameObject>();
        List<GameObject> tempListOfCreatedEnnemy = new List<GameObject>(listOfCreatedEnnemy);
        List<GameObject> indexToRemove = new List<GameObject>();
        Debug.Log(listOfCreatedEnnemy.Count + " | " + tempListOfCreatedEnnemy.Count);
        Debug.Log("Checking Waves");
        foreach (EnnemyWave wave in waves)
        {
            Debug.Log(wave.typeOfEnnemy + " | " + wave.numberSpawn);
            int ennemyToSerch = wave.numberSpawn;
            foreach (GameObject gameObject in tempListOfCreatedEnnemy)
            {
                if (!gameObject.activeSelf)
                {
                    if (ennemyToSerch <= 0) { break; }
                    AEnnemy ennemy = gameObject.GetComponent<AEnnemy>();
                    if (ennemy && ennemy.typeOfEnnemy == wave.typeOfEnnemy)
                    {
                        Debug.Log("Find Ennemy To Spawn");
                        ennemyToSerch -= 1;
                        indexToRemove.Add(gameObject);
                        ennemyToSpawn.Add(gameObject);

                    }
                }
            }
            foreach(GameObject index in indexToRemove)
            {
                tempListOfCreatedEnnemy.Remove(index);
            }
            indexToRemove.Clear();

        }
        
        

        StartCoroutine(SpawnEnnemyThroughTime(ennemyToSpawn,parent));

        return true;
    }

    IEnumerator SpawnEnnemyThroughTime(List<GameObject> ennemyToSpawn, Transform parent)
    {



        int i = 0;
        foreach(GameObject ennemy in ennemyToSpawn)
        {
            i++;
            Debug.Log(ennemy.name);
            ennemy.SetActive(true);
            AEnnemy ennemyComp = ennemy.GetComponent<AEnnemy>();
            
            ennemyComp.currentState = AI_STATE.AI_STATE_SPAWNING;

            float safezonex = UnityEngine.Random.Range(-ennemyToSpawn.Count, ennemyToSpawn.Count);
            float safezonez = UnityEngine.Random.Range(-ennemyToSpawn.Count, ennemyToSpawn.Count);
            ennemy.transform.position = parent.position + new Vector3(safezonex, 1.1f, safezonez);

            ennemyComp.FindNewTarget();
            yield return new WaitForSeconds(0.2f);
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}