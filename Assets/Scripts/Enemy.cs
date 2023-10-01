using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _minSpeed = 2f;
    [SerializeField] private float _maxSpeed = 3.5f;
    
    private Vector3 _initialPosition;
    private Vector3 _targetPosition;
    private Action<Enemy> _onReachedTarget;
    
    private bool _goBack;
    private bool _stop;
    private float _speed;
    
    private void Awake()
    {
        _speed = Random.Range(_minSpeed, _maxSpeed);
    }
    
    private void Update()
    {
        if (_goBack)
        {
            transform.position = Vector3.MoveTowards(transform.position, _initialPosition, _speed * Time.deltaTime);
            transform.LookAt(_initialPosition);
            
            if (transform.position - _initialPosition == Vector3.zero)
            {
                Destroy(gameObject);
            }
        }
        else if (!_stop)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);

            if (transform.position - _targetPosition != Vector3.zero) return;
            
            _onReachedTarget?.Invoke(this);
            _stop = true;
        }
    }
    
    public void Init(Vector3 initialPosition, Vector3 targetPosition)
    {
        _initialPosition = initialPosition;
        _targetPosition = targetPosition;
    }
    
    public void SetOnReachedTarget(Action<Enemy> onReachedTarget)
    {
        _onReachedTarget = onReachedTarget;
    }
    
    public void GoBack()
    {
        _goBack = true;
    }
}