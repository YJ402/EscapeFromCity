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

    private bool isPopupOn;

    public bool isHoverSlot;

    //[Header("�׽��ÿ�")]
    //public int stackCount;

    public void Init(InventoryUI _inventoryUI, Coroutine _Co_ItemInfo, int _slotIndex)
    {
        inventoryUI = _inventoryUI;
        Co_ItemInfo = _Co_ItemInfo;
        slotIndex = _slotIndex;
    }
    public void OnPointerClick(PointerEventData eventData) // ������ ����
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData) // ������ 
    {
        if (slotIndex < inventoryUI.inventoryData.items.Count)
        {
            Co_ItemInfo = StartCoroutine(ItemInfo());
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
        StartCoroutine(DeactivePopup());
    }

    IEnumerator ItemInfo()
    {
        yield return new WaitForSeconds(1.2f);

        //if (isHovering)
        {
            // ������ ���� �˾� ǥ��
            Debug.Log($"{slotIndex}�� ���Կ� ������ ���� ǥ��");
            GameManager.Instance.UI.itemPopupUI.SetActive(slotIndex, transform.position, inventoryUI.inventoryData.items[slotIndex]);
            isPopupOn = true;
        }
    }
    IEnumerator SwitchHover(bool TF)
    {
        yield return new WaitForSeconds(0.1f);

        isHoverSlot = TF;
    }

    IEnumerator DeactivePopup()
    {
        yield return new WaitForSeconds(0.15f);

        GameManager.Instance.UI.itemPopupUI.SetDeactive(isHoverSlot);
        isPopupOn = false;
    }
}
