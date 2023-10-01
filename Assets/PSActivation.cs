using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSActivation : MonoBehaviour
{
    private ParticleSystem _walkingPS;
    private InputWrapper _input;

    void Start()
    {
        _input = FindObjectOfType<Player>().GetComponent<InputWrapper>();
        _walkingPS = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.move.x > 0.3f || _input.move.y > 0.3f || _input.move.y < -0.3f || _input.move.x < -0.3f)
        {
            if (!_walkingPS.isPlaying)
            {
                _walkingPS.Play();
            }
        } 
    }
}
