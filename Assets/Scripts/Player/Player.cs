using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory Inventory { get { return inventory; } }
    private Inventory inventory;

    public PlayerStat stat;
    
    public PlayerController Controller { get { return controller; } }
    private PlayerController controller;
   
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

    private InGameUpgradeManager ingameUpgradeManager = new InGameUpgradeManager();

    // Update is called once per frame
    void Update()
    {
        if (stat.IsDeath)
        {
            return;
        }

        controller?.OnUpdate(Time.deltaTime);

        // UI �Ŵ��� ����� �Ű��ּ���!
        // �κ��丮 UI ��� (I Ű)
        if (Input.GetKeyDown(KeyCode.I) && inventory != null)
        {
            inventory.ToggleInventoryUI();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            stat.TakeDamage(123123123123f);
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
        stat ??= new PlayerStat(this, playerData);

        characterImage ??= GetComponentInChildren<SpriteRenderer>();
        playerAnime ??= GetComponent<Animator>();
        searchTarget ??= GetComponent<SearchTarget>();
        this.inventory = inventory;
        
        SetWeapon();
        ControllerRegister();
    }

    private void SetWeapon()
    {
        //�����ڵ�
        inventory.GetCurrentWeapon().transform.SetParent(weaponPivot, false);
        weaponHandler = inventory.GetCurrentWeapon().GetComponent<WeaponHandler>();
        weaponHandler?.Init(inventory.GetCurrentWeapon(), stat, targetMask);
    }

    public void ControllerRegister()
    {
        controller = new PlayerController(new PlayerIdleState(), this);
        controller.RegisterState(new PlayerAttackState(), this);
        controller.RegisterState(new PlayerMoveState(), this);
        controller.RegisterState(new PlayerJumpState(), this);
        controller.RegisterState(new PlayerDeathState(), this);
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
        ingameUpgradeManager.MergeUpgrade(gameUpgradeData);
        ApplyUpgrade(gameUpgradeData);
    }

    //���׷��̵� ����
    public void ApplyUpgrade(InGameUpgradeData gameUpgradeData)
    {
        if (gameUpgradeData.attackType == AttackTpye.Range)
        {
            (weaponHandler as RangeWeaponHandler).ApplyUpgrade(ingameUpgradeManager.GetRangeUpgrade());
        }
    }
}

public enum PlayerState
{
    Idle,
    Move,
    Attack,
    Jump,
    Death,
}