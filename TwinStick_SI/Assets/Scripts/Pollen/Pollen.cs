using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollen : MonoBehaviour
{

    public int pollenAmount=1;

    GeneratorTower ownerTower;

    public void PickUp()
    {
        ownerTower.generatedPollen -= 1;
    }



}
