using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BougeDeLa : MonoBehaviour
{

    [SerializeField] Vector3 pos;

    public void DeadCaChacal()
    {
        transform.position = pos;
    }
}
