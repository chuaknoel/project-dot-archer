using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEffect : MonoBehaviour
{
    private int count;
    public GameObject smalleffect;

    private void OnEnable()
    {
        IgnoreCollision();
    }
    // 충돌 무시
    protected void IgnoreCollision()
    {
        Collider2D myCollider = GetComponent<Collider2D>();

        Collider2D playerCollider = GameObject.Find("Player").GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(playerCollider, myCollider);

        Collider2D bossCollider = GameObject.Find("Boss").GetComponent<Collider2D>(); ;
        Physics2D.IgnoreCollision(bossCollider, myCollider);

        GameObject[] effectsCollider = GameObject.FindGameObjectsWithTag("BossEffect");
        foreach (var effect in effectsCollider)
        {
            Collider2D _effect = effect.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(_effect, myCollider);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Wall")
        {
            count++;
            // 벽에 두번 부딪히면
            if (count == 2) 
            {
                // 작은 이펙트 세개 생성
                BossSkill.ThreeShot(this.transform, smalleffect);
                // 삭제
                Destroy(gameObject); 
            }
        }
    }
}
