using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowNova : Skill<Player>
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
        ownerCollider = player.GetComponent<Collider2D>();
        if (player.WeaponHandler is RangeWeaponHandler)
        {
            rangeWeaponHandler = player.WeaponHandler as RangeWeaponHandler;
        }
    }

    public override void UseSkill()
    {
        base.UseSkill();
        ActiveArrowNova();
    }

    public void ActiveArrowNova()
    {
        float step = 360f / arrowQuantity;       //360도로 화살을 발사해야 되서 화살갯수 / 360도로 각도를 구한다.

        StartCoroutine(eDelay(step));
    }

    IEnumerator eDelay(float step)
    {
        for (int i = 0; i < arrowQuantity; i++)
        {
            angle = i * step;
            rangeWeaponHandler.GetWeapon().transform.rotation = Quaternion.Euler(0, 0, angle);
            rangeWeaponHandler.connectedPool.Get().SetProjectile(rangeWeaponHandler, rangeWeaponHandler.projectilePivot, Quaternion.Euler(0, 0, 0), player.targetMask, ownerCollider);
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(0.2f);

        player.Controller.ChangeState(nameof(PlayerIdleState));
    }

    public override bool CanUse()
    {
        if (currentCoolTime < coolTime) { return false; }
        if (player.stat.GetUseableCost() < useCost) { return false; }
        return true;
    }
}
