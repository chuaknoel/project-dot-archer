using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory Inventory => inventory;
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

    public LayerMask targetMask;

    // Update is called once per frame
    void Update()
    {
        if (stat.IsDeath)
        {
            return;
        }

        controller?.OnUpdate(Time.deltaTime);

        // UI 매니저 생기면 옮겨주세요!
        // 인벤토리 UI 토글 (I 키)
        if (Input.GetKeyDown(KeyCode.I) && inventory != null)
        {
            inventory.ToggleInventoryUI();
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

    public void Init(PlayerData playerData)
    {
        stat ??= new PlayerStat(this, playerData);

        Debug.Log(stat.IsDeath);

        characterImage ??= GetComponentInChildren<SpriteRenderer>();
        playerAnime ??= GetComponent<Animator>();
        searchTarget ??= GetComponent<SearchTarget>();

        inventory ??= GetComponent<Inventory>();

        SetWeapon();
        ControllerRegister();
    }

    private void SetWeapon()
    {
        //임시코드
        Item item = ItemManager.Instance.itemPrefab.GetComponent<Item>();
        GameObject go = ItemManager.Instance.SpawnItem(item.transform.position, item.ItemData);

        go.transform.SetParent(weaponPivot);
        weaponHandler = go.GetComponent<WeaponHandler>();
        weaponHandler?.Init(item, stat, targetMask);

        //구현코드
        //weaponHandler?.Init(inventory.GetCurrentWeapon() , stat, targetMask);
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
        playerAnime.SetInteger("ChangeState", (int)nextAnime);
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
}

public enum PlayerState
{
    Idle,
    Move,
    Attack,
    Jump,
    Death,
}