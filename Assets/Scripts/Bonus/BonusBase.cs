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

    public void OnBonusCollision()
    {
        if (!_isCollected)
        {
            Collider[] detectPlayer = Physics.OverlapSphere(this.gameObject.transform.position, _radiusDetectionSphere, playerLayerMask);
            if (detectPlayer.Length != 0)
            {
                _isCollected = true;
            }
        }
    }

    public abstract void BonusEffect();

    public void DestroyBonus()
    {
        if (_timerDurationBonus >= bonusCollectableTimeMax)
        {
            //Destroy(this.gameObject);
            OnBonusDestroy?.Invoke(this);
        }
    }



    void Update()
    {
        if (!_isCollected)
        {
            OnBonusCollision();
            DestroyBonus();
        }

        
        if (_isCollected)
        {
            HideVisualsElements();
            BonusEffect();
            _timerDurationBonusEffect += Time.deltaTime;
            if (_timerDurationBonusEffect >= bonusEffectDuration)
            {
                OnBonusDestroy?.Invoke(this);
            }
        }

    }

    private void HideVisualsElements()
    {
        renderer.enabled = false;
        //bonusName.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.gameObject.transform.position, _radiusDetectionSphere);
    }
}
