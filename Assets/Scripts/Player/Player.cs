using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerStat stat;
    public PlayerController controller;
    private SpriteRenderer characterImage;
    private Animator playerAnime;

    private Vector3 inputDir;

    [SerializeField] private Transform weaponPivot;
    //[SerializeField] private WeaponHandler weaponHandler; //추후 추가

    // 인벤토리 참조 추가 (다른 클래스에 스탯 관리 위임)
    [SerializeField] private Inventory inventory;
    public Inventory Inventory => inventory;

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
        controller?.OnUpdate();

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

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
        stat = GetComponent<PlayerStat>();
        characterImage = GetComponentInChildren<SpriteRenderer>();
        playerAnime = GetComponent<Animator>();

        // 인벤토리 참조 확인
        if (inventory == null)
        {
            inventory = GetComponent<Inventory>();
        }

        SetWeapon();
        ControllerRegister();
    }

    private void SetWeapon()
    {
        //weaponHandler.Init(stat, targetMask);
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

        //weaponHandler.Rotate(rotZ);
    }

    public void Attack()
    {
        if (IsAttackable((controller.GetState() as PlayerStates).GetState()))
        {
            //weaponHandler.Attack();

            // 인벤토리에서 계산된 총 데미지 사용
            float totalDamage = stat.AttackDamage + inventory.GetTotalAttackBonus();
            Debug.Log($"공격! 데미지: {totalDamage}");
        }
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