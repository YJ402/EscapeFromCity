using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrapping 
{
    public ItemData _itemData;
}
public interface IInteractable // ���� ��ȣ�ۿ� ������ ��: ��� ������
{
    public void SubscribeMethod();

    public GameObject GetRoot();

    public Wrapping GetNeedThing();
}

public class Interaction : MonoBehaviour
{
    [SerializeField] private float interactDistance;
    [SerializeField] private LayerMask InteractableLayerMask;
    public GameObject interactingObject;  
    public IInteractable interactableObject; 
    
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, interactDistance, InteractableLayerMask))
        {
            if (!hit.transform.TryGetComponent<IInteractable>(out interactableObject))
            {
                    Debug.LogError($"{hit.transform.gameObject} ������Ʈ�� InteractableLayerMask �ӿ��� IInteractable ������Ʈ�� �����ϴ�.");
            }
            interactingObject = interactableObject.GetRoot();
        }
        else
        {
            // ����
            interactableObject = null;
            interactingObject = null;
        }
    }

}
