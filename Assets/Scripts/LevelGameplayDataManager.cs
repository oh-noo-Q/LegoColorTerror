using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorData", menuName = "Data/GameplayData")]
public class LevelGameplayDataManager : ScriptableObject
{
    public LevelGameplay[] levelGameplayData;
}
