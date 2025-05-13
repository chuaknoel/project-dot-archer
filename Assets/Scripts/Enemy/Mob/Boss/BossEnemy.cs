using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossEnemy : BaseEnemy
{
    public int bossIndex;
    public BossStat bossStat;
    //BossSkillController bossSkillController;
    //MoveController moveController;
  
    private void Start()
    {
       // SettingSkills();
       // UseSkill(SelectSkill());
    }


    public override void Init()
    {
        // base.Init();
        monsterImage = GetComponentInChildren<SpriteRenderer>();
        monsterAnime = GetComponent<Animator>();
        AddController();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").gameObject.transform;

        bossStat = GetComponent<BossStat>();

    }
    protected override void AddController()
    {
        EnemyController = this.AddComponent<BossControllerManager>();
      //  skillController = this.AddComponent<Enemy>();
        EnemyController.Init(this);
    }
    //protected override void Damaged(int damage)
    //{

    //}
    //protected override void Reward()
    //{

    //***}
   
}
