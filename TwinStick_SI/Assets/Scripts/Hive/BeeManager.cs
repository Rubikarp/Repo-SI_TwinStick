using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BeeManager : MonoBehaviour
{
    public int maxPlayerBee;
    public List<Bee> playersBees = new List<Bee>();
    public Transform beeContainer;

    [Header("Movement")]
    public float turnSpeed = 180f;
    public float distToPlayer = 2f;
    [Space(5)]
    [ShowNonSerializedField] float angleBtwBees;
    [ShowNonSerializedField] float turnAngle;


    [NaughtyAttributes.Button]
    public void KillTurret()
    {
        if(playersBees.Count > 0)
        {
            Bee sacrifice = playersBees.Last();
            FreeBee(sacrifice);
            sacrifice.Die();
        }

        //insérer destruction de la tour
    }

    public void FreeBee(Bee bee)
    {
        if (playersBees.Count > 0)
        {
            if (!playersBees.Contains(bee))
            {
                Debug.LogError(bee + " is not link to player", bee);
                return;
            }

            playersBees.Remove(bee);
            bee.linkToPlayer = false;
        }
    }
    public void LinkBee(Bee bee)
    {
        if (playersBees.Count < maxPlayerBee)
        {
            if (playersBees.Contains(bee))
            {
                //already link
                return;
            }

            playersBees.Add(bee);
            bee.linkToPlayer = true;
        }
    }
    public void PewPewInDir(Vector3 dir)
    {
        for (int i = 0; i < playersBees.Count; i++)
        {
            playersBees[i].Pew(dir);
        }
    }

    private void Update()
    {
        if(playersBees.Count > 0)
        {
            SpinArroundPlayer();
        }
    }
    public void SpinArroundPlayer()
    {
        turnAngle += Time.deltaTime * turnSpeed;
        float angleBtwBees = 360f / playersBees.Count;
        for (int i = 0; i < playersBees.Count; i++)
        {
            float currentAngle = turnAngle + (angleBtwBees * i);
            Vector3 offSet = distToPlayer * new Vector3(Mathf.Cos(currentAngle * Mathf.Deg2Rad), 0, Mathf.Sin(currentAngle * Mathf.Deg2Rad));
            playersBees[i].transform.position = transform.position + offSet;
        }
    }
}
