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
        PlayerStats.OnDamageTaken += UpdateHealthBar;
    }

    private void UpdateHealthBar(PlayerStats playerStats)
    {
        Debug.Log("DAMAGE");
        _image.fillAmount = playerStats.currentHealth / playerStats.maxHealth;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
