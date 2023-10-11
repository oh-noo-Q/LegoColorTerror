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
        EventDispatcher.Instance.PostEvent(EventID.OnUpdateHealth, maxHealth);
    }

    void UpdateHealth(object obj)
    {
        health += (int)obj;
        health = Mathf.Clamp(health, 0, maxHealth);
        EventDispatcher.Instance.PostEvent(EventID.OnUpdateHealth, health);
        if(health <= 0)
        {
            GameManager.Instance.FinishGameEndless();
        }
    }
}
