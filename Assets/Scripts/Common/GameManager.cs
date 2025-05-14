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
        dataPath = Application.persistentDataPath + directoryPath; //유니티에서 정해준 폴더 아래에 Save라는 디렉토리 폴더를 만들어서 저장 데이터 관리
        directoryInfo = new DirectoryInfo(dataPath);               //디렉토리를 탐색할 디렉토리 인포
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
        CreateSavePath(); //방어 코드 : 경로가 삭제되었을 경우를 대비해 경로 확인을 한번 해준다.

        string saveData = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(dataPath + saveFile, saveData);
        Debug.Log($"{saveData} : 저장 완료");
    }

    public void LoadData()
    {
        string loadData = File.ReadAllText(dataPath + saveFile);
        gameData = JsonUtility.FromJson<GameData>(loadData);
        Debug.Log($"세이브 데이터 로드 : {gameData}");
    }

    public void LoadDefaultData()
    {
        string loadData = File.ReadAllText(dataPath + defaultFile);
        gameData = JsonUtility.FromJson<GameData>(loadData);
        Debug.Log($"디폴트 데이터 로드 : {gameData}");
    }

    public bool CheckSaveData()
    {
        FileInfo[] gameDatas = directoryInfo.GetFiles();
        foreach (FileInfo data in gameDatas)
        {
            if (data.Name == saveFile)
            {
                Debug.Log("파일 불러오기");
                return true;
            }
        }

        Debug.Log("저장된 파일이 없습니다.");
        return false;
    }

    public void CreateSavePath()
    {
        if (!Directory.Exists(dataPath))
        {
            Debug.Log($"저장 경로를 찾이 못했습니다.");
            Debug.Log($"경로 생성 : {dataPath}");
            Directory.CreateDirectory(dataPath);
        }
    }
}
