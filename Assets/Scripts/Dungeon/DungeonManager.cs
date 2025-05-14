using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEditor.EditorTools;
using UnityEngine;
using static UnityEngine.Mesh;

public class DungeonManager : MonoBehaviour //�� �̵�, ��ü �帧 �� �� ��ü å����
{
    public static DungeonManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("���� ���� DungeonManager �ν��Ͻ��� �����մϴ�. �ϳ��� �����ؾ� �մϴ�.");
            Destroy(gameObject); // Ȥ�� �ߺ� ���� ó��
        }
    }

    public RoomManager roomManager;
    public EnemyManager enemyManager;
    public SkillManager skillManager;
    public ProjectileManager projectileManager;

    public Player player;
    public CameraController cameraController;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        roomManager.Init();
        cameraController = Camera.main.GetComponent<CameraController>();
        player.Init(GameManager.Instance.gameData.playerData, GameManager.Instance.inventory); //���� �Ŵ������� ������ �ε� �� �����͸� �Ѱ�����.
    }
}
