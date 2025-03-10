using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour, IInteractable
{
    public ItemData itemData;
    public GameObject rootObject;

    public void AddMethod()
    {
        GameManager.Instance.player.controller.interactAction = GameManager.Instance.player.inventory.GetItem;
    }

    public GameObject GetRoot()
    {
        if (rootObject != null)
            return rootObject; // 필요 없을듯
        return gameObject;
    }

    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
