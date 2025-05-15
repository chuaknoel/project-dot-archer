using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEditor.EditorTools;
using UnityEngine;
using static UnityEngine.Mesh;

public class DungeonManager : MonoBehaviour //�� �̵�, ��ü �帧 �� �� ��ü å����
{
    public static DungeonManager Instance;
    public static int dungeonStage;
    void Awake()
    {
        Instance = this;
        /*
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("���� ���� DungeonManager �ν��Ͻ��� �����մϴ�. �ϳ��� �����ؾ� �մϴ�.");
            Destroy(gameObject); // Ȥ�� �ߺ� ���� ó��
        }
        */
    }

    public RoomManager roomManager;
    public EnemyManager enemyManager;
    public SkillManager skillManager;
    public ProjectileManager projectileManager;

    public Player player;
    public CameraController cameraController;

    public UpgradeSelect UpgradeSelect;

    void Start()
    {
        dungeonStage++;
        Init();   
    }

    public void Init()
    {
        enemyManager.Init();
        roomManager.Init();
        cameraController = Camera.main.GetComponent<CameraController>();
        player.Init(GameManager.Instance.gameData, GameManager.Instance.inventory);
    }
}
