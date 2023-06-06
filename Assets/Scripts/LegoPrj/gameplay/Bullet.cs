using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    public Transform owner;
    public LegoColor color;
    public Transform target;
    public int targetPiece;
    public List<GameObject> details;

    [Space(10)]
    [SerializeField] float speed;
    [SerializeField] float speedRotation;
    private bool isCounter;

    public Bullet(Transform _owner, LegoColor _color)
    {
        this.owner = _owner;
        this.color = _color;
    }

    public void SetColor(LegoColor _color)
    {
        color = _color;
        foreach(GameObject detail in details)
        {
            detail.GetComponent<Renderer>().material = GameManager.Instance.colorDic[color];
        }
    }

    private void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            RotateToTarget(target);
        }
        if(isCounter && transform.position == target.position)
        {
            Destroy(gameObject);
        }
    }

    public void CounterAttack()
    {
        target = owner;
        isCounter = true;
        transform.DOLocalRotate(new Vector3(90, 180, 180f), 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    void RotateToTarget(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        Quaternion toRatation = Quaternion.LookRotation(direction, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRatation, speedRotation * Time.deltaTime);
    }
}
