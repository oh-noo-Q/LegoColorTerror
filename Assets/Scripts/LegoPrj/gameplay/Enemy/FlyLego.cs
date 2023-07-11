using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyLego : LegoEnemy
{
    [Space(20)]
    [Header("Fly Lego")]
    public Transform modelTrans;
    [SerializeField] Collider legoCollider;
    [SerializeField] Rigidbody rigid;
    [SerializeField] GameObject wingsObj;
    [HideInInspector]
    public bool isFlying;

    float fallingForce = 2500000f;
    private Animator anim;

    private void Awake()
    {
        isFlying = true;
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("Fly", true);
        rigid = GetComponent<Rigidbody>();
        legoCollider = GetComponent<Collider>();
        rigid.useGravity = false;
        //legoCollider.isTrigger = true;
    }
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        //if(isFlying)
        //{
        //    if (other.CompareTag("Bullet"))
        //    { 
        //        Bullet bulletColision = other.GetComponent<Bullet>();
        //        if (bulletColision.color == mainColor)
        //        {
        //            if (blockDamage > 0)
        //            {
        //                blockDamage--;
        //                UILegoManager.Instance.inGameUI.SetBlockText(blockDamage);
        //                Destroy(bulletColision.gameObject);
        //                return;
        //            }
        //            else
        //            { 
        //                isFlying = false;
        //                rigid.AddForce(Vector3.down * fallingForce, ForceMode.Force);
        //                speed = speed / buffSpeed;
                        
        //                modelTrans.DORotate(new Vector3(0, 0, 0), 0.3f);
        //                Destroy(bulletColision.gameObject);
        //            }
        //        }
        //        else
        //        {
        //            blockDamage++;
        //            UILegoManager.Instance.inGameUI.SetBlockText(blockDamage);
        //            //bulletColision.CounterAttack();
        //        }
        //    }
        //}
        //else
        //{
        //}
            base.OnTriggerEnter(other);
        if (other.CompareTag("Map"))
        {
            rigid.isKinematic = true;
            legoCollider.isTrigger = true;
        }
    }

    public override void AttackEnemy(LegoColor attackColor, Bullet bullet)
    {
        if(isFlying)
        {
            if (attackColor == mainColor)
            {
                if (blockDamage > 0)
                {
                    blockDamage--;
                    numberBlock.ActiveNumber(blockDamage, mainColor);
                    return;
                }
                else
                {
                    isFlying = false;
                    anim.SetBool("Fly", false);
                    legoCollider.isTrigger = false;
                    wingsObj.SetActive(false);
                    rigid.AddForce(Vector3.down * fallingForce, ForceMode.Force);
                    speed = speed / buffSpeed;
                    
                    modelTrans.DORotate(new Vector3(0, 0, 0), 0.3f);
                }
            }
            else
            {
                blockDamage++;
                numberBlock.ActiveNumber(blockDamage, mainColor);
            }
        }
        else 
            base.AttackEnemy(attackColor, bullet);
    }
}