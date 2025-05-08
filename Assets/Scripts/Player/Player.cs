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
    //[SerializeField] private WeaponHandler weaponHandler; 

    // ������ �ý��� �߰� ���� ---------------------------------
    // �̱��� �ν��Ͻ�
    public static Player Instance { get; private set; }

    // �κ��丮 ����
    [SerializeField] private Inventory inventory;

    // ������ ������ (Dictionary�� �����Ͽ� �ڵ� ����ȭ)
    private Dictionary<ItemCategory, Item> equippedItems = new Dictionary<ItemCategory, Item>();

    // ������ ���ʽ�
    private float attackBonus = 0f;
    private float defenceBonus = 0f;

    // ������Ƽ
    public Inventory Inventory => inventory;
    public float TotalAttack => stat.AttackDamage + attackBonus;
    public float TotalDefence => stat.Defence + defenceBonus;
    // ������ �ý��� �߰� �� -----------------------------------

    public LayerMask targetMask;

    private void Awake()
    {
        // �̱��� ���� �߰�
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // ��� ��ųʸ� �ʱ�ȭ
        equippedItems[ItemCategory.Weapon] = null;
        equippedItems[ItemCategory.Armor] = null;
    }

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

        // �κ��丮 �ʱ�ȭ
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

            // ���ʽ��� ����� �� ���ݷ� ���
            Debug.Log($"����! ������: {TotalAttack}");
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

    // ������ �ý��� �޼��� �߰� ���� ----------------------------
    // ������ ���� (����ȭ�� ���� - ��� ��� Ÿ�Կ� ��� ����)
    public void EquipItem(Item item)
    {
        ItemCategory category = item.ItemData.ItemCategory;

        // ���� ī�װ��� ���� ��� ������ ����
        if (equippedItems[category] != null)
        {
            UnequipItem(category);
        }

        // �� ������ ����
        equippedItems[category] = item;

        // ������ Ÿ�Կ�
        if (category == ItemCategory.Weapon)
        {
            // ���� ���� - ���ݷ� ���ʽ� ����
            AddAttackBonus(item.ItemData.AttackBonus);
            Debug.Log($"���� ����: {item.ItemData.ItemName}");
        }
        else if (category == ItemCategory.Armor)
        {
            // �� ���� - ���� ���ʽ� ����
            AddDefenseBonus(item.ItemData.DefenseBonus);
            Debug.Log($"�� ����: {item.ItemData.ItemName}");
        }

        // ���� ���� ����
        item.SetEquipped(true);
    }

    // ������ ����
    public void UnequipItem(ItemCategory category)
    {
        Item equippedItem = equippedItems[category];

        if (equippedItem != null)
        {
            if (category == ItemCategory.Weapon)
            {
                // ���� ���� - ���ݷ� ���ʽ� ����
                RemoveAttackBonus(equippedItem.ItemData.AttackBonus);
            }
            else if (category == ItemCategory.Armor)
            {
                // �� ���� - ���� ���ʽ� ����
                RemoveDefenseBonus(equippedItem.ItemData.DefenseBonus);
            }

            // ���� ���� ����
            equippedItem.SetEquipped(false);

            // ��� ��Ͽ��� ����
            equippedItems[category] = null;

            Debug.Log($"{equippedItem.ItemData.ItemName} ��� ����");
        }
    }

    // ���� ���ʽ� ����
    private void AddAttackBonus(float bonus)
    {
        attackBonus += bonus;
        Debug.Log($"���ݷ� ���ʽ� �߰�: +{bonus}, �� ���ݷ�: {TotalAttack}");
    }

    private void RemoveAttackBonus(float bonus)
    {
        attackBonus -= bonus;
        Debug.Log($"���ݷ� ���ʽ� ����: -{bonus}, �� ���ݷ�: {TotalAttack}");
    }

    private void AddDefenseBonus(float bonus)
    {
        defenceBonus += bonus;
        Debug.Log($"���� ���ʽ� �߰�: +{bonus}, �� ����: {TotalDefence}");
    }

    private void RemoveDefenseBonus(float bonus)
    {
        defenceBonus -= bonus;
        Debug.Log($"���� ���ʽ� ����: -{bonus}, �� ����: {TotalDefence}");
    }
    // ������ �ý��� �޼��� �߰� �� ----------------------------
}

public enum PlayerState
{
    Idle,
    Move,
    Attack,
    Jump,
    Death,
}