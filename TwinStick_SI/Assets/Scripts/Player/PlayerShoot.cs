using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using NaughtyAttributes;

public class PlayerShoot : MonoBehaviour
{

    [Header("Parameter")]
    [ProgressBar("Charge", "chargedTimeThreshold", EColor.Green)]
    [SerializeField] float chargedTime = 0f;
    [SerializeField] float chargedTimeThreshold = 0.5f;
    [Space(10)]
    [SerializeField] float shootCooldown = 0.5f;
    [Space(10)]
    [SerializeField] float shootLightCost = 2f;
    [SerializeField] float shootHeavyCost = 5f;

    [Header("Data")]
    [SerializeField] bool canShoot = true;
    [SerializeField] bool inCharge = false;
    [SerializeField] bool shootHeavy = false;

    [Header("Event")]
    public UnityEvent onCharge;
    public UnityEvent onCannotCharge;
    public UnityEvent onCannotHeavy;
    [Space(10)]
    public UnityEvent onLightShoot;
    public UnityEvent onHeavyShoot;

    public void Shoot(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                TryCharge();
                break;
                 
            case InputActionPhase.Canceled:
                TryShoot();
                break;
        }
    }

    private void ShootCD() { canShoot = true; }
    private void TryCharge()
    {
        if (!canShoot /* || !enough mana */)
        {
            onCannotCharge?.Invoke();
            return;
        }

        inCharge = true;
        chargedTime = 0f;
        canShoot = false;
        shootHeavy = false;

        onCharge?.Invoke();
    }
    public void TryShoot()
    {
        if (shootHeavy)
        {
            HeavyShoot();
        }
        else
        {
            LightShoot();
        }

        Invoke("ShootCD", shootCooldown);
        chargedTime = 0f;
        inCharge = false;
        shootHeavy = false;
    }

    private void HeavyShoot()
    {
        onHeavyShoot?.Invoke();
        Debug.Log("Heavy");
    }

    private void LightShoot()
    {
        onLightShoot?.Invoke();
        Debug.Log("Light");
    }

    void Update()
    {
        if (inCharge && !shootHeavy)
        {
            chargedTime += Time.deltaTime;

            if(chargedTime > chargedTimeThreshold)
            {
                if (true /*enoughtMana*/)
                {
                    shootHeavy = true;
                }
                else
                {
                    onCannotHeavy?.Invoke();
                    chargedTime = 0f;
                }

            }
        }
    }

}
