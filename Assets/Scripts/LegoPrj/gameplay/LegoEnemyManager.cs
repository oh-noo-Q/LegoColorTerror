using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LegoEnemyManager : MonoBehaviour
{
    public List<LegoEnemy> enemies;
    public LegoEnemy currentTargetEnemy;

    public Bullet bulletPrf;
    public GameObject effectKill;
    public GameObject targetPrf;

    GameObject currentTargetObject;
    private void Awake()
    {
        EventDispatcher.Instance.RegisterListener(EventID.TabAttackLego, AttackEnemy);
    }

    private void OnDestroy()
    {
        EventDispatcher.Instance.RemoveListener(EventID.TabAttackLego, AttackEnemy);
    }

    void AttackEnemy(object obj)
    {
        if (currentTargetEnemy == null) return;
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
            if(currentTargetEnemy.gameObject.GetComponent<FlyLego>() != null
                && currentTargetEnemy.gameObject.GetComponent<FlyLego>().isFlying)
            {
                newBullet.target = currentTargetEnemy.transform;
                //currentTargetEnemy.gameObject.GetComponent<FlyLego>().isFlying = false;
            }
            else
                newBullet.target = currentTargetEnemy.pieces[currentTargetEnemy.injureHit].transform;
            if (currentTargetEnemy.mainColor == ownerSlime.color)
            {
                if (currentTargetEnemy.gameObject.GetComponent<FlyLego>() != null && currentTargetEnemy.injureHit == 0)
                    return;
                currentTargetEnemy.injureHit++;
            }
        }
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
        removedEnemy.Die();
        if(enemies.Count == 0 && GameManager.Instance.enemySpawner.CheckEndLevel())
        {
            GameManager.Instance.NextRound();
            return;
        }
        if (enemies.Count > 0) SetTargetEnemy(enemies[0]);
    }

    public void SetTargetEnemy(LegoEnemy enemy)
    {
        currentTargetEnemy = enemy;
        if (currentTargetObject == null)
        {
            currentTargetObject = Instantiate(targetPrf, enemy.targetIconPosition);
            currentTargetObject.transform.DOLocalMoveY(-2, 0.5f).SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            currentTargetObject.transform.DOKill();
            currentTargetObject.transform.SetParent(enemy.targetIconPosition);
            currentTargetObject.transform.localPosition = Vector3.zero;
            currentTargetObject.transform.DOLocalMoveY(-2, 0.5f).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
