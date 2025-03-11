using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PromptUI promptUI;
    public InventoryUI inventoryUI;
    public ItemPopupUI itemPopupUI;
    public void Init()
    {
        promptUI = GetComponentInChildren<PromptUI>();
        inventoryUI = GetComponentInChildren<InventoryUI>();
        itemPopupUI = GetComponentInChildren<ItemPopupUI>();
    }
}
