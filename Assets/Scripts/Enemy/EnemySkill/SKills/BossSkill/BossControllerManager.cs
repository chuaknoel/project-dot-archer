using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossControllerManager : EnemyControllerManager
{
    BossEffectController effectController;
    private ISkillController skillInterface;
    public float attackDis = 7f; // ���� �Ÿ�
    public float trackingtDis = 20f; // ���� �Ÿ�
    public float stoptDis = 1f; // ��ž �Ÿ�
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
        // ���� �Ÿ��� ����ٸ�
        if (!Distance(trackingtDis)) { return; }
        
        // ���� �Ÿ� �̳����
       if (Distance(attackDis))
        {
            // � ��ų�� ���� ���ϰ�
            // ���� ���� ��ų�̶��
            // �÷��̾������� �̵�
            // �ƴϸ� ��ų ���
            // ���� �ð����� �� ���� ��ų ����
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
