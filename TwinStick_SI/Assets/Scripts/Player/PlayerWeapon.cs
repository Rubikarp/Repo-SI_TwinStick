using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeapon : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] Transform weaponextremum;
    [SerializeField] Transform bulletContainer;
    [Space(5)]
    [SerializeField] GameObject lightShoot;
    [SerializeField] GameObject heavyShoot;

    public UnityEvent<Vector3> onShoot;

    public void HeavyShoot()
    {
        onShoot?.Invoke(weaponextremum.forward);
        Instantiate(heavyShoot, weaponextremum.position, weaponextremum.rotation, bulletContainer);
    }

    public void LightShoot()
    {
        onShoot?.Invoke(weaponextremum.forward);
    }
}
