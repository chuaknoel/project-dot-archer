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

    // 아이템 시스템 추가 시작 ---------------------------------
    // 싱글톤 인스턴스
    public static Player Instance { get; private set; }

    // 인벤토리 참조
    [SerializeField] private Inventory inventory;

    // 장착된 아이템 (Dictionary로 관리하여 코드 간소화)
    private Dictionary<ItemCategory, Item> equippedItems = new Dictionary<ItemCategory, Item>();

    // 아이템 보너스
    private float attackBonus = 0f;
    private float defenceBonus = 0f;

    // 프로퍼티
    public Inventory Inventory => inventory;
    public float TotalAttack => stat.AttackDamage + attackBonus;
    public float TotalDefence => stat.Defence + defenceBonus;
    // 아이템 시스템 추가 끝 -----------------------------------

    public LayerMask targetMask;

    private void Awake()
    {
        // 싱글톤 설정 추가
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // 장비 딕셔너리 초기화
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

        // 인벤토리 초기화
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

            // 보너스가 적용된 총 공격력 사용
            Debug.Log($"공격! 데미지: {TotalAttack}");
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

    // 아이템 시스템 메서드 추가 시작 ----------------------------
    // 아이템 장착 (간소화된 버전 - 모든 장비 타입에 사용 가능)
    public void EquipItem(Item item)
    {
        ItemCategory category = item.ItemData.ItemCategory;

        // 같은 카테고리의 기존 장비가 있으면 해제
        if (equippedItems[category] != null)
        {
            UnequipItem(category);
        }

        // 새 아이템 장착
        equippedItems[category] = item;

        // 아이템 타입에
        if (category == ItemCategory.Weapon)
        {
            // 무기 장착 - 공격력 보너스 적용
            AddAttackBonus(item.ItemData.AttackBonus);
            Debug.Log($"무기 장착: {item.ItemData.ItemName}");
        }
        else if (category == ItemCategory.Armor)
        {
            // 방어구 장착 - 방어력 보너스 적용
            AddDefenseBonus(item.ItemData.DefenseBonus);
            Debug.Log($"방어구 장착: {item.ItemData.ItemName}");
        }

        // 장착 상태 변경
        item.SetEquipped(true);
    }

    // 아이템 해제
    public void UnequipItem(ItemCategory category)
    {
        Item equippedItem = equippedItems[category];

        if (equippedItem != null)
        {
            if (category == ItemCategory.Weapon)
            {
                // 무기 해제 - 공격력 보너스 제거
                RemoveAttackBonus(equippedItem.ItemData.AttackBonus);
            }
            else if (category == ItemCategory.Armor)
            {
                // 방어구 해제 - 방어력 보너스 제거
                RemoveDefenseBonus(equippedItem.ItemData.DefenseBonus);
            }

            // 장착 상태 변경
            equippedItem.SetEquipped(false);

            // 장비 목록에서 제거
            equippedItems[category] = null;

            Debug.Log($"{equippedItem.ItemData.ItemName} 장비 해제");
        }
    }

    // 스탯 보너스 관리
    private void AddAttackBonus(float bonus)
    {
        attackBonus += bonus;
        Debug.Log($"공격력 보너스 추가: +{bonus}, 총 공격력: {TotalAttack}");
    }

    private void RemoveAttackBonus(float bonus)
    {
        attackBonus -= bonus;
        Debug.Log($"공격력 보너스 제거: -{bonus}, 총 공격력: {TotalAttack}");
    }

    private void AddDefenseBonus(float bonus)
    {
        defenceBonus += bonus;
        Debug.Log($"방어력 보너스 추가: +{bonus}, 총 방어력: {TotalDefence}");
    }

    private void RemoveDefenseBonus(float bonus)
    {
        defenceBonus -= bonus;
        Debug.Log($"방어력 보너스 제거: -{bonus}, 총 방어력: {TotalDefence}");
    }
    // 아이템 시스템 메서드 추가 끝 ----------------------------
}

public enum PlayerState
{
    Idle,
    Move,
    Attack,
    Jump,
    Death,
}