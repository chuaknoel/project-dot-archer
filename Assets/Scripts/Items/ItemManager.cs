using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    private static ItemManager instance;
    public static ItemManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ItemManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("ItemManager");
                    instance = obj.AddComponent<ItemManager>();
                }
            }
            return instance;
        }
    }

    // ������ ������
    [SerializeField] private GameObject itemPrefab;

    // ������ ������ ��ųʸ�
    private Dictionary<string, ItemData> itemDataDict = new Dictionary<string, ItemData>();

    // ������ ��������Ʈ (���ҽ� �������� �ε�)
    private Dictionary<string, Sprite> itemSprites = new Dictionary<string, Sprite>();

    private void Awake()
    {
        // �̱��� ����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
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
        Debug.Log("�⺻ ������ ����");

        // ���� ������ ����
        CreateWeaponItems();

        // �� ������ ����
        CreateArmorItems();
    }

    // ���� ������ ����
    private void CreateWeaponItems()
    {
        // �� ������ (4���)
        CreateSwords();

        // Ȱ ������ (4���)
        CreateBows();

        // �� ������ (4���)
        CreateScythes();
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
        // ���� ���� �� �߰�
        sword1.attackRange = 1.5f;
        sword1.attackDelay = 0.2f;
        sword1.attackCooldown = 0.5f;

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
        // ���� ���� �� �߰�
        sword2.attackRange = 1.8f;
        sword2.attackDelay = 0.18f;
        sword2.attackCooldown = 0.45f;

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
        // ���� ���� �� �߰�
        sword3.attackRange = 2.0f;
        sword3.attackDelay = 0.16f;
        sword3.attackCooldown = 0.4f;

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
        // ���� ���� �� �߰�
        sword4.attackRange = 2.5f;
        sword4.attackDelay = 0.15f;
        sword4.attackCooldown = 0.35f;

        if (itemSprites.TryGetValue("sword_epic", out sprite))
        {
            sword4.itemIcon = sprite;
        }

        itemDataDict[sword4.itemId] = sword4;
        itemDataDict[sword4.itemName] = sword4;
    }

    // Ȱ ������ ����
    private void CreateBows()
    {
        // �Ϲ� Ȱ
        ItemData bow1 = new ItemData();
        bow1.itemId = "bow_common";
        bow1.itemName = "�Ϲ�Ȱ";
        bow1.itemDescription = "�⺻���� Ȱ�Դϴ�.";
        bow1.itemCategory = ItemCategory.Weapon;
        bow1.weaponType = WeaponType.Bow;
        bow1.itemRarity = ItemRarity.Common;
        bow1.attackBonus = 4;
        bow1.price = 100;
        // ���� ���� �� �߰�
        bow1.attackRange = 6.0f;
        bow1.attackDelay = 0.5f;
        bow1.attackCooldown = 1.0f;

        if (itemSprites.TryGetValue("bow_common", out Sprite sprite))
        {
            bow1.itemIcon = sprite;
        }

        itemDataDict[bow1.itemId] = bow1;
        itemDataDict[bow1.itemName] = bow1;

        // ���� Ȱ
        ItemData bow2 = new ItemData();
        bow2.itemId = "bow_uncommon";
        bow2.itemName = "����Ȱ";
        bow2.itemDescription = "ǰ���� ���� Ȱ�Դϴ�.";
        bow2.itemCategory = ItemCategory.Weapon;
        bow2.weaponType = WeaponType.Bow;
        bow2.itemRarity = ItemRarity.Uncommon;
        bow2.attackBonus = 8;
        bow2.price = 200;
        // ���� ���� �� �߰�
        bow2.attackRange = 7.0f;
        bow2.attackDelay = 0.45f;
        bow2.attackCooldown = 0.9f;

        if (itemSprites.TryGetValue("bow_uncommon", out sprite))
        {
            bow2.itemIcon = sprite;
        }

        itemDataDict[bow2.itemId] = bow2;
        itemDataDict[bow2.itemName] = bow2;

        // ������ Ȱ
        ItemData bow3 = new ItemData();
        bow3.itemId = "bow_rare";
        bow3.itemName = "������Ȱ";
        bow3.itemDescription = "����� Ȱ�Դϴ�.";
        bow3.itemCategory = ItemCategory.Weapon;
        bow3.weaponType = WeaponType.Bow;
        bow3.itemRarity = ItemRarity.Rare;
        bow3.attackBonus = 12;
        bow3.price = 400;
        // ���� ���� �� �߰�
        bow3.attackRange = 8.0f;
        bow3.attackDelay = 0.4f;
        bow3.attackCooldown = 0.8f;

        if (itemSprites.TryGetValue("bow_rare", out sprite))
        {
            bow3.itemIcon = sprite;
        }

        itemDataDict[bow3.itemId] = bow3;
        itemDataDict[bow3.itemName] = bow3;

        // �ʹ����� Ȱ
        ItemData bow4 = new ItemData();
        bow4.itemId = "bow_epic";
        bow4.itemName = "�ʹ�����Ȱ";
        bow4.itemDescription = "�������� Ȱ�Դϴ�.";
        bow4.itemCategory = ItemCategory.Weapon;
        bow4.weaponType = WeaponType.Bow;
        bow4.itemRarity = ItemRarity.Epic;
        bow4.attackBonus = 20;
        bow4.price = 800;
        // ���� ���� �� �߰�
        bow4.attackRange = 10.0f;
        bow4.attackDelay = 0.35f;
        bow4.attackCooldown = 0.7f;

        if (itemSprites.TryGetValue("bow_epic", out sprite))
        {
            bow4.itemIcon = sprite;
        }

        itemDataDict[bow4.itemId] = bow4;
        itemDataDict[bow4.itemName] = bow4;
    }

    // �� ������ ����
    private void CreateScythes()
    {
        // �Ϲ� ��
        ItemData scythe1 = new ItemData();
        scythe1.itemId = "scythe_common";
        scythe1.itemName = "�Ϲݳ�";
        scythe1.itemDescription = "�⺻���� ���Դϴ�.";
        scythe1.itemCategory = ItemCategory.Weapon;
        scythe1.weaponType = WeaponType.Scythe;
        scythe1.itemRarity = ItemRarity.Common;
        scythe1.attackBonus = 6;
        scythe1.price = 120;
        // ���� ���� �� �߰�
        scythe1.attackRange = 2.0f;
        scythe1.attackDelay = 0.3f;
        scythe1.attackCooldown = 0.7f;

        if (itemSprites.TryGetValue("scythe_common", out Sprite sprite))
        {
            scythe1.itemIcon = sprite;
        }

        itemDataDict[scythe1.itemId] = scythe1;
        itemDataDict[scythe1.itemName] = scythe1;

        // ���� ��
        ItemData scythe2 = new ItemData();
        scythe2.itemId = "scythe_uncommon";
        scythe2.itemName = "������";
        scythe2.itemDescription = "ǰ���� ���� ���Դϴ�.";
        scythe2.itemCategory = ItemCategory.Weapon;
        scythe2.weaponType = WeaponType.Scythe;
        scythe2.itemRarity = ItemRarity.Uncommon;
        scythe2.attackBonus = 12;
        scythe2.price = 240;
        // ���� ���� �� �߰�
        scythe2.attackRange = 2.3f;
        scythe2.attackDelay = 0.28f;
        scythe2.attackCooldown = 0.65f;

        if (itemSprites.TryGetValue("scythe_uncommon", out sprite))
        {
            scythe2.itemIcon = sprite;
        }

        itemDataDict[scythe2.itemId] = scythe2;
        itemDataDict[scythe2.itemName] = scythe2;

        // ������ ��
        ItemData scythe3 = new ItemData();
        scythe3.itemId = "scythe_rare";
        scythe3.itemName = "��������";
        scythe3.itemDescription = "����� ���Դϴ�.";
        scythe3.itemCategory = ItemCategory.Weapon;
        scythe3.weaponType = WeaponType.Scythe;
        scythe3.itemRarity = ItemRarity.Rare;
        scythe3.attackBonus = 18;
        scythe3.price = 480;
        // ���� ���� �� �߰�
        scythe3.attackRange = 2.7f;
        scythe3.attackDelay = 0.25f;
        scythe3.attackCooldown = 0.6f;

        if (itemSprites.TryGetValue("scythe_rare", out sprite))
        {
            scythe3.itemIcon = sprite;
        }

        itemDataDict[scythe3.itemId] = scythe3;
        itemDataDict[scythe3.itemName] = scythe3;

        // �ʹ����� ��
        ItemData scythe4 = new ItemData();
        scythe4.itemId = "scythe_epic";
        scythe4.itemName = "�ʹ�������";
        scythe4.itemDescription = "�������� ���Դϴ�.";
        scythe4.itemCategory = ItemCategory.Weapon;
        scythe4.weaponType = WeaponType.Scythe;
        scythe4.itemRarity = ItemRarity.Epic;
        scythe4.attackBonus = 30;
        scythe4.price = 960;
        // ���� ���� �� �߰�
        scythe4.attackRange = 3.0f;
        scythe4.attackDelay = 0.22f;
        scythe4.attackCooldown = 0.5f;

        if (itemSprites.TryGetValue("scythe_epic", out sprite))
        {
            scythe4.itemIcon = sprite;
        }

        itemDataDict[scythe4.itemId] = scythe4;
        itemDataDict[scythe4.itemName] = scythe4;
    }

    // �� ������ ����
    private void CreateArmorItems()
    {
        // ���� ������ (4���)
        CreateHelmets();

        // ���� ������ (4���)
        CreateArmors();

        // �Ź� ������ (4���)
        CreateBoots();
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

        // ���� ����
        ItemData helmet2 = new ItemData();
        helmet2.itemId = "helmet_uncommon";
        helmet2.itemName = "��������";
        helmet2.itemDescription = "ǰ���� ���� �����Դϴ�.";
        helmet2.itemCategory = ItemCategory.Armor;
        helmet2.armorType = ArmorType.Helmet;
        helmet2.itemRarity = ItemRarity.Uncommon;
        helmet2.defenseBonus = 6;
        helmet2.price = 160;

        if (itemSprites.TryGetValue("helmet_uncommon", out sprite))
        {
            helmet2.itemIcon = sprite;
        }

        itemDataDict[helmet2.itemId] = helmet2;
        itemDataDict[helmet2.itemName] = helmet2;

        // ������ ����
        ItemData helmet3 = new ItemData();
        helmet3.itemId = "helmet_rare";
        helmet3.itemName = "����������";
        helmet3.itemDescription = "����� �����Դϴ�.";
        helmet3.itemCategory = ItemCategory.Armor;
        helmet3.armorType = ArmorType.Helmet;
        helmet3.itemRarity = ItemRarity.Rare;
        helmet3.defenseBonus = 9;
        helmet3.price = 320;

        if (itemSprites.TryGetValue("helmet_rare", out sprite))
        {
            helmet3.itemIcon = sprite;
        }

        itemDataDict[helmet3.itemId] = helmet3;
        itemDataDict[helmet3.itemName] = helmet3;

        // �ʹ����� ����
        ItemData helmet4 = new ItemData();
        helmet4.itemId = "helmet_epic";
        helmet4.itemName = "�ʹ���������";
        helmet4.itemDescription = "�������� �����Դϴ�.";
        helmet4.itemCategory = ItemCategory.Armor;
        helmet4.armorType = ArmorType.Helmet;
        helmet4.itemRarity = ItemRarity.Epic;
        helmet4.defenseBonus = 15;
        helmet4.price = 640;

        if (itemSprites.TryGetValue("helmet_epic", out sprite))
        {
            helmet4.itemIcon = sprite;
        }

        itemDataDict[helmet4.itemId] = helmet4;
        itemDataDict[helmet4.itemName] = helmet4;
    }

    // ���� ������ ����
    private void CreateArmors()
    {
        // �Ϲ� ����
        ItemData armor1 = new ItemData();
        armor1.itemId = "armor_common";
        armor1.itemName = "�Ϲݰ���";
        armor1.itemDescription = "�⺻���� �����Դϴ�.";
        armor1.itemCategory = ItemCategory.Armor;
        armor1.armorType = ArmorType.Armor;
        armor1.itemRarity = ItemRarity.Common;
        armor1.defenseBonus = 5;
        armor1.price = 100;

        if (itemSprites.TryGetValue("armor_common", out Sprite sprite))
        {
            armor1.itemIcon = sprite;
        }

        itemDataDict[armor1.itemId] = armor1;
        itemDataDict[armor1.itemName] = armor1;

        // ���� ����
        ItemData armor2 = new ItemData();
        armor2.itemId = "armor_uncommon";
        armor2.itemName = "��������";
        armor2.itemDescription = "ǰ���� ���� �����Դϴ�.";
        armor2.itemCategory = ItemCategory.Armor;
        armor2.armorType = ArmorType.Armor;
        armor2.itemRarity = ItemRarity.Uncommon;
        armor2.defenseBonus = 10;
        armor2.price = 200;

        if (itemSprites.TryGetValue("armor_uncommon", out sprite))
        {
            armor2.itemIcon = sprite;
        }

        itemDataDict[armor2.itemId] = armor2;
        itemDataDict[armor2.itemName] = armor2;

        // ������ ����
        ItemData armor3 = new ItemData();
        armor3.itemId = "armor_rare";
        armor3.itemName = "����������";
        armor3.itemDescription = "����� �����Դϴ�.";
        armor3.itemCategory = ItemCategory.Armor;
        armor3.armorType = ArmorType.Armor;
        armor3.itemRarity = ItemRarity.Rare;
        armor3.defenseBonus = 15;
        armor3.price = 400;

        if (itemSprites.TryGetValue("armor_rare", out sprite))
        {
            armor3.itemIcon = sprite;
        }

        itemDataDict[armor3.itemId] = armor3;
        itemDataDict[armor3.itemName] = armor3;

        // �ʹ����� ����
        ItemData armor4 = new ItemData();
        armor4.itemId = "armor_epic";
        armor4.itemName = "�ʹ���������";
        armor4.itemDescription = "�������� �����Դϴ�.";
        armor4.itemCategory = ItemCategory.Armor;
        armor4.armorType = ArmorType.Armor;
        armor4.itemRarity = ItemRarity.Epic;
        armor4.defenseBonus = 25;
        armor4.price = 800;

        if (itemSprites.TryGetValue("armor_epic", out sprite))
        {
            armor4.itemIcon = sprite;
        }

        itemDataDict[armor4.itemId] = armor4;
        itemDataDict[armor4.itemName] = armor4;
    }

    // �Ź� ������ ����
    private void CreateBoots()
    {
        // �Ϲ� �Ź�
        ItemData boots1 = new ItemData();
        boots1.itemId = "boots_common";
        boots1.itemName = "�ϹݽŹ�";
        boots1.itemDescription = "�⺻���� �Ź��Դϴ�.";
        boots1.itemCategory = ItemCategory.Armor;
        boots1.armorType = ArmorType.Boots;
        boots1.itemRarity = ItemRarity.Common;
        boots1.defenseBonus = 2;
        boots1.price = 60;

        if (itemSprites.TryGetValue("boots_common", out Sprite sprite))
        {
            boots1.itemIcon = sprite;
        }

        itemDataDict[boots1.itemId] = boots1;
        itemDataDict[boots1.itemName] = boots1;

        // ���� �Ź�
        ItemData boots2 = new ItemData();
        boots2.itemId = "boots_uncommon";
        boots2.itemName = "�����Ź�";
        boots2.itemDescription = "ǰ���� ���� �Ź��Դϴ�.";
        boots2.itemCategory = ItemCategory.Armor;
        boots2.armorType = ArmorType.Boots;
        boots2.itemRarity = ItemRarity.Uncommon;
        boots2.defenseBonus = 4;
        boots2.price = 120;

        if (itemSprites.TryGetValue("boots_uncommon", out sprite))
        {
            boots2.itemIcon = sprite;
        }

        itemDataDict[boots2.itemId] = boots2;
        itemDataDict[boots2.itemName] = boots2;

        // ������ �Ź�
        ItemData boots3 = new ItemData();
        boots3.itemId = "boots_rare";
        boots3.itemName = "�������Ź�";
        boots3.itemDescription = "����� �Ź��Դϴ�.";
        boots3.itemCategory = ItemCategory.Armor;
        boots3.armorType = ArmorType.Boots;
        boots3.itemRarity = ItemRarity.Rare;
        boots3.defenseBonus = 6;
        boots3.price = 240;

        if (itemSprites.TryGetValue("boots_rare", out sprite))
        {
            boots3.itemIcon = sprite;
        }

        itemDataDict[boots3.itemId] = boots3;
        itemDataDict[boots3.itemName] = boots3;

        // �ʹ����� �Ź�
        ItemData boots4 = new ItemData();
        boots4.itemId = "boots_epic";
        boots4.itemName = "�ʹ������Ź�";
        boots4.itemDescription = "�������� �Ź��Դϴ�.";
        boots4.itemCategory = ItemCategory.Armor;
        boots4.armorType = ArmorType.Boots;
        boots4.itemRarity = ItemRarity.Epic;
        boots4.defenseBonus = 10;
        boots4.price = 480;

        if (itemSprites.TryGetValue("boots_epic", out sprite))
        {
            boots4.itemIcon = sprite;
        }

        itemDataDict[boots4.itemId] = boots4;
        itemDataDict[boots4.itemName] = boots4;
    }

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

    // ���� ������ ���� (�� ��� ��� ���)
    public GameObject SpawnRandomItem(Vector3 position, ItemRarity rarity)
    {
        List<ItemData> itemsOfRarity = GetItemsByRarity(rarity);

        if (itemsOfRarity.Count == 0)
        {
            Debug.LogWarning($"�ش� ���({rarity})�� �������� �����ϴ�");
            return null;
        }

        // �������� ������ ����
        int randomIndex = Random.Range(0, itemsOfRarity.Count);
        ItemData randomItem = itemsOfRarity[randomIndex];

        // ������ ����
        return SpawnItem(position, randomItem);
    }

    // ��޺� ������ ��� ��������
    public List<ItemData> GetItemsByRarity(ItemRarity rarity)
    {
        List<ItemData> result = new List<ItemData>();

        foreach (var item in itemDataDict.Values)
        {
            if (item.ItemRarity == rarity)
            {
                if (!result.Contains(item))
                {
                    result.Add(item);
                }
            }
        }

        return result;
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

    // ���� ��޺� ��� ��������
    public List<ItemData> GetWeaponsByRarity(ItemRarity rarity)
    {
        List<ItemData> result = new List<ItemData>();

        foreach (var item in itemDataDict.Values)
        {
            if (item.ItemCategory == ItemCategory.Weapon && item.ItemRarity == rarity)
            {
                if (!result.Contains(item))
                {
                    result.Add(item);
                }
            }
        }

        return result;
    }

    // �� ��޺� ��� ��������
    public List<ItemData> GetArmorsByRarity(ItemRarity rarity)
    {
        List<ItemData> result = new List<ItemData>();

        foreach (var item in itemDataDict.Values)
        {
            if (item.ItemCategory == ItemCategory.Armor && item.ItemRarity == rarity)
            {
                if (!result.Contains(item))
                {
                    result.Add(item);
                }
            }
        }

        return result;
    }

    // ��� ���� ��� ��������
    public List<ItemData> GetAllWeapons()
    {
        List<ItemData> result = new List<ItemData>();

        foreach (var item in itemDataDict.Values)
        {
            if (item.ItemCategory == ItemCategory.Weapon)
            {
                if (!result.Contains(item))
                {
                    result.Add(item);
                }
            }
        }

        return result;
    }

    // ��� �� ��� ��������
    public List<ItemData> GetAllArmors()
    {
        List<ItemData> result = new List<ItemData>();

        foreach (var item in itemDataDict.Values)
        {
            if (item.ItemCategory == ItemCategory.Armor)
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