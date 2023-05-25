using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Enemy : MonoBehaviour
{
    public int id;
    //public Texture[] textureColor;
    //public Material matEnemy;
    public GameObject[] detailEnemy;
    public GameObject enemy;
    public int currentColor;
    public Vector3 target;
    public GameObject arrow;
    public float speed;
    public int health;
    public Animator enemyAnim;

    private bool stateAngry = false;
    private bool alive;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (!alive) return;
        if (transform.position == target)
        {
            if (currentColor == 0)
            {
                EventDispatcher.Instance.PostEvent(EventID.OnChangeValueHealth, 2);
                EventDispatcher.Instance.PostEvent(EventID.KillEnemy, id);
                gameObject.SetActive(false);
            }
            else
            {
                EventDispatcher.Instance.PostEvent(EventID.OnChangeValueHealth, -1);
                EventDispatcher.Instance.PostEvent(EventID.KillEnemy, id);
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    public void Die()
    {
        alive = false;
    }

    public void InitEnemy(int currentIndex, int mainColor, Vector3 mainPlayer, float speedEnemy, Color colorEnemy)
    {
        id = currentIndex;
        target = mainPlayer;
        foreach (GameObject detail in detailEnemy)
        {
            detail.GetComponent<Renderer>().materials[1].color = colorEnemy;
        }
        currentColor = mainColor;
        speed = speedEnemy;
        alive = true;
    }

    public void SetEnemyTarget()
    {
        arrow.SetActive(true);
    }

    public void AngryEnemy()
    {
        if(!stateAngry)
        {
            health += 1;
            enemy.transform.localScale = enemy.transform.localScale * 1.5f;
            stateAngry = true;
        }
    }

    public void InitWhiteEnemy(int mainColor, Color colorEnemy)
    {
        currentColor = mainColor;
        foreach(GameObject detail in detailEnemy)
            detail.GetComponent<Renderer>().materials[1].color = colorEnemy;
        speed = speed * 2;
    }
}
