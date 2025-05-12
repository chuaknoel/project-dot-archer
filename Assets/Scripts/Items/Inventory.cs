using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // �ν����Ϳ��� ID�� ������ �� �ִ� ���� ������
    [Header("���� ���� ������ (ID�� ����)")]
    [Tooltip("���� ID�� �Է��ϼ��� (��: sword_common, bow_common ��)")]
    [SerializeField] private string equippedWeaponId = "bow_common";

    [Tooltip("���� ID�� �Է��ϼ��� (��: helmet_common ��)")]
    [SerializeField] private string equippedHelmetId = "helmet_common";

    [Tooltip("���� ID�� �Է��ϼ��� (��: armor_common ��)")]
    [SerializeField] private string equippedArmorId = "armor_common";

    [Tooltip("�Ź� ID�� �Է��ϼ��� (��: boots_common ��)")]
    [SerializeField] private string equippedBootsId = "boots_common";

    // ������ ������ ���� (ī�װ� ��)
    private Dictionary<ItemCategory, ItemData> equippedItems = new Dictionary<ItemCategory, ItemData>();

    // ���� ��� ���� (Ÿ�� �� ������ ����)
    private Dictionary<WeaponType, ItemData> equippedWeapons = new Dictionary<WeaponType, ItemData>();
    private Dictionary<ArmorType, ItemData> equippedArmors = new Dictionary<ArmorType, ItemData>();

    // ���� ������ ��ü (Player Ŭ�������� ȣȯ����)
    private Dictionary<WeaponType, Item> virtualWeaponItems = new Dictionary<WeaponType, Item>();
    private Dictionary<ArmorType, Item> virtualArmorItems = new Dictionary<ArmorType, Item>();

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
            {
                equippedWeapons[weaponType] = null;
                virtualWeaponItems[weaponType] = null;
            }
        }

        // �� Ÿ�� ��ųʸ� �ʱ�ȭ
        foreach (ArmorType armorType in System.Enum.GetValues(typeof(ArmorType)))
        {
            if (armorType != ArmorType.None)
            {
                equippedArmors[armorType] = null;
                virtualArmorItems[armorType] = null;
            }
        }
    }

    private void Start()
    {
        // �ν����Ϳ��� ������ ���������� �ʱ� ��� ����
        EquipSelectedItems();
    }

    // �ν����Ϳ��� ������ ������ ����
    private void EquipSelectedItems()
    {
        Debug.Log("Inventory: �ʱ� ��� ���� ����");

        // ���� ����
        if (!string.IsNullOrEmpty(equippedWeaponId))
        {
            SetupEquippedWeaponById(equippedWeaponId);
        }

        // ���� ����
        if (!string.IsNullOrEmpty(equippedHelmetId))
        {
            SetupEquippedArmorById(equippedHelmetId, ArmorType.Helmet);
        }

        // ���� ����
        if (!string.IsNullOrEmpty(equippedArmorId))
        {
            SetupEquippedArmorById(equippedArmorId, ArmorType.Armor);
        }

        // �Ź� ����
        if (!string.IsNullOrEmpty(equippedBootsId))
        {
            SetupEquippedArmorById(equippedBootsId, ArmorType.Boots);
        }

        Debug.Log("Inventory: �ʱ� ��� ���� �Ϸ�");
    }

    // ���� ID�� ���� ���� ����
    private void SetupEquippedWeaponById(string weaponId)
    {
        // ItemManager���� ID�� ������ ������ ��������
        ItemData weaponData = ItemManager.Instance.GetItemDataById(weaponId);

        if (weaponData != null)
        {
            // ���� ���� ���� (�κ��丮 �ý��ۿ�)
            UpdateEquippedWeapon(weaponData);

            Debug.Log($"���� '{weaponId}' ���� �Ϸ�");
        }
        else
        {
            Debug.LogWarning($"���� ID '{weaponId}'�� ã�� �� �����ϴ�.");
        }
    }

    // �� ID�� �� ����
    private void SetupEquippedArmorById(string armorId, ArmorType type)
    {
        // ItemManager���� ID�� ������ ������ ��������
        ItemData armorData = ItemManager.Instance.GetItemDataById(armorId);

        if (armorData != null)
        {
            // ���� ���� ���� (�κ��丮 �ý��ۿ�)
            UpdateEquippedArmor(armorData);

            Debug.Log($"�� '{armorId}' ���� �Ϸ�");
        }
        else
        {
            Debug.LogWarning($"�� ID '{armorId}'�� ã�� �� �����ϴ�.");
        }
    }

    // ������ ���� ���� ������Ʈ
    private void UpdateEquippedWeapon(ItemData item)
    {
        WeaponType weaponType = item.WeaponType;

        // ���� ���� ���� ����
        if (equippedWeapons[weaponType] != null)
        {
            attackBonus -= equippedWeapons[weaponType].AttackBonus;
        }

        // �� ���� ����
        equippedWeapons[weaponType] = item;
        equippedItems[ItemCategory.Weapon] = item;

        // ���ݷ� ���ʽ� ����
        attackBonus += item.AttackBonus;

        // ���� ������ ���� (Player ȣȯ����)
        virtualWeaponItems[weaponType] = CreateVirtualItem(item);

        Debug.Log($"���� ���� ���� ������Ʈ: {item.ItemName}, ���ݷ� +{item.AttackBonus}");
    }

    // ������ �� ���� ������Ʈ
    private void UpdateEquippedArmor(ItemData item)
    {
        ArmorType armorType = item.ArmorType;

        // ���� ���� �� ����
        if (equippedArmors[armorType] != null)
        {
            defenseBonus -= equippedArmors[armorType].DefenseBonus;
        }

        // �� �� ����
        equippedArmors[armorType] = item;
        equippedItems[ItemCategory.Armor] = item;

        // ���� ���ʽ� ����
        defenseBonus += item.DefenseBonus;

        // ���� ������ ���� (Player ȣȯ����)
        virtualArmorItems[armorType] = CreateVirtualItem(item);

        Debug.Log($"�� ���� ���� ������Ʈ: {item.ItemName}, ���� +{item.DefenseBonus}");
    }

    // ������ Item ��ü ���� (Player ȣȯ����)
    private Item CreateVirtualItem(ItemData itemData)
    {
        // �ӽ� GameObject ����
        GameObject tempObj = new GameObject($"VirtualItem_{itemData.ItemName}");
        tempObj.transform.SetParent(transform);
        tempObj.SetActive(false);

        // Item ������Ʈ �߰� �� �ʱ�ȭ
        Item item = tempObj.AddComponent<Item>();
        item.Initialize(itemData);

        Debug.Log($"���� ������ ����: {itemData.ItemName}, ID: {itemData.ItemId}");
        return item;
    }

    // ���� ������ ���� ������ ��������
    public ItemData GetCurrentWeaponData()
    {
        // ������ ���� ��ȯ (Ȱ, ��, �� �� �ϳ�)
        ItemData weapon = GetEquippedWeaponData(WeaponType.Bow);
        if (weapon != null) return weapon;

        weapon = GetEquippedWeaponData(WeaponType.Sword);
        if (weapon != null) return weapon;

        weapon = GetEquippedWeaponData(WeaponType.Scythe);
        return weapon;
    }

    // ���� ������ ���� �������� (Player ȣȯ����)
    public Item GetCurrentWeapon()
    {
        // ������ ���� ��ȯ (Ȱ, ��, �� �� �ϳ�)
        Item weapon = GetEquippedWeapon(WeaponType.Bow);
        if (weapon != null)
        {
            Debug.Log($"���� ������ ����: {weapon.ItemData.ItemName}, ID: {weapon.ItemId}");
            return weapon;
        }

        weapon = GetEquippedWeapon(WeaponType.Sword);
        if (weapon != null)
        {
            Debug.Log($"���� ������ ����: {weapon.ItemData.ItemName}, ID: {weapon.ItemId}");
            return weapon;
        }

        weapon = GetEquippedWeapon(WeaponType.Scythe);
        if (weapon != null)
        {
            Debug.Log($"���� ������ ����: {weapon.ItemData.ItemName}, ID: {weapon.ItemId}");
        }
        else
        {
            Debug.LogWarning("GetCurrentWeapon: ������ ���Ⱑ �����ϴ�.");
        }
        return weapon;
    }

    // Ư�� Ÿ���� ������ ���� ������ ��������
    public ItemData GetEquippedWeaponData(WeaponType type)
    {
        return equippedWeapons[type];
    }

    // Ư�� Ÿ���� ������ ���� �������� (Player ȣȯ����)
    public Item GetEquippedWeapon(WeaponType type)
    {
        return virtualWeaponItems[type];
    }

    // Ư�� Ÿ���� ������ �� ������ ��������
    public ItemData GetEquippedArmorData(ArmorType type)
    {
        return equippedArmors[type];
    }

    // Ư�� Ÿ���� ������ �� �������� (Player ȣȯ����)
    public Item GetEquippedArmor(ArmorType type)
    {
        return virtualArmorItems[type];
    }

    // �� ���ݷ� ���ʽ� ���
    public float GetTotalAttackBonus()
    {
        return attackBonus;
    }

    // �� ���� ���ʽ� ���
    public float GetTotalDefenseBonus()
    {
        return defenseBonus;
    }

    // ���� ���� ID ��������
    public string GetCurrentWeaponId()
    {
        ItemData weapon = GetCurrentWeaponData();
        return weapon != null ? weapon.ItemId : string.Empty;
    }

    // ���� ������ ���� �̸� ��������
    public string GetCurrentWeaponName()
    {
        ItemData weapon = GetCurrentWeaponData();
        return weapon != null ? weapon.ItemName : string.Empty;
    }

    // Player �����ڸ� ���� �ӽ� ȣȯ�� �޼����
    // UI �������� ���� ���������θ� ���
    public void ToggleInventoryUI() { Debug.Log("�κ��丮 UI ����� ���� �������� �ʾҽ��ϴ�."); }
    public void ToggleEquipmentUI() { Debug.Log("��� UI ����� ���� �������� �ʾҽ��ϴ�."); }
    public void ToggleAllUI() { Debug.Log("�κ��丮 �� ��� UI ����� ���� �������� �ʾҽ��ϴ�."); }
    public bool AddItem(Item item) { Debug.Log($"������ '{item.ItemData.ItemName}' �߰� ����� ���� �������� �ʾҽ��ϴ�."); return true; }

    // �����: ���� ���� ���� ���
    public void PrintEquippedItems()
    {
        Debug.Log("===== ���� ������ ���� =====");

        // ���� ����
        Debug.Log("== ���� ==");
        foreach (var pair in equippedWeapons)
        {
            if (pair.Value != null)
            {
                Debug.Log($"- {pair.Key}: {pair.Value.ItemName} (ID: {pair.Value.ItemId})");
            }
            else
            {
                Debug.Log($"- {pair.Key}: ���� ����");
            }
        }

        // �� ����
        Debug.Log("== �� ==");
        foreach (var pair in equippedArmors)
        {
            if (pair.Value != null)
            {
                Debug.Log($"- {pair.Key}: {pair.Value.ItemName} (ID: {pair.Value.ItemId})");
            }
            else
            {
                Debug.Log($"- {pair.Key}: ���� ����");
            }
        }

        Debug.Log($"�� ���ݷ� ���ʽ�: +{attackBonus}");
        Debug.Log($"�� ���� ���ʽ�: +{defenseBonus}");
    }

    /*
    ==========================================================================
    WeaponHandler ����ڸ� ���� ����
    ==========================================================================

    ������ �ý����� ����Ǿ� WeaponHandler���� ������ �������� ����ϴ� ������� �����Ǿ����ϴ�.
    
    1. ������ �����տ��� Item ��ũ��Ʈ�� �پ��ְ� itemId�� �����Ǿ� �ֽ��ϴ�.
    2. �κ��丮������ ������ ID�� �����Ͽ� ���� ������ �����մϴ�.
    3. WeaponHandler�� �κ��丮���� ������ ������ ID�� ������ �ش� ID�� ������ 
       �������� ��ȯ�մϴ�.
    
    ���� ID�� ��� ����:
    - �⺻ ����� Resources/Weapons/ ������ �־�� �մϴ�.
    - ������ �̸��� �߿����� ������, Item ������Ʈ�� itemId �ʵ尡 �߿��մϴ�.
    - ��: bow_common �������� Resources/Weapons/bow_common.prefab ��ο� �־�� �մϴ�.
    
    WeaponHandler.Init �޼���� Item ��ü�� �޾Ƽ� ó���մϴ�:
    - weapon.ItemId�� ������ ID�� �����ɴϴ�.
    - Resources.Load<GameObject>($"Weapons/{weapon.ItemId}")�� �������� �ε��մϴ�.
    - �ε�� �������� �ν��Ͻ�ȭ�Ͽ� ���⸦ ǥ���մϴ�.
    
    ������ ������ �α׸� Ȯ���ϰ� ItemManager, Inventory, WeaponHandler ����ڿ�
    �Բ� ������ �ذ��ϼ���.
    ==========================================================================
    */
}