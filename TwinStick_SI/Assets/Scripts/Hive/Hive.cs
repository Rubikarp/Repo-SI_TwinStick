using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(BasicHealth))]
public class Hive : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private BasicHealth health;

    [Header("Bees")]
    [SerializeField] private float beePrice = 10f;
    [SerializeField] GameObject beePrefab;
    [SerializeField] List<Bee> allBees = new List<Bee>();
    [Space(5)]
    public BulletPoolManager beeBulletContainer;

    [Header("Data")]
    [SerializeField] private float pollenStock = 500f;
    [SerializeField] private float pollenStockMax = 1000f;

    private void Reset()
    {
        health = this.gameObject.GetComponent<BasicHealth>();
    }

    public void GetBee()
    {

    }

    public void BuyBee(PlayerPollen playerPollen, BeeManager playerBee)
    {
        if (/*playerPollen.CanConsume(beePrice) &&*/ playerBee.playersBees.Count < playerBee.maxPlayerBee)
        {
            playerPollen.Consume(beePrice);
            GameObject bee = Instantiate(beePrefab, playerBee.beeContainer.position, playerBee.beeContainer.rotation, playerBee.beeContainer);

            Bee myBee = bee.GetComponent<Bee>();
            myBee.bulletPool = beeBulletContainer;

            myBee.hive = this;
            allBees.Add(myBee);
            playerBee.LinkBee(myBee);
        }
    }
}
