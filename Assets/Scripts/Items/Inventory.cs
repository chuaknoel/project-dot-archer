using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // 인스펙터에서 ID로 선택할 수 있는 장착 아이템
    [Header("현재 장착 아이템 (ID로 선택)")]
    [Tooltip("무기 ID를 입력하세요 (예: sword_common, bow_common 등)")]
    [SerializeField] private string equippedWeaponId = "bow_common";

    [Tooltip("투구 ID를 입력하세요 (예: helmet_common 등)")]
    [SerializeField] private string equippedHelmetId = "helmet_common";

    [Tooltip("갑옷 ID를 입력하세요 (예: armor_common 등)")]
    [SerializeField] private string equippedArmorId = "armor_common";

    [Tooltip("신발 ID를 입력하세요 (예: boots_common 등)")]
    [SerializeField] private string equippedBootsId = "boots_common";

    // 장착된 아이템 관리 (카테고리 별)
    private Dictionary<ItemCategory, ItemData> equippedItems = new Dictionary<ItemCategory, ItemData>();

    // 세부 장비 슬롯 (타입 별 관리를 위함)
    private Dictionary<WeaponType, ItemData> equippedWeapons = new Dictionary<WeaponType, ItemData>();
    private Dictionary<ArmorType, ItemData> equippedArmors = new Dictionary<ArmorType, ItemData>();

    // 가상 아이템 객체 (Player 클래스와의 호환성용)
    private Dictionary<WeaponType, Item> virtualWeaponItems = new Dictionary<WeaponType, Item>();
    private Dictionary<ArmorType, Item> virtualArmorItems = new Dictionary<ArmorType, Item>();

    // 스탯 보너스
    private float attackBonus = 0f;
    private float defenseBonus = 0f;

    // 초기화
    private void Awake()
    {
        // 장비 딕셔너리 초기화
        equippedItems[ItemCategory.Weapon] = null;
        equippedItems[ItemCategory.Armor] = null;

        // 무기 타입 딕셔너리 초기화
        foreach (WeaponType weaponType in System.Enum.GetValues(typeof(WeaponType)))
        {
            if (weaponType != WeaponType.None)
            {
                equippedWeapons[weaponType] = null;
                virtualWeaponItems[weaponType] = null;
            }
        }

        // 방어구 타입 딕셔너리 초기화
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
        // 인스펙터에서 선택한 아이템으로 초기 장비 설정
        EquipSelectedItems();
    }

    // 인스펙터에서 선택한 아이템 장착
    private void EquipSelectedItems()
    {
        Debug.Log("Inventory: 초기 장비 설정 시작");

        // 무기 장착
        if (!string.IsNullOrEmpty(equippedWeaponId))
        {
            SetupEquippedWeaponById(equippedWeaponId);
        }

        // 투구 장착
        if (!string.IsNullOrEmpty(equippedHelmetId))
        {
            SetupEquippedArmorById(equippedHelmetId, ArmorType.Helmet);
        }

        // 갑옷 장착
        if (!string.IsNullOrEmpty(equippedArmorId))
        {
            SetupEquippedArmorById(equippedArmorId, ArmorType.Armor);
        }

        // 신발 장착
        if (!string.IsNullOrEmpty(equippedBootsId))
        {
            SetupEquippedArmorById(equippedBootsId, ArmorType.Boots);
        }

        Debug.Log("Inventory: 초기 장비 설정 완료");
    }

    // 무기 ID로 장착 정보 설정
    private void SetupEquippedWeaponById(string weaponId)
    {
        // ItemManager에서 ID로 아이템 데이터 가져오기
        ItemData weaponData = ItemManager.Instance.GetItemDataById(weaponId);

        if (weaponData != null)
        {
            // 장착 정보 갱신 (인벤토리 시스템용)
            UpdateEquippedWeapon(weaponData);

            Debug.Log($"무기 '{weaponId}' 장착 완료");
        }
        else
        {
            Debug.LogWarning($"무기 ID '{weaponId}'를 찾을 수 없습니다.");
        }
    }

    // 방어구 ID로 방어구 설정
    private void SetupEquippedArmorById(string armorId, ArmorType type)
    {
        // ItemManager에서 ID로 아이템 데이터 가져오기
        ItemData armorData = ItemManager.Instance.GetItemDataById(armorId);

        if (armorData != null)
        {
            // 장착 정보 갱신 (인벤토리 시스템용)
            UpdateEquippedArmor(armorData);

            Debug.Log($"방어구 '{armorId}' 장착 완료");
        }
        else
        {
            Debug.LogWarning($"방어구 ID '{armorId}'를 찾을 수 없습니다.");
        }
    }

    // 장착된 무기 정보 업데이트
    private void UpdateEquippedWeapon(ItemData item)
    {
        WeaponType weaponType = item.WeaponType;

        // 기존 장착 무기 제거
        if (equippedWeapons[weaponType] != null)
        {
            attackBonus -= equippedWeapons[weaponType].AttackBonus;
        }

        // 새 무기 장착
        equippedWeapons[weaponType] = item;
        equippedItems[ItemCategory.Weapon] = item;

        // 공격력 보너스 갱신
        attackBonus += item.AttackBonus;

        // 가상 아이템 생성 (Player 호환성용)
        virtualWeaponItems[weaponType] = CreateVirtualItem(item);

        Debug.Log($"무기 장착 정보 업데이트: {item.ItemName}, 공격력 +{item.AttackBonus}");
    }

    // 장착된 방어구 정보 업데이트
    private void UpdateEquippedArmor(ItemData item)
    {
        ArmorType armorType = item.ArmorType;

        // 기존 장착 방어구 제거
        if (equippedArmors[armorType] != null)
        {
            defenseBonus -= equippedArmors[armorType].DefenseBonus;
        }

        // 새 방어구 장착
        equippedArmors[armorType] = item;
        equippedItems[ItemCategory.Armor] = item;

        // 방어력 보너스 갱신
        defenseBonus += item.DefenseBonus;

        // 가상 아이템 생성 (Player 호환성용)
        virtualArmorItems[armorType] = CreateVirtualItem(item);

        Debug.Log($"방어구 장착 정보 업데이트: {item.ItemName}, 방어력 +{item.DefenseBonus}");
    }

    // 가상의 Item 객체 생성 (Player 호환성용)
    private Item CreateVirtualItem(ItemData itemData)
    {
        // 임시 GameObject 생성
        GameObject tempObj = new GameObject($"VirtualItem_{itemData.ItemName}");
        tempObj.transform.SetParent(transform);
        tempObj.SetActive(false);

        // Item 컴포넌트 추가 및 초기화
        Item item = tempObj.AddComponent<Item>();
        item.Initialize(itemData);

        Debug.Log($"가상 아이템 생성: {itemData.ItemName}, ID: {itemData.ItemId}");
        return item;
    }

    // 현재 장착된 무기 데이터 가져오기
    public ItemData GetCurrentWeaponData()
    {
        // 장착된 무기 반환 (활, 검, 낫 중 하나)
        ItemData weapon = GetEquippedWeaponData(WeaponType.Bow);
        if (weapon != null) return weapon;

        weapon = GetEquippedWeaponData(WeaponType.Sword);
        if (weapon != null) return weapon;

        weapon = GetEquippedWeaponData(WeaponType.Scythe);
        return weapon;
    }

    // 현재 장착된 무기 가져오기 (Player 호환성용)
    public Item GetCurrentWeapon()
    {
        // 장착된 무기 반환 (활, 검, 낫 중 하나)
        Item weapon = GetEquippedWeapon(WeaponType.Bow);
        if (weapon != null)
        {
            Debug.Log($"현재 장착된 무기: {weapon.ItemData.ItemName}, ID: {weapon.ItemId}");
            return weapon;
        }

        weapon = GetEquippedWeapon(WeaponType.Sword);
        if (weapon != null)
        {
            Debug.Log($"현재 장착된 무기: {weapon.ItemData.ItemName}, ID: {weapon.ItemId}");
            return weapon;
        }

        weapon = GetEquippedWeapon(WeaponType.Scythe);
        if (weapon != null)
        {
            Debug.Log($"현재 장착된 무기: {weapon.ItemData.ItemName}, ID: {weapon.ItemId}");
        }
        else
        {
            Debug.LogWarning("GetCurrentWeapon: 장착된 무기가 없습니다.");
        }
        return weapon;
    }

    // 특정 타입의 장착된 무기 데이터 가져오기
    public ItemData GetEquippedWeaponData(WeaponType type)
    {
        return equippedWeapons[type];
    }

    // 특정 타입의 장착된 무기 가져오기 (Player 호환성용)
    public Item GetEquippedWeapon(WeaponType type)
    {
        return virtualWeaponItems[type];
    }

    // 특정 타입의 장착된 방어구 데이터 가져오기
    public ItemData GetEquippedArmorData(ArmorType type)
    {
        return equippedArmors[type];
    }

    // 특정 타입의 장착된 방어구 가져오기 (Player 호환성용)
    public Item GetEquippedArmor(ArmorType type)
    {
        return virtualArmorItems[type];
    }

    // 총 공격력 보너스 계산
    public float GetTotalAttackBonus()
    {
        return attackBonus;
    }

    // 총 방어력 보너스 계산
    public float GetTotalDefenseBonus()
    {
        return defenseBonus;
    }

    // 현재 무기 ID 가져오기
    public string GetCurrentWeaponId()
    {
        ItemData weapon = GetCurrentWeaponData();
        return weapon != null ? weapon.ItemId : string.Empty;
    }

    // 현재 장착된 무기 이름 가져오기
    public string GetCurrentWeaponName()
    {
        ItemData weapon = GetCurrentWeaponData();
        return weapon != null ? weapon.ItemName : string.Empty;
    }

    // Player 개발자를 위한 임시 호환성 메서드들
    // UI 구현까지 오류 방지용으로만 사용
    public void ToggleInventoryUI() { Debug.Log("인벤토리 UI 기능이 아직 구현되지 않았습니다."); }
    public void ToggleEquipmentUI() { Debug.Log("장비 UI 기능이 아직 구현되지 않았습니다."); }
    public void ToggleAllUI() { Debug.Log("인벤토리 및 장비 UI 기능이 아직 구현되지 않았습니다."); }
    public bool AddItem(Item item) { Debug.Log($"아이템 '{item.ItemData.ItemName}' 추가 기능이 아직 구현되지 않았습니다."); return true; }

    // 디버그: 현재 장착 정보 출력
    public void PrintEquippedItems()
    {
        Debug.Log("===== 장착 아이템 정보 =====");

        // 무기 정보
        Debug.Log("== 무기 ==");
        foreach (var pair in equippedWeapons)
        {
            if (pair.Value != null)
            {
                Debug.Log($"- {pair.Key}: {pair.Value.ItemName} (ID: {pair.Value.ItemId})");
            }
            else
            {
                Debug.Log($"- {pair.Key}: 장착 없음");
            }
        }

        // 방어구 정보
        Debug.Log("== 방어구 ==");
        foreach (var pair in equippedArmors)
        {
            if (pair.Value != null)
            {
                Debug.Log($"- {pair.Key}: {pair.Value.ItemName} (ID: {pair.Value.ItemId})");
            }
            else
            {
                Debug.Log($"- {pair.Key}: 장착 없음");
            }
        }

        Debug.Log($"총 공격력 보너스: +{attackBonus}");
        Debug.Log($"총 방어력 보너스: +{defenseBonus}");
    }

    /*
    ==========================================================================
    WeaponHandler 담당자를 위한 설명
    ==========================================================================

    아이템 시스템이 변경되어 WeaponHandler에서 아이템 프리팹을 사용하는 방식으로 개선되었습니다.
    
    1. 아이템 프리팹에는 Item 스크립트가 붙어있고 itemId가 설정되어 있습니다.
    2. 인벤토리에서는 아이템 ID를 선택하여 장착 정보를 관리합니다.
    3. WeaponHandler는 인벤토리에서 장착된 무기의 ID를 가져와 해당 ID와 동일한 
       프리팹을 소환합니다.
    
    무기 ID와 경로 설정:
    - 기본 무기는 Resources/Weapons/ 폴더에 있어야 합니다.
    - 프리팹 이름은 중요하지 않지만, Item 컴포넌트의 itemId 필드가 중요합니다.
    - 예: bow_common 아이템은 Resources/Weapons/bow_common.prefab 경로에 있어야 합니다.
    
    WeaponHandler.Init 메서드는 Item 객체를 받아서 처리합니다:
    - weapon.ItemId로 아이템 ID를 가져옵니다.
    - Resources.Load<GameObject>($"Weapons/{weapon.ItemId}")로 프리팹을 로드합니다.
    - 로드된 프리팹을 인스턴스화하여 무기를 표시합니다.
    
    문제가 있으면 로그를 확인하고 ItemManager, Inventory, WeaponHandler 담당자와
    함께 문제를 해결하세요.
    ==========================================================================
    */
}