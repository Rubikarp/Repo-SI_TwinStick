using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] Transform weaponextremum;
    [SerializeField] Transform bulletContainer;
    [Space(5)]
    [SerializeField] GameObject lightShoot;
    [SerializeField] GameObject heavyShoot;
    [SerializeField] ParticleSystem particleS;


    public void HeavyShoot()
    {
        Instantiate(heavyShoot, weaponextremum.position, weaponextremum.rotation, bulletContainer);
    }

    public void LightShoot()
    {
        particleS.Play();
    }
}
