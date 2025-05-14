using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("���� Prefab ����Ʈ (Inspector ���)")]
    [Tooltip("��� ���� Prefab�� �巡��&����ϰ�, �� Prefab�� ItemId�� �����ϼ���.")]
    [SerializeField] private List<GameObject> weaponPrefabs;


    [Header("�� Prefab ����Ʈ (Inspector ���)")]
    [Tooltip("��� �� Prefab�� �巡��&����ϰ�, �� Prefab�� ItemId�� �����ϼ���.")]
    [SerializeField] private List<GameObject> armorPrefabs;


    // ID �� Prefab ����
    public Dictionary<string, GameObject> weaponPrefabDict = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> armorPrefabDict = new Dictionary<string, GameObject>();


    [SerializeField] private List<ItemData> itemsForSale = new List<ItemData>();
    [SerializeField] private Transform itemTransform;
    [SerializeField] private GameObject shopItem;

    [SerializeField] private int currentStage; //�����Ŵ�����?
    public int CurrentStage => currentStage;

    public Inventory inventory;

    public ItemManager itemManager;
    private Sprite sprite;

    private void Awake()
    {

        // (B) Inspector�� ��ӵ� Prefab���� ID��Prefab ��ųʸ��� ä���ֱ�
        weaponPrefabDict.Clear();
        foreach (var prefab in weaponPrefabs)
        {
            var it = prefab.GetComponent<Item>();
            if (it != null && !string.IsNullOrEmpty(it.ItemId))
                weaponPrefabDict[it.ItemId] = prefab;
            else
                Debug.LogWarning($"[Inventory] Weapon Prefab ���� �Ǵ� ItemId �̼���: {prefab.name}");
        }

        armorPrefabDict.Clear();
        foreach (var prefab in armorPrefabs)
        {
            var it = prefab.GetComponent<Item>();
            if (it != null && !string.IsNullOrEmpty(it.ItemId))
                armorPrefabDict[it.ItemId] = prefab;
            else
                Debug.LogWarning($"[Inventory] Armor Prefab ���� �Ǵ� ItemId �̼���: {prefab.name}");
        }
    }

    private void Start()
    {
        OpenShop();
    }
    private void AddShopItems(int currentStage)
    {
        itemsForSale.Clear();
        itemsForSale = itemManager.GetItemsByRarity((ItemRarity)(currentStage));
    }

    public void OpenShop()
    {
        AddShopItems(currentStage);
        CreateShopItemUI();
    }

    public void CreateShopItemUI()
    {
        for (int i = 0; i < itemsForSale.Count; i++)
        {
            //if (i == itemsForSale.Count / 2) //�ٹٲ�
            //{
            //    shopItem.transform.localPosition = Vector3.down * 2;
            //}

            if (i < itemsForSale.Count / 2)
            {
                shopItem = Instantiate(weaponPrefabDict[itemsForSale[i].itemId], itemTransform);
                Debug.Log("�������" + i);
                shopItem.transform.localScale = Vector3.one;
            }
            else
            {
                shopItem = Instantiate(armorPrefabDict[itemsForSale[i].itemId], itemTransform);
                Debug.Log("�Ƹӻ���" + i);
                shopItem.transform.localScale = Vector3.one;
            }
            shopItem.transform.localPosition = Vector3.right;

            int index = i;
            shopItem.AddComponent<Button>().onClick.AddListener(() => BuyItem(index));

            //float price = itemsForSale[i].price;
            //string name = itemsForSale[i].itemName;
            //string description = itemsForSale[i].ItemDescription;
        }


    }

    public void BuyItem(int index) //Ŭ���� ������
    {
        if (inventory.SpendGold((int)itemsForSale[index].price))
        {
            inventory.AddOwnedItem(itemsForSale[index].itemId);
            Debug.Log("buy" + index);
        }
        else
        {
            Debug.Log("nomoney" + index);

        }
    }
}
