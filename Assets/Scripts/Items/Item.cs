using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
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

    // 장착 상태 설정
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
