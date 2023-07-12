using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : UltimateSkill
{
    [SerializeField] GameObject meteorPrf;

    protected override void Activate()
    {
        base.Activate();
        GameObject meteor = Instantiate(meteorPrf);
        meteor.transform.position = new Vector3(0, 20, 0);
        meteor.transform.DOLocalMoveY(0, 0.5f);
        EventDispatcher.Instance.PostEvent(EventID.ShakingCamera);
    }
}
