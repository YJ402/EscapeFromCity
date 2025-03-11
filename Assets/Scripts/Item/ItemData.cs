using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipable,
    Consumable
}

public enum ConsumableType
{
    Health,
    Hunger
}

public enum EquipableType
{
    AttackPower,
    JumpPower,
    MoveSpeed
}

[System.Serializable]
public class ItemData_Consumable
{
    public ConsumableType consumableType;
    public float value;
}

[System.Serializable]
public class ItemData_Equipable
{
    public EquipableType equipableType; // �ʴ� �� �迭�ϱ�?? �ص� ������ �־��µ� ����� �ȳ���.
    public float value;
}

//[System.Serializable]
//public class ItemData_Equipable_Value
//{
//    public EquipableType equipableType;
//    public float value;
//}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject 
{
    [Header("���� ����")]
    public ItemType itemType;
    public string item_name;
    public string item_description;
    public bool canStack;
    public int maxStack = 12;
    public GameObject dropPref;
    public Sprite Image;

    [Header("�Һ���")]
    public ItemData_Consumable[] ItemData_Consumables;

    [Header("�����")]
    public ItemData_Equipable[] itemData_Equipables;
    public GameObject EquipPref;
}
