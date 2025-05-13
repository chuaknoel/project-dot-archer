using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowStorm : Skill<Player>
{
    private Player player;
    private Collider2D ownerCollider;
    private RangeWeaponHandler rangeWeaponHandler;

    [SerializeField] private int arrowQuantity;
    private float angle;

    public override void Init(Player owner)
    {
        base.Init(owner);
        player = owner;
        ownerCollider  = player.GetComponent<Collider2D>();
        if(player.WeaponHandler is RangeWeaponHandler)
        {
            rangeWeaponHandler = player.WeaponHandler as RangeWeaponHandler;
        }
    }

    public override void UseSkill()
    {
        float step = arrowQuantity / 360f;       //360���� ȭ���� �߻��ؾ� �Ǽ� ȭ�찹�� / 360���� ������ ���Ѵ�.

        for (int i = 0; i <= arrowQuantity; i++)
        {
            angle = i * step;

            rangeWeaponHandler.connectedPool.Get().SetProjectile(rangeWeaponHandler, rangeWeaponHandler.projectilePivot, Quaternion.Euler(0, 0, angle), player.targetMask, ownerCollider);
        }
    }
}
