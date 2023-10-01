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
    [SerializeField] private GameObject _shieldObject;
    [SerializeField] private int _healthRegenAmount;
    [SerializeField] private int _healthRegenFrequency;

    [SerializeField] private ParticleSystem _onHitParticleSystem;

    [SerializeField] private SkinnedMeshRenderer _goatMesh;
    [SerializeField] private SkinnedMeshRenderer _invicibleMesh;
    
    private RopeController _ropeCtrRef;
    
    private float _healthTimer;

    public static event Action<PlayerStats> OnHealthChanging;

    public float Timer;
    public float Score;
    private float _scoreTimer;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;

    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private TextMeshProUGUI gameOverscoreText;
    [SerializeField] private TextMeshProUGUI gameOvertimeText;

    public int ShieldCount;
    public float InvincibilityDuration;
    public float ExtendRopeDuration;
    
    private SoundPlayer _soundPlayer;

    private void Start()
    {
        currentHealth = maxHealth;
        _ropeCtrRef = FindObjectOfType<RopeController>();

        InvincibilityBonus.OnInvicibility += ChangeMat;
        _goatMesh.enabled = true;
        _invicibleMesh.enabled = false;
        _soundPlayer = FindObjectOfType<SoundPlayer>();
    }

    void ChangeMat()
    {
        if (_goatMesh != null)
        {
            _goatMesh.enabled = false;
        }

        if (_invicibleMesh != null)
        {
            _invicibleMesh.enabled = true;
        }
        
    }
    
    // Update is called once per frame
    private void Update()
    {
        Timer += Time.deltaTime;
        _scoreTimer += Time.deltaTime;
        TimeSpan tempsSpan = TimeSpan.FromSeconds(Timer);
        if (_scoreTimer >= 1.2f)
        {
            Score += 100;
            _scoreTimer = 0;
        }

        if (currentHealth <= 0)
        {
            gameOverMenu.SetActive(true);
            Time.timeScale = 0f;
            gameOvertimeText.text = "Time: " + string.Format("{0:00}:{1:00}", tempsSpan.Minutes, tempsSpan.Seconds);
            gameOverscoreText.text = "Score: " + Score.ToString("000000");
        }

        timeText.text = "Time: " + string.Format("{0:00}:{1:00}", tempsSpan.Minutes, tempsSpan.Seconds);
        scoreText.text = "Score:\n" + Score.ToString("000000");

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

        if (InvincibilityDuration > 0)
        {
            InvincibilityDuration -= Time.deltaTime;

            if (InvincibilityDuration <= 0)
            {
                InvincibilityDuration = 0;

                _goatMesh.enabled = true;
                _invicibleMesh.enabled = false;
            }
        }
        
        if (ExtendRopeDuration > 0)
        {
            ExtendRopeDuration -= Time.deltaTime;

            if (ExtendRopeDuration <= 0)
            {
                ExtendRopeDuration = 0;
                _ropeCtrRef.CurrentMaxLength /= ExtendRopeBonus.RopeExtensionFactor;
                _ropeCtrRef.MustApplyRopeForce = true;
            }
        }

        if (ShieldCount > 0 && _shieldObject.activeSelf == false)
        {
            _shieldObject.SetActive(true);
        }

        if (ShieldCount <= 0 && _shieldObject.activeSelf == true)
        {
            _shieldObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Projectile projRef = other.gameObject.GetComponent<Projectile>();

        if (projRef)
        {
            if (InvincibilityDuration > 0) { }
            else if (ShieldCount > 0)
            {
                ShieldCount--;
            }
            else
            {
                _soundPlayer.PlaySound(SoundType.GoatHurt);
                UpdateHealth(-projRef.Damage);
                _onHitParticleSystem.Play();
            }

            Destroy(projRef.gameObject);
        }
    }

    public void UpdateHealth(int healthChange)
    {
        currentHealth += healthChange;
        OnHealthChanging?.Invoke(this);
    }
}