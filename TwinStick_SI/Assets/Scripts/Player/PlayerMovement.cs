using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;

public class PlayerMovement : MonoBehaviour
{
    #region Recup Input
    [SerializeField] Vector2 move = Vector2.zero;
    [SerializeField] Vector2 aim = Vector2.right;
    public Vector2 MoveDir { get { return move; } }
    public Vector2 AimDir { get { return aim; } }
    public void Move(InputAction.CallbackContext context)
    {
        move = InputRelativeToCam(context.ReadValue<Vector2>()).normalized;
    }
    public void Aim(InputAction.CallbackContext context)
    {
        aim = context.ReadValue<Vector2>();
    }
    #endregion

    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem sprintParticles;
    [SerializeField] Rigidbody rbody;

    private Camera cam
    {
        get
        {
            return KarpHelper.Camera;
        }
    }
    public Vector2 InputRelativeToCam(Vector2 input)
    {
        Vector2 forward = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up).ToVec2XZ();
        Vector2 right = -Vector2.Perpendicular(forward);
        return input.x * right + input.y * forward;
    }

    [Header("RunTime")]
    [SerializeField] float moveTime = 0f;
    [SerializeField] float accThreshold = 0.3f;
    [SerializeField] float runThreshold = 2f;
    public bool InRunningMode = false;

    [Header("Speed")]
    [ShowNonSerializedField] float speed = 0;
    [Space(10)]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float runBonusSpeed = 5f;
    [Space(10)]
    [SerializeField] float aimSpeed = 10f;


    void Update()
    {
        Running();
        Aiming();
    }

    private void Running()
    {
        if (move.magnitude > 0.1f)
        {
            moveTime += Time.deltaTime;
            animator.SetBool("IsMoving", true);
        }
        else
        {
            MoveTimeReboot();
            animator.SetBool("IsMoving", false);

            sprintParticles.Stop();
        }

        if (moveTime < accThreshold)
        {
            speed = Mathf.Lerp(0, moveSpeed, KarpEase.InOutSine(moveTime / accThreshold));

            animator.SetBool("IsSprinting", false);
        }
        else if (moveTime < runThreshold)
        {
            speed = moveSpeed;
            sprintParticles.Stop();
        }
        else if (moveTime < runThreshold + accThreshold)
        {
            speed = moveSpeed + Mathf.Lerp(0, runBonusSpeed, KarpEase.InOutSine((moveTime - runThreshold) / accThreshold));

            animator.SetBool("IsSprinting", true);
            sprintParticles.Play();
        }
        else
        {
            speed = moveSpeed + runBonusSpeed;
            
        }

        rbody.velocity = Vector3.Scale(rbody.velocity,Vector3.up) + move.ToPlaneXZ() * speed;
        //transform.position += Time.deltaTime * move.ToPlaneXZ() * speed;
    }
    public void MoveTimeReboot()
    {
        moveTime = 0f;
    }
    public void RunTimeReboot()
    {
        if (moveTime > accThreshold)
            moveTime = accThreshold;
    }

    private void Aiming()
    {
        Quaternion aimRot;

        if (aim.magnitude > 0.3)
        {
            aimRot = Quaternion.LookRotation(InputRelativeToCam(aim.normalized).ToPlaneXZ().normalized, Vector3.up);
        }
        else
        {
            aimRot = Quaternion.LookRotation(move.ToPlaneXZ().normalized, Vector3.up);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, aimRot, Time.deltaTime * aimSpeed);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + (move.ToPlaneXZ() * 4f), Color.red);
        Debug.DrawLine(transform.position, transform.position + (aim.ToPlaneXZ() * 4f), Color.green);
    }
}
