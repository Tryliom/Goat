using System;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    GameObject target;
    private void Start()
    {
        target = FindObjectOfType<Player>().gameObject;
    }
    private void Update()
    {
        transform.LookAt(target.transform);
    }
}