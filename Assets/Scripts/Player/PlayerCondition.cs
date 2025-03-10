using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    [Header("기본 수치")]
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health;
    public float Health { get { return health; } set { health = value; } }
    [SerializeField] private float healthCoefficient = 0.3f;

    [SerializeField] private float maxHunger = 100;
    [SerializeField] private float hunger;
    public float Hunger { get { return hunger; } set { hunger = value; } }
    [SerializeField] private float hungerCoefficient = 1f;

    [SerializeField] private int maxBulletCount = 40;
    [SerializeField] private int bulletCount;
    public int BulletCount { get { return bulletCount; } set { bulletCount = value; } }

    private void Start()
    {
        hunger = maxHunger;
        health = maxHealth;
        bulletCount = 0;
    }
    private void Update()
    {
        hunger -= Time.deltaTime * hungerCoefficient;

        if(hunger < 0)
            health -= Time.deltaTime * healthCoefficient;
    }

    public float GetHealthGage()
    {
        return health / maxHealth;
    }

    public float GetHungerGage()
    {
        return hunger / maxHunger;
    }

    public int GetBulletCount()
    {
        return bulletCount;
    }
}
