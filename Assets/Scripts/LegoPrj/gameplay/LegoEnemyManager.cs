using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegoEnemyManager : MonoBehaviour
{
    public List<LegoEnemy> enemies;
    public LegoEnemy currentTargetEnemy;

    public Bullet bulletPrf;
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
            newBullet.target = currentTargetEnemy.pieces[currentTargetEnemy.injureHit].transform;
            if(currentTargetEnemy.mainColor == ownerSlime.color)
                currentTargetEnemy.injureHit++;
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
