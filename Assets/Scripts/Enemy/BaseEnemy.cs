using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [HideInInspector] public EnemyStat stat;
    public TargetPlayer targetPlayer;
    private SpriteRenderer monsterImage;
    private Animator monsterAnime;

    private Rigidbody2D rb;

    public void Init()
    {
        stat = GetComponent<EnemyStat>();
        monsterImage = GetComponentInChildren<SpriteRenderer>();
        monsterAnime = GetComponent<Animator>();
        targetPlayer = GetComponent<TargetPlayer>();
        rb = GetComponent<Rigidbody2D>();
    }




}
