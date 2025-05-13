using System;

namespace Enums
{
    public enum ROOMTYPE
    {
        Normal,     // 일반 방
        Long,       //긴 방
        Boss       // 보스 방
    }

    public enum PlayerState
    {
        Idle,
        Move,
        Attack,
        Jump,
        Death,
    }

    public enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Death,
        Skill
    }

    // 아이템 카테고리 열거형
    // 무기 타입 열거형 - None 추가
    public enum WeaponType
    {
        None, // 기본값으로 추가
        Bow,  // 활
        Sword, // 검
        Scythe // 낫
    }

    public enum AttackTpye
    {
        Range,
        Melee
    }

    // 방어구 타입 열거형 - None 추가
    public enum ArmorType
    {
        None, // 기본값으로 추가
        Helmet, // 투구
        Armor,  // 갑옷
        Boots   // 신발
    }

    // 아이템 카테고리 열거형 - None 추가
    public enum ItemCategory
    {
        None, // 기본값으로 추가
        Weapon,
        Armor
    }

    // 아이템 등급 열거형
    public enum ItemRarity
    {
        Common,     // 일반
        Uncommon,   // 좋은
        Rare,       // 더좋은
        Epic        // 너무좋은
    }
}