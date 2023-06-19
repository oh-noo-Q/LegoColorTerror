using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyLego : LegoEnemy
{
    public Transform modelTrans;
    [HideInInspector]
    public bool isFlying;

    private void Awake()
    {
        isFlying = true;

    }
    protected override void Update()
    {
        base.Update();
        if (GameManager.Instance.enemyManager.currentTargetEnemy == null) return;
        if (gameObject == null) return;
        if(Vector3.Distance(transform.position, distanceDie.position) < 
            Vector3.Distance(GameManager.Instance.enemyManager.currentTargetEnemy.transform.position, distanceDie.position))
        {
            GameManager.Instance.enemyManager.SetTargetEnemy(this);
        }

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
                    isFlying = false;
                    transform.DOLocalMoveY(0, 0.3f);
                    modelTrans.DORotate(new Vector3(0, 0, 0), 0.3f);
                }
                else
                {
                    bulletColision.CounterAttack();
                }
                Destroy(bulletColision.gameObject);
            }
        }
        else
        {
            base.OnTriggerEnter(other);
        }
    }
}
