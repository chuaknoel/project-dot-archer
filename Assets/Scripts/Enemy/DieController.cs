using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieController : MonoBehaviour
{
    public float blinkDuration = 1f;
    public float blinkInterval = 0.1f;
    public bool isDie = false;



    
    

    public void Die(BaseEnemy owner)
    {
        if (!isDie) return;
        isDie = true;
        StartCoroutine(Blink(owner.monsterImage));
    }

    private IEnumerator Blink(SpriteRenderer owner)
    {
        float elapsedTime = 0f;
        Color originalColor = owner.color;
        while (elapsedTime < blinkDuration)
        {
            owner.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.PingPong(elapsedTime / blinkInterval, 1));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        owner.color = originalColor;
        Destroy(gameObject);
    }

}
