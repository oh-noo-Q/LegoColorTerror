using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyLego : LegoEnemy
{
    [HideInInspector]
    public bool isFlying;

    private void Awake()
    {
        isFlying = true;

    }
    protected override void Update()
    {
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
                    isFlying = false;
                    transform.eulerAngles = Vector3.zero;
                    transform.DOLocalMoveY(0, 0.3f);
                }
                else
                {
                    bulletColision.CounterAttack();
                }
            }
        }
        else
        {
            base.OnTriggerEnter(other);
        }
    }
}
