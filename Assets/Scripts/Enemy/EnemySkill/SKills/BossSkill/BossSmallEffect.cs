using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSmallEffect : BossEffect
{
    private void OnEnable()
    {
        base.IgnoreCollision();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Wall")
        {
            Destroy(gameObject);
        }      
    }
}
