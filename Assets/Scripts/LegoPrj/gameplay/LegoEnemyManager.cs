using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LegoEnemyManager : MonoBehaviour
{
    public List<LegoEnemy> enemies;
    public LegoEnemy currentTargetEnemy;
    public SlimeMove currentTargetSlime;

    public Bullet bulletPrf;
    public GameObject effectKill;
    public GameObject targetPrf;

    GameObject currentTargetObject;
    private void Awake()
    {
        EventDispatcher.Instance.RegisterListener(EventID.TabAttackLego, AttackEnemy);
        EventDispatcher.Instance.RegisterListener(EventID.TabAttackLego, AttackSlime);
    }

    private void OnDestroy()
    {
        EventDispatcher.Instance.RemoveListener(EventID.TabAttackLego, AttackEnemy);
        EventDispatcher.Instance.RemoveListener(EventID.TabAttackLego, AttackSlime);
    }

    void AttackEnemy(object obj)
    {
        if (currentTargetEnemy == null) return;
        if (currentTargetSlime != null) return;
        SlimeTouch ownerSlime = (SlimeTouch)obj;

        int amountPiece = currentTargetEnemy.pieces.Count;
        if (currentTargetEnemy.injureHit <= amountPiece - 1)
        {
            Bullet newBullet = Instantiate(bulletPrf);
            newBullet.gameObject.SetActive(true);
            newBullet.transform.position = ownerSlime.posOnCurve.position;
            newBullet.owner = ownerSlime.posOnCurve;
            newBullet.SetColor(ownerSlime.color);
            newBullet.targetPiece = currentTargetEnemy.injureHit;
            if (currentTargetEnemy.gameObject.GetComponent<FlyLego>() != null
                && currentTargetEnemy.gameObject.GetComponent<FlyLego>().isFlying)
            {
                newBullet.target = currentTargetEnemy.transform;
                newBullet.targetPos = currentTargetEnemy.transform.position;
                //currentTargetEnemy.gameObject.GetComponent<FlyLego>().isFlying = false;
            }
            else
            {
                newBullet.target = currentTargetEnemy.pieces[currentTargetEnemy.injureHit].transform;
                newBullet.targetPos = currentTargetEnemy.pieces[currentTargetEnemy.injureHit].transform.position;
            }
            currentTargetEnemy.AttackEnemy(newBullet.color, newBullet);
            //if (currentTargetEnemy.mainColor == ownerSlime.color)
            //{
            //    if (currentTargetEnemy.blockDamage > 0) return;
            //    if (currentTargetEnemy.gameObject.GetComponent<FlyLego>() != null && currentTargetEnemy.injureHit == 0)
            //        return;
            //    currentTargetEnemy.injureHit++;
            //}
        }
    }

    void AttackSlime(object obj)
    {
        if (currentTargetSlime == null) return;
        currentTargetSlime.GetAttack();
    }

    public void EffectLegoExplosion()
    {
        GameManager.Instance.effectController
                    .GenExplosion(currentTargetEnemy.pieces[currentTargetEnemy.injureHit].transform,
                    GameManager.Instance.colorDic[currentTargetEnemy.mainColor]);
    }

    public void KillEnemy(LegoEnemy removedEnemy)
    {
        enemies.Remove(removedEnemy);
        if (enemies.Count > 0) SetTargetEnemy(enemies[0]);
        removedEnemy.Die();
        if(enemies.Count == 0 && GameManager.Instance.enemySpawner.CheckEndLevel())
        {
            GameManager.Instance.NextRound();
            return;
        }
    }

    public void SetNewTargetEnemy()
    {
        float distanceMin = 0;
        LegoEnemy newTargetLego = null;
        foreach (LegoEnemy enemy in enemies)
        {
            float distanceCheck = Vector3.Distance(enemy.transform.position, enemy.distanceDie.position);
            if (distanceMin == 0 || distanceMin > distanceCheck)
            {
                distanceMin = distanceCheck;
                newTargetLego = enemy;
            }

        }
        if (newTargetLego != null)
        {
            currentTargetEnemy = newTargetLego;
            SetTargetEnemy(currentTargetEnemy);
        }    
    }

    public void SetTargetEnemy(LegoEnemy enemy)
    {
        currentTargetSlime = null;
        currentTargetEnemy = enemy;
        if (currentTargetObject == null)
        {
            currentTargetObject = Instantiate(targetPrf, enemy.targetIconPosition);
            
        }
        else
        {
            currentTargetObject.SetActive(true);
            currentTargetObject.transform.SetParent(enemy.targetIconPosition);
            currentTargetObject.transform.DOKill();
            currentTargetObject.transform.localPosition = Vector3.zero;
            currentTargetObject.transform.eulerAngles = new Vector3(0, 90f, 0);
        }
        if(enemy is InviLego)
        {
            currentTargetObject.GetComponent<Renderer>().material = GameManager.Instance.colorDic[LegoColor.White];
        }
        else
            currentTargetObject.GetComponent<Renderer>().material = GameManager.Instance.colorDic[enemy.mainColor];
        currentTargetObject.transform.DOLocalMoveY(-2, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }
    public void SetTargetSlime(SlimeMove slime)
    {
        if (currentTargetObject != null)
            currentTargetObject.SetActive(false);
        currentTargetSlime = slime;
    }

    public void ResetLevel() 
    {
        enemies.Clear();
        gameObject.DestroyAllChildren();
        currentTargetEnemy = null;
    }

    public void KillAllEnemy()
    {
        foreach(LegoEnemy enemy in enemies)
        {
            enemy.DieByUlti();
            GameManager.Instance.effectController
                    .GenExplosion(enemy.transform,
                    GameManager.Instance.colorDic[enemy.mainColor]);
        }
        if(GameManager.Instance.enemySpawner.CheckEndLevel())
        {
            GameManager.Instance.NextRound();
            return;
        }
        GameManager.Instance.StartWaitTime(1.5f);
    }
    
}
