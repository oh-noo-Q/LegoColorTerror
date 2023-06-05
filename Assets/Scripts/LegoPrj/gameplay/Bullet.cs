using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    public Transform owner;
    public LegoColor color;
    public Transform target;

    [Space(10)]
    [SerializeField] float speed;
    [SerializeField] float speedRotation;

    private void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            RotateToTarget(target);
        }
    }

    public void CounterAttack()
    {
        target = owner;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    void RotateToTarget(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        Quaternion toRatation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRatation, speedRotation * Time.deltaTime);
    }
}
