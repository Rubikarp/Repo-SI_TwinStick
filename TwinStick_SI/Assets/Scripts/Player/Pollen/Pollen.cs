using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollen : MonoBehaviour
{

    public int pollenAmount = 1;

    public GeneratorTower ownerTower;
    public Bee transporterBee;
    public float collectPollenDistance;
    public float enterHiveDistance;
    bool attachedToBee = false;

    public void PickUp()
    {
        ownerTower.generatedPollen -= 1;
    }

    public void IsTakenByBee()
    {
        if (Vector2.Distance(transporterBee.transform.position.ToVec2XZ(), transform.position.ToVec2XZ()) < collectPollenDistance)
        {
            transporterBee.navAgent.SetDestination(Hive.Instance.transform.position);
            transform.SetParent(transporterBee.transform);
            ownerTower.generatedPollen--;
            attachedToBee = true;
        }
    }

    public void Update()
    {
        Debug.DrawLine(transporterBee.transform.position, Hive.Instance.transform.position, Vector2.Distance(transporterBee.transform.position.ToVec2XZ(), Hive.Instance.transform.position) <= enterHiveDistance ? Color.green : Color.red);
        if (transporterBee == null && ownerTower.minimumPollenToBee < ownerTower.generatedPollen)
        {
            foreach (Bee bee in Hive.Instance.allBees)
            {
                if (bee.state == BEE_STATE.WAITING)
                {
                    transporterBee = bee;
                    bee.state = BEE_STATE.WORKING;
                    bee.navAgent.SetDestination(transform.position);
                    break;
                }
            }
        }
        if (transporterBee != null && attachedToBee == false)
        {
            IsTakenByBee();
        }
        if (transporterBee != null && attachedToBee == true)
        {
            IsArrivedToHive();
        }
    }

    public void IsArrivedToHive()
    {
        if (Vector2.Distance(transporterBee.transform.position.ToVec2XZ(), Hive.Instance.transform.position) <= enterHiveDistance)
        {
            Debug.DrawLine(transporterBee.transform.position, Hive.Instance.transform.position, Color.red);
            Hive.Instance.pollenStock += pollenAmount;
            transporterBee.state = BEE_STATE.WAITING;
            Destroy(this.gameObject);
        }
    }
}
