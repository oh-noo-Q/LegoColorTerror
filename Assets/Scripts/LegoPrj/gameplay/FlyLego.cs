using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyLego : LegoEnemy
{
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
                }
            }
        }
        else
        {
            base.OnTriggerEnter(other);
        }
    }
}
