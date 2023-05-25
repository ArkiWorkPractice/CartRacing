using System.Collections;
using System.Collections.Generic;
using Obstacles.Abstract;
using UnityEngine;

public class Car : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;
    public void MakeDamage(int value)
    {
        health -= value;
    }
}
