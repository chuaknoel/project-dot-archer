using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSmallEffect : BossEffect
{
    private void OnEnable()
    {
        base.IgnoreCollision();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Wall"))           
        {
            Destroy(gameObject);
        }   
       else if(col.gameObject.tag == "Player")
        {           
            
            Destroy(gameObject);
        }
    }
}
