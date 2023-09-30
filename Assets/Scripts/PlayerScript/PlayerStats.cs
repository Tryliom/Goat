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

    public static event Action<PlayerStats> OnDamageTaken;

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
        timeText.text = "Time: " + Timer.ToString("F2");
        if (scoreTimer >= 1.2f)
        {
            Score += 100;
            scoreTimer = 0;
        }
        scoreText.text = "Score: " + Score.ToString("000000");
    }

    private void OnTriggerEnter(Collider other)
    {
        Projectile projRef = other.gameObject.GetComponent<Projectile>();

        if (projRef)
        {
            OnDamageTaken?.Invoke(this);
            currentHealth -= projRef.Damage;
            Destroy(projRef.gameObject);
        }
    }
}
