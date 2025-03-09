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
    private Slider healthGage;
    private Slider hungerGage;
    private int bulletCount;
    private Image[] bulletArray;
    private int curBullet;

    private void Awake()
    {

    }
    private void Start()
    {
        Bullets = transform.GetChild(0);
        for (int i = 0; i < 40; i++)
        {
            bulletArray = Bullets.GetComponentsInChildren<Image>();
        }
        Health = transform.GetChild(1);
        Hunger = transform.GetChild(1);

        player = Player.Instance.condition;
    }

    private void Update()
    {
        healthGage.value = player.GetHealthGage();
        hungerGage.value = player.GetHungerGage();
        bulletImageEnable(player.GetBulletCount());
    }

    void bulletImageEnable(int bulletNum)
    {
        if (curBullet == bulletNum) return;


        if (curBullet > bulletNum)
        {
            for (int i = 0; i < curBullet - bulletNum; i++)
            {
                bulletArray[curBullet - i].enabled = false;
            }
        }
        else
        {
            for (int i = 0; i < bulletNum - curBullet; i++)
            {
                bulletArray[curBullet + i].enabled = true;
            }

        }
    }
}
