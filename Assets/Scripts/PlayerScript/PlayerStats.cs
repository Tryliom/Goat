using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public float movementSpeed;
    [SerializeField] public float maxSpeed;
    [SerializeField] public int currentHealth;
    [SerializeField] public int maxHealth;

    [SerializeField] private int _healthRegenAmount;
    [SerializeField] private int _healthRegenFrequency;

    private float _healthTimer;
    
    public static event Action<PlayerStats> OnHealthChanging;

    public float Timer = 0f;
    public float Score = 0;
    private float scoreTimer = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        scoreTimer += Time.deltaTime;
        timeText.text = "Time: " + Timer.ToString("F0");
        if (scoreTimer >= 1.2f)
        {
            Score += 100;
            scoreTimer = 0;
        }
        scoreText.text = "Score: " + Score.ToString("000000");

        if (currentHealth < maxHealth)
        {
            _healthTimer += Time.deltaTime;
        }
        
        if (_healthTimer >= _healthRegenFrequency && currentHealth < maxHealth)
        {
            UpdateHealth(_healthRegenAmount);
            _healthTimer = 0f;
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            OnHealthChanging?.Invoke(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Projectile projRef = other.gameObject.GetComponent<Projectile>();

        if (projRef)
        {
            UpdateHealth(-projRef.Damage);
            Destroy(projRef.gameObject);
        }
    }

    public void UpdateHealth(int healthChange)
    {
        currentHealth += healthChange;
        OnHealthChanging?.Invoke(this);
    }
}