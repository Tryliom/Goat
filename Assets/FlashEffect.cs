using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FlashEffect : MonoBehaviour
{
    [SerializeField] private Image _renderer;
    [SerializeField] private float _speed = 6f;
    
    private bool isTransparent;

    private bool _mustFlash;
    // Start is called before the first frame update
    void Start()
    {
        BoomBonus.OnBoom += ActivateFlash;
    }

    // Update is called once per frame
    void Update()
    {
        if (_mustFlash)
        {
            Flash();
        }
        else
        {
            _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, 0);
        }
    }

    void ActivateFlash(BoomBonus b)
    {
        if (b.GetComponent<BoomBonus>() != null)
        {
            StartCoroutine(FlashEffectCoroutine());
        }
    }
    
    void Flash()
    {
        //StartCoroutine(nameof(FlashEffectCoroutine));

        if (isTransparent)
        {
            _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, _renderer.color.a + Time.deltaTime * _speed);
        }
        else
        {
            _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b,
                _renderer.color.a - Time.deltaTime * _speed);
        }


        if (_renderer.color.a >= 1)
        {
            isTransparent = false;
        }
        else if(_renderer.color.a <= 0)
        {
            isTransparent = true;
        }
    }

    public IEnumerator FlashEffectCoroutine()
    {
        _mustFlash = true;

        yield return new WaitForSeconds(0.35f);

        _mustFlash = false;
    }

}
