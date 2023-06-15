using UnityEngine;
using Util;
public enum SoundID
{
    TitleBGM,
    InGameBGM,
    GameOver,
    Start,
    Jump,
    Land,
    Defense,
    Attack,
    Hit
}

public class SoundManager : MonoBehaviour
{
    public AudioClip[] Sounds;
    private SoundPool _pool = new();
    private void Awake() => _pool.Init();
    public Sound PlaySound(SoundID ID) => _pool.GetSoundFromPool(ID);
    public void ReleaseSound(Sound sound) => _pool.Release(sound);    
}
