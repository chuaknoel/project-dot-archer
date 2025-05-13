using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEffect : MonoBehaviour, IDefenceStat
{
    public int count;
    public GameObject smalleffect;
    public float effecDamage;
    Rigidbody rb;
    public float damage;

    public float Defence => 10f;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        IgnoreCollision();
    }
    // 충돌 무시
    protected void IgnoreCollision()
    {
        Collider2D myCollider = GetComponent<Collider2D>();

     //   Collider2D playerCollider = GameObject.Find("Player").GetComponent<Collider2D>();
     //   Physics2D.IgnoreCollision(playerCollider, myCollider);

        Collider2D bossCollider = GameObject.FindGameObjectWithTag("Boss").GetComponent<Collider2D>(); ;
        Physics2D.IgnoreCollision(bossCollider, myCollider);

        GameObject[] effectsCollider = GameObject.FindGameObjectsWithTag("BossEffect");
        foreach (var effect in effectsCollider)
        {
            Collider2D _effect = effect.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(_effect, myCollider);
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            count++;
            // 벽에 두번 부딪히면
            if (count == 2)
            {
                BossSkillController bossSkillController = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossSkillController>();
                // 작은 이펙트 세개 생성
                bossSkillController.ThreeShot(this.transform, smalleffect);
                // 삭제
                Destroy(gameObject);
            }

        }
        else if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // collision.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;

            // 플레이어 피격
            col.gameObject.GetComponent<IDefenceStat>()?.TakeDamage(10f);
          //  stat.GetComponent<IDefenceStat>()?.TakeDamage(10f);
            Debug.Log(col.gameObject.GetComponent<Player>().stat.CurrentHealth);
            // 삭제
            Destroy(gameObject);
        }
        else
        if (col.gameObject.tag == ("Boss"))
        {
           // Debug.Log("보스 충돌");
            // 삭제
            //Destroy(gameObject);
        }
    }
  
}
