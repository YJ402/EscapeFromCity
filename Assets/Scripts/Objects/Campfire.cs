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
        return ("��ں�", "���𰡸� ������ �ɹ��� ȭ���� ��ں��̴�.");
    }

    public GameObject GetRoot()
    {
        throw new System.NotImplementedException();
    }

    public void SubscribeMethod()
    {
        GameManager.Instance.player.controller.interactAction = Cook; // �κ��丮�� �ִ� ���� ������ �ֵ� �ϴ� ��������.
    }

    public void Cook(Wrapping n)
    {
        Dictionary<ItemData, int> CookList = new Dictionary<ItemData, int>();
        PlayerInventory inventory = GameManager.Instance.player.inventory;
        //�丮�� �� �غ�
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

        //�丮 ����
        foreach (var pair in CookList)
        {
            Wrapping cookedThingWrapped = new(pair.Key.CookedThing);
            for (int i = 0; i < pair.Value; i++)
            {
                GameManager.Instance.player.inventory.AddItem(cookedThingWrapped);
            }
        }

        //�丮 ��� ���. (�丮 ����� itemData�� inven_Item�� ���� �����ϴ� ����.)
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
