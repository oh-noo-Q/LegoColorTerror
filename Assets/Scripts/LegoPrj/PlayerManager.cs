using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int health = 5;

    private void Awake()
    {
        EventDispatcher.Instance.RegisterListener(EventID.OnChangeValueHealth, UpdateHealth);
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
