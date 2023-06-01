using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegoEnemyManager : MonoBehaviour
{
    public List<LegoEnemy> enemies;
    public LegoEnemy currentTargetEnemy;

    public GameObject effectKill;

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
        if(currentTargetEnemy.mainColor == (LegoColor)obj)
        {
            int amountPiece = currentTargetEnemy.pieces.Count;
            if (currentTargetEnemy.injureHit < amountPiece - 1)
            {
                currentTargetEnemy.pieces[currentTargetEnemy.injureHit].SetActive(false);
                EffectLegoExplosion();
                currentTargetEnemy.injureHit++;
            }
            else if(amountPiece - 1 == currentTargetEnemy.injureHit)
            {
                EffectLegoExplosion();
                currentTargetEnemy.pieces[amountPiece - 1].SetActive(false);
                KillEnemy(currentTargetEnemy);
            }
        }
        else
        {

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
        if (enemies.Count > 0) currentTargetEnemy = enemies[0];
    }
}
