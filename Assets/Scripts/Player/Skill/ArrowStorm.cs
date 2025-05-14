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

    [SerializeField] float stormAngle;
    [SerializeField] float stormDelay;
    [SerializeField] int stormBurst;

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
        base.UseSkill();
        ActiveArrowStorm();
    }

    public void ActiveArrowStorm()
    {
        StartCoroutine(eStormBurst());
    }

    public void StormProjectile()
    {
        float step = stormAngle / rangeWeaponHandler.GetProjectileCount() -1 ; //간격을 구해려면 배치된 화살수 -1 을 해줘야한다.

        int count = rangeWeaponHandler.GetProjectileCount();
        int halfCount = count / 2;
        float angle = 0;

        for (int i = -halfCount; i <= halfCount; i++)
        {
            if (count % 2 == 0 && i == -halfCount)
            {
                continue;
            }

            angle = (i * step);

            rangeWeaponHandler.connectedPool.Get().SetProjectile(rangeWeaponHandler, rangeWeaponHandler.projectilePivot, Quaternion.Euler(0, 0, angle), player.targetMask, ownerCollider);
        }
    }

    IEnumerator eStormBurst()
    {
        for (int i = 0; i < stormBurst; i++)
        {
            StormProjectile();
            yield return new WaitForSeconds(stormDelay);
        }

        yield return new WaitForSeconds(0.2f); //스킬 시전 후 잠시대기 후 Idle로 이동

        player.Controller.ChangeState(nameof(PlayerIdleState));
    }

    public override bool CanUse()
    {
        if (currentCoolTime < coolTime) { return false; }
        if (player.stat.GetUseableCost() < useCost) { return false; }
        return true;
    }
}
