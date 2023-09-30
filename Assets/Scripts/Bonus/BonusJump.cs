using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BonusJump : BonusBase
{
    private Rigidbody _rb;
    private bool canJump;
    private float timerJump;
    [SerializeField] private float _timeBetweenEffect;


    void Start()
    {
        canJump = true;
        _rb = FindObjectOfType<PlayerStats>().GetComponent<Rigidbody>();
        //bonusName.text = name;
        //renderer = GetComponent<SpriteRenderer>();
        //renderer.sprite = bonusIcon;
    }


    public override void BonusEffect()
    {
        Debug.Log("Effect");
        if (canJump)
        {
            _rb.AddForce(0, 500, 0);
            canJump = false;
            timerJump = 0;
        }

        if (!canJump)
        {
            timerJump += Time.deltaTime;
            if (timerJump >= _timeBetweenEffect)
            {
                canJump = true;
            }
        }
    }
}
