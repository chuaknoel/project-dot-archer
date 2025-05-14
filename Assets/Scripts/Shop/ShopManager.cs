using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("무기 Prefab 리스트 (Inspector 등록)")]
    [Tooltip("모든 무기 Prefab을 드래그&드롭하고, 각 Prefab의 ItemId를 설정하세요.")]
    [SerializeField] private List<GameObject> weaponPrefabs;


    [Header("방어구 Prefab 리스트 (Inspector 등록)")]
    [Tooltip("모든 방어구 Prefab을 드래그&드롭하고, 각 Prefab의 ItemId를 설정하세요.")]
    [SerializeField] private List<GameObject> armorPrefabs;


    // ID → Prefab 매핑
    public Dictionary<string, GameObject> weaponPrefabDict = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> armorPrefabDict = new Dictionary<string, GameObject>();


    [SerializeField] private List<ItemData> itemsForSale = new List<ItemData>();
    [SerializeField] private Transform itemTransform;
    [SerializeField] private GameObject shopItem;

    [SerializeField] private int currentStage; //던전매니저에?
    public int CurrentStage => currentStage;

    public Inventory inventory;

    public ItemManager itemManager;
    private Sprite sprite;

    private void Awake()
    {

        // (B) Inspector에 드롭된 Prefab들을 ID→Prefab 딕셔너리에 채워넣기
        weaponPrefabDict.Clear();
        foreach (var prefab in weaponPrefabs)
        {
            var it = prefab.GetComponent<Item>();
            if (it != null && !string.IsNullOrEmpty(it.ItemId))
                weaponPrefabDict[it.ItemId] = prefab;
            else
                Debug.LogWarning($"[Inventory] Weapon Prefab 누락 또는 ItemId 미설정: {prefab.name}");
        }

        armorPrefabDict.Clear();
        foreach (var prefab in armorPrefabs)
        {
            var it = prefab.GetComponent<Item>();
            if (it != null && !string.IsNullOrEmpty(it.ItemId))
                armorPrefabDict[it.ItemId] = prefab;
            else
                Debug.LogWarning($"[Inventory] Armor Prefab 누락 또는 ItemId 미설정: {prefab.name}");
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
            //if (i == itemsForSale.Count / 2) //줄바꿈
            //{
            //    shopItem.transform.localPosition = Vector3.down * 2;
            //}

            if (i < itemsForSale.Count / 2)
            {
                shopItem = Instantiate(weaponPrefabDict[itemsForSale[i].itemId], itemTransform);
                Debug.Log("무기생성" + i);
                shopItem.transform.localScale = Vector3.one;
            }
            else
            {
                shopItem = Instantiate(armorPrefabDict[itemsForSale[i].itemId], itemTransform);
                Debug.Log("아머생성" + i);
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

    public void BuyItem(int index) //클릭한 아이템
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
