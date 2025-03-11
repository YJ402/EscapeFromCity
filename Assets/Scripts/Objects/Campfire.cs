using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Campfire : MonoBehaviour, IInteractable
{
    public Wrapping GetNeedThing()
    {
        Wrapping n = new Wrapping();
        return n;
    }

    public (string name, string desc) GetPromptInfo()
    {
        return ("모닥불", "무언가를 구워도 될법한 화력의 모닥불이다.");
    }

    public GameObject GetRoot()
    {
        throw new System.NotImplementedException();
    }

    public void SubscribeMethod()
    {
        GameManager.Instance.player.controller.interactAction = Cook; // 인벤토리에 있는 굽기 가능한 애들 싹다 구워버렷.
    }

    public void Cook(Wrapping n)
    {
        Dictionary<ItemData, int> CookList = new Dictionary<ItemData, int>();
        PlayerInventory inventory = GameManager.Instance.player.inventory;
        //요리할 것 준비
        foreach (Inven_Item i in inventory.items)
        {
            if (i.itemData.isCookable)
            {
                if (CookList.ContainsKey(i.itemData))
                {
                    CookList[i.itemData] += i.ItemCount;
                }
                else
                {
                    CookList.Add(i.itemData, i.ItemCount);
                }
            }
        }

        //요리 시작
        foreach (var pair in CookList)
        {
            Wrapping cookedThingWrapped = new(pair.Key.CookedThing);
            for (int i = 0; i < pair.Value; i++)
            {
                GameManager.Instance.player.inventory.AddItem(cookedThingWrapped);
            }
        }

        //요리 재료 폐기. (요리 재료인 itemData인 inven_Item을 전부 삭제하는 원리.)
        foreach (var pair in CookList)
        {
            List<Inven_Item> itemsToRemove = new();

            foreach (var invenItem in inventory.items)
            {
                if (invenItem.itemData == pair.Key)
                {
                    itemsToRemove.Add(invenItem);
                }
            }
            foreach (var invenItem in itemsToRemove)
            {
                inventory.RemoveItem(invenItem);
            }
        }

        GameManager.Instance.UI.inventoryUI.UpdateUI();
    }
}
