using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 1) Inspector ���� �ʵ�
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

    [Header("���� Prefab ����Ʈ (Inspector ���)")]
    [Tooltip("��� ���� Prefab�� �巡��&����ϰ�, �� Prefab�� ItemId�� �����ϼ���.")]
    [SerializeField] private List<GameObject> weaponPrefabs;

    [Header("�� Prefab ����Ʈ (Inspector ���)")]
    [Tooltip("��� �� Prefab�� �巡��&����ϰ�, �� Prefab�� ItemId�� �����ϼ���.")]
    [SerializeField] private List<GameObject> armorPrefabs;

    [Header("�κ��丮 UI �г�")]
    [Tooltip("�κ��丮 â���� ����� UI �г��� �������ּ���.")]
    [SerializeField] private GameObject inventoryUIPanel;

    [Header("�κ��丮 ���� ��ư��")]
    [Tooltip("���ŵ� �������� ǥ���� Button ����Ʈ (Inspector ����)")]
    [SerializeField] private List<Button> itemSlots;

    [Header("�÷��̾� ���")]
    [Tooltip("���� ���� ���� ���")]
    [SerializeField] private int gold = 0;

    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 2) ���� ���� ����� ���� �� ��ųʸ�
    //������������������������������������������������������������������������������������������������������������������������������������������������

    // ID �� Prefab ����
    private Dictionary<string, GameObject> weaponPrefabDict = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> armorPrefabDict = new Dictionary<string, GameObject>();

    // ������ ������(ItemData)
    private Dictionary<WeaponType, ItemData> equippedWeapons = new Dictionary<WeaponType, ItemData>();
    private Dictionary<ArmorType, ItemData> equippedArmors = new Dictionary<ArmorType, ItemData>();

    // ���� Prefab ������ ���� (Instantiate�� Player �ʿ��� ó��)
    private Dictionary<WeaponType, GameObject> equippedWeaponPrefabs = new Dictionary<WeaponType, GameObject>();
    // �� �ν��Ͻ��� Player �� ó��

    // ���ʽ� ���� �ջ�
    private float attackBonus = 0f;
    private float defenseBonus = 0f;

    // UI ���� ����
    private bool isInventoryOpen = false;

    /// <summary>
    /// ���ŵ� ������ ID�� �����մϴ�.
    /// </summary>
    private List<string> ownedItemIds = new List<string>();

    /// <summary>
    /// ��尡 ����� �� �����ڿ��� �˸��� �ݴϴ�.
    /// </summary>
    public event Action<int> OnGoldChanged;

    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 3) Unity �����ֱ� �ݹ�
    //������������������������������������������������������������������������������������������������������������������������������������������������

    private void Awake()
    {
        // (A) ����� ��带 �ҷ��ɴϴ�.
        LoadGold();

        // (B) Inspector�� ��ӵ� Prefab���� ID��Prefab ��ųʸ��� ä���ֱ�
        weaponPrefabDict.Clear();
        foreach (var prefab in weaponPrefabs)
        {
            var it = prefab.GetComponent<Item>();
            if (it != null && !string.IsNullOrEmpty(it.ItemId))
                weaponPrefabDict[it.ItemId] = prefab;
            else
                Debug.LogWarning($"[Inventory] Weapon Prefab ���� �Ǵ� ItemId �̼���: {prefab.name}");
        }

        armorPrefabDict.Clear();
        foreach (var prefab in armorPrefabs)
        {
            var it = prefab.GetComponent<Item>();
            if (it != null && !string.IsNullOrEmpty(it.ItemId))
                armorPrefabDict[it.ItemId] = prefab;
            else
                Debug.LogWarning($"[Inventory] Armor Prefab ���� �Ǵ� ItemId �̼���: {prefab.name}");
        }

        // (C) ��� WeaponType/ArmorType Ű �ʱ�ȭ
        foreach (WeaponType wt in Enum.GetValues(typeof(WeaponType)))
            if (wt != WeaponType.None)
                equippedWeaponPrefabs[wt] = null;
        foreach (ArmorType at in Enum.GetValues(typeof(ArmorType)))
            if (at != ArmorType.None)
                equippedArmors[at] = null;
    }

    private void Start()
    {
        // ���� ���� ���� �缳��
        EquipSelectedItems();
        // UI ����
        RefreshUI();
    }

    private void Update()
    {
        // I Ű�� �κ��丮 ���
        if (Input.GetKeyDown(KeyCode.I) && inventoryUIPanel != null)
        {
            ToggleInventoryUI();
        }
    }

    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 4) ��� ����/�ҷ����� �޼���
    //������������������������������������������������������������������������������������������������������������������������������������������������

    private void LoadGold()
    {
        gold = PlayerPrefs.GetInt("PlayerGold", 0);
    }

    private void SaveGold()
    {
        PlayerPrefs.SetInt("PlayerGold", gold);
        PlayerPrefs.Save();
    }

    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 5) ��� ���ۿ� ���� �޼���
    //������������������������������������������������������������������������������������������������������������������������������������������������

    /// <summary>��带 ������ŵ�ϴ�. (��: ���� ���, ����Ʈ ����)</summary>
    public void AddGold(int amount)
    {
        if (amount <= 0) return;
        gold += amount;
        SaveGold();
        OnGoldChanged?.Invoke(gold);
    }

    /// <summary>��带 ���(����)�մϴ�. ���� ���� ��.</summary>
    public bool SpendGold(int amount)
    {
        if (amount <= 0) return true;
        if (gold < amount) return false;
        gold -= amount;
        OnGoldChanged?.Invoke(gold);
        return true;
    }

    /// <summary>���� ��� ������ ��ȯ�մϴ�.</summary>
    public int GetGold() => gold;

    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 6) ���ŵ� ������ ���� �� UI ����
    //������������������������������������������������������������������������������������������������������������������������������������������������

    /// <summary>
    /// �� ������ �߰� �� �κ��丮 UI ����.
    /// </summary>
    public void AddOwnedItem(string itemId)
    {
        if (!ownedItemIds.Contains(itemId))
        {
            ownedItemIds.Add(itemId);
            RefreshUI();
        }
    }

    /// <summary>
    /// �κ��丮 ���� UI�� ownedItemIds �������� �����մϴ�.
    /// </summary>
    private void RefreshUI()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            var btn = itemSlots[i];
            var icon = btn.transform.Find("Icon").GetComponent<Image>();
            var label = btn.transform.Find("Label").GetComponent<Text>();

            if (i < ownedItemIds.Count)
            {
                string id = ownedItemIds[i];
                var data = ItemManager.Instance.GetItemDataById(id);
                if (data != null)
                {
                    icon.sprite = data.ItemIcon;
                    icon.enabled = true;
                    label.text = data.ItemName;
                    btn.onClick.RemoveAllListeners();
                    btn.onClick.AddListener(() => EquipItemById(id));
                }
            }
            else
            {
                icon.enabled = false;
                label.text = string.Empty;
                btn.onClick.RemoveAllListeners();
            }
        }
    }

    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 7) �ʱ� ���� (ID �� ItemData �� ����/Instantiate ����)
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

    public void EquipItemById(string itemId)
    {
        // ���� ����
        if (weaponPrefabDict.ContainsKey(itemId))
            SetupEquippedWeaponById(itemId);
        // �� ����
        else if (armorPrefabDict.ContainsKey(itemId))
            SetupEquippedArmorById(itemId, DetermineArmorType(itemId));
    }

    private ArmorType DetermineArmorType(string itemId)
    {
        var data = ItemManager.Instance.GetItemDataById(itemId);
        return data != null ? data.ArmorType : ArmorType.None;
    }

    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 8) ���ʽ� ���� ���� + Prefab ����/Instantiate
    //������������������������������������������������������������������������������������������������������������������������������������������������

    private void SetupEquippedWeaponById(string weaponId)
    {
        var data = ItemManager.Instance.GetItemDataById(weaponId);
        if (data != null)
            UpdateEquippedWeapon(data);
        else
            Debug.LogError($"[Inventory] ���� ������ ����: {weaponId}");
    }

    private void SetupEquippedArmorById(string armorId, ArmorType type)
    {
        var data = ItemManager.Instance.GetItemDataById(armorId);
        if (data != null)
            UpdateEquippedArmor(data, type);
        else
            Debug.LogError($"[Inventory] �� ������ ����: {armorId}");
    }

    private void UpdateEquippedWeapon(ItemData item)
    {
        // ���� ���� ���� ���ʽ� ����
        if (equippedWeapons.TryGetValue(item.WeaponType, out var prev) && prev != null)
            attackBonus -= prev.AttackBonus;
        // �� ���� ������ ���� �� ���ʽ� �߰�
        equippedWeapons[item.WeaponType] = item;
        attackBonus += item.AttackBonus;
        // Prefab ����
        if (weaponPrefabDict.TryGetValue(item.ItemId, out var prefab))
            equippedWeaponPrefabs[item.WeaponType] = prefab;
        else
            Debug.LogWarning($"[Inventory] ���ε� Weapon Prefab ����: {item.ItemId}");
    }

    private void UpdateEquippedArmor(ItemData item, ArmorType type)
    {
        // ���� ���� �� ���ʽ� ����
        if (equippedArmors.TryGetValue(type, out var prev) && prev != null)
            defenseBonus -= prev.DefenseBonus;
        // �� �� ������ ���� �� ���ʽ� �߰�
        equippedArmors[type] = item;
        defenseBonus += item.DefenseBonus;
        // Instantiate�Ͽ� �ʱ�ȭ (Player ������ �Űܵ� ����)
        if (armorPrefabDict.TryGetValue(item.ItemId, out var prefab))
        {
            var obj = Instantiate(prefab, transform);
            obj.name = $"Armor_{item.ItemId}";
            var comp = obj.GetComponent<Item>() ?? obj.AddComponent<Item>();
            comp.Initialize(item);
        }
        else
            Debug.LogWarning($"[Inventory] ���ε� Armor Prefab ����: {item.ItemId}");
    }

    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 9) ��ȸ�� �޼��� �� UI ���
    //������������������������������������������������������������������������������������������������������������������������������������������������

    /// <summary>
    /// ���Ž� ȣ�� ����: Player.SetWeapon() ��� ����ϴ� �޼���.
    /// ���� ������ ���� Item ������Ʈ�� ��ȯ�մϴ�.
    /// </summary>
    public Item GetCurrentWeapon()
    {
        var prefab = GetCurrentWeaponPrefab();
        return prefab != null ? prefab.GetComponent<Item>() : null;
    }

    /// <summary>
    /// ���� ������ ���� Prefab(GameObject)�� �켱���� Bow��Sword��Scythe�� ��ȯ�մϴ�.
    /// </summary>
    public GameObject GetCurrentWeaponPrefab()
    {
        if (equippedWeaponPrefabs.TryGetValue(WeaponType.Bow, out var bow) && bow != null) return bow;
        if (equippedWeaponPrefabs.TryGetValue(WeaponType.Sword, out var sword) && sword != null) return sword;
        if (equippedWeaponPrefabs.TryGetValue(WeaponType.Scythe, out var scythe) && scythe != null) return scythe;
        return null;
    }

    /// <summary>�� ���ݷ� ���ʽ� ��ȯ</summary>
    public float GetTotalAttackBonus() => attackBonus;
    /// <summary>�� ���� ���ʽ� ��ȯ</summary>
    public float GetTotalDefenseBonus() => defenseBonus;

    /// <summary>
    /// �κ��丮 UI �г� Ȱ��ȭ/��Ȱ��ȭ ��� (I Ű��)
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
        if (isInventoryOpen)
            RefreshUI();
    }

    /// <summary>����׿�: ���� ��Ȳ �� ���ʽ� ���� ���</summary>
    public void PrintEquippedItems()
    {
        Debug.Log("=== ���� ������ ��Ȳ ===");
        foreach (var kv in equippedWeapons)
            Debug.Log($"���� [{kv.Key}]: {(kv.Value != null ? kv.Value.ItemName : "����")} ");
        foreach (var kv in equippedArmors)
            Debug.Log($"�� [{kv.Key}]: {(kv.Value != null ? kv.Value.ItemName : "����")} ");
        Debug.Log($"���ݷ� ���ʽ�: +{attackBonus}, ���� ���ʽ�: +{defenseBonus}");
    }
}