using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatRandomFire : MonoBehaviour
{
    private bool isTrigger = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            isTrigger = true;
            transform.localScale *= 7f;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            
            Destroy(gameObject, 3f);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject);
        }
        
    }
}
