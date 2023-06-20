using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyLego : LegoEnemy
{
    public Transform modelTrans;
    public Rigidbody rigid;
    [HideInInspector]
    public bool isFlying;

    float fallingForce = 1000000f;

    private void Awake()
    {
        isFlying = true;
        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = false;

    }
    protected override void Update()
    {
        if (GameManager.Instance.enemyManager.currentTargetEnemy == null) return;
        if(Vector3.Distance(transform.position, distanceDie.position) < 
            Vector3.Distance(GameManager.Instance.enemyManager.currentTargetEnemy.transform.position, distanceDie.position))
        {
            GameManager.Instance.enemyManager.SetTargetEnemy(this);
        }
        base.Update();

    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(isFlying)
        {
            if (other.CompareTag("Bullet"))
            { 
                Bullet bulletColision = other.GetComponent<Bullet>();
                if (bulletColision.color == mainColor)
                {
                    if (blockDamage > 0)
                    {
                        blockDamage--;
                        Destroy(bulletColision.gameObject);
                        return;
                    }
                    else
                    { 
                        isFlying = false;
                        rigid.AddForce(Vector3.down * fallingForce, ForceMode.Force);
                        
                        modelTrans.DORotate(new Vector3(0, 0, 0), 0.3f);
                        Destroy(bulletColision.gameObject);
                    }
                }
                else
                {
                    blockDamage++;
                    //bulletColision.CounterAttack();
                }
            }
        }
        else
        {
            base.OnTriggerEnter(other);
        }
    }
}
