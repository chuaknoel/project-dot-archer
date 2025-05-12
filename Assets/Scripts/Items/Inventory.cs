using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private int maxSlots = 20;

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
    private Dictionary<ItemCategory, Item> equippedItems = new Dictionary<ItemCategory, Item>();

    // ���� ��� ���� (Ÿ�� �� ������ ����)
    private Dictionary<WeaponType, Item> equippedWeapons = new Dictionary<WeaponType, Item>();
    private Dictionary<ArmorType, Item> equippedArmors = new Dictionary<ArmorType, Item>();

    // UI ���� ����
    [Header("UI �г�")]
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

        // �ν����Ϳ��� ������ ���������� �ʱ� ��� ����
        //EquipSelectedItems();
    }

    // �ν����Ϳ��� ������ ������ ����
    public void EquipSelectedItems()
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

    // ���� �̸����� WeaponHandler ����
    private void SetupEquippedWeapon(string weaponName)
    {
        // ItemManager���� �̸����� ������ ������ ��������
        ItemData weaponData = ItemManager.Instance.GetItemDataByName(weaponName);

        if (weaponData != null)
        {
            // ������ Item ��ü ����
            Item weaponItem = Instantiate(ItemManager.Instance.TestWeapon);

            // ���� ���� ���� (�κ��丮 �ý��ۿ�)
            UpdateEquippedWeapon(weaponItem);

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
            // ������ Item ��ü ����
            Item armorItem = CreateVirtualItem(armorData);

            // ���� ���� ���� (�κ��丮 �ý��ۿ�)
            UpdateEquippedArmor(armorItem);

            Debug.Log($"�� '{armorName}' ���� �Ϸ�");
        }
        else
        {
            Debug.LogWarning($"�� '{armorName}'�� ã�� �� �����ϴ�.");
        }
    }

    // ������ Item ��ü ���� (���� GameObject ���� ItemData�� ���� ��ü)
    private Item CreateVirtualItem(ItemData itemData)
    {
        // �ӽ� GameObject ����
        GameObject tempObj = new GameObject($"VirtualItem_{itemData.ItemName}");
        tempObj.transform.SetParent(transform);
        tempObj.SetActive(false);

        // Item ������Ʈ �߰� �� �ʱ�ȭ
        Item item = tempObj.AddComponent<Item>();
        item.Initialize(itemData);

        return item;
    }

    // ������ ���� ���� ������Ʈ
    private void UpdateEquippedWeapon(Item item)
    {
        WeaponType weaponType = item.ItemData.WeaponType;

        // ���� ���� ���� ����
        if (equippedWeapons[weaponType] != null)
        {
            equippedWeapons[weaponType] = null;
        }

        // �� ���� ����
        equippedWeapons[weaponType] = item;
        equippedItems[ItemCategory.Weapon] = item;

        // ���ݷ� ���ʽ� ����
        attackBonus = item.ItemData.AttackBonus;
    }

    // ������ �� ���� ������Ʈ
    private void UpdateEquippedArmor(Item item)
    {
        ArmorType armorType = item.ItemData.ArmorType;

        // ���� ���� �� ����
        if (equippedArmors[armorType] != null)
        {
            equippedArmors[armorType] = null;
        }

        // �� �� ����
        equippedArmors[armorType] = item;
        equippedItems[ItemCategory.Armor] = item;

        // ���� ���ʽ� ����
        defenseBonus += item.ItemData.DefenseBonus;
    }

    // ���� ������ ���� ��������
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

    // ========== Player �ڵ� ȣȯ���� ���� �ӽ� �޼��� ==========

    // UI ��� (�ӽ� ����)
    public void ToggleInventoryUI()
    {
        // UI ����� �����Ǹ� �� �ּ��� �����ϰ� ���� �������� ��ü
        Debug.Log("�κ��丮 UI ����� ���� �������� �ʾҽ��ϴ�.");

        /*
        if (inventoryUI != null)
        {
            bool newState = !inventoryUI.activeSelf;
            inventoryUI.SetActive(newState);

            // �κ��丮�� �� �� �׻� ������Ʈ
            if (newState)
                UpdateInventoryUI();
        }
        */
    }

    // ��� UI ��� (�ӽ� ����)
    public void ToggleEquipmentUI()
    {
        // UI ����� �����Ǹ� �� �ּ��� �����ϰ� ���� �������� ��ü
        Debug.Log("��� UI ����� ���� �������� �ʾҽ��ϴ�.");

        /*
        if (equipmentUI != null)
        {
            bool newState = !equipmentUI.activeSelf;
            equipmentUI.SetActive(newState);

            // ��� â�� �� �� �׻� ������Ʈ
            if (newState)
                UpdateEquipmentUI();
        }
        */
    }

    // ��� UI ��� (�ӽ� ����)
    public void ToggleAllUI()
    {
        // UI ����� �����Ǹ� �� �ּ��� �����ϰ� ���� �������� ��ü
        Debug.Log("�κ��丮 �� ��� UI ����� ���� �������� �ʾҽ��ϴ�.");

        /*
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
        */
    }

    // ������ �߰� (�ӽ� ����)
    public bool AddItem(Item item)
    {
        // UI ����� �����Ǹ� �� �ּ��� �����ϰ� ���� �������� ��ü
        Debug.Log($"������ '{item.ItemData.ItemName}' �߰� ����� ���� �������� �ʾҽ��ϴ�.");
        return true;

        /*
        if (items.Count >= maxSlots)
        {
            Debug.Log("�κ��丮�� ���� á���ϴ�!");
            return false;
        }

        items.Add(item);
        UpdateUI();
        return true;
        */
    }

    /* 
    ==============================================================================
    UI�� ���� ������ ���� ��� (���� ��Ȱ��ȭ)
    ==============================================================================
    
    �� �κ��� ���� UI�� ���� �������� �����ϱ� ���� ����Դϴ�.
    ���߿� ���� �κ��丮 UI �ý����� ������ �� Ȱ��ȭ�� �� �ֽ��ϴ�.
    
    �ֿ� ���:
    1. �κ��丮 UI ǥ��/����
    2. ������ �߰�/����
    3. ������ ���(����)
    4. ��� ����
    
    ���� ���:
    - ToggleInventoryUI(): �κ��丮 UI ǥ��/���� ��ȯ
    - AddItem()/RemoveItem(): ������ �߰�/����
    - EquipItem()/UnequipItem(): ������ ����/����
    
    ��� ����:
    - ���� óġ �� AddItem()�� ȣ���Ͽ� ������ �߰�
    - UI ��ư Ŭ�� �� ToggleInventoryUI() ȣ��
    - ������ ���� Ŭ�� �� UseItem() ȣ��
    
    ���� ����:
    - ����� �ν����Ϳ��� ������ �ʱ� �����۸� ���
    - UI ���� �� �� �ּ��� �����ϰ� �Ʒ� �޼������ Ȱ��ȭ
    ==============================================================================
    
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

    // ��� ����
    public void EquipItem(Item item)
    {
        // ������ ���� ����
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
            
            // WeaponHandler�� ������ ���� ����
            if (player != null && player.WeaponHandler != null)
            {
                player.WeaponHandler.Init(item, player.stat, player.targetMask);
            }
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
        }

        // ���� ó��
        equippedItems[category] = item;
        item.SetEquipped(true);

        // UI ������Ʈ
        UpdateUI();
    }

    // ��� ����
    public void UnequipItem(Item item)
    {
        // ��� ���� ����
    }
    
    // UI ������Ʈ
    private void UpdateUI()
    {
        // �κ��丮 UI ������Ʈ
        UpdateInventoryUI();
        
        // ��� UI ������Ʈ
        UpdateEquipmentUI();
    }
    
    // �κ��丮 UI ������Ʈ
    private void UpdateInventoryUI()
    {
        // �κ��丮 UI ������Ʈ ����
    }
    
    // ��� UI ������Ʈ
    private void UpdateEquipmentUI()
    {
        // ��� UI ������Ʈ ����
    }
    */

    /*
    ==========================================================================
    WeaponHandler ����ڸ� ���� ����
    ==========================================================================

    �κ��丮 �ý����� �����Ǿ� ���� ���⸦ �̸����� ������ �� �ֽ��ϴ�.
    ���õ� ����� WeaponHandler�� �ڵ����� ���޵˴ϴ�.

    1. �κ��丮������ ���� �̸��� �����ϸ� ItemManager���� �ش� �����͸� �����ɴϴ�.
    2. �� �����͸� �������� ������ Item ��ü�� �����մϴ�.
    3. �� Item ��ü�� WeaponHandler.Init() �޼��忡 �����մϴ�.

    WeaponHandler������ ������ ���� Init �޼��带 Ȯ���ϸ� �����ϴ�:

    public virtual void Init(Item weapon, IAttackStat ownerStat, LayerMask targetMask)
    {
        this.weapon = weapon;
        animator = GetComponent<Animator>();
        isUseable = true;
        owner = ownerStat;
        this.targetMask = targetMask;
        
        // ���� ������ ����
        attackDamage = weapon.ItemData.AttackBonus;
        attackDelay = weapon.ItemData.AttackDelay;
        
        // ���� Ÿ�Կ� ���� ����
        switch (weapon.ItemData.WeaponType)
        {
            case WeaponType.Sword:
                // �� ���� ����
                break;
            case WeaponType.Bow:
                // Ȱ ���� ����
                break;
            case WeaponType.Scythe:
                // �� ���� ����
                break;
        }
        
        // ���� ��������Ʈ ���� (weaponRenderer�� �ִٸ�)
        if (weaponRenderer != null && weapon.ItemData.ItemIcon != null)
        {
            weaponRenderer.sprite = weapon.ItemData.ItemIcon;
        }

        //�ƴϸ� ������ ���� ������ ������(�ش� �������� ID�� ������ �� ������ ������)
    }

    Item ��ü�� ���� ��� ���� �����Ϳ� ������ �� �ֽ��ϴ�:
    - weapon.ItemData.itemId: ���� ID (��: "sword_common")
    - weapon.ItemData.ItemName: ���� �̸� (��: "�Ϲ�ö��")
    - weapon.ItemData.WeaponType: ���� Ÿ�� (Sword, Bow, Scythe)
    - weapon.ItemData.AttackBonus: ���ݷ� ���ʽ�
    - weapon.ItemData.AttackRange: ���� ����
    - weapon.ItemData.AttackDelay: ���� ������
    - weapon.ItemData.AttackCooldown: ���� ��ٿ�
    - weapon.ItemData.ItemIcon: ���� ������ ��������Ʈ

    �� �ý����� PlayerController�� WeaponHandler �ڵ带 ���� �������� �ʰ�
    ���� ������ �����ϰ� �մϴ�.

    ���ǻ����� ���ڵ忡 �����ּ���! ����. �ϴ� �������..
    ==========================================================================
    */
}