using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeManager : MonoBehaviour
{
    public List<Bee> playersBees = new List<Bee>();
    int distanceBetweenBees;
    public int maxBeeFollowPlayer;
    public GameObject beePrefab;
    public BulletPoolManager beeBulletContainer;
    public Transform beeContainer;

    [NaughtyAttributes.Button]
    public void RearangeBees()
    {
        distanceBetweenBees = 360 / playersBees.Count;
        for (int i = 0; i < playersBees.Count; i++)
        {
            playersBees[i].turnAngle = distanceBetweenBees * i;
        }
    }
    [NaughtyAttributes.Button]
    public void BuyBee()
    {
        if(playersBees.Count < maxBeeFollowPlayer)
        {
            GameObject bee = Instantiate(beePrefab, beeContainer.position, beeContainer.rotation, beeContainer);
            Bee myBee = bee.GetComponent<Bee>();
            myBee.player = this.gameObject;
            myBee.bulletPool = beeBulletContainer;
            playersBees.Add(myBee);
            RearangeBees();
        }
    }
    [NaughtyAttributes.Button]
    public void KillTurret()
    {
        if(playersBees.Count > 0)
        {
            Destroy(playersBees[0].gameObject);
            playersBees.RemoveAt(0);
            RearangeBees();
        }

        //insérer destruction de la tour
    }
    public void PewPewInDir(Vector3 dir)
    {
        for (int i = 0; i < playersBees.Count; i++)
        {
            playersBees[i].Pew(dir);
        }
    }
    public void FreeTheBee()
    {
        if (playersBees.Count > 0)
        {
            //playersBees[playersBees.Count - 1].
        }
    }
}
