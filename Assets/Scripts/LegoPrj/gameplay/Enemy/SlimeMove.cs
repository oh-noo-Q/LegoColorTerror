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
