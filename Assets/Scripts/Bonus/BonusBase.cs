using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class BonusBase : MonoBehaviour
{
    [Header("Bonus Info")]
    public string name;
    public float bonusEffectDuration;

    public float bonusCollectableTimeMax;
    public float _radiusDetectionSphere;
    public LayerMask playerLayerMask;
    //public Sprite bonusIcon;
    //public TextMeshProUGUI bonusName;
    
    public MeshRenderer renderer;

    private float _timerDurationBonus = 0;
    private float _timerDurationBonusEffect = 0;
    private bool _isCollected = false;

    public event Action<BonusBase> OnBonusDestroy;

    public abstract void BonusEffect();
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            BonusEffect();
            renderer.enabled = false;
            _isCollected = true;
        }
    }
    
    protected virtual void Update()
    {
        Debug.Log(_timerDurationBonus + " " + _timerDurationBonusEffect);
        
        if (!_isCollected)
        {
            _timerDurationBonus += Time.deltaTime;
        }
        
        if (_timerDurationBonus >= bonusCollectableTimeMax)
        {
            OnBonusDestroy?.Invoke(this);
        }
        
        if (_isCollected)
        {
            _timerDurationBonusEffect += Time.deltaTime;
            if (_timerDurationBonusEffect >= bonusEffectDuration)
            {
                Debug.Log("Invoke destroy");
                OnBonusDestroy?.Invoke(this);
            }
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.gameObject.transform.position, _radiusDetectionSphere);
    }
}
