using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossControllerManager : EnemyControllerManager
{
    BossEffectController effectController;
    private ISkillController skillInterface;
   
    public float trackingtDis = 20f; // ���� �Ÿ�
    public float rangedAttacktDis = 7f; //  ���Ÿ� ���� �Ÿ�
    public float closeRangeAttack = 2f; // �ٰŸ� ���� �Ÿ�

    private float skillCheckDelay = 6f;
    private float lastSkillCheckTime = -999f;
    private BossSkill currentSkill;
    BossSkillController bossSkillController;
    Animator animator;
    public override void Init(BaseEnemy ownerEnemy)
    {
        // base.Init(ownerEnemy);
        this.ownerEnemy = ownerEnemy;
        ownerStat = ownerEnemy.GetComponent<BaseStat>();
        moveController = this.AddComponent<MoveController>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        InitSkillController();
      //  skillController.AddSkill(ownerEnemy);
        skillInterface = GetComponent<ISkillController>();

    }
    protected override void Update()
    {  
        // ���� �Ÿ��� ����ٸ�
        if (!Distance(trackingtDis)) { return; }

        // ��ų ���� ����
        SelectSkill();

        // �ٰŸ� �����̶��
        if (currentSkill.skillName == "BossSkill04" )
        {
            // ���� 
            if (CheckCloseRangeAttack())
            {
                Attack(closeRangeAttack);
            }
        }
        // ���Ÿ� �����̶��
        else
        {
            // ���� 
            Attack(rangedAttacktDis);
        }
    }
    // ��ų ���� ����
    void SelectSkill()
    {
        if (Time.time - lastSkillCheckTime > skillCheckDelay)
        {
            currentSkill = bossSkillController.SelectRandomSkill();
            if (currentSkill != null)
            {
                bossSkillController.selectSkill = currentSkill.skillName;
            }
            else
            {
                Debug.LogWarning("currentSkill�� null�Դϴ�. SelectRandomSkill()���� ������ �߻��� �� �����ϴ�.");
            }
            // lastSkillCheckTime�� ����
            lastSkillCheckTime = Time.time;
        }
    }
    void Attack(float attackDis)
    {
        // ���� �Ÿ� �̳����
        if (Distance(attackDis))
        {
            if (bossSkillController.canUse)
            {
                // ��ų �߻�
                animator.SetBool("isMoving", false);
                bossSkillController.UseSkill(ownerEnemy);
            }
        }
        else
        {
            // ���� �ߴٸ�
            if(Distance(attackDis))
            {
                animator.SetBool("isMoving", false);
            }
            // �������� �ʾҴٸ�
            else
            {
                // �̵� �����ϴٸ� �̵�
                if (bossSkillController.canMove && ownerStat is IMoveStat moveStat)
                {
                    moveController.MoveToPlayer(ownerEnemy.target, moveStat);
                    animator.SetBool("isMoving", true);
                }
            }
           
        }
    }
    // �̹� ������ ��ų�� �����ϴ��� �˻�
    bool CheckCloseRangeAttack()
    {
        return GameObject.FindGameObjectWithTag("BossRangedAttackEffect") == null;
    }
    bool Distance(float dis )
    {
        return Vector2.Distance(transform.position, DungeonManager.Instance.player.transform.position) <= dis ? true : false;
    }
    public override void InitSkillController()
    {
        effectController = this.AddComponent<BossEffectController>();
        bossSkillController = this.AddComponent<BossSkillController>();
    }

}
