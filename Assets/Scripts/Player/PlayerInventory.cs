using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;



public class PlayerInventory : MonoBehaviour
{
    public List<Inven_Item> items = new(); // ������ ���� �ִ� �����۵�� �� ������ ����.
    public HashSet<ItemData> hash_ItemsData = new(); // ���� �ִ� ������ ���� (�ܼ� �ߺ� ���� üũ��)

    private int inventoryFull = 25;
    public void AddItem(Wrapping wrapping)
    {
        if (items.Count > inventoryFull)
        {
            Debug.Log("�κ��丮 �� ��");
            return;
        }

        Debug.Log($"{wrapping._itemData}�� �κ��丮�� �־������~");

        //�Űܴ��
        ItemData p_itemData = wrapping._itemData;
            var p_itemData_warpped = new Inven_Item();
        p_itemData_warpped.itemData = p_itemData;

        // hash_ItemsData�� �ִ°��� üũ, canstack���� üũ
        if (p_itemData.canStack == true && hash_ItemsData.Contains(p_itemData))
            {
            foreach (var item in items)
            {
                //������ ���� ������ ���� ������ �߿� maxstack�� �ƴѰ� �ִ��� üũ, �ű⿡ ���� �߰��ϰ� ��
                if (item.itemData == p_itemData && item.ItemCount < item.itemData.maxStack)
                {
                    item.ItemCount++;
                    GameManager.Instance.UI.inventoryUI.UpdateUI();
                    return;
                }
            }
        }

        //������ �����ϸ� items�� Hash�� ���� �߰����ְ� ��
        items.Add(p_itemData_warpped);
        hash_ItemsData.Add(p_itemData);

        GameManager.Instance.UI.inventoryUI.UpdateUI();
    }

    public void RemoveItem(Inven_Item _item) //�׽�Ʈ ���غ�
    {
        items.Remove(_item);
        //������ ������ �����Ϳ� ���� ������ �����Ͱ� �κ��丮�� �����ִ��� üũ(�� ��Ʈ�� �κ��丮�� �����ϴ� ��Ȳ)
        foreach (var item in items)
        {
            //���������� �Լ� �����ϰ� �н�.
            if (item.itemData == _item.itemData)
            {
                return;
            }
        }
        // �ȳ��������� �ؽ������� ����.
        hash_ItemsData.Remove(_item.itemData);
    }
}
