using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dark : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStat playerStat = collision.GetComponent<PlayerStat>();
            if (playerStat != null)
            {
                playerStat.TakeDamage(1);
            }
            Destroy(gameObject);
        }

        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject,3f );
        }
    }
}