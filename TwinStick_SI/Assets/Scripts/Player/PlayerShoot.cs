using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using NaughtyAttributes;

public class PlayerShoot : MonoBehaviour
{
    [Header("Component")]
    [SerializeField, Required] PlayerPollen pollenStock;

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
    public UnityEvent onAnyShoot;
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
                if(inCharge)
                TryShoot();
                break;
        }
    }

    private void ShootCD() { canShoot = true; }
    private void TryCharge()
    {
        if (!canShoot || !pollenStock.CanConsume(shootLightCost))
        {
            onCannotCharge?.Invoke();
            Debug.LogError("Can't shoot");
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
        onAnyShoot?.Invoke();
        if (shootHeavy)
        {
            pollenStock.ConsumePollen(shootHeavyCost);
            onHeavyShoot?.Invoke();
            Debug.Log("Heavy");
        }
        else
        {
            pollenStock.ConsumePollen(shootLightCost);
            onLightShoot?.Invoke();
            Debug.Log("Light");
        }

        Invoke("ShootCD", shootCooldown);
        chargedTime = 0f;
        inCharge = false;
        shootHeavy = false;
    }

    void Update()
    {
        if (inCharge && !shootHeavy)
        {
            chargedTime += Time.deltaTime;

            if(chargedTime > chargedTimeThreshold)
            {
                if (pollenStock.CanConsume(shootHeavyCost))
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
