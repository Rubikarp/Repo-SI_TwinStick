using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] float speed = 10f;

    [SerializeField] float life = 0;
    [SerializeField] float lifeTime = 5f;
    [SerializeField] AnimationCurve speedOverLifeTime = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);

    void Start()
    {
        life = 0;
    }

    void Update()
    {
        life += Time.deltaTime;
        transform.position += Time.deltaTime * transform.forward * speed * speedOverLifeTime.Evaluate(life / lifeTime);

        if(life >= lifeTime)
        {
            Destroy();
        }
    }


    public void Destroy()
    {
        Destroy(this.gameObject, 0.2f);
    }
}
