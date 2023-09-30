using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [SerializeField] private List<BonusBase> _bonus;

    [SerializeField] private float _bonusSpawnFrequency;

    private float _elapsedTime;

    private bool _isAnObjectInGame = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_isAnObjectInGame);
        
        if (!_isAnObjectInGame)
        {
            _elapsedTime += Time.deltaTime;
        }

        if (_elapsedTime >= _bonusSpawnFrequency)
        {
            int rndIdx = Random.Range(0, _bonus.Count);

            var bonus = Instantiate(_bonus[rndIdx].gameObject);

            bonus.GetComponent<BonusBase>().OnEffectEnd += UpdateObjectInGame;

            _elapsedTime = 0f;

            _isAnObjectInGame = true;
        }
    }

    private void UpdateObjectInGame(BonusBase bonus)
    {
        _isAnObjectInGame = false;
        
        Destroy(bonus.gameObject);
    }
}
