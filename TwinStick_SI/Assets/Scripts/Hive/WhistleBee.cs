using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class WhistleBee : MonoBehaviour
{
    public float whistleRange = 5f;

    public bool callingMode = false;
    public void Interact(InputAction.CallbackContext context)
    {

    }

    public void CallBee(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                callingMode = true;
                break;
            case InputActionPhase.Canceled:
                callingMode = false;
                break;
        }
    }

    public void FreeBee(InputAction.CallbackContext context)
    {

    }

    private void Update()
    {
        if (callingMode)
        {
            Calling();
        }
    }

    private void Calling()
    {
        var objInRange = Physics.OverlapSphere(transform.position, whistleRange);
        foreach (var obj in objInRange)
        {

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
