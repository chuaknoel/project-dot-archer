using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// �÷��̾��� ���� ������(���⡤��)�� �����մϴ�.
/// - ���� �� �� Prefab�� Inspector���� �巡��&������� ���  
/// - �κ��丮�� Prefab�� Instantiate���� �ʰ�, �ܼ��� ���θ� ����  
/// - Player.SetWeapon() ���� �ܺ� ������ Instantiate �� �θ� ������ å����  
/// - �ɷ�ġ ���, ��ȸ�� GetCurrentWeapon(), UI ��� ����� �״�� ����  
/// </summary>
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

    // ���� Inventory���� Instantiate�� Item ������Ʈ ����
    private Dictionary<ArmorType, Item> equippedArmorInstances = new Dictionary<ArmorType, Item>();

    // ���ʽ� ���� �ջ�
    private float attackBonus = 0f;
    private float defenseBonus = 0f;

    // UI ��� ����
    private bool isInventoryOpen = false;


    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 3) Unity �����ֱ� �ݹ�
    //������������������������������������������������������������������������������������������������������������������������������������������������

    private void Awake()
    {
        // (1) Inspector�� ��ӵ� Prefab���� ID��Prefab ��ųʸ��� ä���ֱ�
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

        // (2) ��� WeaponType/ArmorType Ű �ʱ�ȭ
        foreach (WeaponType wt in System.Enum.GetValues(typeof(WeaponType)))
            if (wt != WeaponType.None)
                equippedWeaponPrefabs[wt] = null;

        foreach (ArmorType at in System.Enum.GetValues(typeof(ArmorType)))
            if (at != ArmorType.None)
                equippedArmorInstances[at] = null;
    }

    private void Start()
    {
        // Inspector�� �Էµ� ID�� �ʱ� ���� ����
        //EquipSelectedItems();
    }


    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 4) �ʱ� ���� (ID �� ItemData �� ����/Instantiate ����)
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


    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 5) ���ʽ� ���� ���� + Prefab ����/Instantiate
    //������������������������������������������������������������������������������������������������������������������������������������������������

    private void UpdateEquippedWeapon(ItemData item)
    {
        // (1) ���� ���� ���� ���ʽ� ����
        if (equippedWeapons.TryGetValue(item.WeaponType, out var prev) && prev != null)
            attackBonus -= prev.AttackBonus;

        // (2) �� ���� ������ ���� �� ���ʽ� �߰�
        equippedWeapons[item.WeaponType] = item;
        attackBonus += item.AttackBonus;

        // (3) ���� Prefab�� Instantiate���� �ʰ� ���θ�
        if (weaponPrefabDict.TryGetValue(item.ItemId, out var prefab))
            equippedWeaponPrefabs[item.WeaponType] = prefab;
        else
            Debug.LogWarning($"[Inventory] ���ε� Weapon Prefab ����: {item.ItemId}");
    }

    private void UpdateEquippedArmor(ItemData item, ArmorType type)
    {
        // (1) ���� ���� �� ���ʽ� ����
        if (equippedArmors.TryGetValue(type, out var prev) && prev != null)
            defenseBonus -= prev.DefenseBonus;

        // (2) �� �� ������ ���� �� ���ʽ� �߰�
        equippedArmors[type] = item;
        defenseBonus += item.DefenseBonus;

        // (3) ���� Inventory���� Instantiate
        if (armorPrefabDict.TryGetValue(item.ItemId, out var prefab))
        {
            var obj = Instantiate(prefab, transform);
            obj.name = $"Armor_{item.ItemId}";
            var comp = obj.GetComponent<Item>() ?? obj.AddComponent<Item>();
            comp.Initialize(item);
            equippedArmorInstances[type] = comp;
        }
        else
        {
            Debug.LogWarning($"[Inventory] ���ε� Armor Prefab ����: {item.ItemId}");
        }
    }


    //������������������������������������������������������������������������������������������������������������������������������������������������
    // 6) ��ȸ�� �޼��� �� UI ���
    //������������������������������������������������������������������������������������������������������������������������������������������������

    /// <summary>
    /// ���Ž� ȣ�� ����: Player.SetWeapon() ��� ����ϴ� �޼���.
    /// ���� ������ ���� Item ������Ʈ�� ��ȯ�մϴ�.
    /// </summary>
    public Item GetCurrentWeapon()
    {
        // ���ο� ���ε� Prefab(GameObject)���� Item ������Ʈ�� ������ ��ȯ
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

    /// <summary>�κ��丮 UI �г� Ȱ��ȭ/��Ȱ��ȭ ���</summary>
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

    /// <summary>����׿�: ���� ��Ȳ �� ���ʽ� ���� ���</summary>
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
