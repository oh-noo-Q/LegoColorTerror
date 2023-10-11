using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
        transform.DOScale(0.8f, 0.2f).OnComplete(() => 
        {
        });
        SoundManager.instance.PlaySingle(SoundType.LayerSound, SoundName.Click);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(1.0f, 0.2f);
    }
}
