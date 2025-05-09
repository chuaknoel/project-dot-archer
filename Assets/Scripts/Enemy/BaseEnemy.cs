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
    protected BaseEnemyController EnemyController;
    
    protected Rigidbody2D rb;
    public Transform target;

    public void Awake()
    {
        Init();
    }

    void Update()
    {
        //EnemyController.MoveToPlayer(target);
    }

    public virtual void Init()
    {
        monsterImage = GetComponentInChildren<SpriteRenderer>();
        monsterAnime = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        EnemyController = this.AddComponent<BaseEnemyController>();
        EnemyController.Init(this);
        target??= GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void UseSkill()
    {

    }






}
