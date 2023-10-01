using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerMovementwithPS : MonoBehaviour
{
    [Header("Basic Movement")]
    private InputWrapper _inputs;
    private Rigidbody _rb;
    private Vector2 _movementVelocity;
    private PlayerStats _stats;
    
    [SerializeField]
    private ParticleSystem walkingPS;
    
    private SoundPlayer _soundPlayer;


    private void Start()
    {
        _inputs = GetComponent<InputWrapper>();
        _rb = GetComponent<Rigidbody>();
        _stats = GetComponent<PlayerStats>();
        _soundPlayer = FindObjectOfType<SoundPlayer>();
    }

    // Update is called once per frame
    private void Update()
    {
        _movementVelocity = _inputs.move * _stats.movementSpeed;
        PlayMovingParticuleSystem();
    }

    private void FixedUpdate()
    {
        // Apply force to the rigidbody
        _rb.velocity += new Vector3(_movementVelocity.x, 0, _movementVelocity.y);
       

        // Limit the velocity
        if (_rb.velocity.magnitude > _stats.movementSpeed)
        {
            _rb.velocity = _rb.velocity.normalized * _stats.movementSpeed;
        }
    }

    //particule System effect for the goat
    void PlayMovingParticuleSystem()
    {
        if (_inputs.move.x > 0.3f || _inputs.move.x < -0.3f)
        {
            if (!walkingPS.isPlaying)
            {
                walkingPS.Play();
                _soundPlayer.PlaySound(SoundType.GoatWalk);
                
            }
        }
        if (_inputs.move.y > 0.3f || _inputs.move.y < -0.3f)
        {
            if (!walkingPS.isPlaying)
            {
                walkingPS.Play();
                _soundPlayer.PlaySound(SoundType.GoatWalk);
            }
        }
    }
}
