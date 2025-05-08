using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    // ��� ����
    private bool isEquipped = false;

    // ������Ƽ
    public ItemData ItemData => itemData;
    public bool IsEquipped => isEquipped;

    // �ʱ�ȭ
    public void Initialize(ItemData dataToSet)
    {
        itemData = dataToSet.Clone();

        // ������ �̸� ����
        gameObject.name = $"Item_{itemData.ItemName}";

        // SpriteRenderer ����
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && itemData.ItemIcon != null)
        {
            spriteRenderer.sprite = itemData.ItemIcon;
        }
    }

    // ������ ȿ�� ���� (��� ����)
    public void ApplyEffect(Player player)
    {
        // ��� �������� ��� ����
        if (itemData.ItemCategory == ItemCategory.Weapon ||
            itemData.ItemCategory == ItemCategory.Armor)
        {
            player.EquipItem(this);
        }
    }

    // ���� ���� ���� (Player���� ȣ��)
    public void SetEquipped(bool equipped)
    {
        isEquipped = equipped;
    }

    // ������ ȹ��
    public void Pickup(Player player)
    {
        if (player.Inventory.AddItem(this))
        {
            gameObject.SetActive(false);
        }
    }
}
