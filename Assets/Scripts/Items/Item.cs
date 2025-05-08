using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    // 장비 상태
    private bool isEquipped = false;

    // 프로퍼티
    public ItemData ItemData => itemData;
    public bool IsEquipped => isEquipped;

    // 초기화
    public void Initialize(ItemData dataToSet)
    {
        itemData = dataToSet.Clone();

        // 아이템 이름 설정
        gameObject.name = $"Item_{itemData.ItemName}";

        // SpriteRenderer 설정
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && itemData.ItemIcon != null)
        {
            spriteRenderer.sprite = itemData.ItemIcon;
        }
    }

    // 아이템 효과 적용 (장비 장착)
    public void ApplyEffect(Player player)
    {
        // 장비 아이템일 경우 장착
        if (itemData.ItemCategory == ItemCategory.Weapon ||
            itemData.ItemCategory == ItemCategory.Armor)
        {
            player.EquipItem(this);
        }
    }

    // 장착 상태 변경 (Player에서 호출)
    public void SetEquipped(bool equipped)
    {
        isEquipped = equipped;
    }

    // 아이템 획득
    public void Pickup(Player player)
    {
        if (player.Inventory.AddItem(this))
        {
            gameObject.SetActive(false);
        }
    }
}
