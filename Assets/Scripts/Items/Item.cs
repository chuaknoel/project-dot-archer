using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemId; // �ν����Ϳ��� ���� ������ ������ ID
    private ItemData itemData;
    private bool isEquipped = false;

    // ������Ƽ
    public ItemData ItemData => itemData;
    public bool IsEquipped => isEquipped;
    public string ItemId => itemId;

    private void Awake()
    {
        // ID�� �����Ǿ� ������ �ش� ������ �ε�
        if (!string.IsNullOrEmpty(itemId))
        {
            LoadItemData();
        }
    }

    // ������ ID�� ������ �ε�
    private void LoadItemData()
    {
        // ItemManager�� �����ϴ��� Ȯ��
        if (ItemManager.Instance != null)
        {
            itemData = ItemManager.Instance.GetItemDataById(itemId);

            if (itemData != null)
            {
                // ������ �̸� ����
                gameObject.name = $"Item_{itemData.ItemName}";

                // ��������Ʈ ����
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                if (spriteRenderer != null && itemData.ItemIcon != null)
                {
                    spriteRenderer.sprite = itemData.ItemIcon;
                }
            }
            else
            {
                Debug.LogError($"������ ID '{itemId}'�� �ش��ϴ� �����͸� ã�� �� �����ϴ�!");
            }
        }
        else
        {
            Debug.LogError("ItemManager�� �������� �ʽ��ϴ�!");
        }
    }

    // ���� �����ͷ� �ʱ�ȭ (�ڵ忡�� ȣ��)
    public void Initialize(ItemData dataToSet)
    {
        itemData = dataToSet.Clone();
        itemId = dataToSet.ItemId;

        // ������ �̸� ����
        gameObject.name = $"Item_{itemData.ItemName}";

        // SpriteRenderer ����
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && itemData.ItemIcon != null)
        {
            spriteRenderer.sprite = itemData.ItemIcon;
        }
    }

    // �ܺο��� ID ����
    public void SetItemId(string id)
    {
        itemId = id;
        LoadItemData();
    }

    // ���� ���� ����
    public void SetEquipped(bool equipped)
    {
        isEquipped = equipped;
    }

    // ������ ȹ�� (�÷��̾� �浹 ��)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �κ��丮 ȹ�� ������ �� �����ڰ� ����
            Debug.Log($"������ '{itemData.ItemName}' ȹ��!");

            // ������ ��Ȱ��ȭ (���� ȹ�� ������ ���� ����)
            gameObject.SetActive(false);
        }
    }
}
