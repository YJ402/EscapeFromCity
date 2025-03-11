using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    InventoryUI inventoryUI;
    [HideInInspector] public Coroutine Co_ItemInfo;
    [HideInInspector] public int slotIndex;

    public Image image;
    public TextMeshProUGUI TMP_Count;

    //private bool isPopupOn;

    public bool isHoverSlot;

    //[Header("테스팅용")]
    //public int stackCount;

    public void Init(InventoryUI _inventoryUI, Coroutine _Co_ItemInfo, int _slotIndex)
    {
        inventoryUI = _inventoryUI;
        Co_ItemInfo = _Co_ItemInfo;
        slotIndex = _slotIndex;
    }
    public void OnPointerClick(PointerEventData eventData) // 아이템 장전
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData) // 아이템 
    {
        if (slotIndex < inventoryUI.inventoryData.items.Count)
        {
            Co_ItemInfo = StartCoroutine(ShowItemPopup());
            //isHoverSlot = true;
            StartCoroutine(SwitchHover(true));

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //isHoverSlot = false;
        StartCoroutine(SwitchHover(false));


        if (Co_ItemInfo != null)
        {
            StopCoroutine(Co_ItemInfo);
            Co_ItemInfo = null;
        }

        //GameManager.Instance.UI.itemPopupUI.SetDeactive(isHoverSlot);
        //isPopupOn = false;
        StartCoroutine(HideItemPopup());
    }

    IEnumerator ShowItemPopup()
    {
        yield return new WaitForSeconds(0.8f);

        //if (isHovering)
        {
            // 아이템 정보 팝업 표시
            Debug.Log($"{slotIndex}번 슬롯에 아이템 정보 표시");
            GameManager.Instance.UI.itemPopupUI.SetActive(slotIndex, transform.position, inventoryUI.inventoryData.items[slotIndex]);
            //isPopupOn = true;
        }
    }
    IEnumerator SwitchHover(bool TF)
    {
        yield return new WaitForSeconds(0.01f);

        isHoverSlot = TF;
    }

    IEnumerator HideItemPopup()
    {
        yield return new WaitForSeconds(0.15f);

        GameManager.Instance.UI.itemPopupUI.SetDeactive(isHoverSlot);
        //isPopupOn = false;
    }
}
