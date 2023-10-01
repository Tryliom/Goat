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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Flash();
        StartCoroutine(FlashEffectCoroutine());
    }

    void Flash()
    {
        if (isTransparent)
        {
            _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, _renderer.color.a + Time.deltaTime * _speed);
        }
        else
        {
            _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, _renderer.color.a - Time.deltaTime * _speed);
        }
        Debug.Log(_renderer.color.a);


        if (_renderer.color.a >= 1)
        {
            isTransparent = false;
        }
        else if(_renderer.color.a <= 0)
        {
            isTransparent = true;
        }
    }

    IEnumerator FlashEffectCoroutine()
    {
        if (isTransparent)
        {
            _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, _renderer.color.a + Time.deltaTime * _speed);
        }
        else
        {
            _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, _renderer.color.a - Time.deltaTime * _speed);
        }
        Debug.Log(_renderer.color.a);


        if (_renderer.color.a >= 1)
        {
            isTransparent = false;
        }
        else if (_renderer.color.a <= 0)
        {
            isTransparent = true;
        }

        yield return new WaitForSeconds(0.2f);
    }

}
