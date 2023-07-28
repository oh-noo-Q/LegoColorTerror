using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum LegoColor
{
    BLUE,
    GREEN,
    RED,
    PINK,
    PURPLE, 
    White,
}

public class LegoEnemy : Movement
{
    public List<GameObject> pieces;
    public List<GameObject> decors;
    public NumberController numberBlock;
    public LegoColor mainColor;
    public Transform targetIconPosition;

    [HideInInspector]
    public float buffSpeed = 1.0f;
    [HideInInspector]
    public Transform distanceDie;

    [HideInInspector]
    public int blockDamage;
    [HideInInspector]
    public int injureHit = 0;

    protected override void Update()
    {
        base.Update();
        if (Mathf.Abs(transform.position.z - distanceDie.position.z) < 0.5f)
        {
            AttackPlayer();
        }
        if (GameManager.Instance.enemyManager.currentTargetEnemy == null)
        {

        }
        else if (Vector3.Distance(transform.position, distanceDie.position) <
            Vector3.Distance(GameManager.Instance.enemyManager.currentTargetEnemy.transform.position, distanceDie.position))
        {
            GameManager.Instance.enemyManager.SetTargetEnemy(this);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        EventDispatcher.Instance.PostEvent(EventID.UpdateMeteorStack, 1);
    }

    public void DieByUlti()
    {
        Destroy(gameObject);
    }

    protected virtual void AttackPlayer()
    {
        GameManager.Instance.enemyManager.KillEnemy(this);
        EventDispatcher.Instance.PostEvent(EventID.OnChangeValueHealth, -1);
        GameManager.Instance.effectController
                    .GenExplosion(transform, GameManager.Instance.colorDic[mainColor]);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            Bullet bulletColision = other.GetComponent<Bullet>();
            //if (bulletColision.color == mainColor)
            //{
            //    if(blockDamage > 0)
            //    {
            //        blockDamage--;
            //        UILegoManager.Instance.inGameUI.SetBlockText(blockDamage);
            //        Destroy(bulletColision.gameObject);
            //        return;
            //    }
            //    pieces[bulletColision.targetPiece].SetActive(false);
            //    GameManager.Instance.effectController
            //            .GenExplosion(pieces[bulletColision.targetPiece].transform,
            //            GameManager.Instance.colorDic[mainColor]);
            //    if (bulletColision.targetPiece == pieces.Count - 1)
            //        GameManager.Instance.enemyManager.KillEnemy(this);
            //}
            //else
            //{
            //    blockDamage++;
            //    UILegoManager.Instance.inGameUI.SetBlockText(blockDamage);

            //    //Bullet target slime if fail
            //    //bulletColision.CounterAttack();
            //}
            Destroy(bulletColision.gameObject);
        }
    }

    public virtual void AttackEnemy(LegoColor attackColor, Bullet bullet)
    {
        if (attackColor == mainColor)
        {
            if (blockDamage > 0)
            {
                blockDamage--;
                numberBlock.ActiveNumber(blockDamage, mainColor);
                return;
            }
            injureHit++;
            pieces[bullet.targetPiece].SetActive(false);
            SoundManager.instance.PlaySingle(SoundType.balloonExplosion);
            GameManager.Instance.effectController
                    .GenExplosion(pieces[bullet.targetPiece].transform,
                    GameManager.Instance.colorDic[mainColor]);
            if (bullet.targetPiece == pieces.Count - 1)
                GameManager.Instance.enemyManager.KillEnemy(this);
        }
        else
        {
            blockDamage++;
            if (blockDamage > 2) AttackPlayer();
            numberBlock.ActiveNumber(blockDamage, mainColor);
        }
    }

    public virtual void SetColor(LegoColor _color)
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            pieces[i].GetComponent<Renderer>().material = GameManager.Instance.colorDic[_color];
        }
        for (int i = 0; i < decors.Count; i++)
        {
            decors[i].GetComponent<Renderer>().material = GameManager.Instance.colorDic[_color];
        }
        mainColor = _color;
    }

    public override void SetTarget(GameObject targetGO)
    {
        base.SetTarget(targetGO);
    }
}

