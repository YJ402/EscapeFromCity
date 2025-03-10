using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropItem : MonoBehaviour, IInteractable
{
    public ItemData itemData;
    public GameObject rootObject;

    public void SubscribeMethod()
    {
        GameManager.Instance.player.controller.interactAction = GameManager.Instance.player.inventory.AddItem;
    }

    public GameObject GetRoot()
    {
        if (rootObject != null)
            return rootObject; // 필요 없을듯
        return gameObject;
    }

    public Wrapping GetNeedThing()
    {
        var wrapped = new Wrapping();
        wrapped._itemData = itemData;
        return wrapped;
    }

    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
