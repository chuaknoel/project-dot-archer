using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory Inventory => inventory;
    [SerializeField] private Inventory inventory;

    [HideInInspector] public PlayerStat stat;
    
    public PlayerController Controller { get { return controller; } }
    private PlayerController controller;
    private Vector3 inputDir;

    private SpriteRenderer characterImage;
    private Animator playerAnime;

    public SearchTarget SearchTarget { get { return searchTarget; } }
    private SearchTarget searchTarget;

    [SerializeField] private Transform weaponPivot;
    [SerializeField] private WeaponHandler weaponHandler;
    public WeaponHandler WeaponHandler { get { return weaponHandler; } }

    public LayerMask targetMask;

    // Update is called once per frame
    void Update()
    {
        if (stat.IsDeath)
        {
            return;
        }

        GetInputDir();
        LookRotate();
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

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        stat ??= GetComponent<PlayerStat>();
        characterImage ??= GetComponentInChildren<SpriteRenderer>();
        playerAnime ??= GetComponent<Animator>();
        searchTarget ??= GetComponent<SearchTarget>();

        inventory ??= GetComponent<Inventory>();
        
        SetWeapon();
        ControllerRegister();
    }

    private void SetWeapon()
    {
        weaponHandler.Init(inventory.GetCurrentWeapon() , stat, targetMask);
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

    public Vector3 GetInputDir()
    {
        inputDir.x = Input.GetAxisRaw("Horizontal");
        inputDir.y = Input.GetAxisRaw("Vertical");
        return inputDir;
    }

    public void LookRotate()
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = worldPos - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        float rotZ = Mathf.Abs(angle);

        bool isLeft = (rotZ > 90);

        if (isLeft)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        weaponHandler.Rotate(rotZ);
    }

    public float TotalDamage()
    {
        // 인벤토리에서 계산된 총 데미지 사용
        return stat.AttackDamage + inventory.GetTotalAttackBonus();
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