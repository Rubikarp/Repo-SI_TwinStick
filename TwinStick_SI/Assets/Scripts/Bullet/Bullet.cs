using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] TARGET_TYPE shooterType = TARGET_TYPE.TARGET_TYPE_PLAYER;
    [SerializeField] float life = 0;
    [SerializeField] float lifeTime = 5f;
    [SerializeField] int damage = 1;
    [SerializeField] AnimationCurve speedOverLifeTime = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);

    bool haveTouch = false;

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

    private void OnTriggerEnter(Collider other)
    {
        if (haveTouch)
        {
            return;
        }

        GameObject col = other.gameObject;

        BasicHealth bh = col.GetComponent<BasicHealth>();

        if (bh != null)
        {
            switch (shooterType)
            {
                case TARGET_TYPE.TARGET_TYPE_BUILDING:
                    if(bh.TargetType== TARGET_TYPE.TARGET_TYPE_ENNEMY)
                    {
                        Debug.Log("Dealing Building Damage to " + col.name);
                        bh.TakeDamage(damage);
                        haveTouch = true;
                        Destroy();
                    }
                    break;
                case TARGET_TYPE.TARGET_TYPE_PLAYER:
                    if (bh.TargetType == TARGET_TYPE.TARGET_TYPE_ENNEMY)
                    {
                        Debug.Log("Dealing Player Damage to " + col.name);
                        bh.TakeDamage(damage);
                        bh.gameObject.GetComponent<AEnnemy>().target = GameObject.FindObjectOfType<PlayerHealth>().gameObject;
                        bh.gameObject.GetComponent<AEnnemy>().bh = GameObject.FindObjectOfType<PlayerHealth>();
                        haveTouch = true;
                        Destroy();
                    }
                    break;
                case TARGET_TYPE.TARGET_TYPE_ENNEMY:
                    if (bh.TargetType == TARGET_TYPE.TARGET_TYPE_PLAYER || bh.TargetType == TARGET_TYPE.TARGET_TYPE_BUILDING || bh.TargetType == TARGET_TYPE.TARGET_TYPE_HIVE)
                    {
                        Debug.Log("Dealing Ennemy Damage to " + col.name);
                        bh.TakeDamage(damage);
                        
                        haveTouch = true;
                        Destroy();


                    }
                    break;
                default:
                    break;
            }
            
        }
    }


}
