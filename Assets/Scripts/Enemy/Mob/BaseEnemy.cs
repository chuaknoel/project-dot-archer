using Enums;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{

    public LayerMask obstacleLayer = 1 << 4;

    public SpriteRenderer monsterImage;
    protected Animator monsterAnime;
    public EnemyController Controller { get { return controller; } }
    private EnemyController controller;

    public EnemyStat enemyStat;
    public List<EnemySkill> skills = new List<EnemySkill>();
    public EnemySkill currentSkill;
    public EnemySkill skillPrefab;

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

    public virtual void Init()
    {
        monsterImage = GetComponent<SpriteRenderer>();
        monsterAnime = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        enemyStat = GetComponent<EnemyStat>();
        ControllerRegister();
        currentSkill = Instantiate(skillPrefab, transform);
        currentSkill.Init();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStat playerStat = collision.gameObject.GetComponent<PlayerStat>();
            if (this is IAttackStat attack)
                playerStat.TakeDamage(attack.AttackDamage);
        }
        
    }

    public void ControllerRegister()
    {
        controller = new EnemyController(new EnemyMoveState(), this);
        controller.RegisterState(new EnemySkillState(), this);
    }

    //public void ChangeState(EnemyState nextState)
    //{
    //    controller.ChangeState(nextState);
    //}

    public void UseSkill()
    {
        if (currentSkill.CanUse())
        {
            currentSkill.UseSkill(this);
            controller.ChangeState(nameof(EnemySkillState));
        }
    }
}
