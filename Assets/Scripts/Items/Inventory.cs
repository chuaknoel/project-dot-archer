using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("ÀÎº¥Åä¸® ½½·Ô ¹öÆ°µé")]
    [Tooltip("±¸¸ÅµÈ ¾ÆÀÌÅÛÀ» Ç¥½ÃÇÒ Button ¸®½ºÆ® (Inspector ¿¬°á)")]
    [SerializeField] private List<Button> itemSlots;

    [Header("ÇÃ·¹ÀÌ¾î °ñµå")]
    [Tooltip("ÇöÀç º¸À¯ ÁßÀÎ °ñµå")]
    [SerializeField] private int gold = 0;

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
    // ¹æ¾î±¸ ÀÎ½ºÅÏ½º´Â Player ÂÊ Ã³¸®

    // º¸³Ê½º ½ºÅÈ ÇÕ»ê
    private float attackBonus = 0f;
    private float defenseBonus = 0f;

    // UI ¿­¸² »óÅÂ
    private bool isInventoryOpen = false;

    /// <summary>
    /// ±¸¸ÅµÈ ¾ÆÀÌÅÛ ID¸¦ ÃßÀûÇÕ´Ï´Ù.
    /// </summary>
    private List<string> ownedItemIds = new List<string>();

    /// <summary>
    /// °ñµå°¡ º¯°æµÉ ¶§ ±¸µ¶ÀÚ¿¡°Ô ¾Ë¸²À» ÁÝ´Ï´Ù.
    /// </summary>
    public event Action<int> OnGoldChanged;

    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // 3) Unity »ý¸íÁÖ±â ÄÝ¹é
    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡

    private void Awake()
    {
        // (A) ÀúÀåµÈ °ñµå¸¦ ºÒ·¯¿É´Ï´Ù.
        LoadGold();

        // (B) Inspector¿¡ µå·ÓµÈ PrefabµéÀ» ID¡æPrefab µñ¼Å³Ê¸®¿¡ Ã¤¿ö³Ö±â
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

        // (C) ¸ðµç WeaponType/ArmorType Å° ÃÊ±âÈ­
        foreach (WeaponType wt in Enum.GetValues(typeof(WeaponType)))
            if (wt != WeaponType.None)
                equippedWeaponPrefabs[wt] = null;
        foreach (ArmorType at in Enum.GetValues(typeof(ArmorType)))
            if (at != ArmorType.None)
                equippedArmors[at] = null;
    }

    private void Start()
    {
        // ±âÁ¸ ÀåÂø »çÇ× Àç¼³Á¤
        EquipSelectedItems();
        // UI °»½Å
        RefreshUI();
    }

    private void Update()
    {
        // I Å°·Î ÀÎº¥Åä¸® Åä±Û
        if (Input.GetKeyDown(KeyCode.I) && inventoryUIPanel != null)
        {
            ToggleInventoryUI();
        }
    }

    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // 4) °ñµå ÀúÀå/ºÒ·¯¿À±â ¸Þ¼­µå
    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡

    private void LoadGold()
    {
        gold = PlayerPrefs.GetInt("PlayerGold", 0);
    }

    private void SaveGold()
    {
        PlayerPrefs.SetInt("PlayerGold", gold);
        PlayerPrefs.Save();
    }

    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // 5) °ñµå Á¶ÀÛ¿ë °ø¿ë ¸Þ¼­µå
    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡

    /// <summary>°ñµå¸¦ Áõ°¡½ÃÅµ´Ï´Ù. (¿¹: ¸ó½ºÅÍ µå¶ø, Äù½ºÆ® º¸»ó)</summary>
    public void AddGold(int amount)
    {
        if (amount <= 0) return;
        gold += amount;
        SaveGold();
        OnGoldChanged?.Invoke(gold);
    }

    /// <summary>°ñµå¸¦ »ç¿ë(Â÷°¨)ÇÕ´Ï´Ù. »óÁ¡ ±¸¸Å µî.</summary>
    public bool SpendGold(int amount)
    {
        if (amount <= 0) return true;
        if (gold < amount) return false;
        gold -= amount;
        OnGoldChanged?.Invoke(gold);
        return true;
    }

    /// <summary>ÇöÀç °ñµå ¼ö·®À» ¹ÝÈ¯ÇÕ´Ï´Ù.</summary>
    public int GetGold() => gold;

    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // 6) ±¸¸ÅµÈ ¾ÆÀÌÅÛ °ü¸® ¹× UI °»½Å
    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡

    /// <summary>
    /// »õ ¾ÆÀÌÅÛ Ãß°¡ ÈÄ ÀÎº¥Åä¸® UI °»½Å.
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
    /// ÀÎº¥Åä¸® ½½·Ô UI¸¦ ownedItemIds ±âÁØÀ¸·Î °»½ÅÇÕ´Ï´Ù.
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

    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // 7) ÃÊ±â ÀåÂø (ID ¡æ ItemData ¡æ ¸ÅÇÎ/Instantiate °»½Å)
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

    public void EquipItemById(string itemId)
    {
        // ¹«±â ÀåÂø
        if (weaponPrefabDict.ContainsKey(itemId))
            SetupEquippedWeaponById(itemId);
        // ¹æ¾î±¸ ÀåÂø
        else if (armorPrefabDict.ContainsKey(itemId))
            SetupEquippedArmorById(itemId, DetermineArmorType(itemId));
    }

    private ArmorType DetermineArmorType(string itemId)
    {
        var data = ItemManager.Instance.GetItemDataById(itemId);
        return data != null ? data.ArmorType : ArmorType.None;
    }

    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // 8) º¸³Ê½º ½ºÅÈ Àû¿ë + Prefab ¸ÅÇÎ/Instantiate
    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡

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

    private void UpdateEquippedWeapon(ItemData item)
    {
        // ÀÌÀü ÀåÂø ¹«±â º¸³Ê½º Á¦°Å
        if (equippedWeapons.TryGetValue(item.WeaponType, out var prev) && prev != null)
            attackBonus -= prev.AttackBonus;
        // »õ ¹«±â µ¥ÀÌÅÍ ÀúÀå ¹× º¸³Ê½º Ãß°¡
        equippedWeapons[item.WeaponType] = item;
        attackBonus += item.AttackBonus;
        // Prefab ¸ÅÇÎ
        if (weaponPrefabDict.TryGetValue(item.ItemId, out var prefab))
            equippedWeaponPrefabs[item.WeaponType] = prefab;
        else
            Debug.LogWarning($"[Inventory] ¸ÅÇÎµÈ Weapon Prefab ¾øÀ½: {item.ItemId}");
    }

    private void UpdateEquippedArmor(ItemData item, ArmorType type)
    {
        // ÀÌÀü ÀåÂø ¹æ¾î±¸ º¸³Ê½º Á¦°Å
        if (equippedArmors.TryGetValue(type, out var prev) && prev != null)
            defenseBonus -= prev.DefenseBonus;
        // »õ ¹æ¾î±¸ µ¥ÀÌÅÍ ÀúÀå ¹× º¸³Ê½º Ãß°¡
        equippedArmors[type] = item;
        defenseBonus += item.DefenseBonus;
        // InstantiateÇÏ¿© ÃÊ±âÈ­ (Player ÂÊÀ¸·Î ¿Å°Üµµ ¹«¹æ)
        if (armorPrefabDict.TryGetValue(item.ItemId, out var prefab))
        {
            var obj = Instantiate(prefab, transform);
            obj.name = $"Armor_{item.ItemId}";
            var comp = obj.GetComponent<Item>() ?? obj.AddComponent<Item>();
            comp.Initialize(item);
        }
        else
            Debug.LogWarning($"[Inventory] ¸ÅÇÎµÈ Armor Prefab ¾øÀ½: {item.ItemId}");
    }

    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡
    // 9) Á¶È¸¿ë ¸Þ¼­µå ¹× UI Åä±Û
    //¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡¦¡

    /// <summary>
    /// ·¹°Å½Ã È£Ãâ Áö¿ø: Player.SetWeapon() µî¿¡¼­ »ç¿ëÇÏ´Â ¸Þ¼­µå.
    /// ÇöÀç ÀåÂøµÈ ¹«±â Item ÄÄÆ÷³ÍÆ®¸¦ ¹ÝÈ¯ÇÕ´Ï´Ù.
    /// </summary>
    public Item GetCurrentWeapon()
    {
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

    /// <summary>
    /// ÀÎº¥Åä¸® UI ÆÐ³Î È°¼ºÈ­/ºñÈ°¼ºÈ­ Åä±Û (I Å°·Î)
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
        if (isInventoryOpen)
            RefreshUI();
    }

    /// <summary>µð¹ö±×¿ë: ÀåÂø ÇöÈ² ¹× º¸³Ê½º ½ºÅÈ Ãâ·Â</summary>
    public void PrintEquippedItems()
    {
        Debug.Log("=== ÀåÂø ¾ÆÀÌÅÛ ÇöÈ² ===");
        foreach (var kv in equippedWeapons)
            Debug.Log($"¹«±â [{kv.Key}]: {(kv.Value != null ? kv.Value.ItemName : "¾øÀ½")} ");
        foreach (var kv in equippedArmors)
            Debug.Log($"¹æ¾î±¸ [{kv.Key}]: {(kv.Value != null ? kv.Value.ItemName : "¾øÀ½")} ");
        Debug.Log($"°ø°Ý·Â º¸³Ê½º: +{attackBonus}, ¹æ¾î·Â º¸³Ê½º: +{defenseBonus}");
    }
}