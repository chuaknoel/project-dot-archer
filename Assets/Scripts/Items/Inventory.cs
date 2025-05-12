using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // 인스펙터에서 이름으로 선택할 수 있는 장착 아이템
    [Header("현재 장착 아이템 (이름으로 선택)")]
    [Tooltip("무기 이름을 선택하세요 (예: 일반철검, 좋은활, 더좋은낫 등)")]
    [SerializeField] private string equippedWeaponName = "일반철검";

    [Tooltip("투구 이름을 선택하세요 (예: 일반투구, 좋은투구 등)")]
    [SerializeField] private string equippedHelmetName = "일반투구";

    [Tooltip("갑옷 이름을 선택하세요 (예: 일반갑옷, 좋은갑옷 등)")]
    [SerializeField] private string equippedArmorName = "일반갑옷";

    [Tooltip("신발 이름을 선택하세요 (예: 일반신발, 좋은신발 등)")]
    [SerializeField] private string equippedBootsName = "일반신발";

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
        // 무기 장착
        if (!string.IsNullOrEmpty(equippedWeaponName))
        {
            SetupEquippedWeapon(equippedWeaponName);
        }

        // 투구 장착
        if (!string.IsNullOrEmpty(equippedHelmetName))
        {
            SetupEquippedArmor(equippedHelmetName, ArmorType.Helmet);
        }

        // 갑옷 장착
        if (!string.IsNullOrEmpty(equippedArmorName))
        {
            SetupEquippedArmor(equippedArmorName, ArmorType.Armor);
        }

        // 신발 장착
        if (!string.IsNullOrEmpty(equippedBootsName))
        {
            SetupEquippedArmor(equippedBootsName, ArmorType.Boots);
        }
    }

    // 무기 이름으로 장착 정보 설정
    private void SetupEquippedWeapon(string weaponName)
    {
        // ItemManager에서 이름으로 아이템 데이터 가져오기
        ItemData weaponData = ItemManager.Instance.GetItemDataByName(weaponName);

        if (weaponData != null)
        {
            // 장착 정보 갱신 (인벤토리 시스템용)
            UpdateEquippedWeapon(weaponData);

            Debug.Log($"무기 '{weaponName}' 장착 완료");
        }
        else
        {
            Debug.LogWarning($"무기 '{weaponName}'을 찾을 수 없습니다.");
        }
    }

    // 방어구 이름으로 방어구 설정
    private void SetupEquippedArmor(string armorName, ArmorType type)
    {
        // ItemManager에서 이름으로 아이템 데이터 가져오기
        ItemData armorData = ItemManager.Instance.GetItemDataByName(armorName);

        if (armorData != null)
        {
            // 장착 정보 갱신 (인벤토리 시스템용)
            UpdateEquippedArmor(armorData);

            Debug.Log($"방어구 '{armorName}' 장착 완료");
        }
        else
        {
            Debug.LogWarning($"방어구 '{armorName}'을 찾을 수 없습니다.");
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
        item.SetItemId(itemData.ItemId);

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
        if (weapon != null) return weapon;

        weapon = GetEquippedWeapon(WeaponType.Sword);
        if (weapon != null) return weapon;

        weapon = GetEquippedWeapon(WeaponType.Scythe);
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

    /*
    ==========================================================================
    WeaponHandler 담당자를 위한 설명
    ==========================================================================

    아이템 시스템이 변경되어 WeaponHandler에서 아이템 프리팹을 사용하는 방식으로 개선되었습니다.
    
    1. 아이템 프리팹에는 Item 스크립트가 붙어있고 itemId가 설정되어 있습니다.
    2. 인벤토리에서는 아이템 이름을 선택하여 장착 정보를 관리합니다.
    3. WeaponHandler는 인벤토리에서 장착된 무기의 ID를 가져와 해당 ID와 동일한 
       프리팹을 소환합니다.
    
    WeaponHandler 사용 방법:
    
    1. 인벤토리에서 현재 장착된 무기 ID 가져오기:
       string weaponId = inventory.GetCurrentWeaponId();
    
    2. 해당 ID와 일치하는 프리팹 불러오기:
       GameObject weaponPrefab = Resources.Load<GameObject>($"Weapons/{weaponId}");
    
    3. 무기 프리팹을 인스턴스화하여 부착:
       Instantiate(weaponPrefab, transform);
    
    WeaponHandler.Init은 호환성을 위해 유지되었지만, 향후에는 아이템 ID를 통한
    프리팹 로드 방식으로 전환될 예정입니다.
    
    각 무기 프리팹에는 이미 Item 컴포넌트가 있으며, itemId 필드에 
    해당 무기의 ID가 설정되어 있습니다. 프리팹을 생성하면 자동으로 
    해당 ID에 맞는 아이템 데이터를 로드합니다.
    ==========================================================================
    */
}