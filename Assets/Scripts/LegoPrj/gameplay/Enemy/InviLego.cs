using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InviLego : LegoEnemy
{
    public List<Renderer> legoMat;

    [SerializeField] Material inviMat;
    private float timeInvi = 2f;
    private float timeShow = 1f;

    bool isInvi;
    bool isShow;
    float timeCountInvi;
    float timeCountShow;

    private void Awake()
    {
        isInvi = true;
        foreach(Renderer render in legoMat)
        {
            render.material.color = new Color(1, 1, 1, 0);
        }
    }

    protected override void Update()
    {
        if(isInvi)
        {
            timeCountInvi += Time.deltaTime;
            if(timeCountInvi >= timeInvi)
            {
                isInvi = false; 
                timeCountInvi = 0;
                //for(int i = 0; i < legoMat.Count; i++)
                //{
                //    legoMat[i].material.DOFade(1f, 0.5f).OnComplete(() =>
                //    {
                //        isShow = true;
                //        legoMat[i].material = GameManager.Instance.colorDic[mainColor];
                //    }); 
                //}
                foreach (Renderer render in legoMat)
                {
                    render.material.DOFade(1f, 0.5f).OnComplete(() =>
                    {
                        isShow = true;
                        render.material = GameManager.Instance.colorDic[mainColor];
                    });
                }
            }
        }
        if(isShow)
        {
            timeCountShow += Time.deltaTime;
            if(timeCountShow > timeShow)
            {
                isShow = false;
                timeCountShow = 0;
                foreach (Renderer render in legoMat)
                {
                    render.material = inviMat;
                    render.material.DOFade(0f, 0.5f).OnComplete(() =>
                    {
                        isInvi = true;
                    });
                }
            }
        }
        base.Update();

    }

    public override void SetColor(LegoColor _color)
    {
        base.SetColor(_color);
        inviMat = GameManager.Instance.inviColorDic[_color];
        foreach(Renderer render in legoMat)
        {
            render.material = inviMat;
        }
    }
}
