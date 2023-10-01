using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _controlsMenu;
    [SerializeField] private GameObject _settingsMenu;

    [Header("Sliders")]
    [SerializeField] private Slider _sliderGlobalAudio;
    [SerializeField] private Slider _sliderMusicAudio;
    [SerializeField] private Slider _sliderSoundAudio;
    
    [Header("Audio")]
    [SerializeField] private AudioData _audioData;
    [SerializeField] private MusicPlayer _musicPlayer;
    [SerializeField] private SoundPlayer _soundPlayer;
    
    [Header("Transition")]
    [SerializeField] private TransitionScreen _transitionScreen;
    
    private bool _disableInput;

    private void OnEnable()
    {
        _disableInput = false;
        
        _sliderGlobalAudio.value = _audioData.GlobalVolume;
        _sliderMusicAudio.value = _audioData.MusicVolume;
        _sliderSoundAudio.value = _audioData.SoundVolume;
        
        _musicPlayer.Pause(0.5f);
        
        Time.timeScale = 0f;
        
        DisableAll();
        _menu.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToGame();
        }
    }

    public void ToGame()
    {
        Debug.Log("Back to game");
        if (_disableInput) return;
        
        _disableInput = true;
        Time.timeScale = 1f;
        _musicPlayer.Resume(0.5f);
        gameObject.SetActive(false);
    }
    
    public void ToMenu()
    {
        if (_disableInput) return;
        
        DisableAll();
        _menu.SetActive(true);
        
        _soundPlayer.PlaySound(SoundType.ButtonClick);
    }
    
    public void ToSettings()
    {
        if (_disableInput) return;
        
        DisableAll();
        _settingsMenu.SetActive(true);
        
        _soundPlayer.PlaySound(SoundType.ButtonClick);
    }
    
    public void ToControls()
    {
        if (_disableInput) return;
        
        DisableAll();
        _controlsMenu.SetActive(true);
        
        _soundPlayer.PlaySound(SoundType.ButtonClick);
    }
    
    public void ToMainMenu()
    {
        if (_disableInput) return;
        
        _disableInput = true;
        
        _soundPlayer.PlaySound(SoundType.ButtonClick);
        _transitionScreen.FadeIn(0.5f);
        _musicPlayer.Pause(0.5f);
        
        Invoke(nameof(LoadMainMenu), 1f);
    }
    
    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void DisableAll()
    {
        _menu.SetActive(false);
        _controlsMenu.SetActive(false);
        _settingsMenu.SetActive(false);
    }
    
    public void OnGlobalAudioChange()
    {
        _audioData.GlobalVolume = _sliderGlobalAudio.value;
    }
    
    public void OnMusicAudioChange()
    {
        _audioData.MusicVolume = _sliderMusicAudio.value;
    }
    
    public void OnSoundAudioChange()
    {
        _audioData.SoundVolume = _sliderSoundAudio.value;
    }
}