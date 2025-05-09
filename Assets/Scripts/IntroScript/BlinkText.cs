using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlinkText : MonoBehaviour
{
    public float interval = 0.4f;
    private Text uiText;

    void Awake()
    {
        uiText = GetComponent<Text>();
    }

    void OnEnable()
    {
        StartCoroutine(Blink());
    }

    void OnDisable()
    {
        StopAllCoroutines();
        uiText.enabled = true;
    }

    IEnumerator Blink()
    {
        while (true)
        {
            uiText.enabled = !uiText.enabled;
            yield return new WaitForSeconds(interval);
        }
    }
}

