using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMove : Movement
{
    public Transform destination;
    protected override void Update()
    {
        base.Update();
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

    void Die()
    {
        Destroy(gameObject);
    }
}
