using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;



public class PlayerInventory : MonoBehaviour
{
    public List<Inven_Item> items = new(); // 실제로 갖고 있는 아이템들과 그 갯수등 정보.
    public HashSet<ItemData> hash_ItemsData = new(); // 갖고 있는 아이템 종류 (단순 중복 여부 체크용)

    private int inventoryFull = 25;
    public void AddItem(Wrapping wrapping)
    {
        if (items.Count > inventoryFull)
        {
            Debug.Log("인벤토리 꽉 참");
            return;
        }

        Debug.Log($"{wrapping._itemData}를 인벤토리에 넣어버리기~");

        //옮겨담기
        ItemData p_itemData = wrapping._itemData;
            var p_itemData_warpped = new Inven_Item();
        p_itemData_warpped.itemData = p_itemData;

        // hash_ItemsData에 있는건지 체크, canstack인지 체크
        if (p_itemData.canStack == true && hash_ItemsData.Contains(p_itemData))
            {
            foreach (var item in items)
            {
                //맞으면 제일 갯수가 적은 아이템 중에 maxstack이 아닌게 있는지 체크, 거기에 갯수 추가하고 끝
                if (item.itemData == p_itemData && item.ItemCount < item.itemData.maxStack)
                {
                    item.ItemCount++;
                    GameManager.Instance.UI.inventoryUI.UpdateUI();
                    return;
                }
            }
        }

        //위에서 실패하면 items랑 Hash에 새로 추가해주고 끝
        items.Add(p_itemData_warpped);
        hash_ItemsData.Add(p_itemData);

        GameManager.Instance.UI.inventoryUI.UpdateUI();
    }

    public void RemoveItem(Inven_Item _item) //테스트 안해봄
    {
        items.Remove(_item);
        //삭제된 아이템 데이터와 같은 아이템 데이터가 인벤토리에 남아있는지 체크(두 세트가 인벤토리에 존재하는 상황)
        foreach (var item in items)
        {
            //남아있으면 함수 종료하고 패스.
            if (item.itemData == _item.itemData)
            {
                return;
            }
        }
        // 안남아있으면 해쉬에서도 삭제.
        hash_ItemsData.Remove(_item.itemData);
    }
}
