using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    
    [Header("Clips")]
    [SerializeField] private AudioClip _menu;
    [SerializeField] private AudioClip _game;
    [SerializeField] private AudioClip _loose;

    private bool _isFadingOut;
    private bool _isFadingIn;
    private float _fadingSpeed;
    
    private void Update()
    {
        if (_isFadingOut)
        {
            _audioSource.volume -= Time.unscaledDeltaTime * 2f / _fadingSpeed;

            if (_audioSource.volume <= 0f)
            {
                _audioSource.volume = 0f;
                _audioSource.Pause();
                _isFadingOut = false;
            }
        }
        else if (_isFadingIn)
        {
            _audioSource.volume += Time.unscaledDeltaTime * 2f / _fadingSpeed;

            if (_audioSource.volume >= 1)
            {
                _audioSource.volume = 1;
                _isFadingIn = false;
            }
        }
        else
        {
            _audioSource.volume = 1;
        }
    }
    
    public void PlayLoose()
    {
        _audioSource.Stop();
        _audioSource.clip = _loose;
        _audioSource.Play();
        _audioSource.loop = false;
    }

    public void PlayGame()
    {
        _audioSource.Stop();
        _audioSource.clip = _game;
        _audioSource.Play();
        _audioSource.loop = true;
    }
    
    public void PlayMenu()
    {
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