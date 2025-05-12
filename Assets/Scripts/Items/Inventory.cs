using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ÇÃ·¹ÀÌ¾îÀÇ ÀåÂø ¾ÆÀÌÅÛ(¹«±â¡¤¹æ¾î±¸)À» °ü¸®ÇÕ´Ï´Ù.
/// - ¹«±â ¡¤ ¹æ¾î±¸ PrefabÀº Inspector¿¡¼­ µå·¡±×&µå·ÓÀ¸·Î µî·Ï  
/// - ÀÎº¥Åä¸®´Â PrefabÀ» InstantiateÇÏÁö ¾Ê°í, ´Ü¼øÈ÷ ¸ÅÇÎ¸¸ ¼öÇà  
/// - Player.SetWeapon() °°Àº ¿ÜºÎ ·ÎÁ÷ÀÌ Instantiate ¹× ºÎ¸ð ¼³Á¤À» Ã¥ÀÓÁü  
/// - ´É·ÂÄ¡ °è»ê, Á¶È¸¿ë GetCurrentWeapon(), UI Åä±Û ±â´ÉÀº ±×´ë·Î À¯Áö  
/// </summary>
public class Inventory : MonoBehaviour
{
    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // 1) Inspector ¼³Á¤ ÇÊµå
    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡

    [Header("ÀåÂøÇÒ ¾ÆÀÌÅÛ ID (Inspector¿¡ ÀÔ·Â)")]
    [Tooltip("ÀåÂøÇÒ ¹«±â ID (¿¹: bow_common)")]
    [SerializeField] private string equippedWeaponId = "bow_common";
    [Tooltip("ÀåÂøÇÒ Åõ±¸ ID (¿¹: helmet_common)")]
    [SerializeField] private string equippedHelmetId = "helmet_common";
    [Tooltip("ÀåÂøÇÒ °©¿Ê ID (¿¹: armor_common)")]
    [SerializeField] private string equippedArmorId = "armor_common";
    [Tooltip("ÀåÂøÇÒ ½Å¹ß ID (¿¹: boots_common)")]
    [SerializeField] private string equippedBootsId = "boots_common";

    [Header("¹«±â Prefab ¸®½ºÆ® (Inspector µî·Ï)")]
    [Tooltip("¸ðµç ¹«±â PrefabÀ» µå·¡±×&µå·ÓÇÏ°í, °¢ PrefabÀÇ ItemId¸¦ ¼³Á¤ÇÏ¼¼¿ä.")]
    [SerializeField] private List<GameObject> weaponPrefabs;

    [Header("¹æ¾î±¸ Prefab ¸®½ºÆ® (Inspector µî·Ï)")]
    [Tooltip("¸ðµç ¹æ¾î±¸ PrefabÀ» µå·¡±×&µå·ÓÇÏ°í, °¢ PrefabÀÇ ItemId¸¦ ¼³Á¤ÇÏ¼¼¿ä.")]
    [SerializeField] private List<GameObject> armorPrefabs;

    [Header("ÀÎº¥Åä¸® UI ÆÐ³Î")]
    [Tooltip("ÀÎº¥Åä¸® Ã¢À¸·Î »ç¿ëÇÒ UI ÆÐ³ÎÀ» ¿¬°áÇØÁÖ¼¼¿ä.")]
    [SerializeField] private GameObject inventoryUIPanel;


    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // 2) ³»ºÎ »óÅÂ ÀúÀå¿ë º¯¼ö ¹× µñ¼Å³Ê¸®
    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡

    // ID ¡æ Prefab ¸ÅÇÎ
    private Dictionary<string, GameObject> weaponPrefabDict = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> armorPrefabDict = new Dictionary<string, GameObject>();

    // ÀåÂøµÈ µ¥ÀÌÅÍ(ItemData)
    private Dictionary<WeaponType, ItemData> equippedWeapons = new Dictionary<WeaponType, ItemData>();
    private Dictionary<ArmorType, ItemData> equippedArmors = new Dictionary<ArmorType, ItemData>();

    // ¹«±â Prefab ÂüÁ¶¸¸ ÀúÀå (Instantiate´Â Player ÂÊ¿¡¼­ Ã³¸®)
    private Dictionary<WeaponType, GameObject> equippedWeaponPrefabs = new Dictionary<WeaponType, GameObject>();

    // ¹æ¾î±¸´Â Inventory¿¡¼­ InstantiateÇÑ Item ÄÄÆ÷³ÍÆ® ÂüÁ¶
    private Dictionary<ArmorType, Item> equippedArmorInstances = new Dictionary<ArmorType, Item>();

    // º¸³Ê½º ½ºÅÈ ÇÕ»ê
    private float attackBonus = 0f;
    private float defenseBonus = 0f;

    // UI Åä±Û »óÅÂ
    private bool isInventoryOpen = false;


    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // 3) Unity »ý¸íÁÖ±â ÄÝ¹é
    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡

    private void Awake()
    {
        // (1) Inspector¿¡ µå·ÓµÈ PrefabµéÀ» ID¡æPrefab µñ¼Å³Ê¸®¿¡ Ã¤¿ö³Ö±â
        weaponPrefabDict.Clear();
        foreach (var prefab in weaponPrefabs)
        {
            var it = prefab.GetComponent<Item>();
            if (it != null && !string.IsNullOrEmpty(it.ItemId))
                weaponPrefabDict[it.ItemId] = prefab;
            else
                Debug.LogWarning($"[Inventory] Weapon Prefab ´©¶ô ¶Ç´Â ItemId ¹Ì¼³Á¤: {prefab.name}");
        }

        armorPrefabDict.Clear();
        foreach (var prefab in armorPrefabs)
        {
            var it = prefab.GetComponent<Item>();
            if (it != null && !string.IsNullOrEmpty(it.ItemId))
                armorPrefabDict[it.ItemId] = prefab;
            else
                Debug.LogWarning($"[Inventory] Armor Prefab ´©¶ô ¶Ç´Â ItemId ¹Ì¼³Á¤: {prefab.name}");
        }

        // (2) ¸ðµç WeaponType/ArmorType Å° ÃÊ±âÈ­
        foreach (WeaponType wt in System.Enum.GetValues(typeof(WeaponType)))
            if (wt != WeaponType.None)
                equippedWeaponPrefabs[wt] = null;

        foreach (ArmorType at in System.Enum.GetValues(typeof(ArmorType)))
            if (at != ArmorType.None)
                equippedArmorInstances[at] = null;
    }

    private void Start()
    {
        // Inspector¿¡ ÀÔ·ÂµÈ ID·Î ÃÊ±â ÀåÂø ½ÇÇà
        //EquipSelectedItems();
    }


    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // 4) ÃÊ±â ÀåÂø (ID ¡æ ItemData ¡æ ¸ÅÇÎ/Instantiate °»½Å)
    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡

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
            Debug.LogError($"[Inventory] ¹«±â µ¥ÀÌÅÍ ¾øÀ½: {weaponId}");
    }

    private void SetupEquippedArmorById(string armorId, ArmorType type)
    {
        var data = ItemManager.Instance.GetItemDataById(armorId);
        if (data != null)
            UpdateEquippedArmor(data, type);
        else
            Debug.LogError($"[Inventory] ¹æ¾î±¸ µ¥ÀÌÅÍ ¾øÀ½: {armorId}");
    }


    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // 5) º¸³Ê½º ½ºÅÈ Àû¿ë + Prefab ¸ÅÇÎ/Instantiate
    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡

    private void UpdateEquippedWeapon(ItemData item)
    {
        // (1) ±âÁ¸ ÀåÂø ¹«±â º¸³Ê½º Á¦°Å
        if (equippedWeapons.TryGetValue(item.WeaponType, out var prev) && prev != null)
            attackBonus -= prev.AttackBonus;

        // (2) »õ ¹«±â µ¥ÀÌÅÍ ÀúÀå ¹× º¸³Ê½º Ãß°¡
        equippedWeapons[item.WeaponType] = item;
        attackBonus += item.AttackBonus;

        // (3) ¹«±â PrefabÀº InstantiateÇÏÁö ¾Ê°í ¸ÅÇÎ¸¸
        if (weaponPrefabDict.TryGetValue(item.ItemId, out var prefab))
            equippedWeaponPrefabs[item.WeaponType] = prefab;
        else
            Debug.LogWarning($"[Inventory] ¸ÅÇÎµÈ Weapon Prefab ¾øÀ½: {item.ItemId}");
    }

    private void UpdateEquippedArmor(ItemData item, ArmorType type)
    {
        // (1) ±âÁ¸ ÀåÂø ¹æ¾î±¸ º¸³Ê½º Á¦°Å
        if (equippedArmors.TryGetValue(type, out var prev) && prev != null)
            defenseBonus -= prev.DefenseBonus;

        // (2) »õ ¹æ¾î±¸ µ¥ÀÌÅÍ ÀúÀå ¹× º¸³Ê½º Ãß°¡
        equippedArmors[type] = item;
        defenseBonus += item.DefenseBonus;

        // (3) ¹æ¾î±¸´Â Inventory¿¡¼­ Instantiate
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
            Debug.LogWarning($"[Inventory] ¸ÅÇÎµÈ Armor Prefab ¾øÀ½: {item.ItemId}");
        }
    }


    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // 6) Á¶È¸¿ë ¸Þ¼­µå ¹× UI Åä±Û
    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡

    /// <summary>
    /// ·¹°Å½Ã È£Ãâ Áö¿ø: Player.SetWeapon() µî¿¡¼­ »ç¿ëÇÏ´Â ¸Þ¼­µå.
    /// ÇöÀç ÀåÂøµÈ ¹«±â Item ÄÄÆ÷³ÍÆ®¸¦ ¹ÝÈ¯ÇÕ´Ï´Ù.
    /// </summary>
    public Item GetCurrentWeapon()
    {
        // ³»ºÎ¿¡ ¸ÅÇÎµÈ Prefab(GameObject)¿¡¼­ Item ÄÄÆ÷³ÍÆ®¸¦ ²¨³»¼­ ¹ÝÈ¯
        var prefab = GetCurrentWeaponPrefab();
        return prefab != null ? prefab.GetComponent<Item>() : null;
    }

    /// <summary>
    /// ÇöÀç ÀåÂøµÈ ¹«±â Prefab(GameObject)À» ¿ì¼±¼øÀ§ Bow¡æSword¡æScythe·Î ¹ÝÈ¯ÇÕ´Ï´Ù.
    /// </summary>
    public GameObject GetCurrentWeaponPrefab()
    {
        if (equippedWeaponPrefabs.TryGetValue(WeaponType.Bow, out var bow) && bow != null) return bow;
        if (equippedWeaponPrefabs.TryGetValue(WeaponType.Sword, out var sword) && sword != null) return sword;
        if (equippedWeaponPrefabs.TryGetValue(WeaponType.Scythe, out var scythe) && scythe != null) return scythe;
        return null;
    }

    /// <summary>ÃÑ °ø°Ý·Â º¸³Ê½º ¹ÝÈ¯</summary>
    public float GetTotalAttackBonus() => attackBonus;
    /// <summary>ÃÑ ¹æ¾î·Â º¸³Ê½º ¹ÝÈ¯</summary>
    public float GetTotalDefenseBonus() => defenseBonus;

    /// <summary>ÀÎº¥Åä¸® UI ÆÐ³Î È°¼ºÈ­/ºñÈ°¼ºÈ­ Åä±Û</summary>
    public void ToggleInventoryUI()
    {
        if (inventoryUIPanel == null)
        {
            Debug.LogWarning("[Inventory] inventoryUIPanel ¹Ì¿¬°á");
            return;
        }
        isInventoryOpen = !isInventoryOpen;
        inventoryUIPanel.SetActive(isInventoryOpen);
    }

    /// <summary>µð¹ö±×¿ë: ÀåÂø ÇöÈ² ¹× º¸³Ê½º ½ºÅÈ Ãâ·Â</summary>
    public void PrintEquippedItems()
    {
        Debug.Log("=== ÀåÂø ¾ÆÀÌÅÛ ÇöÈ² ===");
        foreach (var kv in equippedWeapons)
            Debug.Log($"¹«±â [{kv.Key}]: {(kv.Value != null ? kv.Value.ItemName : "¾øÀ½")}");
        foreach (var kv in equippedArmors)
            Debug.Log($"¹æ¾î±¸ [{kv.Key}]: {(kv.Value != null ? kv.Value.ItemName : "¾øÀ½")}");
        Debug.Log($"°ø°Ý·Â º¸³Ê½º: +{attackBonus}, ¹æ¾î·Â º¸³Ê½º: +{defenseBonus}");
    }
}
