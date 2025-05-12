using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class RandomFlame : EnemySkill
{
    public GameObject firePrefab;
    public float fireInterval = 1f;
    public int fireCount = 3;
    public float fireRange = 3f;
    public bool isTrigger = false;
    void Start()
    {
        currentCooldown = cooldown;
    }
    void Update()
    {
        currentCooldown += Time.deltaTime;
    }
    public override void UseSkill(BaseEnemy owner)
    {
        if (CanUse())
        {
            Debug.Log("Throwing fire!");
            GameObject fireball = Instantiate(firePrefab, owner.transform.position, Quaternion.identity);
            Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
            Vector3 dir = (owner.target.position - fireball.transform.position).normalized;

            rb.velocity = dir * 7f;

        }
    }

   

          
}

