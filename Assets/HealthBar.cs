using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private PlayerStats _playerStats;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _playerStats = FindObjectOfType<PlayerStats>();
        PlayerStats.OnDamageTaken += UpdateHealthBar;
    }

    private void UpdateHealthBar(PlayerStats playerStats)
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
