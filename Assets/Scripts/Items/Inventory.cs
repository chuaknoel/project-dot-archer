using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // �ν����Ϳ��� �̸����� ������ �� �ִ� ���� ������
    [Header("���� ���� ������ (�̸����� ����)")]
    [Tooltip("���� �̸��� �����ϼ��� (��: �Ϲ�ö��, ����Ȱ, �������� ��)")]
    [SerializeField] private string equippedWeaponName = "�Ϲ�ö��";

    [Tooltip("���� �̸��� �����ϼ��� (��: �Ϲ�����, �������� ��)")]
    [SerializeField] private string equippedHelmetName = "�Ϲ�����";

    [Tooltip("���� �̸��� �����ϼ��� (��: �Ϲݰ���, �������� ��)")]
    [SerializeField] private string equippedArmorName = "�Ϲݰ���";

    [Tooltip("�Ź� �̸��� �����ϼ��� (��: �ϹݽŹ�, �����Ź� ��)")]
    [SerializeField] private string equippedBootsName = "�ϹݽŹ�";

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
        // ���� ����
        if (!string.IsNullOrEmpty(equippedWeaponName))
        {
            SetupEquippedWeapon(equippedWeaponName);
        }

        // ���� ����
        if (!string.IsNullOrEmpty(equippedHelmetName))
        {
            SetupEquippedArmor(equippedHelmetName, ArmorType.Helmet);
        }

        // ���� ����
        if (!string.IsNullOrEmpty(equippedArmorName))
        {
            SetupEquippedArmor(equippedArmorName, ArmorType.Armor);
        }

        // �Ź� ����
        if (!string.IsNullOrEmpty(equippedBootsName))
        {
            SetupEquippedArmor(equippedBootsName, ArmorType.Boots);
        }
    }

    // ���� �̸����� ���� ���� ����
    private void SetupEquippedWeapon(string weaponName)
    {
        // ItemManager���� �̸����� ������ ������ ��������
        ItemData weaponData = ItemManager.Instance.GetItemDataByName(weaponName);

        if (weaponData != null)
        {
            // ���� ���� ���� (�κ��丮 �ý��ۿ�)
            UpdateEquippedWeapon(weaponData);

            Debug.Log($"���� '{weaponName}' ���� �Ϸ�");
        }
        else
        {
            Debug.LogWarning($"���� '{weaponName}'�� ã�� �� �����ϴ�.");
        }
    }

    // �� �̸����� �� ����
    private void SetupEquippedArmor(string armorName, ArmorType type)
    {
        // ItemManager���� �̸����� ������ ������ ��������
        ItemData armorData = ItemManager.Instance.GetItemDataByName(armorName);

        if (armorData != null)
        {
            // ���� ���� ���� (�κ��丮 �ý��ۿ�)
            UpdateEquippedArmor(armorData);

            Debug.Log($"�� '{armorName}' ���� �Ϸ�");
        }
        else
        {
            Debug.LogWarning($"�� '{armorName}'�� ã�� �� �����ϴ�.");
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
        item.SetItemId(itemData.ItemId);

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
        if (weapon != null) return weapon;

        weapon = GetEquippedWeapon(WeaponType.Sword);
        if (weapon != null) return weapon;

        weapon = GetEquippedWeapon(WeaponType.Scythe);
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

    /*
    ==========================================================================
    WeaponHandler ����ڸ� ���� ����
    ==========================================================================

    ������ �ý����� ����Ǿ� WeaponHandler���� ������ �������� ����ϴ� ������� �����Ǿ����ϴ�.
    
    1. ������ �����տ��� Item ��ũ��Ʈ�� �پ��ְ� itemId�� �����Ǿ� �ֽ��ϴ�.
    2. �κ��丮������ ������ �̸��� �����Ͽ� ���� ������ �����մϴ�.
    3. WeaponHandler�� �κ��丮���� ������ ������ ID�� ������ �ش� ID�� ������ 
       �������� ��ȯ�մϴ�.
    
    WeaponHandler ��� ���:
    
    1. �κ��丮���� ���� ������ ���� ID ��������:
       string weaponId = inventory.GetCurrentWeaponId();
    
    2. �ش� ID�� ��ġ�ϴ� ������ �ҷ�����:
       GameObject weaponPrefab = Resources.Load<GameObject>($"Weapons/{weaponId}");
    
    3. ���� �������� �ν��Ͻ�ȭ�Ͽ� ����:
       Instantiate(weaponPrefab, transform);
    
    WeaponHandler.Init�� ȣȯ���� ���� �����Ǿ�����, ���Ŀ��� ������ ID�� ����
    ������ �ε� ������� ��ȯ�� �����Դϴ�.
    
    �� ���� �����տ��� �̹� Item ������Ʈ�� ������, itemId �ʵ忡 
    �ش� ������ ID�� �����Ǿ� �ֽ��ϴ�. �������� �����ϸ� �ڵ����� 
    �ش� ID�� �´� ������ �����͸� �ε��մϴ�.
    ==========================================================================
    */
}