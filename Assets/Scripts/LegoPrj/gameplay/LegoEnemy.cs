using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum LegoColor
{
    BLUE,
    GREEN,
    RED,
    PINK,
    PURPLE
}

public class LegoEnemy : MonoBehaviour
{
    public List<GameObject> pieces;
    public LegoColor mainColor;
    public Transform targetIconPosition;

    public Vector3 moveDirection;
    public float speed;
    public float buffSpeed = 1.0f;
    public Transform distanceDie;

    public int blockDamage;
    public int injureHit = 0;

    virtual protected void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
        if(Mathf.Abs(transform.position.z - distanceDie.position.z) < 0.5f)
        {
            GameManager.Instance.enemyManager.KillEnemy(this);
            EventDispatcher.Instance.PostEvent(EventID.OnChangeValueHealth, -1);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            Bullet bulletColision = other.GetComponent<Bullet>();
            if (bulletColision.color == mainColor)
            {
                if(blockDamage > 0)
                {
                    blockDamage--;
                    UILegoManager.Instance.inGameUI.SetBlockText(blockDamage);
                    Destroy(bulletColision.gameObject);
                    return;
                }
                pieces[bulletColision.targetPiece].SetActive(false);
                GameManager.Instance.effectController
                        .GenExplosion(pieces[bulletColision.targetPiece].transform,
                        GameManager.Instance.colorDic[mainColor]);
                if (bulletColision.targetPiece == pieces.Count - 1)
                    GameManager.Instance.enemyManager.KillEnemy(this);
                Destroy(bulletColision.gameObject);
            }
            else
            {
                blockDamage++;
                UILegoManager.Instance.inGameUI.SetBlockText(blockDamage);

                //Bullet target slime if fail
                //bulletColision.CounterAttack();
            }
        }
    }
}

