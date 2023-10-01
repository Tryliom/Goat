using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class BonusBase : MonoBehaviour
{
    public float bonusEffectDuration;

    //public float bonusCollectableTimeMax;

    private MeshRenderer _renderer;
    private SphereCollider _capsuleCol;

    //private float _timerDurationBonus = 0;
    private const float _timerDurationBonusEffect = 0;
    private bool _isCollected = false;

    [SerializeField] private float _rotationSpeed = 30f;

    private const float InitialYPosition = 1.3f;
    
    private SoundPlayer _soundPlayer;

    public event Action<BonusBase> OnBonusDestroy;

    public abstract void BonusEffect();

    protected virtual void Awake()
    {
        _renderer = GetComponentInChildren<MeshRenderer>();

        _capsuleCol = GetComponent<SphereCollider>();


        _soundPlayer = FindObjectOfType<SoundPlayer>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            BonusEffect();
            _renderer.enabled = false;
            _capsuleCol.enabled = false;
            _isCollected = true;
            _soundPlayer.PlaySound(SoundType.Bonus);
        }
    }
    
    protected virtual void Update()
    {
        transform.Rotate(Vector3.up * (_rotationSpeed * Time.deltaTime));

        // Move the object up and down using a sine wave
        if (!_isCollected)
        {
            float yOffset = Mathf.Cos(Time.time) * 0.1f; // Adjust the amplitude as needed
            transform.position = new Vector3(transform.position.x, InitialYPosition + yOffset, transform.position.z);
        }

        if (_isCollected)
        {
            float yOffset = Mathf.Cos(Time.time) * 0.1f; // Adjust the amplitude as needed
            transform.position = new Vector3(transform.position.x, InitialYPosition + yOffset, transform.position.z);

            // Handle the duration of the bonus effect
            if (Time.time >= _timerDurationBonusEffect + bonusEffectDuration)
            {
                OnBonusDestroy?.Invoke(this);
            }
        }
    }
}
