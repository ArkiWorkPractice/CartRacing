using System;
using System.Collections;
using System.Collections.Generic;
using Obstacles.Abstract;
using UnityEngine;
using UnityEngine.Serialization;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int damageAmount;
    [SerializeField] private bool isDestroyable;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.MakeDamage(damageAmount);

            if (isDestroyable) Destroy(gameObject);
        }
    }
}