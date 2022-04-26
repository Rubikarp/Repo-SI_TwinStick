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

    public PlayerInput input;

    private void Start()
    {
        
    }

    void Update()
    {      
        transform.position += Time.deltaTime * move.ToPlaneXZ() * moveSpeed;
        transform.LookAt(transform.position + aim.ToPlaneXZ().normalized,Vector3.up);
    }

    public void Move(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
    public void Aim(InputAction.CallbackContext context)
    {
        aim = context.ReadValue<Vector2>();
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        Debug.Log(context);
    }
}
