using RotaryHeart.Lib.SerializableDictionary;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class SoundNameDictionary : SerializableDictionaryBase<SoundName, string>
{

}

[System.Serializable]
public class DefaultSoundDictionary : SerializableDictionaryBase<SoundName, AudioClip>
{

}

[System.Serializable]
public class SoundSourceDictionary : SerializableDictionaryBase<SoundType, AudioSource>
{

}

[CreateAssetMenu(fileName = "SoundSourceManager", menuName = "Manager/SoundSourceManager")]
public class SoundSourceManager : SingletonScriptableObject<SoundSourceManager>
{
    [Header("Sounds")]
    public SoundNameDictionary soundNames;
    public DefaultSoundDictionary defaultSound;

    public SoundSourceManager()
    {
    }

    public AudioClip GetSoundWithType(SoundName type)
    {
        return defaultSound[type];
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SoundSourceManager))]
public class SoundSourceManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        return;
        //if (GUILayout.Button("Import Sound From Data Manager"))
        //{
        //    SoundSourceManager manager = (SoundSourceManager)target;
        //    SoundNameDictionary names = new SoundNameDictionary();

        //    foreach(var pair in names)
        //    {
        //        manager.defaultSound.Add(pair.Key, AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Resources/AudioClips" + pair.Value));
        //    }

        //    EditorUtility.SetDirty(target);
        //    AssetDatabase.SaveAssets();
        //    AssetDatabase.Refresh();
        //    EditorUtility.DisplayDialog("Alert", "Import success", "OK");
        //}

    }
}
#endif