using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemId; // 인스펙터에서 설정 가능한 아이템 ID
    private ItemData itemData;
    private bool isEquipped = false;

    // 프로퍼티
    public ItemData ItemData => itemData;
    public bool IsEquipped => isEquipped;
    public string ItemId => itemId;

    private void Awake()
    {
        // ID가 설정되어 있으면 해당 데이터 로드
        if (!string.IsNullOrEmpty(itemId))
        {
            LoadItemData();
        }
    }

    // 아이템 ID로 데이터 로드
    private void LoadItemData()
    {
        // ItemManager가 존재하는지 확인
        if (ItemManager.Instance != null)
        {
            itemData = ItemManager.Instance.GetItemDataById(itemId);

            if (itemData != null)
            {
                // 아이템 이름 설정
                gameObject.name = $"Item_{itemData.ItemName}";

                // 스프라이트 설정
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                if (spriteRenderer != null && itemData.ItemIcon != null)
                {
                    spriteRenderer.sprite = itemData.ItemIcon;
                }
            }
            else
            {
                Debug.LogError($"아이템 ID '{itemId}'에 해당하는 데이터를 찾을 수 없습니다!");
            }
        }
        else
        {
            Debug.LogError("ItemManager가 존재하지 않습니다!");
        }
    }

    // 직접 데이터로 초기화 (코드에서 호출)
    public void Initialize(ItemData dataToSet)
    {
        itemData = dataToSet.Clone();
        itemId = dataToSet.ItemId;

        // 아이템 이름 설정
        gameObject.name = $"Item_{itemData.ItemName}";

        // SpriteRenderer 설정
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && itemData.ItemIcon != null)
        {
            spriteRenderer.sprite = itemData.ItemIcon;
        }
    }

    // 외부에서 ID 설정
    public void SetItemId(string id)
    {
        itemId = id;
        LoadItemData();
    }

    // 장착 상태 설정
    public void SetEquipped(bool equipped)
    {
        isEquipped = equipped;
    }

    // 아이템 획득 (플레이어 충돌 시)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 인벤토리 획득 로직은 각 개발자가 구현
            Debug.Log($"아이템 '{itemData.ItemName}' 획득!");

            // 아이템 비활성화 (실제 획득 로직은 구현 예정)
            gameObject.SetActive(false);
        }
    }
}
