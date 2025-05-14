using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossRangedAttackEffect : MonoBehaviour
{
   public bool attackAble;

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

        // ���� ����
        TriggerAttack();

    }
    // ���� �����ȿ� �÷��̾ ������ ����
    private void TriggerAttack()
    {
        if(attackAble)
        {
            var target = DungeonManager.Instance.player.GetComponent<IDefenceStat>();
            target?.TakeDamage(10f);
        }

        // ����Ʈ�� 1�� �� �ڵ� ����
        Destroy(this.gameObject, 1f);
    }
}
