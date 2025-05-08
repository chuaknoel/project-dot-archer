using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static ItemManager Instance { get; private set; }

    // ������ ������
    [SerializeField] private GameObject itemPrefab;

    // ������ ������ ��ųʸ�
    private Dictionary<string, ItemData> itemDataDict = new Dictionary<string, ItemData>();

    // ������ ��������Ʈ 
    private Dictionary<string, Sprite> itemSprites = new Dictionary<string, Sprite>();

    private void Awake()
    {
        // �̱��� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // ������ ��������Ʈ �ε�
        LoadItemSprites();

        // �⺻ ������ ����
        CreateDefaultItems();
    }

    // ������ ��������Ʈ �ε�
    private void LoadItemSprites()
    {
        // Resources �������� ������ ��������Ʈ �ε�
        Sprite[] sprites = Resources.LoadAll<Sprite>("ItemSprites");
        foreach (Sprite sprite in sprites)
        {
            itemSprites[sprite.name] = sprite;
        }
    }

    // �⺻ ������ ����
    private void CreateDefaultItems()
    {
        // ���� ������ ����
        CreateWeaponItems();

        // �� ������ ����
        CreateArmorItems();
    }

    // ���� ������ ����
    private void CreateWeaponItems()
    {
        CreateSwords();  // �� ������ (4���)
        CreateBows();    // Ȱ ������ (4���)
        CreateScythes(); // �� ������ (4���)
    }

    // �� ������ ����
    private void CreateSwords()
    {
        // �Ϲ� ö��
        ItemData sword1 = new ItemData();
        sword1.itemId = "sword_common";
        sword1.itemName = "�Ϲ�ö��";
        sword1.itemDescription = "�⺻���� ö���Դϴ�.";
        sword1.itemCategory = ItemCategory.Weapon;
        sword1.weaponType = WeaponType.Sword;
        sword1.itemRarity = ItemRarity.Common;
        sword1.attackBonus = 5;
        sword1.price = 100;

        if (itemSprites.TryGetValue("sword_common", out Sprite sprite))
        {
            sword1.itemIcon = sprite;
        }

        itemDataDict[sword1.itemId] = sword1;
        itemDataDict[sword1.itemName] = sword1;

        // ���� ö��
        ItemData sword2 = new ItemData();
        sword2.itemId = "sword_uncommon";
        sword2.itemName = "����ö��";
        sword2.itemDescription = "ǰ���� ���� ö���Դϴ�.";
        sword2.itemCategory = ItemCategory.Weapon;
        sword2.weaponType = WeaponType.Sword;
        sword2.itemRarity = ItemRarity.Uncommon;
        sword2.attackBonus = 10;
        sword2.price = 200;

        if (itemSprites.TryGetValue("sword_uncommon", out sprite))
        {
            sword2.itemIcon = sprite;
        }

        itemDataDict[sword2.itemId] = sword2;
        itemDataDict[sword2.itemName] = sword2;

        // ������ ö��
        ItemData sword3 = new ItemData();
        sword3.itemId = "sword_rare";
        sword3.itemName = "������ö��";
        sword3.itemDescription = "����� ö���Դϴ�.";
        sword3.itemCategory = ItemCategory.Weapon;
        sword3.weaponType = WeaponType.Sword;
        sword3.itemRarity = ItemRarity.Rare;
        sword3.attackBonus = 15;
        sword3.price = 400;

        if (itemSprites.TryGetValue("sword_rare", out sprite))
        {
            sword3.itemIcon = sprite;
        }

        itemDataDict[sword3.itemId] = sword3;
        itemDataDict[sword3.itemName] = sword3;

        // �ʹ����� ö��
        ItemData sword4 = new ItemData();
        sword4.itemId = "sword_epic";
        sword4.itemName = "�ʹ�����ö��";
        sword4.itemDescription = "�������� ö���Դϴ�.";
        sword4.itemCategory = ItemCategory.Weapon;
        sword4.weaponType = WeaponType.Sword;
        sword4.itemRarity = ItemRarity.Epic;
        sword4.attackBonus = 25;
        sword4.price = 800;

        if (itemSprites.TryGetValue("sword_epic", out sprite))
        {
            sword4.itemIcon = sprite;
        }

        itemDataDict[sword4.itemId] = sword4;
        itemDataDict[sword4.itemName] = sword4;
    }

    // Ȱ ������ ������ ������ ������� ����
    private void CreateBows()
    {
        // �Ϲ� Ȱ, ���� Ȱ, ������ Ȱ, �ʹ����� Ȱ ���� (�� ���ϰ� ����)
    }

    // �� ������ ������ ������ ������� ����
    private void CreateScythes()
    {
        // �Ϲ� ��, ���� ��, ������ ��, �ʹ����� �� ���� (�� ���ϰ� ����)
    }

    // �� ������ ����
    private void CreateArmorItems()
    {
        CreateHelmets(); // ���� ������ (4���)
        CreateArmors();  // ���� ������ (4���)
        CreateBoots();   // �Ź� ������ (4���)
    }

    // ���� ������ ����
    private void CreateHelmets()
    {
        // �Ϲ� ����
        ItemData helmet1 = new ItemData();
        helmet1.itemId = "helmet_common";
        helmet1.itemName = "�Ϲ�����";
        helmet1.itemDescription = "�⺻���� �����Դϴ�.";
        helmet1.itemCategory = ItemCategory.Armor;
        helmet1.armorType = ArmorType.Helmet;
        helmet1.itemRarity = ItemRarity.Common;
        helmet1.defenseBonus = 3;
        helmet1.price = 80;

        if (itemSprites.TryGetValue("helmet_common", out Sprite sprite))
        {
            helmet1.itemIcon = sprite;
        }

        itemDataDict[helmet1.itemId] = helmet1;
        itemDataDict[helmet1.itemName] = helmet1;

        // ����, ������, �ʹ����� ������ ������ ������� ����
    }

    // ���ʰ� �Ź� �����۵� ������ ������� ����
    private void CreateArmors() { /* ���� */ }
    private void CreateBoots() { /* ���� */ }

    // �̸����� ������ ������ ��������
    public ItemData GetItemDataByName(string name)
    {
        if (itemDataDict.TryGetValue(name, out ItemData data))
        {
            return data;
        }

        Debug.LogWarning($"�������� ã�� �� ����: {name}");
        return null;
    }

    // ID�� ������ ������ ��������
    public ItemData GetItemDataById(string id)
    {
        if (itemDataDict.TryGetValue(id, out ItemData data))
        {
            return data;
        }

        Debug.LogWarning($"������ ID�� ã�� �� ����: {id}");
        return null;
    }

    // ���� �� ������ �����ϱ�
    public GameObject SpawnItem(Vector3 position, ItemData dataToSpawn)
    {
        if (dataToSpawn == null || itemPrefab == null)
        {
            Debug.LogError("������ �Ǵ� �������� null�Դϴ�");
            return null;
        }

        // ������ �ν��Ͻ� ����
        GameObject itemInstance = Instantiate(itemPrefab, position, Quaternion.identity);

        // ������ ������Ʈ �ʱ�ȭ
        Item item = itemInstance.GetComponent<Item>();
        if (item != null)
        {
            item.Initialize(dataToSpawn);
        }
        else
        {
            Debug.LogError("������ �ν��Ͻ��� Item ������Ʈ�� �����ϴ�");
            Destroy(itemInstance);
            return null;
        }

        return itemInstance;
    }

    // ���� Ÿ�Ժ� ������ ��� ��������
    public List<ItemData> GetWeaponsByType(WeaponType type)
    {
        List<ItemData> result = new List<ItemData>();

        foreach (var item in itemDataDict.Values)
        {
            if (item.ItemCategory == ItemCategory.Weapon && item.WeaponType == type)
            {
                if (!result.Contains(item))
                {
                    result.Add(item);
                }
            }
        }

        return result;
    }

    // �� Ÿ�Ժ� ������ ��� ��������
    public List<ItemData> GetArmorsByType(ArmorType type)
    {
        List<ItemData> result = new List<ItemData>();

        foreach (var item in itemDataDict.Values)
        {
            if (item.ItemCategory == ItemCategory.Armor && item.ArmorType == type)
            {
                if (!result.Contains(item))
                {
                    result.Add(item);
                }
            }
        }

        return result;
    }
}
