using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private int maxSlots = 20;

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
    private Dictionary<ItemCategory, Item> equippedItems = new Dictionary<ItemCategory, Item>();

    // 세부 장비 슬롯 (타입 별 관리를 위함)
    private Dictionary<WeaponType, Item> equippedWeapons = new Dictionary<WeaponType, Item>();
    private Dictionary<ArmorType, Item> equippedArmors = new Dictionary<ArmorType, Item>();

    // UI 관련 변수
    [Header("UI 패널")]
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject equipmentUI;

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
                equippedWeapons[weaponType] = null;
        }

        // 방어구 타입 딕셔너리 초기화
        foreach (ArmorType armorType in System.Enum.GetValues(typeof(ArmorType)))
        {
            if (armorType != ArmorType.None)
                equippedArmors[armorType] = null;
        }
    }

    private void Start()
    {
        // UI 초기 상태 설정
        if (inventoryUI != null)
            inventoryUI.SetActive(false);

        if (equipmentUI != null)
            equipmentUI.SetActive(false);

        // 인스펙터에서 선택한 아이템으로 초기 장비 설정
        //EquipSelectedItems();
    }

    // 인스펙터에서 선택한 아이템 장착
    public void EquipSelectedItems()
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

    // 무기 이름으로 WeaponHandler 설정
    private void SetupEquippedWeapon(string weaponName)
    {
        // ItemManager에서 이름으로 아이템 데이터 가져오기
        ItemData weaponData = ItemManager.Instance.GetItemDataByName(weaponName);

        if (weaponData != null)
        {
            // 가상의 Item 객체 생성
            Item weaponItem = Instantiate(ItemManager.Instance.TestWeapon);

            // 장착 정보 갱신 (인벤토리 시스템용)
            UpdateEquippedWeapon(weaponItem);

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
            // 가상의 Item 객체 생성
            Item armorItem = CreateVirtualItem(armorData);

            // 장착 정보 갱신 (인벤토리 시스템용)
            UpdateEquippedArmor(armorItem);

            Debug.Log($"방어구 '{armorName}' 장착 완료");
        }
        else
        {
            Debug.LogWarning($"방어구 '{armorName}'을 찾을 수 없습니다.");
        }
    }

    // 가상의 Item 객체 생성 (실제 GameObject 없이 ItemData만 가진 객체)
    private Item CreateVirtualItem(ItemData itemData)
    {
        // 임시 GameObject 생성
        GameObject tempObj = new GameObject($"VirtualItem_{itemData.ItemName}");
        tempObj.transform.SetParent(transform);
        tempObj.SetActive(false);

        // Item 컴포넌트 추가 및 초기화
        Item item = tempObj.AddComponent<Item>();
        item.Initialize(itemData);

        return item;
    }

    // 장착된 무기 정보 업데이트
    private void UpdateEquippedWeapon(Item item)
    {
        WeaponType weaponType = item.ItemData.WeaponType;

        // 기존 장착 무기 제거
        if (equippedWeapons[weaponType] != null)
        {
            equippedWeapons[weaponType] = null;
        }

        // 새 무기 장착
        equippedWeapons[weaponType] = item;
        equippedItems[ItemCategory.Weapon] = item;

        // 공격력 보너스 갱신
        attackBonus = item.ItemData.AttackBonus;
    }

    // 장착된 방어구 정보 업데이트
    private void UpdateEquippedArmor(Item item)
    {
        ArmorType armorType = item.ItemData.ArmorType;

        // 기존 장착 방어구 제거
        if (equippedArmors[armorType] != null)
        {
            equippedArmors[armorType] = null;
        }

        // 새 방어구 장착
        equippedArmors[armorType] = item;
        equippedItems[ItemCategory.Armor] = item;

        // 방어력 보너스 갱신
        defenseBonus += item.ItemData.DefenseBonus;
    }

    // 현재 장착된 무기 가져오기
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

    // 특정 타입의 장착된 무기 가져오기
    public Item GetEquippedWeapon(WeaponType type)
    {
        return equippedWeapons[type];
    }

    // 특정 타입의 장착된 방어구 가져오기
    public Item GetEquippedArmor(ArmorType type)
    {
        return equippedArmors[type];
    }

    // 총 공격력 보너스 계산 (Player에서 호출)
    public float GetTotalAttackBonus()
    {
        return attackBonus;
    }

    // 총 방어력 보너스 계산 (Player에서 호출)
    public float GetTotalDefenseBonus()
    {
        return defenseBonus;
    }

    // ========== Player 코드 호환성을 위한 임시 메서드 ==========

    // UI 토글 (임시 구현)
    public void ToggleInventoryUI()
    {
        // UI 기능이 구현되면 이 주석을 제거하고 실제 구현으로 대체
        Debug.Log("인벤토리 UI 기능이 아직 구현되지 않았습니다.");

        /*
        if (inventoryUI != null)
        {
            bool newState = !inventoryUI.activeSelf;
            inventoryUI.SetActive(newState);

            // 인벤토리를 열 때 항상 업데이트
            if (newState)
                UpdateInventoryUI();
        }
        */
    }

    // 장비 UI 토글 (임시 구현)
    public void ToggleEquipmentUI()
    {
        // UI 기능이 구현되면 이 주석을 제거하고 실제 구현으로 대체
        Debug.Log("장비 UI 기능이 아직 구현되지 않았습니다.");

        /*
        if (equipmentUI != null)
        {
            bool newState = !equipmentUI.activeSelf;
            equipmentUI.SetActive(newState);

            // 장비 창을 열 때 항상 업데이트
            if (newState)
                UpdateEquipmentUI();
        }
        */
    }

    // 모든 UI 토글 (임시 구현)
    public void ToggleAllUI()
    {
        // UI 기능이 구현되면 이 주석을 제거하고 실제 구현으로 대체
        Debug.Log("인벤토리 및 장비 UI 기능이 아직 구현되지 않았습니다.");

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

    // 아이템 추가 (임시 구현)
    public bool AddItem(Item item)
    {
        // UI 기능이 구현되면 이 주석을 제거하고 실제 구현으로 대체
        Debug.Log($"아이템 '{item.ItemData.ItemName}' 추가 기능이 아직 구현되지 않았습니다.");
        return true;

        /*
        if (items.Count >= maxSlots)
        {
            Debug.Log("인벤토리가 가득 찼습니다!");
            return false;
        }

        items.Add(item);
        UpdateUI();
        return true;
        */
    }

    /* 
    ==============================================================================
    UI를 통한 아이템 관리 기능 (현재 비활성화)
    ==============================================================================
    
    이 부분은 게임 UI를 통해 아이템을 관리하기 위한 기능입니다.
    나중에 실제 인벤토리 UI 시스템을 구현할 때 활성화할 수 있습니다.
    
    주요 기능:
    1. 인벤토리 UI 표시/숨김
    2. 아이템 추가/제거
    3. 아이템 사용(장착)
    4. 장비 해제
    
    구현 방법:
    - ToggleInventoryUI(): 인벤토리 UI 표시/숨김 전환
    - AddItem()/RemoveItem(): 아이템 추가/제거
    - EquipItem()/UnequipItem(): 아이템 장착/해제
    
    사용 예시:
    - 몬스터 처치 시 AddItem()을 호출하여 아이템 추가
    - UI 버튼 클릭 시 ToggleInventoryUI() 호출
    - 아이템 슬롯 클릭 시 UseItem() 호출
    
    주의 사항:
    - 현재는 인스펙터에서 선택한 초기 아이템만 사용
    - UI 구현 시 이 주석을 제거하고 아래 메서드들을 활성화
    ==============================================================================
    
    // 아이템 제거
    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);

            // 장착된 아이템인 경우 해제
            if (item.IsEquipped)
                UnequipItem(item);

            UpdateUI();
        }
    }

    // 아이템 사용 (인덱스 기반)
    public void UseItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            Item item = items[index];
            ApplyItemEffect(item);
        }
    }

    // 아이템 효과 적용
    public void ApplyItemEffect(Item item)
    {
        ItemCategory category = item.ItemData.ItemCategory;

        // 장비 아이템일 경우 장착
        if (category == ItemCategory.Weapon || category == ItemCategory.Armor)
        {
            EquipItem(item);
        }
    }

    // 장비 장착
    public void EquipItem(Item item)
    {
        // 아이템 장착 로직
        ItemCategory category = item.ItemData.ItemCategory;

        // 일반 카테고리 관리
        if (equippedItems[category] != null)
        {
            UnequipItem(equippedItems[category]);
        }

        // 세부 타입별 관리
        if (category == ItemCategory.Weapon)
        {
            WeaponType weaponType = item.ItemData.WeaponType;
            if (equippedWeapons[weaponType] != null)
            {
                UnequipItem(equippedWeapons[weaponType]);
            }

            // 새 무기 장착
            equippedWeapons[weaponType] = item;
            attackBonus += item.ItemData.AttackBonus;
            
            // WeaponHandler에 아이템 정보 전달
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

            // 새 방어구 장착
            equippedArmors[armorType] = item;
            defenseBonus += item.ItemData.DefenseBonus;
        }

        // 공통 처리
        equippedItems[category] = item;
        item.SetEquipped(true);

        // UI 업데이트
        UpdateUI();
    }

    // 장비 해제
    public void UnequipItem(Item item)
    {
        // 장비 해제 로직
    }
    
    // UI 업데이트
    private void UpdateUI()
    {
        // 인벤토리 UI 업데이트
        UpdateInventoryUI();
        
        // 장비 UI 업데이트
        UpdateEquipmentUI();
    }
    
    // 인벤토리 UI 업데이트
    private void UpdateInventoryUI()
    {
        // 인벤토리 UI 업데이트 로직
    }
    
    // 장비 UI 업데이트
    private void UpdateEquipmentUI()
    {
        // 장비 UI 업데이트 로직
    }
    */

    /*
    ==========================================================================
    WeaponHandler 담당자를 위한 설명
    ==========================================================================

    인벤토리 시스템이 개선되어 이제 무기를 이름으로 선택할 수 있습니다.
    선택된 무기는 WeaponHandler에 자동으로 전달됩니다.

    1. 인벤토리에서는 무기 이름을 선택하면 ItemManager에서 해당 데이터를 가져옵니다.
    2. 이 데이터를 바탕으로 가상의 Item 객체를 생성합니다.
    3. 이 Item 객체를 WeaponHandler.Init() 메서드에 전달합니다.

    WeaponHandler에서는 다음과 같이 Init 메서드를 확장하면 좋습니다:

    public virtual void Init(Item weapon, IAttackStat ownerStat, LayerMask targetMask)
    {
        this.weapon = weapon;
        animator = GetComponent<Animator>();
        isUseable = true;
        owner = ownerStat;
        this.targetMask = targetMask;
        
        // 무기 데이터 설정
        attackDamage = weapon.ItemData.AttackBonus;
        attackDelay = weapon.ItemData.AttackDelay;
        
        // 무기 타입에 따른 설정
        switch (weapon.ItemData.WeaponType)
        {
            case WeaponType.Sword:
                // 검 관련 설정
                break;
            case WeaponType.Bow:
                // 활 관련 설정
                break;
            case WeaponType.Scythe:
                // 낫 관련 설정
                break;
        }
        
        // 무기 스프라이트 설정 (weaponRenderer가 있다면)
        if (weaponRenderer != null && weapon.ItemData.ItemIcon != null)
        {
            weaponRenderer.sprite = weapon.ItemData.ItemIcon;
        }

        //아니면 동일한 값을 가지는 프리팹(해당 아이템의 ID를 가지게 될 아이템 프리팹)
    }

    Item 객체를 통해 모든 무기 데이터에 접근할 수 있습니다:
    - weapon.ItemData.itemId: 무기 ID (예: "sword_common")
    - weapon.ItemData.ItemName: 무기 이름 (예: "일반철검")
    - weapon.ItemData.WeaponType: 무기 타입 (Sword, Bow, Scythe)
    - weapon.ItemData.AttackBonus: 공격력 보너스
    - weapon.ItemData.AttackRange: 공격 범위
    - weapon.ItemData.AttackDelay: 공격 딜레이
    - weapon.ItemData.AttackCooldown: 공격 쿨다운
    - weapon.ItemData.ItemIcon: 무기 아이콘 스프라이트

    이 시스템은 PlayerController나 WeaponHandler 코드를 직접 수정하지 않고도
    무기 설정을 가능하게 합니다.

    문의사항이 디스코드에 남겨주세요! 흙흙. 일단 여기까지..
    ==========================================================================
    */
}