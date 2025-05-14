using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    private DirectoryInfo directoryInfo;

    [SerializeField] private string directoryPath;
    private string dataPath;

    [SerializeField] private string saveFile;
    [SerializeField] private string defaultFile;

    public GameData gameData = new();

    private void Start()
    {
        DataSetting();
        Init();
    }

    public void Init()
    {
        itemManager.Init();
        inventory.Init();
        upgradeManager.Init();
    }

    public void DataSetting()
    {
        dataPath = Application.persistentDataPath + directoryPath; //����Ƽ���� ������ ���� �Ʒ��� Save��� ���丮 ������ ���� ���� ������ ����
        directoryInfo = new DirectoryInfo(dataPath);               //���丮�� Ž���� ���丮 ����
        CreateSavePath();
        Debug.Log(dataPath);

        if (CheckSaveData())
        {
            LoadData();
        }
        else
        {
            LoadDefaultData();
        }
    }

    public void SaveData()
    {
        CreateSavePath(); //��� �ڵ� : ��ΰ� �����Ǿ��� ��츦 ����� ��� Ȯ���� �ѹ� ���ش�.

        string saveData = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(dataPath + saveFile, saveData);
        Debug.Log($"{saveData} : ���� �Ϸ�");
    }

    public void LoadData()
    {
        string loadData = File.ReadAllText(dataPath + saveFile);
        gameData = JsonUtility.FromJson<GameData>(loadData);
        Debug.Log($"���̺� ������ �ε� : {gameData}");
    }

    public void LoadDefaultData()
    {
        string loadData = File.ReadAllText(dataPath + defaultFile);
        gameData = JsonUtility.FromJson<GameData>(loadData);
        Debug.Log($"����Ʈ ������ �ε� : {gameData}");
    }

    public bool CheckSaveData()
    {
        FileInfo[] gameDatas = directoryInfo.GetFiles();
        foreach (FileInfo data in gameDatas)
        {
            if (data.Name == saveFile)
            {
                Debug.Log("���� �ҷ�����");
                return true;
            }
        }

        Debug.Log("����� ������ �����ϴ�.");
        return false;
    }

    public void CreateSavePath()
    {
        if (!Directory.Exists(dataPath))
        {
            Debug.Log($"���� ��θ� ã�� ���߽��ϴ�.");
            Debug.Log($"��� ���� : {dataPath}");
            Directory.CreateDirectory(dataPath);
        }
    }
}
