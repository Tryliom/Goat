using UnityEngine;

public class GameInit : MonoBehaviour
{
    [SerializeField] private TransitionScreen _transitionScreen;
    [SerializeField] private MusicPlayer _musicPlayer;

    [SerializeField] private PauseMenu _pauseMenu;

    private void Awake()
    {
        Time.timeScale = 1f;
        _transitionScreen.FadeOut(1f);
        _musicPlayer.PlayGame();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_pauseMenu.gameObject.activeSelf)
        {
            _pauseMenu.gameObject.SetActive(true);
        }
    }
}