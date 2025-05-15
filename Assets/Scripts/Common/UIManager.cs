using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        GameManager.Instance.inventory.OnGoldChanged += SetCoinUI;
    }

    public TextMeshProUGUI coinText;

    public void SetCoinUI(int coin)
    {
        coinText.text = coin.ToString();
    }
}
