using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable] // �׽��� ������ ����ȭ
public class Inven_ItemData
{
    public ItemData itemData;
    public ItemType type;
    public int itemCount = 1;
    bool isEquiped = false;

    public void Init()
    {
        type = itemData.itemType;
    }

}

public class PlayerInventory : MonoBehaviour
{
    public List<Inven_ItemData> items = new(); // ������ ���� �ִ� �����۵�� �� ������ ����.
    public HashSet<ItemData> hash_ItemsData = new(); // ���� �ִ� ������ ���� (�ܼ� �ߺ� ���� üũ��)

    private int inventoryFull = 24;
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
        var p_itemData_warpped = new Inven_ItemData();
        p_itemData_warpped.itemData = p_itemData;

        // hash_ItemsData�� �ִ°��� üũ, canstack���� üũ
        if (p_itemData.canStack == true && hash_ItemsData.Contains(p_itemData))
        {
            foreach (var item in items)
            {
                //������ ���� ������ ���� ������ �߿� maxstack�� �ƴѰ� �ִ��� üũ, �ű⿡ ���� �߰��ϰ� ��
                if (item.itemData == p_itemData && item.itemCount < item.itemData.maxStack)
                {
                    item.itemCount++;
                    return;
                }
            }
        }

        //������ �����ϸ� items�� Hash�� ���� �߰����ְ� ��
        items.Add(p_itemData_warpped);
        hash_ItemsData.Add(p_itemData);
    }

    public void RemoveItem(Inven_ItemData _item) //�׽�Ʈ ���غ�
    {
        items.Remove(_item);
        //������ ������ �����Ͱ� �κ��丮�� �����ִ��� üũ
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

    public void UseItem()
    {

    }
}
