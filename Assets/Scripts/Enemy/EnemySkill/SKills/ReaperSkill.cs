using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ReaperSkill : EnemySkill
{
    public GameObject darkPrefab;
    public int Count = 3;
    public float fireRange = 3f;

    public override void Init()
    {
        base.Init();
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
            GameObject dark = Instantiate(darkPrefab, owner.target.position + randomPosition, Quaternion.identity);

            StartCoroutine(SendDarkToPlayer(dark, owner.target));
        }

        currentCooldown = 0f;
    }

    IEnumerator SendDarkToPlayer(GameObject dark, Transform target)
    {
        yield return new WaitForSeconds(1f);

        Vector3 dir = (target.position - dark.transform.position).normalized;
        Rigidbody2D rb = dark.GetComponent<Rigidbody2D>();

        rb.velocity = dir * 7f;
    }

}