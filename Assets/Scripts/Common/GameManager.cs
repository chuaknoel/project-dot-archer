using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public ItemManager itemManager;
    public Inventory inventory;
    public UpgradeManager upgradeManager;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        itemManager.Init();
        inventory.Init();
        upgradeManager.Init();
    }

    public void SaveData()
    {

    }

    public void LoadData()
    {

    }
}
