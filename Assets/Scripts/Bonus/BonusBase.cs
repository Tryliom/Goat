using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class BonusBase : MonoBehaviour
{
    public float bonusEffectDuration;

    //public float bonusCollectableTimeMax;

    private MeshRenderer renderer;
    private CapsuleCollider _capsuleCol;

    //private float _timerDurationBonus = 0;
    private float _timerDurationBonusEffect = 0;
    private bool _isCollected = false;

    [SerializeField] private float _rotationSpeed = 30f;
    
    private float _initialYPosition;
   
    public event Action<BonusBase> OnBonusDestroy;

    public abstract void BonusEffect();

    protected virtual void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        _capsuleCol = GetComponent<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            BonusEffect();
            renderer.enabled = false;
            _capsuleCol.enabled = false;
            _isCollected = true;
        }
    }
    
    protected virtual void Update()
    {
        transform.Rotate(Vector3.up * (_rotationSpeed * Time.deltaTime));

        // Move the object up and down using a sine wave
        if (!_isCollected)
        {
            float yOffset = Mathf.Cos(Time.time) * 0.5f; // Adjust the amplitude as needed
            transform.position = new Vector3(transform.position.x, _initialYPosition + yOffset, transform.position.z);
        }

        if (_isCollected)
        {
            float yOffset = Mathf.Cos(Time.time) * 0.5f; // Adjust the amplitude as needed
            transform.position = new Vector3(transform.position.x, _initialYPosition + yOffset, transform.position.z);

            // Handle the duration of the bonus effect
            if (Time.time >= _timerDurationBonusEffect + bonusEffectDuration)
            {
                OnBonusDestroy?.Invoke(this);
            }
        }
    }
}

// using System;
// using System.Collections;
// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;

// public abstract class BonusBase : MonoBehaviour
// {
//     public float bonusEffectDuration;
//     private MeshRenderer renderer;
//     private CapsuleCollider _capsuleCol;
//     private bool _isCollected = false;
//     private float _initialYPosition;
//     private float _rotationSpeed = 30f; // Adjust the rotation speed as needed
//
//     public event Action<BonusBase> OnBonusDestroy;
//
//     public abstract void BonusEffect();
//
//     protected virtual void Awake()
//     {
//         renderer = GetComponent<MeshRenderer>();
//         _capsuleCol = GetComponent<CapsuleCollider>();
//         _initialYPosition = transform.position.y;
//     }
//
//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.gameObject.GetComponent<Player>())
//         {
//             BonusEffect();
//             renderer.enabled = false;
//             _capsuleCol.enabled = false;
//             _isCollected = true;
//         }
//     }
//
//     protected virtual void Update()
//     {
//         // Rotate the object
//         transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
//
//         // Move the object up and down using a sine wave
//         if (!_isCollected)
//         {
//             float yOffset = Mathf.Sin(Time.time) * 0.5f; // Adjust the amplitude as needed
//             transform.position = new Vector3(transform.position.x, _initialYPosition + yOffset, transform.position.z);
//         }
//
//         if (_isCollected)
//         {
//             float yOffset = Mathf.Sin(Time.time) * 0.5f; // Adjust the amplitude as needed
//             transform.position = new Vector3(transform.position.x, _initialYPosition + yOffset, transform.position.z);
//
//             // Handle the duration of the bonus effect
//             if (Time.time >= _timerDurationBonusEffect + bonusEffectDuration)
//             {
//                 OnBonusDestroy?.Invoke(this);
//             }
//         }
//     }
// }
