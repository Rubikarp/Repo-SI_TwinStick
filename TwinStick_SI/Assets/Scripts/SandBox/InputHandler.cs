using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public Vector2 move;
    public Vector2 aim;

    public float moveSpeed = 10f;

    void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad is null)
        {
            Debug.LogError("Fréro il y a pas de manette !!!");
            return;
        }


        if (gamepad.rightTrigger.wasPressedThisFrame)
        {
            Shoot();
        }

        move = gamepad.leftStick.ReadValue();
        transform.position += Time.deltaTime * move.ToPlaneXZ() * moveSpeed;

        aim = gamepad.rightStick.ReadValue();
        transform.LookAt(transform.position + aim.ToPlaneXZ().normalized,Vector3.up);

    }

    private void Shoot()
    {
        Debug.Log("Bim!");

    }

    public void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire!");
    }
}
