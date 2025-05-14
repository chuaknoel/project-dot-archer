using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Enums;

public class Player : MonoBehaviour
{
    public Inventory Inventory { get { return inventory; } }
    private Inventory inventory;

    public PlayerStat stat;
    
    public PlayerController Controller { get { return controller; } }
    private PlayerController controller;

    public SkillExecutor SkillExecutor { get { return skillExecutor; } }
    private SkillExecutor skillExecutor;
   
    private SpriteRenderer characterImage;
    private Animator playerAnime;

    public SearchTarget SearchTarget { get { return searchTarget; } }
    private SearchTarget searchTarget;

    [SerializeField] private Transform weaponPivot;
    public WeaponHandler WeaponHandler { get { return weaponHandler; } }
    private WeaponHandler weaponHandler;

    public ParticleSystem PlalyerDeathParticle { get { return plalyerDeathParticle; } }
    [SerializeField] private ParticleSystem plalyerDeathParticle;

    public LayerMask targetMask;

    public UpgradeManager UpgradeManager { get { return upgradeManager; } }
    private UpgradeManager upgradeManager;

    // Update is called once per frame
    void Update()
    {
        if (stat.IsDeath)
        {
            return;
        }

        controller?.OnUpdate(Time.deltaTime);
        skillExecutor?.OnUpdate(Time.deltaTime);

        // UI �Ŵ��� ����� �Ű��ּ���!
        // �κ��丮 UI ��� (I Ű)
        if (Input.GetKeyDown(KeyCode.I) && inventory != null)
        {
            inventory.ToggleInventoryUI();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            stat.GetComponent<IDefenceStat>().TakeDamage(123123);
        }
    }

    private void FixedUpdate()
    {
        if (stat.IsDeath)
        {
            return;
        }

        controller?.OnFixedUpdate();
    }

    public void Init(PlayerData playerData, Inventory inventory)
    {
        stat ??= GetComponent<PlayerStat>();
        stat.Init(this, playerData);

        characterImage ??= GetComponentInChildren<SpriteRenderer>();
        playerAnime ??= GetComponent<Animator>();
        searchTarget ??= GetComponent<SearchTarget>();
        this.inventory = inventory;

        upgradeManager = DungeonManager.Instance.upgradeManager;

        SetWeapon();

        ControllerRegister();
        skillExecutor = new SkillExecutor(this, DungeonManager.Instance.skillManager.skillList);
    }

    public void SetWeapon()
    {
        // 1) Inventory���� ���� ������(GameObject) ��������
        GameObject prefab = inventory.GetCurrentWeaponPrefab();
        if (prefab == null)
        {
            Debug.LogWarning("������ ���� Prefab�� �����ϴ�.");
            return;
        }

        // 2) ���� �ν��Ͻ� ���� (������ ������ �ǵ帮�� �ʱ� ���� �ݵ�� Instantiate)
        GameObject instance = Instantiate(prefab, weaponPivot);
        instance.name = prefab.name;  // �̸� ����

        // 3) Item ������Ʈ Ȯ�� (�ʿ��)
        Item itemComp = instance.GetComponent<Item>();
        if (itemComp == null)
        {
            Debug.LogError("���� �ν��Ͻ��� Item ������Ʈ�� �����ϴ�!");
            return;
        }

        // 4) WeaponHandler ������Ʈ �Ҵ� �� �ʱ�ȭ
        weaponHandler = instance.GetComponent<WeaponHandler>();
        if (weaponHandler == null)
        {
            Debug.LogError("���� �ν��Ͻ��� WeaponHandler ������Ʈ�� �����ϴ�!");
            return;
        }
        weaponHandler.Init(itemComp, stat, targetMask, GetComponent<Collider2D>());
    }

    public void ControllerRegister()
    {
        controller = new PlayerController(new PlayerIdleState(), this);
        controller.RegisterState(new PlayerAttackState(), this);
        controller.RegisterState(new PlayerMoveState(), this);
        controller.RegisterState(new PlayerJumpState(), this);
        controller.RegisterState(new PlayerDeathState(), this);
        controller.RegisterState(new PlayerSkillState(), this);
    }

    private void SkillRegister(Skill<Player> skill)
    {
        skillExecutor.RegisterSkill(skill);
    }

    public void ChangeAnime(PlayerState nextAnime)
    {
        if (nextAnime == PlayerState.Death)
        {
            playerAnime.SetTrigger("IsDeath");
        }
        else
        {
            playerAnime.SetInteger("ChangeState", (int)nextAnime);
        }
    }

    public void LookRotate(bool isLeft)
    {
        characterImage.flipX = isLeft;
    }

    public bool IsAttackable(PlayerState curstate)
    {
        switch (curstate)
        {
            case PlayerState.Idle:
            case PlayerState.Move:
                return true;

            default: return false;
        }
    }

    //���׷��̵� �� ������ �޾� ������ ���� ����
    public void Upgrade(InGameUpgradeData gameUpgradeData)
    {
        upgradeManager.MergeUpgrade(gameUpgradeData);
        ApplyUpgrade(gameUpgradeData);
    }

    //���׷��̵� ����
    public void ApplyUpgrade(InGameUpgradeData gameUpgradeData)
    {
        if (gameUpgradeData.attackType == AttackTpye.Range)
        {
            (weaponHandler as RangeWeaponHandler).ApplyUpgrade(upgradeManager.GetRangeUpgrade());
        }
    }
}

