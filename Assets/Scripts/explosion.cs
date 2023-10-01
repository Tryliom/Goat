using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    [SerializeField] private float _lifeTime;

    private float _elapsedTime;
    
    // Start is called before the first frame update
    private void OnEnable()
    {
        var partilces = GetComponentsInChildren<ParticleSystem>();

        foreach (var p in partilces)
        {
            p.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
