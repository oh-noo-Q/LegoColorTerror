using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        if(Vector3.Distance(transform.position, distanceDie.position) < 0.5f)
        {
            Die(null);
        }
    }

    public void Die(GameObject enemyExplosionPrefab)
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(enemyExplosionPrefab, transform.position,
            enemyExplosionPrefab.transform.rotation);
        explosion.transform.SetParent(transform);
        Destroy(explosion, 1.0f);
    }
}

