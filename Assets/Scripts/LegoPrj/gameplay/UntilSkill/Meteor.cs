using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Meteor : UltimateSkill
{
    [SerializeField] GameObject meteorPrf;

    private void Awake()
    {
        currentStack = 0;
        EventDispatcher.Instance.RegisterListener(EventID.ActiveMeteor, Activate);
        EventDispatcher.Instance.RegisterListener(EventID.UpdateMeteorStack, UpdateStack);
        UpdateStack(0);
    }

    protected override void Action()
    {
        base.Action();
        GameObject meteor = Instantiate(meteorPrf);
        meteor.transform.position = new Vector3(0, 20, 0);
        meteor.transform.DOLocalMoveY(0, 0.5f).OnComplete(() =>
        {
            EventDispatcher.Instance.PostEvent(EventID.ShakingCamera);
            GameManager.Instance.enemyManager.KillAllEnemy();
            Destroy(meteor.gameObject);
        });
        UpdateStack(0);
    }

    protected override void UpdateStack(object value)
    {
        base.UpdateStack(value);
        if (currentStack % 5 == 0 && currentStack > 0)
        {
            SoundManager.instance.PlayComboSound(currentStack / 5);
        }
        EventDispatcher.Instance.PostEvent(EventID.UpdateMeteorProcess, currentStack);
    }

    
}
