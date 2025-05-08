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
    protected TargetPlayer targetPlayer;
    protected Rigidbody2D rb;


    public void Awake()
    {
        Init();
    }

     void Update()
    {
        targetPlayer.FollowPlayer();
    }

    public virtual void Init()
    {
        monsterImage = GetComponentInChildren<SpriteRenderer>();
        monsterAnime = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        targetPlayer = this.AddComponent<TargetPlayer>();
        targetPlayer.Init();
    }

    public virtual void UseSkill()
    {

    }






}
