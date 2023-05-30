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

    void AttackEnemy(object obj)
    {
        if (currentTargetEnemy == null) return;
        if(currentTargetEnemy.mainColor == (LegoColor)obj)
        {
            int amountPiece = currentTargetEnemy.pieces.Count;
            if (currentTargetEnemy.injureHit < amountPiece - 1)
            {
                currentTargetEnemy.pieces[currentTargetEnemy.injureHit].SetActive(false);
                currentTargetEnemy.injureHit++;
            }
            else if(amountPiece - 1 == currentTargetEnemy.injureHit)
            {
                enemies.Remove(currentTargetEnemy);
                currentTargetEnemy.Die(effectKill);
                if (enemies.Count > 0) currentTargetEnemy = enemies[0];
            }
        }
        else
        {

        }
    }
}
