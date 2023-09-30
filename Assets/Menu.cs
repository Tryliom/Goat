using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainMenu;
    [SerializeField]
    private GameObject _controlsMenu;
    [SerializeField]
    private GameObject _settingsMenu;
    [SerializeField]
    private GameObject _creditsMenu;

    [SerializeField]
    private Slider _slider;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 1.0f);
        }
    }

    private void Start()
    {
        _slider.value = PlayerPrefs.GetFloat("Volume");
    }

    public void ToGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void ToMainMenu()
    {
        UnableAll();
        _mainMenu.SetActive(true);
    }
    public void ToSettings()
    {
        UnableAll();
        _settingsMenu.SetActive(true);
    }
    public void ToControls()
    {
        UnableAll();
        _controlsMenu.SetActive(true);
    }
    public void ToCredits()
    {
        UnableAll();
        _creditsMenu.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void UnableAll()
    {
        _mainMenu.SetActive(false);
        _controlsMenu.SetActive(false);
        _settingsMenu.SetActive(false);
        _creditsMenu.SetActive(false);
    }
    public void OnValueChanged(float value)
    {
        PlayerPrefs.SetFloat("Volume", value);
    }
}
