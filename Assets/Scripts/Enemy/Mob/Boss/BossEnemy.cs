using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossEnemy : BaseEnemy
{
    public int bossIndex;
    public BossStat bossStat;


    public override void Init()
    {
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
        EnemyController.Init(this);
    }
    //protected override void Damaged(int damage)
    //{

    //}
    //protected override void Reward()
    //{

    //***}
   
}
