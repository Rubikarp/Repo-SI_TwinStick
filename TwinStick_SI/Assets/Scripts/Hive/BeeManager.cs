using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeManager : MonoBehaviour
{
    public List<Bee> playersBees = new List<Bee>();
    int distanceBetweenBees;
    public int maxBeeFollowPlayer;
    public GameObject beePrefab;
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
            playersBees.Add(bee.GetComponent<Bee>());
            playersBees[playersBees.Count-1].player = this.gameObject;
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
