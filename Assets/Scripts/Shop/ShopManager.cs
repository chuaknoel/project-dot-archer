using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private List<Sprite> itemicons = new List<Sprite>();
    

    [SerializeField] private int currentStage; //던전매니저에?
    public int CurrentStage => currentStage;

    public Inventory inventory;

    public ItemManager itemManager;

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
            GameObject gameObject = Instantiate(shopItem, itemTransform);
            Debug.Log("prefab" + i);
            gameObject.transform.localScale = Vector3.one;
            gameObject.transform.localPosition = Vector3.right;

            if (i < itemsForSale.Count / 2)
            {
                itemicons.Add(Resources.Load<Sprite>(itemsForSale[i].ItemId));
            }
            else
            {
                itemicons.Add (Resources.Load<Sprite>(itemsForSale[i].ItemId));
            }
            if (itemicons[i] == null)
            {
                Debug.LogWarning("itemicon null");
            }
            SpriteRenderer icon = gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>();
            icon.sprite = itemicons[i];
            if (icon == null)
            {
                Debug.LogWarning("Image 컴포넌트 없음!");
            }
            else if (icon.sprite == null)
            {
                Debug.LogWarning("Image는 있지만 sprite가 비어 있음!");
            }
            else
            {
                Debug.Log("정상적으로 sprite 있음: " + icon.name);
            }
            TMP_Text price = gameObject.transform.Find("Price").GetComponent<TMP_Text>();
            price.text = itemsForSale[i].price.ToString();
            Text name = gameObject.transform.Find("Name").GetComponent<Text>();
            name.text = itemsForSale[i].itemName;
            Text description = gameObject.transform.Find("Description").GetComponent<Text>();
            description.text = itemsForSale[i].ItemDescription;



            //gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = itemsForSale[i].ItemIcon;
            //gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = itemsForSale[i].price.ToString();
            //gameObject.transform.GetChild(2).GetComponent<TMP_Text>().text = itemsForSale[i].itemName;
            //gameObject.transform.GetChild(3).GetComponent<TMP_Text>().text = itemsForSale[i].ItemDescription;


            int index = i;
            gameObject.transform.GetComponent<Button>().onClick.AddListener(() => BuyItem(index));

            //sprite = itemsForSale[i].ItemIcon;
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
