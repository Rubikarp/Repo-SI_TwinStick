using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class WhistleBee : MonoBehaviour
{

    [SerializeField] BeeManager playerBee;
    public float whistleRange = 5f;

    public bool callingMode = false;
    public Coroutine CallingNearbyBee;
    public void CallBee(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                callingMode = true;
                CallingNearbyBee = StartCoroutine(Calling());
                break;
            case InputActionPhase.Canceled:
                StopCoroutine(Calling());
                callingMode = false;
                break;
        }
    }
    public void FreeBee(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                //if n'est pas a coté d'une tourelle
                playerBee.FreeABee();
                break;
        }
    }

    private IEnumerator Calling()
    {
        while (callingMode)
        {
            var tempList = playerBee.hive.allBees.
                Where(x => !playerBee.playersBees.Contains(x)).
                Where(bee => bee.state != BEE_STATE.GROUNDED).ToList();

            foreach (Bee bee in tempList)
            {
                float distance = (bee.transform.position - transform.position).magnitude;

                if (distance< whistleRange)
                {
                    playerBee.LinkBee(bee);
                    break;
                }
            }
            yield return new WaitForSecondsRealtime(0.2f);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        using (new Handles.DrawingScope(Color.yellow))
        {
            Handles.DrawWireDisc(transform.position, Vector3.up, whistleRange);
        }
    }
#endif
}
