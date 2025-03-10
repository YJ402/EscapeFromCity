using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable // 현재 상호작용 가능한 것: 드랍 아이템
{
    //void 
}

public class Interaction : MonoBehaviour
{
    [SerializeField] private float interactDistance;
    [SerializeField] private LayerMask InteractableLayerMask;
    public GameObject interactingItem;  
    public DropItem interactableItem; 
    

    private void Start()
    {
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, interactDistance, InteractableLayerMask))
        {
            if (!hit.transform.TryGetComponent<DropItem>(out interactableItem))
            {
                if(!hit.transform.TryGetComponent<DropItem>(out interactableItem))
                {
                    Debug.LogError($"{hit.transform.gameObject} 오브젝트는 InteractableLayerMask 임에도 자신 또는 부모에 IInteractable 컴포넌트가 없습니다.");
                }

            }
            interactingItem = interactableItem.gameObject;
        }
        else
        {
            // 비우기
            interactableItem = null;
            interactingItem = null;
        }
    }
}
