using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private int maxSlots = 20;

    // 장착된 아이템 관리 (카테고리 별)
    private Dictionary<ItemCategory, Item> equippedItems = new Dictionary<ItemCategory, Item>();

    // 세부 장비 슬롯 (타입 별 관리를 위함)
    private Dictionary<WeaponType, Item> equippedWeapons = new Dictionary<WeaponType, Item>();
    private Dictionary<ArmorType, Item> equippedArmors = new Dictionary<ArmorType, Item>();

    // UI 관련 변수
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
    }

    // 아이템 추가
    public bool AddItem(Item item)
    {
        if (items.Count >= maxSlots)
        {
            Debug.Log("인벤토리가 가득 찼습니다!");
            return false;
        }

        items.Add(item);
        UpdateUI();
        return true;
    }

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

    // UI 업데이트
    private void UpdateUI()
    {
        // 인벤토리 UI 업데이트 로직
        UpdateInventoryUI();

        // 장비 UI 업데이트
        UpdateEquipmentUI();
    }

    // 인벤토리 UI 업데이트
    private void UpdateInventoryUI()
    {
        // 실제 구현은 프로젝트에 맞게 작성 필요
        // 인벤토리 슬롯 UI에 아이템 표시
        // 장착된 아이템은 특별한 표시를 추가
    }

    // 장비 UI 업데이트
    private void UpdateEquipmentUI()
    {
        // 장비 슬롯에 현재 장착된 아이템 표시
        // 각 장비 타입별 슬롯에 해당 아이템 표시
    }

    // 인벤토리 UI 토글
    public void ToggleInventoryUI()
    {
        if (inventoryUI != null)
        {
            bool newState = !inventoryUI.activeSelf;
            inventoryUI.SetActive(newState);

            // 인벤토리를 열 때 항상 업데이트
            if (newState)
                UpdateInventoryUI();
        }
    }

    // 장비 UI 토글
    public void ToggleEquipmentUI()
    {
        if (equipmentUI != null)
        {
            bool newState = !equipmentUI.activeSelf;
            equipmentUI.SetActive(newState);

            // 장비 창을 열 때 항상 업데이트
            if (newState)
                UpdateEquipmentUI();
        }
    }

    // 통합 UI 토글 (둘 다 표시/숨김)
    public void ToggleAllUI()
    {
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

    // 장비 장착 - 수정된 버전
    public void EquipItem(Item item)
    {
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
            Debug.Log($"무기 장착: {item.ItemData.ItemName}, 공격력 보너스: +{item.ItemData.AttackBonus}");
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
            Debug.Log($"방어구 장착: {item.ItemData.ItemName}, 방어력 보너스: +{item.ItemData.DefenseBonus}");
        }

        // 공통 처리
        equippedItems[category] = item;
        item.SetEquipped(true);

        // UI 업데이트
        UpdateUI();
    }

    // 장비 해제 - 직접 아이템 전달 버전
    public void UnequipItem(Item item)
    {
        if (item == null) return;

        ItemCategory category = item.ItemData.ItemCategory;

        // 일반 카테고리에서 제거
        if (equippedItems[category] == item)
        {
            equippedItems[category] = null;
        }

        // 세부 타입별 관리에서 제거
        if (category == ItemCategory.Weapon)
        {
            WeaponType weaponType = item.ItemData.WeaponType;
            if (equippedWeapons[weaponType] == item)
            {
                equippedWeapons[weaponType] = null;
                attackBonus -= item.ItemData.AttackBonus;
                Debug.Log($"무기 해제: {item.ItemData.ItemName}");
            }
        }
        else if (category == ItemCategory.Armor)
        {
            ArmorType armorType = item.ItemData.ArmorType;
            if (equippedArmors[armorType] == item)
            {
                equippedArmors[armorType] = null;
                defenseBonus -= item.ItemData.DefenseBonus;
                Debug.Log($"방어구 해제: {item.ItemData.ItemName}");
            }
        }

        // 아이템 장착 상태 변경
        item.SetEquipped(false);

        // UI 업데이트
        UpdateUI();
    }

    // 장비 해제 - 카테고리 기반 버전
    public void UnequipItem(ItemCategory category)
    {
        Item equippedItem = equippedItems[category];
        if (equippedItem != null)
        {
            UnequipItem(equippedItem);
        }
    }

    // 특정 무기 타입 장착 해제
    public void UnequipWeaponByType(WeaponType type)
    {
        Item equippedWeapon = equippedWeapons[type];
        if (equippedWeapon != null)
        {
            UnequipItem(equippedWeapon);
        }
    }

    // 특정 방어구 타입 장착 해제
    public void UnequipArmorByType(ArmorType type)
    {
        Item equippedArmor = equippedArmors[type];
        if (equippedArmor != null)
        {
            UnequipItem(equippedArmor);
        }
    }

    // 모든 장비 해제
    public void UnequipAllItems()
    {
        // 무기 장비 해제
        foreach (WeaponType type in equippedWeapons.Keys)
        {
            if (equippedWeapons[type] != null)
            {
                UnequipItem(equippedWeapons[type]);
            }
        }

        // 방어구 장비 해제
        foreach (ArmorType type in equippedArmors.Keys)
        {
            if (equippedArmors[type] != null)
            {
                UnequipItem(equippedArmors[type]);
            }
        }
    }

    // 특정 아이템 찾기
    public Item FindItem(string itemName)
    {
        foreach (Item item in items)
        {
            if (item.ItemData.ItemName == itemName)
            {
                return item;
            }
        }

        return null;
    }

    // 장착된 아이템만 가져오기
    public List<Item> GetEquippedItems()
    {
        List<Item> equipped = new List<Item>();

        // 무기 추가
        foreach (Item weapon in equippedWeapons.Values)
        {
            if (weapon != null)
                equipped.Add(weapon);
        }

        // 방어구 추가
        foreach (Item armor in equippedArmors.Values)
        {
            if (armor != null)
                equipped.Add(armor);
        }

        return equipped;
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

    // 총 장비 수 구하기
    public int GetEquippedItemCount()
    {
        int count = 0;

        foreach (Item weapon in equippedWeapons.Values)
        {
            if (weapon != null)
                count++;
        }

        foreach (Item armor in equippedArmors.Values)
        {
            if (armor != null)
                count++;
        }

        return count;
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

    // 장비 효과 문자열 얻기 (UI 표시용)
    public string GetEquipmentEffectText()
    {
        string text = "장비 효과:\n";
        text += $"공격력 보너스: +{attackBonus}\n";
        text += $"방어력 보너스: +{defenseBonus}\n";
        return text;
    }
}