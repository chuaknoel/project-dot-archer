using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.UIElements;

public class BossRangedAttackEffect : MonoBehaviour
{
   public bool attackAble;
    GameObject effect;
    private void OnEnable()
    {
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        transform.SetParent(boss.transform);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            attackAble = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            attackAble = false;
        }
    }
    public IEnumerator ExecuteAttack(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 공격 실행
        TriggerAttack();

    }
    // 공격 범위안에 플레이어가 있을시 공격
    private void TriggerAttack()
    {  
        if (attackAble)
        {
            var target = DungeonManager.Instance.player.GetComponent<IDefenceStat>();
            target?.TakeDamage(10f);
        }
        // 폭발 이펙트 추가
        GameObject effectPrefab = Resources.Load<GameObject>("Effects/BossParticleSystem");
        effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);

        // 이펙트는 1초 후 자동 삭제
        Destroy(effect,0.8f);  
        Destroy(this.gameObject,0.2f);
    }
}
