using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponHandler : WeaponHandler
{
    public override void Rotate(Vector3 angle)
    {
        base.Rotate(angle);
        transform.rotation = Quaternion.Euler(angle);
    }

    public override void AttackAction()
    {
        base.AttackAction();

    }
}
