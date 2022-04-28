using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using NaughtyAttributes;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BeeManager : MonoBehaviour
{
    public Hive hive;

    public int maxPlayerBee = 5;
    public List<Bee> playersBees = new List<Bee>();
    public Transform beeContainer;
    public float recupDist = 2f;

    [Header("Event")]
    public UnityEvent onGetBee;
    public UnityEvent onFreeBee;

    [Header("Movement")]
    public float turnSpeed = 180f;
    public float distToPlayer = 2f;
    [Space(5)]
    [ShowNonSerializedField] float angleBtwBees;
    [ShowNonSerializedField] float turnAngle;

    [Header("Interact Actions")]
    public float distanceToBuy;

    public void Interact(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                if (Vector3.Distance(transform.position, hive.transform.position) <= distanceToBuy)
                {
                    
                    hive.BuyBee(GetComponent<PlayerPollen>(), this);
                }
                break;
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

            onGetBee?.Invoke();
            playersBees.Add(bee);
            bee.state = BEE_STATE.FOLLOWING;
        }
    }
    public void UnlinkBee(Bee bee)
    {
        if (playersBees.Count > 0)
        {
            onFreeBee?.Invoke();
            playersBees.Remove(bee);
            bee.transform.parent = hive.transform;
        }
    }
    public Bee FreeABee()
    {
        if (playersBees.Count > 0)
        {
            Bee bee = playersBees.Last();
            UnlinkBee(bee);
            bee.state = BEE_STATE.WORKING;

            return bee;
        }
        return null;
    }
    public void KillTurret()
    {
        if(playersBees.Count > 0)
        {
            FreeABee().Die();
        }

        //insérer destruction de la tour
    }
    public void BeeShield()
    {
        while (playersBees.Count > 0)
        {
            Bee bee = playersBees[0];
            UnlinkBee(bee);
            bee.state = BEE_STATE.GROUNDED;
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
        CheckForGroundedBee();

        if (playersBees.Count > 0)
        {
            SpinArroundPlayer();
        }

    }
    public void CheckForGroundedBee()
    {
        var tempList = hive.allBees.
            Where(bee => Vector2.Distance(bee.transform.position.ToVec2XZ(), transform.position.ToVec2XZ()) < recupDist).
            Where(bee => bee.state is BEE_STATE.GROUNDED).
            Where(bee => !playersBees.Contains(bee)).ToList();

        foreach (Bee bee in tempList)
        {
            LinkBee(bee);
        }
    }
    public void SpinArroundPlayer()
    {
        turnAngle += Time.deltaTime * turnSpeed;
        float angleBtwBees = 360f / playersBees.Count;
        for (int i = 0; i < playersBees.Count; i++)
        {
            float currentAngle = turnAngle + (angleBtwBees * i);
            Vector3 offSet = distToPlayer * new Vector3(Mathf.Cos(currentAngle * Mathf.Deg2Rad), 0.5f, Mathf.Sin(currentAngle * Mathf.Deg2Rad));
            playersBees[i].transform.position = transform.position + offSet;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        using (new Handles.DrawingScope(Color.red))
        {
            Handles.DrawWireDisc(transform.position, Vector3.up, recupDist);
        }
    }
#endif
}
