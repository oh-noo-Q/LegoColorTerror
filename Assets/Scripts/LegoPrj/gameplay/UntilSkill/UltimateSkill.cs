using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateSkill : MonoBehaviour
{
    [SerializeField] int maxStack;
    [SerializeField] int currentStack;

    protected virtual void Activate()
    {
        if(currentStack == maxStack)
        {
            currentStack = 0;
        }
        else
        {
            return;
        }
    }

    protected virtual void UpdateStack(int value)
    {
        currentStack += value;
        currentStack = Mathf.Clamp(currentStack, 0, maxStack);
    }

    protected virtual void Update()
    {
        
    }
}
