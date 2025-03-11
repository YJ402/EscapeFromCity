using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public PlayerInventory inventoryData;
    public Coroutine Co_ItemInfo;
    GameObject[] gameObjects;
    SlotUI[] slotUIs;

    private void Start()
    {
        inventoryData = GameManager.Instance.player.inventory;

        //gameObjects = new GameObject[transform.childCount];
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    gameObjects[i] = transform.GetChild(i).gameObject;
        //}

        //foreach (GameObject go in gameObjects)
        //{
        //    go.AddComponent<SlotUI>();
        //}

        slotUIs = GetComponentsInChildren<SlotUI>();
        for (int i = 0; i < slotUIs.Length; i++)
        {
            slotUIs[i].Init(this, Co_ItemInfo, i);
        }

        gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slotUIs.Length; i++)
        {
            //if (inventoryData.items[i] != null)
            if (i < inventoryData.items.Count)
            {
                slotUIs[i].image.gameObject.SetActive(true);

                slotUIs[i].image.sprite = inventoryData.items[i].itemData.Image;
                if (inventoryData.items[i].itemData.canStack)
                {
                slotUIs[i].TMP_Count.gameObject.SetActive(true);
                    slotUIs[i].TMP_Count.text = inventoryData.items[i].itemCount.ToString();
                }

            }
            else
            {
                slotUIs[i].TMP_Count.gameObject.SetActive(false);
                slotUIs[i].image.gameObject.SetActive(false);
            }
        }

    }
}
