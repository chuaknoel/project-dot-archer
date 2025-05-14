using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossControllerManager : EnemyControllerManager
{
    BossEffectController effectController;
    private ISkillController skillInterface;
   
    public float trackingtDis = 20f; // 추적 거리
    public float rangedAttacktDis = 7f; //  원거리 공격 거리
    public float closeRangeAttack = 2f; // 근거리 공격 거리

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
        // 추적 거리를 벗어났다면
        if (!Distance(trackingtDis)) { return; }

        // 스킬 랜덤 선택
        SelectSkill();

        // 근거리 공격이라면
        if (currentSkill.skillName == "BossSkill04" )
        {
            // 공격 
            if (CheckCloseRangeAttack())
            {
                Attack(closeRangeAttack);
            }
        }
        // 원거리 공격이라면
        else
        {
            // 공격 
            Attack(rangedAttacktDis);
        }
    }
    // 스킬 랜덤 선택
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
                Debug.LogWarning("currentSkill이 null입니다. SelectRandomSkill()에서 문제가 발생한 것 같습니다.");
            }
            // lastSkillCheckTime을 갱신
            lastSkillCheckTime = Time.time;
        }
    }
    void Attack(float attackDis)
    {
        // 공격 거리 이내라면
        if (Distance(attackDis))
        {
            if (bossSkillController.canUse)
            {
                // 스킬 발사
                animator.SetBool("isMoving", false);
                bossSkillController.UseSkill(ownerEnemy);
            }
        }
        else
        {
            // 도착 했다면
            if(Distance(attackDis))
            {
                animator.SetBool("isMoving", false);
            }
            // 도착하지 않았다면
            else
            {
                // 이동 가능하다면 이동
                if (bossSkillController.canMove && ownerStat is IMoveStat moveStat)
                {
                    moveController.MoveToPlayer(ownerEnemy.target, moveStat);
                    animator.SetBool("isMoving", true);
                }
            }
           
        }
    }
    // 이미 장판형 스킬이 존재하는지 검사
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
