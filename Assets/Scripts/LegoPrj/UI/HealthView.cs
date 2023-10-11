using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthView : MonoBehaviour
{
    [SerializeField] GameObject activeObj, deactiveObj;

    public void Active(bool isActive)
    {
        if(isActive)
        {
            activeObj.SetActive(true);
        }
        else 
        {
            activeObj.SetActive(false);
        }
    }
}
