using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private PlayerStats _playerStats;

    [SerializeField] private Image _image;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerStats = FindObjectOfType<PlayerStats>();
        PlayerStats.OnHealthChanging += UpdateHealthBar;
    }

    private void UpdateHealthBar(PlayerStats playerStats)
    {
        _image.fillAmount = (float)playerStats.currentHealth / (float)playerStats.maxHealth;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
