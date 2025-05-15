using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowStone : EnemySkill
{
    public GameObject stonePrefab;
    public Transform throwPoint;
    public float throwForce = 10f;
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
        Debug.Log("Throwing stone!");
        Vector3 dir = (owner.target.position - throwPoint.position).normalized;
        GameObject stone = Instantiate(stonePrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody2D rb = stone.GetComponent<Rigidbody2D>();
        rb.velocity = dir * throwForce;
        base.UseSkill(owner);
    }
}
