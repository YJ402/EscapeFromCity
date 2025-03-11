using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    [Header("기본 수치")]
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health;
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            if (value > maxHealth)
            {
                health = maxHealth;
            }
            else if (value < 0)
            {
                health = 0;
            }
            else
            {
                health = value;
            }
        }
    }
    [SerializeField] private float healthCoefficient = 0.3f;


    [SerializeField] private float maxHunger = 100;
    [SerializeField] private float hunger;
    public float Hunger
    {
        get
        {
            return hunger;
        }
        set
        {
            if (value > maxHunger)
            {
                hunger = maxHunger;
            }
            else if(value < 0)
            {
                hunger = 0;
            }
            else
            {
                hunger = value;
            }
        }
    }
    [SerializeField] private float hungerCoefficient = 1f;


    [SerializeField] private int maxBulletCount = 40;
    [SerializeField] private int bulletCount;
    public int BulletCount { get { return bulletCount; } set { bulletCount = value; } }

    private void Start()
    {
        Hunger = maxHunger;
        Health = maxHealth;
        BulletCount = 0;
    }
    private void Update()
    {
        if (Hunger > 70)
        {
            Hunger -= Time.deltaTime * hungerCoefficient;
            Health += Time.deltaTime * hungerCoefficient;
        }
        else if (Hunger > 0)
        {
            Hunger -= Time.deltaTime * hungerCoefficient;
        }
        else
        {
            Health -= Time.deltaTime * healthCoefficient;
        }

    }

    public float GetHealthGage()
    {
        return Health / maxHealth;
    }

    public float GetHungerGage()
    {
        return Hunger / maxHunger;
    }

    public int GetBulletCount()
    {
        return BulletCount;
    }

    public void ChangeHealth(float value)
    {
        Health += value;
    }

    public void ChangeHunger(float value)
    {
        Hunger += value;
    }

    private void Die()
    {
        Debug.Log("플레이어 사망~");
    }
}
