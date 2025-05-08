using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private int maxSlots = 20;

    // UI ���� ����
    [SerializeField] private GameObject inventoryUI;

    // ������Ƽ
    public List<Item> Items => items;

    private void Start()
    {
        // UI ���� Ȯ��
        if (inventoryUI == null)
        {
            // ������ �κ��丮 UI ã�� �Ǵ� ���� ����
            // ���⼭�� ����
        }

        // UI �ʱ� ���� ����
        if (inventoryUI != null)
        {
            inventoryUI.SetActive(false);
        }
    }

    // ������ �߰�
    public bool AddItem(Item item)
    {
        if (items.Count >= maxSlots)
        {
            Debug.Log("�κ��丮�� ���� á���ϴ�!");
            return false;
        }

        items.Add(item);

        // UI ������Ʈ
        UpdateUI();

        return true;
    }

    // ������ ����
    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);

            // UI ������Ʈ
            UpdateUI();
        }
    }

    // UI ������Ʈ
    private void UpdateUI()
    {
        // �κ��丮 UI ������Ʈ ����
        // ���� ������ UI �ý��ۿ� �°� �ۼ� �ʿ�
    }

    // �κ��丮 UI ���
    public void ToggleInventoryUI()
    {
        if (inventoryUI != null)
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);

            // UI�� Ȱ��ȭ�� �� ���� ������Ʈ
            if (inventoryUI.activeSelf)
            {
                UpdateUI();
            }
        }
    }

    // ������ ��� (�ε��� ���)
    public void UseItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            Item item = items[index];
            item.ApplyEffect(Player.Instance);
        }
    }

    // Ư�� ������ ã��
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
