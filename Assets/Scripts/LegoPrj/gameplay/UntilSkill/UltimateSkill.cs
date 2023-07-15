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
        }
        else
        {
            return;
        }
    }

    protected virtual void UpdateStack(object value)
    {
        currentStack += (int)value;
        currentStack = Mathf.Clamp(currentStack, 0, maxStack);
    }

    protected virtual void Update()
    {
        
    }
}
