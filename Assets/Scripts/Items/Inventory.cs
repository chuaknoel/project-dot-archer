using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// �÷��̾��� ���� ������(���⡤��)�� �����ϰ�,
/// ������ �������� Instantiate�Ͽ� ���� ���ʽ��� �����ϸ�,
/// ���� ���� ������ ��ȸ �� �κ��丮 UI ��� ����� �����մϴ�.
/// </summary>
public class Inventory : MonoBehaviour
{
    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 1) Inspector ���� ����
    //������������������������������������������������������������������������������������������������������������������������������������������������

    [Header("������ ������ ID (Inspector�� �Է�)")]
    [Tooltip("������ ���� ID (��: bow_common)")]
    [SerializeField] private string equippedWeaponId = "bow_common";
    [Tooltip("������ ���� ID (��: helmet_common)")]
    [SerializeField] private string equippedHelmetId = "helmet_common";
    [Tooltip("������ ���� ID (��: armor_common)")]
    [SerializeField] private string equippedArmorId = "armor_common";
    [Tooltip("������ �Ź� ID (��: boots_common)")]
    [SerializeField] private string equippedBootsId = "boots_common";

    [Header("���� ������ Prefab ����Ʈ")]
    [Tooltip("��� ���� Prefab (Item ������Ʈ�� ItemId�� ���� �� �巡��&���)")]
    [SerializeField] private List<GameObject> weaponPrefabs;
    [Tooltip("��� �� Prefab (Item ������Ʈ�� ItemId�� ���� �� �巡��&���)")]
    [SerializeField] private List<GameObject> armorPrefabs;

    [Header("UI �г� (�κ��丮 â)")]
    [Tooltip("�κ��丮 UI �г��� �����ϼ���")]
    [SerializeField] private GameObject inventoryUIPanel;

    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 2) ���� ���� ����� ��ųʸ�
    //������������������������������������������������������������������������������������������������������������������������������������������������

    // ID �� Prefab ����
    private Dictionary<string, GameObject> weaponPrefabDict = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> armorPrefabDict = new Dictionary<string, GameObject>();

    // ������ ������(ItemData)
    private Dictionary<WeaponType, ItemData> equippedWeapons = new Dictionary<WeaponType, ItemData>();
    private Dictionary<ArmorType, ItemData> equippedArmors = new Dictionary<ArmorType, ItemData>();

    // Instantiate�� Item ������Ʈ ����
    private Dictionary<WeaponType, Item> virtualWeaponItems = new Dictionary<WeaponType, Item>();
    private Dictionary<ArmorType, Item> virtualArmorItems = new Dictionary<ArmorType, Item>();

    // ���ʽ� ���� �ջ�
    private float attackBonus = 0f;
    private float defenseBonus = 0f;

    // �κ��丮 UI ��� ����
    private bool isInventoryOpen = false;

    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 3) Unity �̺�Ʈ �ݹ�
    //������������������������������������������������������������������������������������������������������������������������������������������������

    private void Awake()
    {
        // �ǵ�: Awake���� Prefab ����Ʈ�� �о� ID��Prefab ������ �ʱ�ȭ
        InitializeWeaponPrefabDict();
        InitializeArmorPrefabDict();
    }

    private void Start()
    {
        // �ǵ�: Start���� Inspector�� �Էµ� ID�� �ʱ� ���� ������ ����
        EquipSelectedItems();
    }

    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 4) Prefab ����Ʈ �� ��ųʸ� �ʱ�ȭ
    //������������������������������������������������������������������������������������������������������������������������������������������������

    private void InitializeWeaponPrefabDict()
    {
        weaponPrefabDict.Clear();
        foreach (var prefab in weaponPrefabs)
        {
            var it = prefab.GetComponent<Item>();
            if (it != null && !string.IsNullOrEmpty(it.ItemId))
                weaponPrefabDict[it.ItemId] = prefab;
            else
                Debug.LogWarning($"[Inventory] Weapon Prefab ����: {prefab.name}");
        }
    }

    private void InitializeArmorPrefabDict()
    {
        armorPrefabDict.Clear();
        foreach (var prefab in armorPrefabs)
        {
            var it = prefab.GetComponent<Item>();
            if (it != null && !string.IsNullOrEmpty(it.ItemId))
                armorPrefabDict[it.ItemId] = prefab;
            else
                Debug.LogWarning($"[Inventory] Armor Prefab ����: {prefab.name}");
        }
    }

    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 5) �ʱ� ���� ���� (Inspector ID �� ����)
    //������������������������������������������������������������������������������������������������������������������������������������������������

    public void EquipSelectedItems()
    {
        if (!string.IsNullOrEmpty(equippedWeaponId))
            SetupEquippedWeaponById(equippedWeaponId);
        if (!string.IsNullOrEmpty(equippedHelmetId))
            SetupEquippedArmorById(equippedHelmetId, ArmorType.Helmet);
        if (!string.IsNullOrEmpty(equippedArmorId))
            SetupEquippedArmorById(equippedArmorId, ArmorType.Armor);
        if (!string.IsNullOrEmpty(equippedBootsId))
            SetupEquippedArmorById(equippedBootsId, ArmorType.Boots);
    }

    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 6) ID �� ItemData ��ȸ �� ���� ������Ʈ
    //������������������������������������������������������������������������������������������������������������������������������������������������

    private void SetupEquippedWeaponById(string weaponId)
    {
        // �ǵ�: ItemManager���� ������ ��ȸ
        var data = ItemManager.Instance.GetItemDataById(weaponId);
        if (data != null)
            UpdateEquippedWeapon(data);
        else
            Debug.LogError($"[Inventory] ���� ������ ����: {weaponId}");
    }

    private void SetupEquippedArmorById(string armorId, ArmorType type)
    {
        // �ǵ�: ItemManager���� ������ ��ȸ
        var data = ItemManager.Instance.GetItemDataById(armorId);
        if (data != null)
            UpdateEquippedArmor(data, type);
        else
            Debug.LogError($"[Inventory] �� ������ ����: {armorId}");
    }

    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 7) ���� ���� ����: ���ʽ� ���� ���� + Prefab Instantiate
    //������������������������������������������������������������������������������������������������������������������������������������������������

    private void UpdateEquippedWeapon(ItemData item)
    {
        // �ǵ�: ���� ���� ���� ���ʽ� ����
        if (equippedWeapons.TryGetValue(item.WeaponType, out var prev) && prev != null)
            attackBonus -= prev.AttackBonus;

        // �ǵ�: �� ���� ������ ���� �� ���ʽ� �߰�
        equippedWeapons[item.WeaponType] = item;
        attackBonus += item.AttackBonus;

        // �ǵ�: Prefab���� ���� Item ������Ʈ ���� �ν��Ͻ� ����
        virtualWeaponItems[item.WeaponType] = InstantiateItemPrefab(item, weaponPrefabDict);
    }

    private void UpdateEquippedArmor(ItemData item, ArmorType type)
    {
        // �ǵ�: ���� ���� �� ���ʽ� ����
        if (equippedArmors.TryGetValue(type, out var prev) && prev != null)
            defenseBonus -= prev.DefenseBonus;

        // �ǵ�: �� �� ������ ���� �� ���ʽ� �߰�
        equippedArmors[type] = item;
        defenseBonus += item.DefenseBonus;

        // �ǵ�: Prefab���� ���� Item ������Ʈ ���� �ν��Ͻ� ����
        virtualArmorItems[type] = InstantiateItemPrefab(item, armorPrefabDict);
    }

    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 8) Prefab ��ųʸ����� Instantiate �� Item ��ȯ
    //������������������������������������������������������������������������������������������������������������������������������������������������

    private Item InstantiateItemPrefab(ItemData itemData, Dictionary<string, GameObject> prefabDict)
    {
        if (prefabDict.TryGetValue(itemData.ItemId, out var prefab))
        {
            var obj = Instantiate(prefab, transform);           // Inventory �ڽ����� ��ġ
            obj.name = $"Item_{itemData.ItemId}";                // �ĺ��� �̸� ����
            var comp = obj.GetComponent<Item>() ?? obj.AddComponent<Item>();
            comp.Initialize(itemData);                           // ������ �ʱ�ȭ
            return comp;
        }
        else
        {
            Debug.LogError($"[Inventory] Prefab �̵��: {itemData.ItemId}");
            return CreateFallbackItem(itemData);
        }
    }

    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 9) Fallback: Prefab ���� �� �� GameObject�� Item ������Ʈ�� ����
    //������������������������������������������������������������������������������������������������������������������������������������������������

    private Item CreateFallbackItem(ItemData itemData)
    {
        var go = new GameObject($"VirtualItem_{itemData.ItemId}");
        go.transform.SetParent(transform);
        go.SetActive(false);
        var comp = go.AddComponent<Item>();
        comp.Initialize(itemData);
        return comp;
    }

    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 10) ��ȸ �� UI ����� ���� �޼���
    //������������������������������������������������������������������������������������������������������������������������������������������������

    /// <summary>
    /// ���� ������ Item ������Ʈ ��ȯ (Bow��Sword��Scythe �켱����)
    /// </summary>
    public Item GetCurrentWeapon()
    {
        if (virtualWeaponItems.TryGetValue(WeaponType.Bow, out var bow) && bow != null) return bow;
        if (virtualWeaponItems.TryGetValue(WeaponType.Sword, out var sword) && sword != null) return sword;
        if (virtualWeaponItems.TryGetValue(WeaponType.Scythe, out var scythe) && scythe != null) return scythe;
        return null; // ������ ���� ����
    }

    /// <summary>�� ���ݷ� ���ʽ� ��ȯ</summary>
    public float GetTotalAttackBonus() => attackBonus;
    /// <summary>�� ���� ���ʽ� ��ȯ</summary>
    public float GetTotalDefenseBonus() => defenseBonus;

    /// <summary>
    /// �κ��丮 UI �г� Ȱ��ȭ/��Ȱ��ȭ ���
    /// </summary>
    public void ToggleInventoryUI()
    {
        if (inventoryUIPanel == null)
        {
            Debug.LogWarning("[Inventory] inventoryUIPanel �̿���");
            return;
        }
        isInventoryOpen = !isInventoryOpen;
        inventoryUIPanel.SetActive(isInventoryOpen);
    }

    /// <summary>
    /// ����׿�: �ֿܼ� ������ �����۰� ���ʽ� ���� ���
    /// </summary>
    public void PrintEquippedItems()
    {
        Debug.Log("=== ���� ������ ��Ȳ ===");
        foreach (var kv in equippedWeapons)
            Debug.Log($"���� [{kv.Key}]: {(kv.Value != null ? kv.Value.ItemName : "����")}");
        foreach (var kv in equippedArmors)
            Debug.Log($"�� [{kv.Key}]: {(kv.Value != null ? kv.Value.ItemName : "����")}");
        Debug.Log($"���ݷ� ���ʽ�: +{attackBonus}, ���� ���ʽ�: +{defenseBonus}");
    }
}

