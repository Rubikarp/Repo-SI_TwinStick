using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamPolish : MonoBehaviour
{
    [SerializeField] float maxDist = 2f;

    public void Aim(InputAction.CallbackContext context)
    {
        float magn = Mathf.Clamp01(context.ReadValue<Vector2>().magnitude);
        transform.localPosition = Vector3.forward * magn * maxDist;
    }

}
