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

    public void SetTargetEnemy(LegoEnemy enemy)
    {
        currentTargetEnemy = enemy;
        if (currentTargetObject == null)
        {
            currentTargetObject = Instantiate(targetPrf, enemy.targetIconPosition);
            currentTargetObject.GetComponent<Renderer>().material = GameManager.Instance.colorDic[enemy.mainColor];
            currentTargetObject.transform.DOLocalMoveY(-2, 0.5f).SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            currentTargetObject.transform.SetParent(enemy.targetIconPosition);
            currentTargetObject.transform.DOKill();
            currentTargetObject.GetComponent<Renderer>().material = GameManager.Instance.colorDic[enemy.mainColor];
            currentTargetObject.transform.localPosition = Vector3.zero;
            currentTargetObject.transform.eulerAngles = new Vector3(0, 90f, 0);
            currentTargetObject.transform.DOLocalMoveY(-2, 0.5f).SetLoops(-1, LoopType.Yoyo);
        }
    }
    public void SetTargetSlime(SlimeMove slime)
    {
        if (currentTargetObject != null)
            currentTargetObject.SetActive(false);
    }

    public void ResetLevel() 
    {
        enemies.Clear();
        gameObject.DestroyAllChildren();
        currentTargetEnemy = null;
    }
    
}
