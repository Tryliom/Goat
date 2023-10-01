using UnityEngine;

public class GrassBonus : BonusBase
{
    [SerializeField] private int _bonusScore = 125;
    [SerializeField] private float _bonusScale = 0.1f;
    [SerializeField] private float _maxScale = 2f;
    
    private PlayerStats _player;
    
    private void Start()
    {
        _player = FindObjectOfType<PlayerStats>();
    }
    
    public override void BonusEffect()
    {
        _player.transform.localScale += Vector3.one * _bonusScale;
        _player.Score += _bonusScore;
        
        if (_player.transform.localScale.x > _maxScale)
        {
            _player.transform.localScale = Vector3.one * _maxScale;
        }
    }
}