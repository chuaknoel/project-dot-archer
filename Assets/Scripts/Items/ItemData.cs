using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템 카테고리 열거형
public enum ItemCategory
{
    Weapon,
    Armor
}

// 무기 타입 열거형
public enum WeaponType
{
    Bow,    // 활
    Sword,  // 검
    Scythe  // 낫
}

// 방어구 타입 열거형
public enum ArmorType
{
    Helmet,  // 투구
    Armor,   // 갑옷
    Boots    // 신발
}

// 아이템 등급 열거형
public enum ItemRarity
{
    Common,     // 일반
    Uncommon,   // 좋은
    Rare,       // 더좋은
    Epic        // 너무좋은
}

[System.Serializable]
public class ItemData
{
    // 기본 정보
    public string itemId;
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;
    public ItemRarity itemRarity;

    // 분류
    public ItemCategory itemCategory;
    public WeaponType weaponType;
    public ArmorType armorType;

    // 스탯
    public float attackBonus;  // 공격력 보너스
    public float defenseBonus; // 방어력 보너스
    public float price;        // 가격

    // 프로퍼티
    public string ItemId => itemId;
    public string ItemName => itemName;
    public string ItemDescription => itemDescription;
    public Sprite ItemIcon => itemIcon;
    public ItemRarity ItemRarity => itemRarity;
    public ItemCategory ItemCategory => itemCategory;
    public WeaponType WeaponType => weaponType;
    public ArmorType ArmorType => armorType;
    public float AttackBonus => attackBonus;
    public float DefenseBonus => defenseBonus;
    public float Price => price;

    // 깊은 복사 메서드
    public ItemData Clone()
    {
        return (ItemData)this.MemberwiseClone();
    }
}