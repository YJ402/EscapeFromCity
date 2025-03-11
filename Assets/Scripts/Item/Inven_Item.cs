using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable] // �׽��� ������ ����ȭ
public class Inven_Item
{
    public ItemData itemData;
    private int itemCount = 1;
 
    public int ItemCount
    {
        get { return itemCount; }
        set
        {
            if (value == 0)
            {
                GameManager.Instance.player.inventory.RemoveItem(this);
            }
            else { itemCount = value; }
        }
    }
    public bool isEquiped = false;

    public void DestroyInvenItem()
    {

    }

    public void Use()
    {
        foreach (ItemData_Consumable stat in itemData.ItemData_Consumables)
        {
            switch (stat.consumableType)
            {
                case ConsumableType.Health:
                    GameManager.Instance.player.condition.ChangeHealth(stat.value);
                    break;
                case ConsumableType.Hunger:
                    GameManager.Instance.player.condition.ChangeHunger(stat.value);
                    break;
            }
        }
        ItemCount--;
        GameManager.Instance.UI.inventoryUI.UpdateUI();
    }
    public void Equip()
    {
        Debug.Log($"{itemData.item_name}�� ������");
        isEquiped = true;
        GameManager.Instance.UI.inventoryUI.UpdateUI();
    }

    public void UnEquip()
    {
        Debug.Log($"{itemData.item_name}�� ����������");
        isEquiped = false;
        GameManager.Instance.UI.inventoryUI.UpdateUI();
    }

    public void Throw()
    {
        Debug.Log($"{itemData.item_name}�� ����������");
        // ������ ������
            //������ 1�� ����
        ItemCount--;
        GameManager.Instance.UI.inventoryUI.UpdateUI();
            //dropItem �����ؼ� �տ� ������.
    }
}

//public class Inven_ConsumableItem : Inven_Item
//{
//}

//public class Inven_EquipableItem : Inven_Item
//{
//}

