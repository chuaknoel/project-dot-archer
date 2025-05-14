using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        for (int i = 0; i < itemsForSale.Count; i++)
        {

        }

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

            if(i < itemsForSale.Count / 2)
            {
                itemsForSale[i].itemIcon = Resources.Load<Sprite>("Weapons/" + itemsForSale[i].ItemId);
            }
            else
            {
                itemsForSale[i].itemIcon = Resources.Load<Sprite>("Armors/" + itemsForSale[i].ItemId);
            }
            if (itemsForSale[i].itemIcon == null)
            {
                Debug.LogWarning("itemicon null");
            }
            SpriteRenderer icon = gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>();
            icon.sprite = itemsForSale[i].itemIcon;
            if (icon == null)
            {
                Debug.LogWarning("Image ������Ʈ ����!");
            }
            else if (icon.sprite == null)
            {
                Debug.LogWarning("Image�� ������ sprite�� ��� ����!");
            }
            else
            {
                Debug.Log("���������� sprite ����: " + icon.sprite.name);
            }
            TMP_Text price = gameObject.transform.Find("Price").GetComponent<TMP_Text>();
            price.text = itemsForSale[i].price.ToString();
            TMP_Text name = gameObject.transform.Find("Name").GetComponent<TMP_Text>();
            name.text = itemsForSale[i].itemName;
            TMP_Text description = gameObject.transform.Find("Description").GetComponent<TMP_Text>();
            description.text = itemsForSale[i].ItemDescription;



            //gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = itemsForSale[i].ItemIcon;
            //gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = itemsForSale[i].price.ToString();
            //gameObject.transform.GetChild(2).GetComponent<TMP_Text>().text = itemsForSale[i].itemName;
            //gameObject.transform.GetChild(3).GetComponent<TMP_Text>().text = itemsForSale[i].ItemDescription;


            int index = i;
            gameObject.GetComponent<Button>().onClick.AddListener(() => BuyItem(index));

            //sprite = itemsForSale[i].ItemIcon;
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
