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
    protected Animator anim;

    [HideInInspector]
    public float buffSpeed = 1.0f;
    [HideInInspector]
    public Transform distanceDie;

    [HideInInspector]
    public int blockDamage;
    [HideInInspector]
    public int injureHit = 0;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        
    }

    protected override void Update()
    {
        base.Update();
        if (GameManager.Instance.enemyManager.currentTargetEnemy == null)
        {
            return;
        }
        else if (Vector3.Distance(transform.position, distanceDie.position) <
            Vector3.Distance(GameManager.Instance.enemyManager.currentTargetEnemy.transform.position, distanceDie.position))
        {
            GameManager.Instance.enemyManager.SetTargetEnemy(this);
        }
        if (Mathf.Abs(transform.position.z - distanceDie.position.z) < 0.5f)
        {
            AttackPlayer();
        }
    }

    public virtual void Setup()
    {
        anim.SetBool("Run", true);
    }

    public void Die()
    {
        int ranDieSound = Random.Range(1, 11);
        SoundManager.instance.PlayDieSound(ranDieSound);
        Destroy(gameObject);
        GameManager.Instance.UpdateScore(1);
    }

    public void DieByUlti()
    {
        Destroy(gameObject);
    }

    protected virtual void AttackPlayer()
    {
        GameManager.Instance.enemyManager.KillEnemy(this);
        EventDispatcher.Instance.PostEvent(EventID.UpdateMeteorStack, -1);
        EventDispatcher.Instance.PostEvent(EventID.OnChangeValueHealth, -1);
        GameManager.Instance.EffectLegoExplosion(transform, mainColor);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            Bullet bulletColision = other.GetComponent<Bullet>();
            Destroy(bulletColision.gameObject);
            /*
            if (bulletColision.color == mainColor)
            {
                if(blockDamage > 0)
                {
                    blockDamage--;
                    UILegoManager.Instance.inGameUI.SetBlockText(blockDamage);
                    Destroy(bulletColision.gameObject);
                    return;
                }
                pieces[bulletColision.targetPiece].SetActive(false);
                GameManager.Instance.effectController
                        .GenExplosion(pieces[bulletColision.targetPiece].transform,
                        GameManager.Instance.colorDic[mainColor]);
                if (bulletColision.targetPiece == pieces.Count - 1)
                    GameManager.Instance.enemyManager.KillEnemy(this);
            }
            else
            {
                blockDamage++;
                UILegoManager.Instance.inGameUI.SetBlockText(blockDamage);

                //Bullet target slime if fail
                //bulletColision.CounterAttack();
            }
            */
        }
    }

    public virtual void AttackEnemy(LegoColor attackColor, Bullet bullet, BulletType type)
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
            EventDispatcher.Instance.PostEvent(EventID.UpdateMeteorStack, 1);
            GameManager.Instance.EffectLegoExplosion(pieces[bullet.targetPiece].transform, mainColor);
            GameManager.Instance.EffecBullet(type, pieces[bullet.targetPiece].transform);
            if (bullet.targetPiece == pieces.Count - 1)
                GameManager.Instance.enemyManager.KillEnemy(this);
        }
        else
        {
            blockDamage++;
            SoundManager.instance.PlaySingle(SoundType.LayerSound, SoundName.BulletWrongLayer);
            EventDispatcher.Instance.PostEvent(EventID.UpdateMeteorStack, -1);
            numberBlock.ActiveNumber(blockDamage, mainColor);
            if (blockDamage > 2) AttackPlayer();
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

