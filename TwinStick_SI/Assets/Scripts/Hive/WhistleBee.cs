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
    public ParticleSystem whistleWave;
    public ParticleSystem whistleGlow;
    public void CallBee(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                whistleGlow.Play();
                whistleWave.Play();
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
                whistleGlow.Play();
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
                    if (bee.hasPollen == true)
                    {
                        this.GetComponent<PlayerPollen>().Refill(bee.pollenCaried.pollenAmount);
                        bee.pollenCaried.ownerTower.towerPollens.Remove(bee.pollenCaried);
                        Destroy(bee.pollenCaried.gameObject);
                        bee.hasPollen = false;
                    }
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
