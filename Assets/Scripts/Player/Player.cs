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
    //[SerializeField] private WeaponHandler weaponHandler; //���� �߰�

    // �κ��丮 ���� �߰� (�ٸ� Ŭ������ ���� ���� ����)
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

        // �κ��丮 UI ��� (I Ű)
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

        // �κ��丮 ���� Ȯ��
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

            // �κ��丮���� ���� �� ������ ���
            float totalDamage = stat.AttackDamage + inventory.GetTotalAttackBonus();
            Debug.Log($"����! ������: {totalDamage}");
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