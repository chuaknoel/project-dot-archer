using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ReaperSkill : EnemySkill
{
    public GameObject darkPrefab;
    public int Count = 3;
    public float fireRange = 3f;

    private Transform target;

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
        for (int i = 0; i < Count; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-fireRange, fireRange),
                Random.Range(-fireRange, fireRange),
                0
            );

            target = owner.target;

            GameObject dark = Instantiate(darkPrefab, target.position + randomPosition, Quaternion.identity);

            StartCoroutine(SendDarkToPlayer(dark, target));
        }
        base.UseSkill(owner);
    }

    IEnumerator SendDarkToPlayer(GameObject dark, Transform target)
    {
        yield return new WaitForSeconds(1f);

        Vector3 dir = (target.position - dark.transform.position).normalized;
        Rigidbody2D rb = dark.GetComponent<Rigidbody2D>();

        rb.velocity = dir * 7f;
    }

}