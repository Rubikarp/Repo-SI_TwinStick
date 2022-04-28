using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NaughtyAttributes;

[Serializable]
public struct TowerPercent
{
    public UnityEngine.Object TowerPrefab;

    public int spawnForce;
}



public class TowerPoolManager : Singleton<TowerPoolManager>
{
    #region Inspector Variables
    [Header("TowerType")]
    [SerializeField]
    public List<TowerPercent> listOfTowerType;
    public int numberMultipier = 10;
    
    #endregion

    #region Internal Variables
    [NaughtyAttributes.ReadOnly]
    public List<GameObject> listOfCreatedTower;
    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        listOfCreatedTower = new List<GameObject>();
        // Generate a bunch of Tower based of listOfTowerType, We will multiply numberMultiplier by theur respective spawnForce to have a large amount of created Tower
        foreach (TowerPercent typeOfTower in listOfTowerType){
            CreateTower(typeOfTower.TowerPrefab, typeOfTower.spawnForce*numberMultipier);
        }
        

    }
    [Button]
    void GenerateTowerAtStart()
    {

        foreach (TowerPercent typeOfTower in listOfTowerType)
        {
            CreateTower(typeOfTower.TowerPrefab, typeOfTower.spawnForce * numberMultipier);
        }
    }

    bool CreateTower(UnityEngine.Object type,int nb)
    {
        for (int i = 0; i < nb; i++)
        {
            GameObject TowerCreated = (GameObject)Instantiate(type, new Vector3(0,0,0), Quaternion.identity);
            TowerCreated.name = "Tower #" + (listOfCreatedTower.Count);
            TowerCreated.transform.SetParent(gameObject.transform);
            TowerCreated.SetActive(false);
            listOfCreatedTower.Add(TowerCreated);
        }
        return true;
    }

    public bool SpawnTowerAtLocation(TOWER_TYPE towerType,Transform parent)
    {

        foreach (GameObject tower in listOfCreatedTower)
        {
            if (!tower.activeSelf && (tower.GetComponent<ATower>().tower_type==towerType))
            {
                Vector3 towerpos = parent.position;
                foreach (GameObject towerActif in listOfCreatedTower)
                {
                    if (towerActif.activeSelf)
                    {
                        if (Vector3.Distance(towerActif.transform.position, tower.transform.position) < 5)
                        {
                            towerpos = parent.transform.position + ((parent.transform.position - tower.transform.position).normalized * 4);
                        }
                    }
                }

                tower.SetActive(true);
                tower.transform.position = towerpos;
                BasicHealth bh = tower.GetComponent<BasicHealth>();
                bh.Initialise();
                return true;
            }
        }

        return false;
    }

    public void RemoveTower(ATower tower)
    {

        tower.gameObject.SetActive(false);
        tower.gameObject.transform.position = new Vector3(0, 0, 0);
        
    }

    public IEnumerable<GameObject> GetAllActiveTower()
    {
        List<GameObject> activeTower = new List<GameObject>();
        foreach (GameObject tower in listOfCreatedTower)
        {
            if (tower.activeSelf)
            {
                activeTower.Add(tower);
            }
        }
        return activeTower;

    }
}
