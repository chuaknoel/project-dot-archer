using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

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

    // 새로 추가한 무기 관련 스탯
    public float attackRange;   // 공격 범위
    public float attackDelay;   // 공격 후 다른 행동 가능까지의 시간
    public float attackCooldown; // 공격 쿨타임(다음 공격까지의 시간)

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
    public float AttackRange => attackRange;
    public float AttackDelay => attackDelay;
    public float AttackCooldown => attackCooldown;

    // 깊은 복사 메서드
    public ItemData Clone()
    {
        return (ItemData)this.MemberwiseClone();
    }
}