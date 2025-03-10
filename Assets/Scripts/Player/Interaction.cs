using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable // ���� ��ȣ�ۿ� ������ ��: ��� ������
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
                    Debug.LogError($"{hit.transform.gameObject} ������Ʈ�� InteractableLayerMask �ӿ��� �ڽ� �Ǵ� �θ� IInteractable ������Ʈ�� �����ϴ�.");
                }

            }
            interactingItem = interactableItem.gameObject;
        }
        else
        {
            // ����
            interactableItem = null;
            interactingItem = null;
        }
    }
}
