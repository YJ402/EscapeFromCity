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
public interface IInteractable // 현재 상호작용 가능한 것: 드랍 아이템
{
    public void SubscribeMethod();

    //public GameObject GetRoot(); // 필요없을 수도.

    public Wrapping GetNeedThing();

    public (string name,string desc) GetPromptInfo();
}

public class Interaction : MonoBehaviour
{
    [SerializeField] private float interactDistance;
    [SerializeField] private LayerMask InteractableLayerMask;
    public GameObject interactingObject;   // 아직 쓰는 곳 없음.
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
                    Debug.LogError($"{hit.transform.gameObject} 오브젝트는 InteractableLayerMask 임에도 IInteractable 컴포넌트가 없습니다.");
            }
            //interactingObject = interactableObject.GetRoot();

            promptUI.ShowPrompt(interactableObject);
        }
        else
        {
            // 비우기
            interactableObject = null;
            //interactingObject = null;
            promptUI.HidePrompt();
        }
    }

}
