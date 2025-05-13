using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

[System.Serializable]
public class ItemData
{
    // �⺻ ����
    public string itemId;
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;
    public ItemRarity itemRarity;

    // �з�
    public ItemCategory itemCategory;
    public WeaponType weaponType;
    public ArmorType armorType;

    // ����
    public float attackBonus;  // ���ݷ� ���ʽ�
    public float defenseBonus; // ���� ���ʽ�
    public float price;        // ����

    // ���� �߰��� ���� ���� ����
    public float attackRange;   // ���� ����
    public float attackDelay;   // ���� �� �ٸ� �ൿ ���ɱ����� �ð�
    public float attackCooldown; // ���� ��Ÿ��(���� ���ݱ����� �ð�)

    // ������Ƽ
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

    // ���� ���� �޼���
    public ItemData Clone()
    {
        return (ItemData)this.MemberwiseClone();
    }
}