using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public class SoundManager : MonoBehaviour
{
    public SoundSourceDictionary soundSourceDict;
    public AudioSource musicSource;
    public AudioSource soundSource;
    //public AudioSourcePoolSO OneShotPool = null;
    //public AudioSourcePoolSO UIOneShotPool = null;


    public static SoundManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        //musicSource.enabled = PlayerPrefsManager.Sound;
        //soundSource.enabled = PlayerPrefsManager.Sound;
        //noise.enabled = PlayerPrefsManager.Sound;
        EventDispatcher.Instance.RegisterListener(EventID.OnSoundChangeValue, OnSoundChangeValue);
        EventDispatcher.Instance.RegisterListener(EventID.OnMusicChangeValue, OnMusicChangeValue);

        DontDestroyOnLoad(gameObject);
    }

    private void OnSoundChangeValue(object obj)
    {
        if (this == null) return;
        foreach(var item in soundSourceDict)
        {
            if(item.Key != SoundType.Music)
            {
                item.Value.volume = (float)obj;
            }
        }
    }

    private void OnMusicChangeValue(object obj)
    {
        if (this == null) return;
        soundSourceDict[SoundType.Music].volume = (float)obj;
    }

    void OnEnable()
    {
        if (musicSource == null) return;
        //musicSource.enabled = PlayerPrefsManager.Sound;
    }

    public void PlayMusic(SoundName type, bool looping = true)
    {
        AudioClip clip = SoundSourceManager.Instance.GetSoundWithType(type);
        PlayMusic(clip, looping);
        soundSourceDict[SoundType.Music].volume = PlayerPrefsManager.Music ? 1f : 0f;
    }

    public void PlayMusic(AudioClip clip, bool looping = true)
    {
        if (soundSourceDict[SoundType.Music] == null) return;
        soundSourceDict[SoundType.Music].clip = clip;
        soundSourceDict[SoundType.Music].loop = looping;
        soundSourceDict[SoundType.Music].volume = 0.8f;
        soundSourceDict[SoundType.Music].Play();
    }

    public void PlaySingle(SoundType typeSound, AudioClip clip, float volume = 1f, bool pauseMusic = false)
    {
        if (!PlayerPrefsManager.Sound) return;
        if (soundSourceDict[typeSound] == null)
        {
            GameObject audioGO = new GameObject(soundSourceDict[typeSound].ToString());
            audioGO.transform.parent = transform;
            AudioSource newAudioSoucre = audioGO.AddComponent<AudioSource>();
            soundSourceDict[typeSound] = newAudioSoucre;
        }
        soundSourceDict[typeSound].pitch = UnityEngine.Random.Range(0.98f, 1.02f);
        soundSourceDict[typeSound].PlayOneShot(clip, volume);

        if (pauseMusic) StartCoroutine(PauseMusic(clip.length));
    }

    public void PlaySingle(SoundType typeSound, SoundName name, float volume = 1f, bool pauseMusic = false)
    {
        AudioClip clip = SoundSourceManager.Instance.GetSoundWithType(name);
        PlaySingle(typeSound, clip, volume, pauseMusic);

    }

    public void PlaySingleWithDelay(SoundName type,
        float volume = 1f,
        float delayTime = 0,
        bool pauseMusic = false)
    {
        AudioClip clip = SoundSourceManager.Instance.GetSoundWithType(type);
        PlaySingleWithDelay(clip,
                volume,
                delayTime,
                pauseMusic);

    }

    public void PlaySingleWithDelay(AudioClip clip,
        float volume = 1f,
        float delayTime = 0,
        bool pauseMusic = false)
    {
        StartCoroutine(PlaySingleDelay(clip, volume, delayTime, pauseMusic));
    }

    IEnumerator PlaySingleDelay(AudioClip clip, float volume = 1f, float delayTime = 0, bool pauseMusic = false)
    {
        yield return new WaitForSeconds(delayTime);

        soundSource.PlayOneShot(clip, 1);

        if (pauseMusic) StartCoroutine(PauseMusic(clip.length));
    }

    IEnumerator PauseMusic(float delay)
    {
        if (musicSource == null) yield break;
        musicSource.volume = 0f;
        yield return new WaitForSecondsRealtime(delay);
        musicSource.volume = 1;
    }

    public void PlayComboSound(int number)
    {
        if (number > 15) number = 15;
        Debug.Log($"Instrumental_Combo{number}");
        if (Enum.TryParse($"Instrumental_Combo{number}", out SoundName comboType)) 
        {
            PlaySingle(SoundType.ComboSound, comboType);
        }
    }

    public void PlayDieSound(int number)
    {
        if (Enum.TryParse($"EnemyDie{number}", out SoundName dieSound))
        {
            PlaySingle(SoundType.DieSound, dieSound);
        }
    }

    public void PlayerSingleLazeDelay(SoundName type, float volume = 1f, float delayTime = 0, GameObject enemy = null, bool pauseMusic = false)
    {
        AudioClip clip = SoundSourceManager.Instance.GetSoundWithType(type);
        PlayerSingleLazeDelay(clip,
                volume,
                delayTime,
                enemy,
                pauseMusic);

    }

    public void PlayerSingleLazeDelay(AudioClip clip, float volume = 1f, float delayTime = 0, GameObject enemy = null, bool pauseMusic = false)
    {
        //if (PlayerPrefsManager.Sound) return;
        StartCoroutine(
          PlayerSingleLaze(clip, volume, delayTime, enemy, pauseMusic));
    }

    IEnumerator PlayerSingleLaze(AudioClip clip, float volume = 1f, float delayTime = 0, GameObject enemy = null, bool pauseMusic = false)
    {
        yield return new WaitForSeconds(delayTime);

        if (enemy)
        {
            if (enemy.activeSelf)
            {
                soundSource.PlayOneShot(clip, 1);
                if (pauseMusic) StartCoroutine(PauseMusic(clip.length));
            }
        }
    }


    IEnumerator IEWaitAudioLoad(AudioClip clip, UnityAction action)
    {
        yield return new WaitUntil(() => clip.LoadAudioData());

        action.Invoke();
    }
}
