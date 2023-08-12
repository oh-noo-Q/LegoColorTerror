using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int maxHealth = 5;
    public int health;

    private void Awake()
    {
        EventDispatcher.Instance.RegisterListener(EventID.OnChangeValueHealth, UpdateHealth);
    }

    public void SetupStart()
    {
        health = maxHealth;

    }

    void UpdateHealth(object obj)
    {
        health += (int)obj;
        EventDispatcher.Instance.PostEvent(EventID.OnUpdateHealth, health);
        if(health <= 0)
        {
            GameManager.Instance.FinishGameEndless();
        }
    }
}
