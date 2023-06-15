using UnityEngine;
using Util;

public class SoundPool
{
    private ObjectPool<Sound> _pool;
    public Sound GetSoundFromPool(SoundID ID)
    {
        Sound sound = _pool.Get();
        AudioClip clip =Managers.Instance.SoundManager.Sounds[(int)ID];
        sound.Clip = clip;

        sound.gameObject.SetActive(false);
        sound.gameObject.SetActive(true);

        return sound;
    }
    public void Init() => _pool = new ObjectPool<Sound>(CreateSound, OnGetSoundFromPool, OnReleaseSoundToPool, OnDestroySound);
    public void Release(Sound sound) => _pool.Release(sound);
    private Sound CreateSound()
    {
        GameObject gameObject = new();
        gameObject.AddComponent<AudioSource>();
        Sound sound = gameObject.AddComponent<Sound>();

        return sound;
    }
    private void OnGetSoundFromPool(Sound sound) => sound.gameObject.SetActive(true);
    private void OnReleaseSoundToPool(Sound sound) => sound.gameObject.SetActive(false);
    private void OnDestroySound(Sound sound) => Object.Destroy(sound.gameObject);
}