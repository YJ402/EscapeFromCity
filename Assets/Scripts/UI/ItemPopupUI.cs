using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class ItemPopupUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject BG;

    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI statName;
    public TextMeshProUGUI statValue;

    public GameObject equipBtn;
    public GameObject unequipBtn;
    public GameObject useBtn;
    public GameObject dropBtn;

    Inven_Item item;

    public bool isHoverPopup;
    private void Start()
    {
        SetDeactive(false);
        equipBtn.SetActive(false);
        unequipBtn.SetActive(false);
        useBtn.SetActive(false);
    }
    public void SetActive(int slotIndex, Vector3 position, Inven_Item _item)
    {
        BG.SetActive(true);
        item = _item;
        itemName.text = item.itemData.item_name;
        itemDescription.text = item.itemData.item_description;

        switch (item.itemData.itemType)
        {
            case ItemType.Equipable:
                {
                    foreach (ItemData_Equipable stat in item.itemData.itemData_Equipables)
                    {
                        statName.text += stat.equipableType.ToString() + "\n";
                        statValue.text += stat.value.ToString() + "\n";
                    }
                    if (item.isEquiped)
                    {
                        unequipBtn.SetActive(true);
                        unequipBtn.GetComponent<Button>().onClick.RemoveAllListeners();
                        unequipBtn.GetComponent<Button>().onClick.AddListener(item.Equip);
                    }
                    else
                    {
                        equipBtn.SetActive(true);
                        equipBtn.GetComponent<Button>().onClick.RemoveAllListeners();
                        equipBtn.GetComponent<Button>().onClick.AddListener(item.Equip);
                    }
                }
                break;

            case ItemType.Consumable:
                foreach (ItemData_Consumable stat in item.itemData.ItemData_Consumables)
                {
                    statName.text += stat.consumableType.ToString() + "\n";
                    statValue.text += stat.value.ToString() + "\n";
                }
                useBtn.SetActive(true);
                useBtn.GetComponent<Button>().onClick.RemoveAllListeners();
                useBtn.GetComponent<Button>().onClick.AddListener(item.Use);
                break;
        }

        //위치 조정. 슬롯 바로 옆에
        transform.position = new Vector3(position.x + 150, position.y - 200);
    }

    public void SetDeactive(bool isHoverSlot)
    {
        if (!isHoverSlot && !isHoverPopup) // 근데 팝업이 꺼지면 참인채로 남아있을 수도.
        {
            BG.SetActive(false);

            statName.text = null; // 아이템 이름이랑 설명은 어차피 새로 넣으니까 ㅇ
            statValue.text = null;

            equipBtn.SetActive(false);
            unequipBtn.SetActive(false);
            useBtn.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //isHoverPopup = true;
        StartCoroutine(SwitchHover(true));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //isHoverPopup = false;
        StartCoroutine(SwitchHover(false));
    }

    IEnumerator SwitchHover(bool TF)
    {
        yield return new WaitForSeconds(0.1f);
        isHoverPopup = TF;
    }


}
