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

    public List<EnemySkill> skills = new List<EnemySkill>();


    protected Rigidbody2D rb;
    public Transform target;

    public void Awake()
    {
        Init();
    }

    
    public virtual void Init()
    {
        monsterImage = GetComponent<SpriteRenderer>();
        monsterAnime = GetComponent<Animator>();
        EnemyController = this.AddComponent<EnemyControllerManager>();
        EnemyController.Init(this);
        rb = GetComponent<Rigidbody2D>();
        target= GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDefenceStat>(out IDefenceStat damage))
        {
            if (TryGetComponent<IAttackStat>(out IAttackStat attack))
                damage.TakeDamage(attack.AttackDamage);
        }
    }
    
}
