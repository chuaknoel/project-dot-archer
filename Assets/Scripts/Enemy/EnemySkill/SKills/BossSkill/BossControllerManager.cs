using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossControllerManager : EnemyControllerManager
{
    BossEffectController effectController;
    private ISkillController skillInterface;
    public float attackDis = 7f; // 공격 거리
    public float trackingtDis = 20f; // 추적 거리
    public float stoptDis = 1f; // 스탑 거리
    private float skillCheckDelay = 0.5f;
    private float lastSkillCheckTime = 0f;
    private BossSkill currentSkill;
    BossSkillController bossSkillController;
    public override void Init(BaseEnemy ownerEnemy)
    {
        // base.Init(ownerEnemy);
        this.ownerEnemy = ownerEnemy;
        ownerStat = ownerEnemy.GetComponent<BaseStat>();
        moveController = this.AddComponent<MoveController>();
        InitSkillController();
      //  skillController.AddSkill(ownerEnemy);
        skillInterface = GetComponent<ISkillController>();

    }
    protected override void Update()
    { 
    }
    protected  void FixedUpdate()
    {
        // 추적 거리를 벗어났다면
        if (!Distance(trackingtDis)) { return; }
        
        // 공격 거리 이내라면
       if (Distance(attackDis))
        {
            // 어떤 스킬을 쓸지 정하고
            // 범위 공격 스킬이라면
            // 플레이어쪽으로 이동
            // 아니면 스킬 사용
            // 일정 시간마다 한 번만 스킬 선택
            if (Time.time - lastSkillCheckTime > skillCheckDelay)
            {
                currentSkill = bossSkillController.SelectRandomSkill();
                bossSkillController.selectSkill = currentSkill.skillName;
                lastSkillCheckTime = Time.time;

                if (currentSkill.skillName == "BossSkill04")
                {
                    if (bossSkillController.canMove && ownerStat is IMoveStat moveStat && !Distance(stoptDis))
                    {
                        moveController.MoveToPlayer(ownerEnemy.target, moveStat);
                    }
                }
                else
                {
                    if (bossSkillController.canUse)
                    {
                        skillInterface.UseSkill(ownerEnemy);
                    }
                }
            }

          //  var skill = bossSkillController.SelectRandomSkill();
          //  bossSkillController.selectSkill = skill.skillName;

           
           
        }
        else
        {
            if (bossSkillController.canMove && ownerStat is IMoveStat moveStat)
            {
                moveController.MoveToPlayer(ownerEnemy.target, moveStat);
            }
        }
    }

    bool Distance(float dis )
    {
        return Vector2.Distance(transform.position, DungeonManager.Instance.player.transform.position) <= dis ? true : false;
    }
    public override void InitSkillController()
    {
        effectController = this.AddComponent<BossEffectController>();
        bossSkillController = this.AddComponent<BossSkillController>();
        //skillController = this.AddComponent<SkillController>();


    }
}
