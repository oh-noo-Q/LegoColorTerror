using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource soundSource;
    public AudioSource noise;
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
        //EventDispatcher.Instance.RegisterListener(EventID.OnSoundChangeValue, OnSoundChangeValue);
        //EventDispatcher.Instance.RegisterListener(EventID.OnMusicChangeValue, OnMusicChangeValue);

        DontDestroyOnLoad(gameObject);
    }

    private void OnSoundChangeValue(object obj)
    {
        if (this == null) return;
        musicSource.enabled = (bool)obj;
        soundSource.enabled = (bool)obj;
        noise.enabled = (bool)obj;
    }

    private void OnMusicChangeValue(object obj)
    {
        if (this == null) return;
        musicSource.enabled = (bool)obj;
        soundSource.enabled = (bool)obj;
        noise.enabled = (bool)obj;
        //musicSource.volume = 0.4f * MusicVolume.Value;
    }

    void OnEnable()
    {
        if (musicSource == null) return;
        //musicSource.enabled = PlayerPrefsManager.Sound;
    }

    public void PlayMusic(SoundType type, bool looping = true)
    {
        AudioClip clip = SoundSourceManager.Instance.GetSoundWithType(type);
        musicSource.volume = 0.8f;
        PlayMusic(clip, looping);
    }

    public void PlayMusic(AudioClip clip, bool looping = true)
    {
        if (musicSource == null) return;
        musicSource.clip = clip;
        musicSource.loop = looping;
        musicSource.Play();
    }

    public void PlayNoise(SoundType type, bool looping = true)
    {
        AudioClip clip = SoundSourceManager.Instance.GetSoundWithType(type);
        PlayNoise(clip, looping);
    }
    public void PlayNoise(AudioClip clip, bool looping = true)
    {
        if (noise == null) return;
        noise.clip = clip;
        noise.loop = looping;
        noise.Play();
    }

    public void StopNoise()
    {
        noise.Stop();
    }

    public void PlaySingle(AudioClip clip, float volume = 1f, bool pauseMusic = false)
    {
        //if (!PlayerPrefsManager.Sound) return;
        soundSource.pitch = Random.Range(0.98f, 1.02f);
        soundSource.PlayOneShot(clip, volume);
        if (pauseMusic) StartCoroutine(PauseMusic(clip.length));
    }

    public void PlaySingle(SoundType type, float volume = 1f, bool pauseMusic = false)
    {
        AudioClip clip = SoundSourceManager.Instance.GetSoundWithType(type);
        PlaySingle(clip, volume, pauseMusic);

    }

    public void PlaySingleUI(SoundType type)
    {
        AudioClip clip = SoundSourceManager.Instance.GetSoundWithType(type);
        PlaySingleUI(clip);

    }

    public void PlaySingleUI(AudioClip clip)
    {
        //if (PlayerPrefsManager.Sound) return;
        soundSource.PlayOneShot(clip, 1);
    }

    public void PlaySingleWithDelay(SoundType type,
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

    public void PlayDefaultSound(int type)
    {
        PlaySingle((SoundType)type);
    }

    public void PlayerSingleLazeDelay(SoundType type, float volume = 1f, float delayTime = 0, GameObject enemy = null, bool pauseMusic = false)
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
