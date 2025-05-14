using Enums;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public SpriteRenderer monsterImage;
    protected Animator monsterAnime;
    public EnemyController Controller { get { return controller; } }
    private EnemyController controller;

    public List<EnemySkill> skills = new List<EnemySkill>();
    public EnemyStat enemyStat;

    public EnemySkill currentSkill;
    public EnemySkill skillPrefab;

    public EnemyStates State { get { return state; } }
    private EnemyStates state;

    public LayerMask obstacleLayer = 1 << 4;
    
    protected Rigidbody2D rb;
    public Transform target;

    public void Awake()
    {
        Init();
    }
    private void Update()
    {
        controller?.OnUpdate(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        controller?.OnFixedUpdate();
    }

    public virtual void Init()
    {
        enemyStat = GetComponent<EnemyStat>();
        monsterImage = GetComponent<SpriteRenderer>();
        monsterAnime = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        ControllerRegister();
        currentSkill = Instantiate(skillPrefab, transform);
        currentSkill.Init();
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStat player = collision.gameObject.GetComponent<PlayerStat>();
            if (this.enemyStat is IAttackStat attackStat)
            {
                player.TakeDamage(attackStat.AttackDamage);
                Debug.Log($"{this.gameObject.name} took {attackStat.AttackDamage} damage from enemy melee attack.");
            }
            else
            {
                // Handle case where player does not implement IDefenceStat
                Debug.LogWarning("Player does not implement IDefenceStat");
            }
        }
    }

    public void ControllerRegister()
    {
        controller = new EnemyController(new EnemyMoveState(), this);
        controller.RegisterState(new EnemyAttackState(), this);
        controller.RegisterState(new EnemySkillState(), this);
    }

    public void UseSkill()
    {
            currentSkill.UseSkill(this);
            controller.ChangeState(nameof(EnemySkillState));
    }
}

//public void ChangeAnime(EnemyState nextAnim)
//{
//    if (nextAnim == EnemyState.Death)
//    {
//        monsterAnime.SetTrigger("Death");
//    }
//    else
//    {
//        monsterAnime.SetInteger("ChangeState", (int)nextAnim);
//    }

//}



