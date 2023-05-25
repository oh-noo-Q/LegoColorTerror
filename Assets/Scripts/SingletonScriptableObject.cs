using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif
using System.Collections;

public abstract class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T>
{
	private static string FileName
	{
		get
		{
			return typeof(T).Name;
		}
	}

#if UNITY_EDITOR
	private static string AssetPath
	{
		get
		{
			return "Assets/Resources/" + FileName + ".asset";
		}
	}
#endif

	private static string ResourcePath
	{
		get
		{
			return FileName;
		}
	}

	public static T Instance
	{
		get
		{
			if (cachedInstance == null)
			{
				cachedInstance = Resources.Load(ResourcePath) as T;
				//				cachedInstance = Resources.LoadAsync(ResourcePath).asset as T;
			}
#if UNITY_EDITOR
			if (cachedInstance == null)
			{
				cachedInstance = CreateAndSave();
			}
#endif
			if (cachedInstance == null)
			{
				cachedInstance = ScriptableObject.CreateInstance<T>();
				cachedInstance.OnCreate();
			}

			return cachedInstance;
		}
	}

	private static T cachedInstance;

#if UNITY_EDITOR
	protected static T CreateAndSave()
	{
		T instance = ScriptableObject.CreateInstance<T>();
		instance.OnCreate();
		//Saving during Awake() will crash Unity, delay saving until next editor frame
		if (EditorApplication.isPlayingOrWillChangePlaymode)
		{
			EditorApplication.delayCall += () => SaveAsset(instance);
		}
		else
		{
			SaveAsset(instance);
		}
		return instance;
	}

	private static void SaveAsset(T obj)
	{
		string dirName = Path.GetDirectoryName(AssetPath);
		if (!Directory.Exists(dirName))
		{
			Directory.CreateDirectory(dirName);
		}
		AssetDatabase.CreateAsset(obj, AssetPath);
		AssetDatabase.SaveAssets();
	}
#endif

	protected virtual void OnCreate()
	{
		// Do setup particular to your class here
	}
}
