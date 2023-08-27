using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateSkill : MonoBehaviour
{
    [SerializeField] protected int maxStack;
    [SerializeField] protected int currentStack;

    protected virtual void Activate(object obj)
    {
        if(currentStack >= maxStack)
        {
            currentStack = 0;
            Action();
        }
        else
        {
            return;
        }
    }

    protected virtual void Action()
    {

    }

    protected virtual void UpdateStack(object value)
    {
        if ((int)value >= 0)
        {
            currentStack += (int)value;
            currentStack = Mathf.Clamp(currentStack, 0, maxStack);
        }
        else
        {
            if(currentStack < maxStack) currentStack = 0;
        }
    }

    protected virtual void Update()
    {
        
    }
}
