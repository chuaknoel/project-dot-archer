using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    protected SpriteRenderer monsterImage;
    protected Animator monsterAnime;
    protected EnemyControllerManager EnemyController;
    protected SkillController skillController;

    public List<EnemySkill> skills = new List<EnemySkill>();

    protected Rigidbody2D rb;
    public Transform target;

    public void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        monsterImage = GetComponentInChildren<SpriteRenderer>();
        monsterAnime = GetComponent<Animator>();
        AddController();
        rb = GetComponent<Rigidbody2D>();
        target= GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDefenceStat>(out IDefenceStat damage))
        {
            if (TryGetComponent<IAttackStat>(out IAttackStat attack))
                damage.TakeDamage(attack.AttackDamage);
        }
    }
    protected virtual void AddController()
    {
        EnemyController = this.AddComponent<EnemyControllerManager>();
        skillController = this.AddComponent<SkillController>();
        EnemyController.Init(this);
    }
}
