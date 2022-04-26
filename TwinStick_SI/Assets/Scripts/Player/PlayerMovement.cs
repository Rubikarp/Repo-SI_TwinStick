using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Recup Input
    [SerializeField] Vector2 move = Vector2.zero;
    [SerializeField] Vector2 aim = Vector2.up;
    public Vector2 MoveDir { get { return move; } }
    public Vector2 AimDir { get { return aim; } }
    public void Move(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
    public void Aim(InputAction.CallbackContext context)
    {
        if(context.ReadValue<Vector2>().magnitude > 0.1)
        aim = context.ReadValue<Vector2>().normalized;
    }
    #endregion

    [Header("RunTime")]
    [SerializeField] float runTime = 0f;
    [SerializeField] float runTimeThreshold = 2f;
    public bool IsRunning{get{ return runTime > runTimeThreshold; } }

    [Header("Speed")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float runSpeed = 10f;

    void Update()
    {
        Running();
        Aiming();
    }

    private void Running()
    {
        if (move.magnitude > 0.1f)
        {
            runTime += Time.deltaTime;
        }
        else
        {
            RunTimeReboot();
        }

        if (IsRunning)
        {
            transform.position += Time.deltaTime * move.ToPlaneXZ() * runSpeed;
        }
        else
        {
            transform.position += Time.deltaTime * move.ToPlaneXZ() * moveSpeed;
        }
    }
    private void RunTimeReboot()
    {
        runTime = 0f;
    }

    private void Aiming()
    {
        transform.LookAt(transform.position + aim.ToPlaneXZ().normalized, Vector3.up);
    }

}
