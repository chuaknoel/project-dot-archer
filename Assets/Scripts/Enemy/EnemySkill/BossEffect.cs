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
    // �浹 ����
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
            // ���� �ι� �ε�����
            if (count == 2) 
            {
                // ���� ����Ʈ ���� ����
                BossSkill.ThreeShot(this.transform, smalleffect);
                // ����
                Destroy(gameObject); 
            }
        }
    }
}
