using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ToggleViewController : MonoBehaviour
{
    [SerializeField] Text onTxt, offTxt;
    [SerializeField] Image onIcon, offIcon;

    private float durationTime = 0.3f;

    public void Active(bool isOn)
    {
        if(isOn)
        {
            onTxt.gameObject.SetActive(true);
            onIcon.gameObject.SetActive(true);
            onTxt.color = new Color(onTxt.color.r, onTxt.color.g, onTxt.color.b, 1);
            onIcon.color = new Color(onIcon.color.r, onIcon.color.g, onIcon.color.b, 1);
            offIcon.gameObject.SetActive(false);
            offTxt.gameObject.SetActive(false);
        }
        else
        {
            onTxt.gameObject.SetActive(false);
            onIcon.gameObject.SetActive(false);
            offIcon.gameObject.SetActive(true);
            offTxt.gameObject.SetActive(true);
            offIcon.color = new Color(offIcon.color.r, offIcon.color.g, offIcon.color.b, 1);
            offTxt.color = new Color(offTxt.color.r, offTxt.color.g, offTxt.color.b, 1);
        }
    }

    public void SetStatus(bool active)
    {
        if(active)
        {
            offIcon.DOFade(0, durationTime).SetUpdate(true);
            offTxt.DOFade(0, durationTime).SetUpdate(true);
            onTxt.color = new Color(onTxt.color.r, onTxt.color.g, onTxt.color.b, 0);
            onIcon.color = new Color(onIcon.color.r, onIcon.color.g, onIcon.color.b, 0);
            onIcon.gameObject.SetActive(true);
            onTxt.gameObject.SetActive(true);
            onIcon.DOFade(1, durationTime).SetUpdate(true);
            onTxt.DOFade(1, durationTime).SetUpdate(true);
            Vector3 defaultPos = offIcon.transform.position;
            offIcon.transform.DOMove(onIcon.transform.position, durationTime).SetUpdate(true).OnComplete(() => 
            {
                offIcon.gameObject.SetActive(false);
                offTxt.gameObject.SetActive(false);
                offIcon.transform.position = defaultPos;
            });
        }
        else
        {
            onIcon.DOFade(0, durationTime).SetUpdate(true);
            onTxt.DOFade(0, durationTime).SetUpdate(true);
            offIcon.color = new Color(offIcon.color.r, offIcon.color.g, offIcon.color.b, 0);
            offTxt.color = new Color(offTxt.color.r, offTxt.color.g, offTxt.color.b, 0);
            offTxt.gameObject.SetActive(true);
            offIcon.gameObject.SetActive(true);
            offIcon.DOFade(1, durationTime).SetUpdate(true);
            offTxt.DOFade(1, durationTime).SetUpdate(true);
            Vector3 defaultPos = onIcon.transform.position;
            onIcon.transform.DOMove(offIcon.transform.position, durationTime).SetUpdate(true).OnComplete(() =>
            {
                onIcon.gameObject.SetActive(false);
                onTxt.gameObject.SetActive(false);
                onIcon.transform.position = defaultPos;
            });
        }
    }

    void ChangeAlphaColor(Color obj, float a)
    {
        
    }

    
}
