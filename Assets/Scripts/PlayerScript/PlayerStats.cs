using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public float movementSpeed;
    [SerializeField] public float maxSpeed;
    [SerializeField] public int currentHealth;
    [SerializeField] public int maxHealth;

    public static event Action<PlayerStats> OnDamageTaken;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Projectile projRef = other.gameObject.GetComponent<Projectile>();

        if (projRef)
        {
            OnDamageTaken?.Invoke(this);
            currentHealth -= projRef.Damage;
            Debug.Log(currentHealth);
            Destroy(projRef.gameObject);
        }
    }
}
