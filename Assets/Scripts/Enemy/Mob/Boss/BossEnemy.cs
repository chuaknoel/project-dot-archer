using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossEnemy : BaseEnemy
{
    public int bossIndex;
    public BossStat bossStat;
    BossSkillController bossSkillController;
    MoveController moveController;
  
    private void Start()
    {
       // SettingSkills();
       // UseSkill(SelectSkill());
    }


    public override void Init()
    {
        monsterImage = GetComponentInChildren<SpriteRenderer>();
        monsterAnime = GetComponent<Animator>();
        EnemyController = this.AddComponent<EnemyControllerManager>();
        bossSkillController = this.AddComponent<BossSkillController>();

        EnemyController.Init(this);
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        moveController = this.AddComponent<MoveController>();
        bossStat = GetComponent<BossStat>();

    }
    
    //protected override void Damaged(int damage)
    //{

    //}
    //protected override void Reward()
    //{

    //***}

}
