using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private int maxSlots = 20;

    // UI 관련 변수
    [SerializeField] private GameObject inventoryUI;

    // 프로퍼티
    public List<Item> Items => items;

    private void Start()
    {
        // UI 참조 확인
        if (inventoryUI == null)
        {
            // 씬에서 인벤토리 UI 찾기 또는 생성 로직
            // 여기서는 생략
        }

        // UI 초기 상태 설정
        if (inventoryUI != null)
        {
            inventoryUI.SetActive(false);
        }
    }

    // 아이템 추가
    public bool AddItem(Item item)
    {
        if (items.Count >= maxSlots)
        {
            Debug.Log("인벤토리가 가득 찼습니다!");
            return false;
        }

        items.Add(item);

        // UI 업데이트
        UpdateUI();

        return true;
    }

    // 아이템 제거
    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);

            // UI 업데이트
            UpdateUI();
        }
    }

    // UI 업데이트
    private void UpdateUI()
    {
        // 인벤토리 UI 업데이트 로직
        // 실제 구현은 UI 시스템에 맞게 작성 필요
    }

    // 인벤토리 UI 토글
    public void ToggleInventoryUI()
    {
        if (inventoryUI != null)
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);

            // UI가 활성화될 때 내용 업데이트
            if (inventoryUI.activeSelf)
            {
                UpdateUI();
            }
        }
    }

    // 아이템 사용 (인덱스 기반)
    public void UseItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            Item item = items[index];
            item.ApplyEffect(Player.Instance);
        }
    }

    // 특정 아이템 찾기
    public Item FindItem(string itemName)
    {
        foreach (Item item in items)
        {
            if (item.ItemData.ItemName == itemName)
            {
                return item;
            }
        }

        return null;
    }
}
