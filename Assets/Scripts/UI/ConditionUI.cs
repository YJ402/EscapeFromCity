using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionUI : MonoBehaviour//, Updateable
{
    private PlayerCondition player;
    [Header("트랜스폼")]
    private Transform Bullets;
    private Transform Health;
    private Transform Hunger;

    [Header("이미지")]
    [SerializeField] private Slider healthGage;
    [SerializeField] private Slider hungerGage;
    private int bulletCount;
    private Image[] bulletArray;
    private int curBulletCount;

    private void Start()
    {
        Bullets = transform.GetChild(0);
        for (int i = 0; i < 40; i++)
        {
            bulletArray = Bullets.GetComponentsInChildren<Image>();
        }
        curBulletCount = 40;

        Health = transform.GetChild(1);
        Hunger = transform.GetChild(1);

        player = GameManager.Instance.player.condition;
    }

    private void Update()
    {
        healthGage.value = player.GetHealthGage();
        hungerGage.value = player.GetHungerGage();
        bulletImageEnable(player.GetBulletCount());
    }

    void bulletImageEnable(int bulletCount)
    {
        if (curBulletCount == bulletCount) return;


        if (curBulletCount > bulletCount)
        {
            for (int i = bulletCount; i < curBulletCount; i++)
            {
                bulletArray[i].enabled = false;
            }
        }
        else
        {
            for (int i = curBulletCount; i < bulletCount; i++)
            {
                bulletArray[i].enabled = true;
            }

        }

        curBulletCount = bulletCount;
    }
}
