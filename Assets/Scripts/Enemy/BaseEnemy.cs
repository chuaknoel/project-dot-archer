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
    protected EnemyController EnemyController;
    
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
        rb = GetComponent<Rigidbody2D>();
        EnemyController = this.AddComponent<EnemyController>();
        EnemyController.Init(this);
        target= GameObject.FindGameObjectWithTag("Player").transform;
    }

    

    protected virtual void UseSkill()
    {

    }


}
