using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ÇÃ·¹ÀÌ¾îÀÇ ÀåÂø ¾ÆÀÌÅÛ(¹«±â¡¤¹æ¾î±¸)À» °ü¸®ÇÏ°í,
/// ÀåÂøµÈ ÇÁ¸®ÆÕÀ» InstantiateÇÏ¿© ½ºÅÈ º¸³Ê½º¸¦ Àû¿ëÇÏ¸ç,
/// ÇöÀç ÀåÂø ¾ÆÀÌÅÛ Á¶È¸ ¹× ÀÎº¥Åä¸® UI Åä±Û ±â´ÉÀ» Á¦°øÇÕ´Ï´Ù.
/// </summary>
public class Inventory : MonoBehaviour
{
    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // 1) Inspector ¼³Á¤ ¿µ¿ª
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

    [Header("ÀåÂø ¾ÆÀÌÅÛ Prefab ¸®½ºÆ®")]
    [Tooltip("¸ðµç ¹«±â Prefab (Item ÄÄÆ÷³ÍÆ®ÀÇ ItemId¸¦ ¼³Á¤ ÈÄ µå·¡±×&µå·Ó)")]
    [SerializeField] private List<GameObject> weaponPrefabs;
    [Tooltip("¸ðµç ¹æ¾î±¸ Prefab (Item ÄÄÆ÷³ÍÆ®ÀÇ ItemId¸¦ ¼³Á¤ ÈÄ µå·¡±×&µå·Ó)")]
    [SerializeField] private List<GameObject> armorPrefabs;

    [Header("UI ÆÐ³Î (ÀÎº¥Åä¸® Ã¢)")]
    [Tooltip("ÀÎº¥Åä¸® UI ÆÐ³ÎÀ» ¿¬°áÇÏ¼¼¿ä")]
    [SerializeField] private GameObject inventoryUIPanel;

    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // 2) ³»ºÎ »óÅÂ ÀúÀå¿ë µñ¼Å³Ê¸®
    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡

    // ID ¡æ Prefab ¸ÅÇÎ
    private Dictionary<string, GameObject> weaponPrefabDict = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> armorPrefabDict = new Dictionary<string, GameObject>();

    // ÀåÂøµÈ µ¥ÀÌÅÍ(ItemData)
    private Dictionary<WeaponType, ItemData> equippedWeapons = new Dictionary<WeaponType, ItemData>();
    private Dictionary<ArmorType, ItemData> equippedArmors = new Dictionary<ArmorType, ItemData>();

    // InstantiateµÈ Item ÄÄÆ÷³ÍÆ® ÂüÁ¶
    private Dictionary<WeaponType, Item> virtualWeaponItems = new Dictionary<WeaponType, Item>();
    private Dictionary<ArmorType, Item> virtualArmorItems = new Dictionary<ArmorType, Item>();

    // º¸³Ê½º ½ºÅÈ ÇÕ»ê
    private float attackBonus = 0f;
    private float defenseBonus = 0f;

    // ÀÎº¥Åä¸® UI Åä±Û »óÅÂ
    private bool isInventoryOpen = false;

    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // 3) Unity ÀÌº¥Æ® ÄÝ¹é
    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡

    private void Awake()
    {
        // ÀÇµµ: Awake¿¡¼­ Prefab ¸®½ºÆ®¸¦ ÀÐ¾î ID¡æPrefab ¸ÅÇÎÀ» ÃÊ±âÈ­
        InitializeWeaponPrefabDict();
        InitializeArmorPrefabDict();
    }

    private void Start()
    {
        // ÀÇµµ: Start¿¡¼­ Inspector¿¡ ÀÔ·ÂµÈ ID·Î ÃÊ±â ÀåÂø ¾ÆÀÌÅÛ ¼¼ÆÃ
        EquipSelectedItems();
    }

    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // 4) Prefab ¸®½ºÆ® ¡æ µñ¼Å³Ê¸® ÃÊ±âÈ­
    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡

    private void InitializeWeaponPrefabDict()
    {
        weaponPrefabDict.Clear();
        foreach (var prefab in weaponPrefabs)
        {
            var it = prefab.GetComponent<Item>();
            if (it != null && !string.IsNullOrEmpty(it.ItemId))
                weaponPrefabDict[it.ItemId] = prefab;
            else
                Debug.LogWarning($"[Inventory] Weapon Prefab ´©¶ô: {prefab.name}");
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
                Debug.LogWarning($"[Inventory] Armor Prefab ´©¶ô: {prefab.name}");
        }
    }

    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // 5) ÃÊ±â ÀåÂø ¼³Á¤ (Inspector ID ¡æ ÀåÂø)
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

    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // 6) ID ¡æ ItemData Á¶È¸ ÈÄ ÀåÂø ¾÷µ¥ÀÌÆ®
    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡

    private void SetupEquippedWeaponById(string weaponId)
    {
        // ÀÇµµ: ItemManager¿¡¼­ µ¥ÀÌÅÍ Á¶È¸
        var data = ItemManager.Instance.GetItemDataById(weaponId);
        if (data != null)
            UpdateEquippedWeapon(data);
        else
            Debug.LogError($"[Inventory] ¹«±â µ¥ÀÌÅÍ ¾øÀ½: {weaponId}");
    }

    private void SetupEquippedArmorById(string armorId, ArmorType type)
    {
        // ÀÇµµ: ItemManager¿¡¼­ µ¥ÀÌÅÍ Á¶È¸
        var data = ItemManager.Instance.GetItemDataById(armorId);
        if (data != null)
            UpdateEquippedArmor(data, type);
        else
            Debug.LogError($"[Inventory] ¹æ¾î±¸ µ¥ÀÌÅÍ ¾øÀ½: {armorId}");
    }

    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // 7) ½ÇÁ¦ ÀåÂø ·ÎÁ÷: º¸³Ê½º ½ºÅÈ Àû¿ë + Prefab Instantiate
    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡

    private void UpdateEquippedWeapon(ItemData item)
    {
        // ÀÇµµ: ±âÁ¸ ÀåÂø ¹«±â º¸³Ê½º Á¦°Å
        if (equippedWeapons.TryGetValue(item.WeaponType, out var prev) && prev != null)
            attackBonus -= prev.AttackBonus;

        // ÀÇµµ: »õ ¹«±â µ¥ÀÌÅÍ ÀúÀå ¹× º¸³Ê½º Ãß°¡
        equippedWeapons[item.WeaponType] = item;
        attackBonus += item.AttackBonus;

        // ÀÇµµ: Prefab¿¡¼­ ½ÇÁ¦ Item ÄÄÆ÷³ÍÆ® Æ÷ÇÔ ÀÎ½ºÅÏ½º »ý¼º
        virtualWeaponItems[item.WeaponType] = InstantiateItemPrefab(item, weaponPrefabDict);
    }

    private void UpdateEquippedArmor(ItemData item, ArmorType type)
    {
        // ÀÇµµ: ±âÁ¸ ÀåÂø ¹æ¾î±¸ º¸³Ê½º Á¦°Å
        if (equippedArmors.TryGetValue(type, out var prev) && prev != null)
            defenseBonus -= prev.DefenseBonus;

        // ÀÇµµ: »õ ¹æ¾î±¸ µ¥ÀÌÅÍ ÀúÀå ¹× º¸³Ê½º Ãß°¡
        equippedArmors[type] = item;
        defenseBonus += item.DefenseBonus;

        // ÀÇµµ: Prefab¿¡¼­ ½ÇÁ¦ Item ÄÄÆ÷³ÍÆ® Æ÷ÇÔ ÀÎ½ºÅÏ½º »ý¼º
        virtualArmorItems[type] = InstantiateItemPrefab(item, armorPrefabDict);
    }

    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // 8) Prefab µñ¼Å³Ê¸®¿¡¼­ Instantiate ÈÄ Item ¹ÝÈ¯
    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡

    private Item InstantiateItemPrefab(ItemData itemData, Dictionary<string, GameObject> prefabDict)
    {
        if (prefabDict.TryGetValue(itemData.ItemId, out var prefab))
        {
            var obj = Instantiate(prefab, transform);           // Inventory ÀÚ½ÄÀ¸·Î ¹èÄ¡
            obj.name = $"Item_{itemData.ItemId}";                // ½Äº°¿ë ÀÌ¸§ ¼³Á¤
            var comp = obj.GetComponent<Item>() ?? obj.AddComponent<Item>();
            comp.Initialize(itemData);                           // µ¥ÀÌÅÍ ÃÊ±âÈ­
            return comp;
        }
        else
        {
            Debug.LogError($"[Inventory] Prefab ¹Ìµî·Ï: {itemData.ItemId}");
            return CreateFallbackItem(itemData);
        }
    }

    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // 9) Fallback: Prefab ¾øÀ» ¶§ ºó GameObject¿¡ Item ÄÄÆ÷³ÍÆ®¸¸ ºÙÀÓ
    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡

    private Item CreateFallbackItem(ItemData itemData)
    {
        var go = new GameObject($"VirtualItem_{itemData.ItemId}");
        go.transform.SetParent(transform);
        go.SetActive(false);
        var comp = go.AddComponent<Item>();
        comp.Initialize(itemData);
        return comp;
    }

    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // 10) Á¶È¸ ¹× UI Á¦¾î¿ë °ø¿ë ¸Þ¼­µå
    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡

    /// <summary>
    /// ÇöÀç ÀåÂøµÈ Item ÄÄÆ÷³ÍÆ® ¹ÝÈ¯ (Bow¡æSword¡æScythe ¿ì¼±¼øÀ§)
    /// </summary>
    public Item GetCurrentWeapon()
    {
        if (virtualWeaponItems.TryGetValue(WeaponType.Bow, out var bow) && bow != null) return bow;
        if (virtualWeaponItems.TryGetValue(WeaponType.Sword, out var sword) && sword != null) return sword;
        if (virtualWeaponItems.TryGetValue(WeaponType.Scythe, out var scythe) && scythe != null) return scythe;
        return null; // ÀåÂøµÈ ¹«±â ¾øÀ½
    }

    /// <summary>ÃÑ °ø°Ý·Â º¸³Ê½º ¹ÝÈ¯</summary>
    public float GetTotalAttackBonus() => attackBonus;
    /// <summary>ÃÑ ¹æ¾î·Â º¸³Ê½º ¹ÝÈ¯</summary>
    public float GetTotalDefenseBonus() => defenseBonus;

    /// <summary>
    /// ÀÎº¥Åä¸® UI ÆÐ³Î È°¼ºÈ­/ºñÈ°¼ºÈ­ Åä±Û
    /// </summary>
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

    /// <summary>
    /// µð¹ö±×¿ë: ÄÜ¼Ö¿¡ ÀåÂøµÈ ¾ÆÀÌÅÛ°ú º¸³Ê½º ½ºÅÈ Ãâ·Â
    /// </summary>
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

