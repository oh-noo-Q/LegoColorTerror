using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    public BulletType type;
    public Transform owner;
    public LegoColor color;
    public Transform target;
    public Vector3 targetPos;
    public int targetPiece;
    public List<GameObject> details;

    [Space(10)]
    [SerializeField] float speed;

    private void Awake()
    {
        Destroy(gameObject, 1.5f);    
    }

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
        if(targetPos != Vector3.zero)
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        //transform.LookAt(target);
        if(transform.position == targetPos)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

}
