using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField] Transform target;

    private void Update()
    {
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
    }
}
