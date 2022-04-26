using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : Singleton<BulletPoolManager>
{
    #region Inspector Variables

    [Header("Bullet Generator")]
    public Object bulletPrefab;
    public int numberOfBulletGenerated = 200;
    #endregion

    [HideInInspector]
    public List<GameObject> listOfCreatedBullet;
    void Start()
    {
       CreateEnnemy(numberOfBulletGenerated);


    }

    bool CreateEnnemy(int nb)
    {
        GameObject bulletCreated = (GameObject)Instantiate(bulletPrefab, new Vector3(500, -500, -500), Quaternion.identity);
        bulletCreated.name = "Bullet #" + (listOfCreatedBullet.Count);
        bulletCreated.transform.SetParent(gameObject.transform);
        bulletCreated.SetActive(false);
        return true;
    }

    public GameObject Shoot(Vector3 position,Quaternion rotation)
    {
        foreach (GameObject bul in listOfCreatedBullet)
        {
            if (bul.activeSelf) {
                bul.SetActive(true);
                bul.transform.position = position;
                bul.transform.rotation = rotation;
                return bul;
            } 
        }

        GameObject bulletCreated = (GameObject)Instantiate(bulletPrefab, new Vector3(500, -500, -500), Quaternion.identity);
        bulletCreated.name = "Bullet #" + (listOfCreatedBullet.Count);
        bulletCreated.transform.SetParent(gameObject.transform);
        bulletCreated.SetActive(true);


        return bulletCreated;

    }
}
