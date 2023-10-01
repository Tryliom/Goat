using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [SerializeField] private List<BonusBase> _bonus;

    [SerializeField] private float _bonusSpawnFrequency;

    private float _elapsedTime;

    private bool _isAnObjectInGame = false;
    
    private float xMin;
    private float xMax;
    private float zMin;
    private float zMax;
    
    // Start is called before the first frame update
    void Start()
    {
        xMin = -transform.localScale.x * 3;
        xMax = transform.localScale.x * 3;
        zMin = -transform.localScale.z * 3;
        zMax = transform.localScale.z * 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isAnObjectInGame)
        {
            _elapsedTime += Time.deltaTime;
        }

        if (_elapsedTime >= _bonusSpawnFrequency)
        {
            int rndIdx = Random.Range(0, _bonus.Count);

            var rndPos = new Vector3(Random.Range(xMin, xMax), 1f, Random.Range(zMin, zMax));
            
            var bonus = Instantiate(_bonus[rndIdx].gameObject, rndPos, Quaternion.identity);

            bonus.GetComponent<BonusBase>().OnBonusDestroy += UpdateObjectInGame;

            _elapsedTime = 0f;

            _isAnObjectInGame = true;
        }
    }

    private void UpdateObjectInGame(BonusBase bonus)
    {
        _isAnObjectInGame =  false;
        
        Destroy(bonus.gameObject);
    }
}
