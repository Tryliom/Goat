using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _controlsMenu;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _creditsMenu;

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

    private void Start()
    {
        _sliderGlobalAudio.value = _audioData.GlobalVolume;
        _sliderMusicAudio.value = _audioData.MusicVolume;
        _sliderSoundAudio.value = _audioData.SoundVolume;
        
        _musicPlayer.PlayMenu();
    }

    public void ToGame()
    {
        if (_disableInput) return;
        
        _disableInput = true;
        _transitionScreen.FadeIn(0.5f);
        _musicPlayer.Pause(0.5f);
        
        Invoke(nameof(LoadGame), 1f);
    }

    private void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void ToMainMenu()
    {
        if (_disableInput) return;
        
        DisableAll();
        _mainMenu.SetActive(true);
        
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
    
    public void ToCredits()
    {
        if (_disableInput) return;
        
        DisableAll();
        _creditsMenu.SetActive(true);
        
        _soundPlayer.PlaySound(SoundType.ButtonClick);
    }
    
    public void Exit()
    {
        Application.Quit();
    }

    private void DisableAll()
    {
        _mainMenu.SetActive(false);
        _controlsMenu.SetActive(false);
        _settingsMenu.SetActive(false);
        _creditsMenu.SetActive(false);
    }
    public void OnGlobalAudioChange(float value)
    {
        _audioData.GlobalVolume = value;
    }
    
    public void OnMusicAudioChange(float value)
    {
        _audioData.MusicVolume = value;
    }
    
    public void OnSoundAudioChange(float value)
    {
        _audioData.SoundVolume = value;
    }
}
