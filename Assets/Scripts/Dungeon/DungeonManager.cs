using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEditor.EditorTools;
using UnityEngine;
using static UnityEngine.Mesh;

public class DungeonManager : MonoBehaviour //방 이동, 전체 흐름 등 맵 전체 책임자
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
            Debug.LogWarning("여러 개의 DungeonManager 인스턴스가 존재합니다. 하나만 유지해야 합니다.");
            Destroy(gameObject); // 혹은 중복 방지 처리
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
        player.Init(GameManager.Instance.gameData.playerData, GameManager.Instance.inventory); //게임 매니저에서 데이터 로드 후 데이터를 넘겨주자.
    }
}
