using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private int maxSlots = 20;

    // ������ ������ ���� (ī�װ� ��)
    private Dictionary<ItemCategory, Item> equippedItems = new Dictionary<ItemCategory, Item>();

    // ���� ��� ���� (Ÿ�� �� ������ ����)
    private Dictionary<WeaponType, Item> equippedWeapons = new Dictionary<WeaponType, Item>();
    private Dictionary<ArmorType, Item> equippedArmors = new Dictionary<ArmorType, Item>();

    // UI ���� ����
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject equipmentUI;

    // ���� ���ʽ�
    private float attackBonus = 0f;
    private float defenseBonus = 0f;

    // �ʱ�ȭ
    private void Awake()
    {
        // ��� ��ųʸ� �ʱ�ȭ
        equippedItems[ItemCategory.Weapon] = null;
        equippedItems[ItemCategory.Armor] = null;

        // ���� Ÿ�� ��ųʸ� �ʱ�ȭ
        foreach (WeaponType weaponType in System.Enum.GetValues(typeof(WeaponType)))
        {
            if (weaponType != WeaponType.None)
                equippedWeapons[weaponType] = null;
        }

        // �� Ÿ�� ��ųʸ� �ʱ�ȭ
        foreach (ArmorType armorType in System.Enum.GetValues(typeof(ArmorType)))
        {
            if (armorType != ArmorType.None)
                equippedArmors[armorType] = null;
        }
    }

    private void Start()
    {
        // UI �ʱ� ���� ����
        if (inventoryUI != null)
            inventoryUI.SetActive(false);

        if (equipmentUI != null)
            equipmentUI.SetActive(false);
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
        UpdateUI();
        return true;
    }

    // ������ ����
    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);

            // ������ �������� ��� ����
            if (item.IsEquipped)
                UnequipItem(item);

            UpdateUI();
        }
    }

    // UI ������Ʈ
    private void UpdateUI()
    {
        // �κ��丮 UI ������Ʈ ����
        UpdateInventoryUI();

        // ��� UI ������Ʈ
        UpdateEquipmentUI();
    }

    // �κ��丮 UI ������Ʈ
    private void UpdateInventoryUI()
    {
        // ���� ������ ������Ʈ�� �°� �ۼ� �ʿ�
        // �κ��丮 ���� UI�� ������ ǥ��
        // ������ �������� Ư���� ǥ�ø� �߰�
    }

    // ��� UI ������Ʈ
    private void UpdateEquipmentUI()
    {
        // ��� ���Կ� ���� ������ ������ ǥ��
        // �� ��� Ÿ�Ժ� ���Կ� �ش� ������ ǥ��
    }

    // �κ��丮 UI ���
    public void ToggleInventoryUI()
    {
        if (inventoryUI != null)
        {
            bool newState = !inventoryUI.activeSelf;
            inventoryUI.SetActive(newState);

            // �κ��丮�� �� �� �׻� ������Ʈ
            if (newState)
                UpdateInventoryUI();
        }
    }

    // ��� UI ���
    public void ToggleEquipmentUI()
    {
        if (equipmentUI != null)
        {
            bool newState = !equipmentUI.activeSelf;
            equipmentUI.SetActive(newState);

            // ��� â�� �� �� �׻� ������Ʈ
            if (newState)
                UpdateEquipmentUI();
        }
    }

    // ���� UI ��� (�� �� ǥ��/����)
    public void ToggleAllUI()
    {
        bool newState = !inventoryUI.activeSelf;

        if (inventoryUI != null)
            inventoryUI.SetActive(newState);

        if (equipmentUI != null)
            equipmentUI.SetActive(newState);

        if (newState)
        {
            UpdateInventoryUI();
            UpdateEquipmentUI();
        }
    }

    // ������ ��� (�ε��� ���)
    public void UseItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            Item item = items[index];
            ApplyItemEffect(item);
        }
    }

    // ������ ȿ�� ����
    public void ApplyItemEffect(Item item)
    {
        ItemCategory category = item.ItemData.ItemCategory;

        // ��� �������� ��� ����
        if (category == ItemCategory.Weapon || category == ItemCategory.Armor)
        {
            EquipItem(item);
        }
    }

    // ��� ���� - ������ ����
    public void EquipItem(Item item)
    {
        ItemCategory category = item.ItemData.ItemCategory;

        // �Ϲ� ī�װ� ����
        if (equippedItems[category] != null)
        {
            UnequipItem(equippedItems[category]);
        }

        // ���� Ÿ�Ժ� ����
        if (category == ItemCategory.Weapon)
        {
            WeaponType weaponType = item.ItemData.WeaponType;
            if (equippedWeapons[weaponType] != null)
            {
                UnequipItem(equippedWeapons[weaponType]);
            }

            // �� ���� ����
            equippedWeapons[weaponType] = item;
            attackBonus += item.ItemData.AttackBonus;
            Debug.Log($"���� ����: {item.ItemData.ItemName}, ���ݷ� ���ʽ�: +{item.ItemData.AttackBonus}");
        }
        else if (category == ItemCategory.Armor)
        {
            ArmorType armorType = item.ItemData.ArmorType;
            if (equippedArmors[armorType] != null)
            {
                UnequipItem(equippedArmors[armorType]);
            }

            // �� �� ����
            equippedArmors[armorType] = item;
            defenseBonus += item.ItemData.DefenseBonus;
            Debug.Log($"�� ����: {item.ItemData.ItemName}, ���� ���ʽ�: +{item.ItemData.DefenseBonus}");
        }

        // ���� ó��
        equippedItems[category] = item;
        item.SetEquipped(true);

        // UI ������Ʈ
        UpdateUI();
    }

    // ��� ���� - ���� ������ ���� ����
    public void UnequipItem(Item item)
    {
        if (item == null) return;

        ItemCategory category = item.ItemData.ItemCategory;

        // �Ϲ� ī�װ����� ����
        if (equippedItems[category] == item)
        {
            equippedItems[category] = null;
        }

        // ���� Ÿ�Ժ� �������� ����
        if (category == ItemCategory.Weapon)
        {
            WeaponType weaponType = item.ItemData.WeaponType;
            if (equippedWeapons[weaponType] == item)
            {
                equippedWeapons[weaponType] = null;
                attackBonus -= item.ItemData.AttackBonus;
                Debug.Log($"���� ����: {item.ItemData.ItemName}");
            }
        }
        else if (category == ItemCategory.Armor)
        {
            ArmorType armorType = item.ItemData.ArmorType;
            if (equippedArmors[armorType] == item)
            {
                equippedArmors[armorType] = null;
                defenseBonus -= item.ItemData.DefenseBonus;
                Debug.Log($"�� ����: {item.ItemData.ItemName}");
            }
        }

        // ������ ���� ���� ����
        item.SetEquipped(false);

        // UI ������Ʈ
        UpdateUI();
    }

    // ��� ���� - ī�װ� ��� ����
    public void UnequipItem(ItemCategory category)
    {
        Item equippedItem = equippedItems[category];
        if (equippedItem != null)
        {
            UnequipItem(equippedItem);
        }
    }

    // Ư�� ���� Ÿ�� ���� ����
    public void UnequipWeaponByType(WeaponType type)
    {
        Item equippedWeapon = equippedWeapons[type];
        if (equippedWeapon != null)
        {
            UnequipItem(equippedWeapon);
        }
    }

    // Ư�� �� Ÿ�� ���� ����
    public void UnequipArmorByType(ArmorType type)
    {
        Item equippedArmor = equippedArmors[type];
        if (equippedArmor != null)
        {
            UnequipItem(equippedArmor);
        }
    }

    // ��� ��� ����
    public void UnequipAllItems()
    {
        // ���� ��� ����
        foreach (WeaponType type in equippedWeapons.Keys)
        {
            if (equippedWeapons[type] != null)
            {
                UnequipItem(equippedWeapons[type]);
            }
        }

        // �� ��� ����
        foreach (ArmorType type in equippedArmors.Keys)
        {
            if (equippedArmors[type] != null)
            {
                UnequipItem(equippedArmors[type]);
            }
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

    // ������ �����۸� ��������
    public List<Item> GetEquippedItems()
    {
        List<Item> equipped = new List<Item>();

        // ���� �߰�
        foreach (Item weapon in equippedWeapons.Values)
        {
            if (weapon != null)
                equipped.Add(weapon);
        }

        // �� �߰�
        foreach (Item armor in equippedArmors.Values)
        {
            if (armor != null)
                equipped.Add(armor);
        }

        return equipped;
    }

    // Ư�� Ÿ���� ������ ���� ��������
    public Item GetEquippedWeapon(WeaponType type)
    {
        return equippedWeapons[type];
    }

    // Ư�� Ÿ���� ������ �� ��������
    public Item GetEquippedArmor(ArmorType type)
    {
        return equippedArmors[type];
    }

    // �� ��� �� ���ϱ�
    public int GetEquippedItemCount()
    {
        int count = 0;

        foreach (Item weapon in equippedWeapons.Values)
        {
            if (weapon != null)
                count++;
        }

        foreach (Item armor in equippedArmors.Values)
        {
            if (armor != null)
                count++;
        }

        return count;
    }

    // �� ���ݷ� ���ʽ� ��� (Player���� ȣ��)
    public float GetTotalAttackBonus()
    {
        return attackBonus;
    }

    // �� ���� ���ʽ� ��� (Player���� ȣ��)
    public float GetTotalDefenseBonus()
    {
        return defenseBonus;
    }

    // ��� ȿ�� ���ڿ� ��� (UI ǥ�ÿ�)
    public string GetEquipmentEffectText()
    {
        string text = "��� ȿ��:\n";
        text += $"���ݷ� ���ʽ�: +{attackBonus}\n";
        text += $"���� ���ʽ�: +{defenseBonus}\n";
        return text;
    }
}