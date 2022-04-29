using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTower : ATower
{
    public GameObject prefabPollen;
    public float distanceSpawnPollen = 1;

    public int maxGeneratedPollen = 5;
    public int generatedPollen = 0;
    public int minimumPollenToBee;
    public List<Pollen> towerPollens = new List<Pollen>();

    public override void Action()
    {
        
        generatedPollen += (int)actionAmount;
        for (int i = 0; i < actionAmount; i++)
        {
            GameObject pollen = Instantiate(prefabPollen);
            float x = Random.Range(-1, 1);
            float y = Random.Range(-1, 1);
            pollen.transform.position = transform.position + new Vector3(Mathf.Cos(x), -0.5f, Mathf.Sin(y)) * (distanceSpawnPollen+Random.Range(0,1));
            pollen.GetComponent<Pollen>().ownerTower = this;
            towerPollens.Add(pollen.GetComponent<Pollen>());
            anim.SetTrigger("isAttacking");

        }
        if (generatedPollen > minimumPollenToBee)
        {
            foreach (Pollen pollens in towerPollens)
            {
                if (generatedPollen >= minimumPollenToBee)
                {
                    foreach (Bee bee in Hive.Instance.allBees)
                    {
                        if (bee.state == BEE_STATE.WAITING)
                        {
                            bee.state = BEE_STATE.WORKING;
                            pollens.transporterBee = bee;
                            pollens.transporterBee.pollenCaried = pollens;
                            pollens.transporterBee.hasPollen = true;
                            bee.navAgent.SetDestination(pollens.transform.position);
                            generatedPollen--;

                            break;
                        }
                    }

                }
            }
        }
        
    }


}
