using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserk : EnemySkill
{
    [SerializeField] private float activeTime = 3f;
    private float currentActiveTime = 0f;
    [SerializeField] private float value = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        cooldown = 8f;
        currentCooldown = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        currentCooldown += Time.deltaTime;

    }

    public override void UseSkill(BaseEnemy owner)
    {
        if (CanUse())
        {
            Debug.Log("Berserk!");
            if (owner is IAttackStat attack)
            {
                attack.AddDamage(value);
                currentActiveTime += Time.deltaTime;
            }

            if (owner is IMoveStat move)
            {
                move.AddSpeed(value);
                currentActiveTime += Time.deltaTime;
            }
        }
    }

    public void EndSkill(BaseEnemy owner)
    {
        Debug.Log("EndSkill!");
        if (owner is IAttackStat attack && currentActiveTime > activeTime)
        {
            attack.AddDamage(-value);
            currentActiveTime = 0f;
        }

        if (owner is IMoveStat move && currentActiveTime > activeTime)
        {
            move.AddSpeed(-value);
            currentActiveTime = 0f;
        }
    }
}
