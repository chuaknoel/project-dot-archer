using Enums;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public SpriteRenderer monsterImage;
    protected Animator monsterAnime;
    protected EnemyControllerManager EnemyController;
    public EnemyController Controller { get { return controller; } }
    private EnemyController controller;

    public List<EnemySkill> skills = new List<EnemySkill>();


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
        //EnemyController = this.AddComponent<EnemyControllerManager>();
        //EnemyController.Init(this);
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        ControllerRegister();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDefenceStat>(out IDefenceStat damage))
        {
            if (TryGetComponent<IAttackStat>(out IAttackStat attack))
                damage.TakeDamage(attack.AttackDamage);
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

    public void ControllerRegister()
    {
        controller = new EnemyController(new EnemyMoveState(), this);
    }
}
