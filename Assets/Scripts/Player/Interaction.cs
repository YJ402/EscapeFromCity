using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wrapping 
{
    public ItemData _itemData;

    public Wrapping(ItemData itemdata)
    {
        _itemData = itemdata;
    }

    public Wrapping()
    {
    }
}
public interface IInteractable // ���� ��ȣ�ۿ� ������ ��: ��� ������
{
    public void SubscribeMethod();

    //public GameObject GetRoot(); // �ʿ���� ����.

    public Wrapping GetNeedThing();

    public (string name,string desc) GetPromptInfo();
}

public class Interaction : MonoBehaviour
{
    [SerializeField] private float interactDistance;
    [SerializeField] private LayerMask InteractableLayerMask;
    public GameObject interactingObject;   // ���� ���� �� ����.
    public IInteractable interactableObject; 
    public PromptUI promptUI;

    private void Start()
    {
        promptUI = GameManager.Instance.UI.promptUI;
    }

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
            //interactingObject = interactableObject.GetRoot();

            promptUI.ShowPrompt(interactableObject);
        }
        else
        {
            // ����
            interactableObject = null;
            //interactingObject = null;
            promptUI.HidePrompt();
        }
    }

}
