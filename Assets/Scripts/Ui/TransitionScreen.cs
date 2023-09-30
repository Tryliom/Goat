using UnityEngine;
using UnityEngine.UI;

public class TransitionScreen : MonoBehaviour
{
    [SerializeField] private Image _transitionScreen;

    private float _timer;
    private bool _isFadingIn;
    
    private void Update()
    {
        if (_timer <= 0f) return;
        
        _timer -= Time.deltaTime;

        _transitionScreen.color = new Color(0f, 0f, 0f, _isFadingIn ? 1f - _timer : _timer);
        
        if (_timer <= 0f && !_isFadingIn)
        {
            gameObject.SetActive(false);
        }
    }
    
    public void FadeIn(float time = 1f)
    {
        gameObject.SetActive(true);
        
        _timer = time;
        _transitionScreen.color = new Color(0f, 0f, 0f, 0f);
        _isFadingIn = true;
    }
    
    public void FadeOut(float time = 1f)
    {
        gameObject.SetActive(true);
        
        _timer = time;
        _transitionScreen.color = new Color(0f, 0f, 0f, 1f);
        _isFadingIn = false;
    }
}