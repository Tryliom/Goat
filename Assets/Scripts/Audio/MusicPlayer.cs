using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioData _audioData;
    
    [Header("Clips")]
    [SerializeField] private AudioClip _menu;
    [SerializeField] private AudioClip _game;
    [SerializeField] private AudioClip _loose;

    private bool _isFadingOut;
    private bool _isFadingIn;
    private float _fadingSpeed;
    
    private void Update()
    {
        var maxVolume = _audioData.GetMusicVolume();
        
        if (_isFadingOut)
        {
            _audioSource.volume -= Time.unscaledDeltaTime * 2f / _fadingSpeed * maxVolume;

            if (_audioSource.volume <= 0f)
            {
                _audioSource.volume = 0f;
                _audioSource.Pause();
                _isFadingOut = false;
            }
        }
        else if (_isFadingIn)
        {
            _audioSource.volume += Time.unscaledDeltaTime * 2f / _fadingSpeed * maxVolume;

            if (_audioSource.volume >= maxVolume)
            {
                _audioSource.volume = maxVolume;
                _isFadingIn = false;
            }
        }
        else
        {
            _audioSource.volume = maxVolume;
        }
    }
    
    public void PlayLoose()
    {
        if (_loose == null) return;
        
        _audioSource.Stop();
        _audioSource.clip = _loose;
        _audioSource.Play();
        _audioSource.loop = false;
    }

    public void PlayGame()
    {
        if (_game == null) return;
        
        _audioSource.Stop();
        _audioSource.clip = _game;
        _audioSource.Play();
        _audioSource.loop = true;
    }
    
    public void PlayMenu()
    {
        if (_menu == null) return;
        
        _audioSource.Stop();
        _audioSource.clip = _menu;
        _audioSource.Play();
        _audioSource.loop = true;
    }

    public void Pause(float fadingSpeed = 1f)
    {
        _isFadingOut = true;
        _isFadingIn = false;
        _fadingSpeed = fadingSpeed;
    }
    
    public void Resume(float fadingSpeed = 1f)
    {
        _isFadingIn = true;
        _isFadingOut = false;
        _fadingSpeed = fadingSpeed;
        
        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }
}