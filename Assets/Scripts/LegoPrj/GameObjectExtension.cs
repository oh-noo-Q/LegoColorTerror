using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtension
{
    public static void DestroyAllChildren(this GameObject gameObject)
    {
        for(int i = gameObject.transform.childCount - 1; i >= 0; i--)
        {
            Object.DestroyImmediate(gameObject.transform.GetChild(i).gameObject);
        }
    }
}
