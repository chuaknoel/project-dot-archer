using Enums;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ы溯檜橫曖 濰雜 嬴檜蠱(鼠晦﹞寞橫掘)婁 埤萄, 檣漸饜葬 UI蒂 婦葬м棲棻.
/// - 鼠晦 ﹞ 寞橫掘 Prefab擎 Inspector縑憮 萄楚斜&萄照戲煎 蛔煙
/// - Inventory朝 Prefab擊 Instantiateж雖 彊堅, 欽牖�� 衙ё虜 熱ч
/// - Player.SetWeapon() 偽擎 諼睡 煎霜檜 Instantiate 塽 睡賅 撲薑擊 疇歜颶
/// - 棟溘纂 啗骯, 褻�蛾� GetCurrentWeapon(), UI 饜旋 晦棟擎 斜渠煎 嶸雖
/// - 萄照, 鼻薄 蛔縑憮 AddGold()/SpendGold()蒂 鱔п 埤萄蒂 機等檜おж堅 盪濰
/// - ownedItemIds煎 掘衙脹 嬴檜蠱 蹺瞳, itemSlots煎 UI 幗が 偵褐
/// </summary>
public class Inventory : MonoBehaviour
{
    //式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式
    // 1) Inspector 撲薑 в萄
    //式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式

    [Header("濰雜й 嬴檜蠱 ID (Inspector縑 殮溘)")]
    [Tooltip("濰雜й 鼠晦 ID (蕨: bow_common)")]
    [SerializeField] private string equippedWeaponId = "bow_common";
    [Tooltip("濰雜й 癱掘 ID (蕨: helmet_common)")]
    [SerializeField] private string equippedHelmetId = "helmet_common";
    [Tooltip("濰雜й 骨褡 ID (蕨: armor_common)")]
    [SerializeField] private string equippedArmorId = "armor_common";
    [Tooltip("濰雜й 褐嫦 ID (蕨: boots_common)")]
    [SerializeField] private string equippedBootsId = "boots_common";

    [Header("鼠晦 Prefab 葬蝶お (Inspector 蛔煙)")]
    [Tooltip("賅萇 鼠晦 Prefab擊 萄楚斜&萄照ж堅, 陝 Prefab曖 ItemId蒂 撲薑ж撮蹂.")]
    [SerializeField] private List<GameObject> weaponPrefabs;

    [Header("寞橫掘 Prefab 葬蝶お (Inspector 蛔煙)")]
    [Tooltip("賅萇 寞橫掘 Prefab擊 萄楚斜&萄照ж堅, 陝 Prefab曖 ItemId蒂 撲薑ж撮蹂.")]
    [SerializeField] private List<GameObject> armorPrefabs;

    [Header("檣漸饜葬 UI ぬ割")]
    [Tooltip("檣漸饜葬 璽戲煎 餌辨й UI ぬ割擊 翱唸п輿撮蹂.")]
    [SerializeField] private GameObject inventoryUIPanel;

    [Header("檣漸饜葬 蝸煜 幗が菟")]
    [Tooltip("掘衙脹 嬴檜蠱擊 ル衛й Button 葬蝶お (Inspector 翱唸)")]
    [SerializeField] private List<Button> itemSlots;

    [Header("Ы溯檜橫 埤萄")]
    [Tooltip("⑷營 爾嶸 醞檣 埤萄")]
    [SerializeField] private int gold = 0;

    //式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式
    // 2) 頂睡 鼻鷓 盪濰辨 滲熱 塽 蛐敷傘葬
    //式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式

    // ID ⊥ Prefab 衙ё
    private Dictionary<string, GameObject> weaponPrefabDict = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> armorPrefabDict = new Dictionary<string, GameObject>();

    // 濰雜脹 等檜攪(ItemData)
    private Dictionary<WeaponType, ItemData> equippedWeapons = new Dictionary<WeaponType, ItemData>();
    private Dictionary<ArmorType, ItemData> equippedArmors = new Dictionary<ArmorType, ItemData>();

    // 鼠晦 Prefab 霤褻虜 盪濰 (Instantiate朝 Player 薹縑憮 籀葬)
    private Dictionary<WeaponType, GameObject> equippedWeaponPrefabs = new Dictionary<WeaponType, GameObject>();
    // 寞橫掘 檣蝶欐蝶朝 Player 薹 籀葬

    // 爾傘蝶 蝶囌 м骯
    private float attackBonus = 0f;
    private float defenseBonus = 0f;

    // UI 翮葡 鼻鷓
    private bool isInventoryOpen = false;

    /// <summary>
    /// 掘衙脹 嬴檜蠱 ID蒂 蹺瞳м棲棻.
    /// </summary>
    private List<string> ownedItemIds = new List<string>();

    /// <summary>
    /// 埤萄陛 滲唳腆 陽 掘絮濠縑啪 憲葡擊 鄹棲棻.
    /// </summary>
    public event Action<int> OnGoldChanged;

    //式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式
    // 3) Unity 儅貲輿晦 屬寥
    //式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式

    public void Init()
    {
        // (A) 盪濰脹 埤萄蒂 碳楝褫棲棻.
        LoadGold(GameManager.Instance.gameData);
        AddGold(gold);

        // (B) Inspector縑 萄照脹 Prefab菟擊 ID⊥Prefab 蛐敷傘葬縑 瓣錶厥晦
        weaponPrefabDict.Clear();
        foreach (var prefab in weaponPrefabs)
        {
            var it = prefab.GetComponent<Item>();
            if (it != null && !string.IsNullOrEmpty(it.ItemId))
                weaponPrefabDict[it.ItemId] = prefab;
            else
                Debug.LogWarning($"[Inventory] Weapon Prefab 援塊 傳朝 ItemId 嘐撲薑: {prefab.name}");
        }

        armorPrefabDict.Clear();
        foreach (var prefab in armorPrefabs)
        {
            var it = prefab.GetComponent<Item>();
            if (it != null && !string.IsNullOrEmpty(it.ItemId))
                armorPrefabDict[it.ItemId] = prefab;
            else
                Debug.LogWarning($"[Inventory] Armor Prefab 援塊 傳朝 ItemId 嘐撲薑: {prefab.name}");
        }

        // (C) 賅萇 WeaponType/ArmorType 酈 蟾晦��
        foreach (WeaponType wt in Enum.GetValues(typeof(WeaponType)))
            if (wt != WeaponType.None)
                equippedWeaponPrefabs[wt] = null;
        foreach (ArmorType at in Enum.GetValues(typeof(ArmorType)))
            if (at != ArmorType.None)
                equippedArmors[at] = null;

        // 晦襄 濰雜 餌о 營撲薑
        EquipSelectedItems();
        // UI 偵褐
        RefreshUI();
    }

    private void Update()
    {
        // I 酈煎 檣漸饜葬 饜旋
        if (Input.GetKeyDown(KeyCode.I) && inventoryUIPanel != null)
        {
            ToggleInventoryUI();
        }
    }

    //式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式
    // 4) 埤萄 盪濰/碳楝螃晦 詭憮萄
    //式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式

    private void LoadGold(GameData gameData)
    {
        gold = gameData.playerData.gold;
    }

    private void SaveGold()
    {
        GameManager.Instance.gameData.playerData.gold = gold;
    }

    //式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式
    // 5) 埤萄 褻濛辨 奢辨 詭憮萄
    //式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式

    /// <summary>埤萄蒂 隸陛衛霾棲棻. (蕨: 跨蝶攪 萄奧, 蠡蝶お 爾鼻)</summary>
    public void AddGold(int amount)
    {
        if (amount <= 0) return;
        gold += amount;
        SaveGold();
        OnGoldChanged?.Invoke(gold);
    }

    /// <summary>埤萄蒂 餌辨(離馬)м棲棻. 鼻薄 掘衙 蛔.</summary>
    public bool SpendGold(int amount)
    {
        if (amount <= 0) return true;
        if (gold < amount) return false;
        gold -= amount;
        OnGoldChanged?.Invoke(gold);
        return true;
    }

    /// <summary>⑷營 埤萄 熱榆擊 奩�納桭炴�.</summary>
    public int GetGold() => gold;

    //式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式
    // 6) 掘衙脹 嬴檜蠱 婦葬 塽 UI 偵褐
    //式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式

    /// <summary>
    /// 億 嬴檜蠱 蹺陛 �� 檣漸饜葬 UI 偵褐.
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
    /// 檣漸饜葬 蝸煜 UI蒂 ownedItemIds 晦遽戲煎 偵褐м棲棻.
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
                var data = GameManager.Instance.itemManager.GetItemDataById(id);
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

    //式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式
    // 7) 蟾晦 濰雜 (ID ⊥ ItemData ⊥ 衙ё/Instantiate 偵褐)
    //式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式

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
        // 鼠晦 濰雜
        if (weaponPrefabDict.ContainsKey(itemId))
            SetupEquippedWeaponById(itemId);
        // 寞橫掘 濰雜
        else if (armorPrefabDict.ContainsKey(itemId))
            SetupEquippedArmorById(itemId, DetermineArmorType(itemId));
    }

    private ArmorType DetermineArmorType(string itemId)
    {
        var data = GameManager.Instance.itemManager.GetItemDataById(itemId);
        return data != null ? data.ArmorType : ArmorType.None;
    }

    //式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式
    // 8) 爾傘蝶 蝶囌 瞳辨 + Prefab 衙ё/Instantiate
    //式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式

    private void SetupEquippedWeaponById(string weaponId)
    {
        var data = GameManager.Instance.itemManager.GetItemDataById(weaponId);
        if (data != null)
            UpdateEquippedWeapon(data);
        else
            Debug.LogError($"[Inventory] 鼠晦 等檜攪 橈擠: {weaponId}");
    }

    private void SetupEquippedArmorById(string armorId, ArmorType type)
    {
        var data = GameManager.Instance.itemManager.GetItemDataById(armorId);
        if (data != null)
            UpdateEquippedArmor(data, type);
        else
            Debug.LogError($"[Inventory] 寞橫掘 等檜攪 橈擠: {armorId}");
    }

    private void UpdateEquippedWeapon(ItemData item)
    {
        // 檜瞪 濰雜 鼠晦 爾傘蝶 薯剪
        if (equippedWeapons.TryGetValue(item.WeaponType, out var prev) && prev != null)
            attackBonus -= prev.AttackBonus;
        // 億 鼠晦 等檜攪 盪濰 塽 爾傘蝶 蹺陛
        equippedWeapons[item.WeaponType] = item;
        attackBonus += item.AttackBonus;
        // Prefab 衙ё
        if (weaponPrefabDict.TryGetValue(item.ItemId, out var prefab))
            equippedWeaponPrefabs[item.WeaponType] = prefab;
        else
            Debug.LogWarning($"[Inventory] 衙ё脹 Weapon Prefab 橈擠: {item.ItemId}");
    }

    private void UpdateEquippedArmor(ItemData item, ArmorType type)
    {
        // 檜瞪 濰雜 寞橫掘 爾傘蝶 薯剪
        if (equippedArmors.TryGetValue(type, out var prev) && prev != null)
            defenseBonus -= prev.DefenseBonus;
        // 億 寞橫掘 等檜攪 盪濰 塽 爾傘蝶 蹺陛
        equippedArmors[type] = item;
        defenseBonus += item.DefenseBonus;
        // Instantiateж罹 蟾晦�� (Player 薹戲煎 衡啖紫 鼠寞)
        if (armorPrefabDict.TryGetValue(item.ItemId, out var prefab))
        {
            var obj = Instantiate(prefab, transform);
            
            obj.name = $"Armor_{item.ItemId}";
            var comp = obj.GetComponent<Item>() ?? obj.AddComponent<Item>();
            comp.LoadItemData();
            comp.Initialize(item);
        }
        else
            Debug.LogWarning($"[Inventory] 衙ё脹 Armor Prefab 橈擠: {item.ItemId}");
    }

    //式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式
    // 9) 褻�蛾� 詭憮萄 塽 UI 饜旋
    //式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式式

    /// <summary>
    /// 溯剪衛 ��轎 雖錳: Player.SetWeapon() 蛔縑憮 餌辨ж朝 詭憮萄.
    /// ⑷營 濰雜脹 鼠晦 Item 闡ん凱お蒂 奩�納桭炴�.
    /// </summary>
    public Item GetCurrentWeapon()
    {
        var prefab = GetCurrentWeaponPrefab();
        return prefab != null ? prefab.GetComponent<Item>() : null;
    }

    /// <summary>
    /// ⑷營 濰雜脹 鼠晦 Prefab(GameObject)擊 辦摹牖嬪 Bow⊥Sword⊥Scythe煎 奩�納桭炴�.
    /// </summary>
    public GameObject GetCurrentWeaponPrefab()
    {
        if (equippedWeaponPrefabs.TryGetValue(WeaponType.Bow, out var bow) && bow != null) return bow;
        if (equippedWeaponPrefabs.TryGetValue(WeaponType.Sword, out var sword) && sword != null) return sword;
        if (equippedWeaponPrefabs.TryGetValue(WeaponType.Scythe, out var scythe) && scythe != null) return scythe;
        return null;
    }

    /// <summary>識 奢問溘 爾傘蝶 奩��</summary>
    public float GetTotalAttackBonus() => attackBonus;
    /// <summary>識 寞橫溘 爾傘蝶 奩��</summary>
    public float GetTotalDefenseBonus() => defenseBonus;

    /// <summary>
    /// 檣漸饜葬 UI ぬ割 �側瘓�/綠�側瘓� 饜旋 (I 酈煎)
    /// </summary>
    public void ToggleInventoryUI()
    {
        if (inventoryUIPanel == null)
        {
            Debug.LogWarning("[Inventory] inventoryUIPanel 嘐翱唸");
            return;
        }
        isInventoryOpen = !isInventoryOpen;
        inventoryUIPanel.SetActive(isInventoryOpen);
        if (isInventoryOpen)
            RefreshUI();
    }

    /// <summary>蛤幗斜辨: 濰雜 ⑷�� 塽 爾傘蝶 蝶囌 轎溘</summary>
    public void PrintEquippedItems()
    {
        Debug.Log("=== 濰雜 嬴檜蠱 ⑷�� ===");
        foreach (var kv in equippedWeapons)
            Debug.Log($"鼠晦 [{kv.Key}]: {(kv.Value != null ? kv.Value.ItemName : "橈擠")} ");
        foreach (var kv in equippedArmors)
            Debug.Log($"寞橫掘 [{kv.Key}]: {(kv.Value != null ? kv.Value.ItemName : "橈擠")} ");
        Debug.Log($"奢問溘 爾傘蝶: +{attackBonus}, 寞橫溘 爾傘蝶: +{defenseBonus}");
    }
}