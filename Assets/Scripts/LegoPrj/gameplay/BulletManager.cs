using RotaryHeart.Lib.SerializableDictionary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletObjectDictionary : SerializableDictionaryBase<BulletType, Bullet> { };

[System.Serializable]
public class EffectBulletDictionary : SerializableDictionaryBase<BulletType, GameObject> { };

public class BulletManager : MonoBehaviour
{
    public BulletObjectDictionary bulletGODictionary;
    public Transform parentObj;

    public void DestroyAllBullet()
    {
        parentObj.gameObject.DestroyAllChildren();
    }
}