using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioClip Clip
    {
        set
        {
            _clipTime = value.length;
            _audioSource.clip = value;
            if (_audioSource.clip == Managers.Instance.SoundManager.Sounds[(int)SoundID.InGameBGM] ||
           _audioSource.clip == Managers.Instance.SoundManager.Sounds[(int)SoundID.TitleBGM] ||
           _audioSource.clip == Managers.Instance.SoundManager.Sounds[(int)SoundID.GameOver])
            {
                _audioSource.loop = true;
                _clipTime = 987654321;
                _audioSource.volume = 0.5f;
            }
        }
    }

    private AudioSource _audioSource;
    private float _elapsedTime = 0f;
    private float _clipTime = 0f;
    private bool _isReleased = false;
    private void Awake()
    {
        _audioSource = gameObject.GetComponentAssert<AudioSource>();
    }

    private void OnEnable()
    {
        _elapsedTime = 0f;
        _isReleased = false;
        _audioSource.Play();
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_clipTime <= _elapsedTime)
        {
            _isReleased = true;
            Managers.Instance.SoundManager.ReleaseSound(this);
        }
    }
    public void Play() => _audioSource.Play();
    public void StopPlaying() => _audioSource.Stop();
    public bool IsPlaying() => _isReleased == false;
    public void SetVolume(float value) => _audioSource.volume = value;
}