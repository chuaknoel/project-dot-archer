using System;

namespace Enums
{
    public enum ROOMTYPE
    {
        Normal,     // �Ϲ� ��
        Long,       //�� ��
        Boss       // ���� ��
    }

    public enum PlayerState
    {
        Idle,
        Move,
        Attack,
        Jump,
        Death,
        Action,
    }

    public enum SkillType
    {
        None,
        Attack,
        Defence,
        Buff,
        Debuff,
        Dot
    }

    // ������ ī�װ� ������
    // ���� Ÿ�� ������ - None �߰�
    public enum WeaponType
    {
        None, // �⺻������ �߰�
        Bow,  // Ȱ
        Sword, // ��
        Scythe // ��
    }

    public enum AttackTpye
    {
        Range,
        Melee
    }

    // �� Ÿ�� ������ - None �߰�
    public enum ArmorType
    {
        None, // �⺻������ �߰�
        Helmet, // ����
        Armor,  // ����
        Boots   // �Ź�
    }

    // ������ ī�װ� ������ - None �߰�
    public enum ItemCategory
    {
        None, // �⺻������ �߰�
        Weapon,
        Armor
    }

    // ������ ��� ������
    public enum ItemRarity
    {
        Common,     // �Ϲ�
        Uncommon,   // ����
        Rare,       // ������
        Epic        // �ʹ�����
    }
}