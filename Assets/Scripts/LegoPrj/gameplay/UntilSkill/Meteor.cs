using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : UltimateSkill
{
    [SerializeField] GameObject meteorPrf;

    private void Awake()
    {
        EventDispatcher.Instance.RegisterListener(EventID.ActiveMeteor, Activate);
        EventDispatcher.Instance.RegisterListener(EventID.UpdateMeteorStack, UpdateStack);
    }

    protected override void Activate(object obj)
    {
        base.Activate(obj);
        GameObject meteor = Instantiate(meteorPrf);
        meteor.transform.position = new Vector3(0, 20, 0);
        meteor.transform.DOLocalMoveY(0, 0.75f).OnComplete(() =>
        {
            EventDispatcher.Instance.PostEvent(EventID.ShakingCamera);
            GameManager.Instance.enemyManager.KillAllEnemy();
        });
    }

    protected override void UpdateStack(object value)
    {
        base.UpdateStack(value);
        EventDispatcher.Instance.PostEvent(EventID.UpdateMeteorProcess, (float)currentStack / maxStack);
    }
}
