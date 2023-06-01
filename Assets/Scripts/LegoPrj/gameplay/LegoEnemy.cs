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

    public Vector3 moveDirection;
    public float speed;
    public Transform distanceDie;

    public int injureHit = 0;

    private void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
        if(Mathf.Abs(transform.position.z - distanceDie.position.z) < 0.5f)
        {
            EventDispatcher.Instance.PostEvent(EventID.OnChangeValueHealth, -1);
            GameManager.Instance.enemyManager.KillEnemy(this);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}

