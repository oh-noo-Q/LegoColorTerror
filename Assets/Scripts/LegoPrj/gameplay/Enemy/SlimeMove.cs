using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMove : Movement
{
    public Transform destination;
    [SerializeField] Animator anim;

    private void Awake()
    {
        AnimStart();
    }

    void AnimStart()
    {
        float ran = Random.Range(0.0f, 2.0f);
        if(ran <= 1)
        {
            anim.SetBool("Jump", true);
        }
    }
    protected override void Update()
    {
        base.Update();
        if(GameManager.Instance.enemyManager.currentTargetEnemy == null)
        {
            return;
        }
        else
        {
            GameManager.Instance.enemyManager.SetTargetSlime(this);
        }

        if (Vector3.Distance(transform.position, destination.position) <
            Vector3.Distance(GameManager.Instance.enemyManager.currentTargetEnemy.transform.position, destination.position))
        {
            GameManager.Instance.enemyManager.SetTargetSlime(this);
        }
        if (Mathf.Abs(transform.position.z - destination.position.z) < 0.5f)
        {
            Die();
        }
    }

    public void GetAttack()
    {
        GameManager.Instance.effectController
                    .GenExplosion(transform,
                    GameManager.Instance.colorDic[LegoColor.White]);
        Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public override void SetTarget(GameObject targetGO)
    {
        base.SetTarget(targetGO);
    }
}
