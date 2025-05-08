using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static ItemManager Instance { get; private set; }

    // 아이템 프리팹
    [SerializeField] private GameObject itemPrefab;

    // 아이템 데이터 딕셔너리
    private Dictionary<string, ItemData> itemDataDict = new Dictionary<string, ItemData>();

    // 아이템 스프라이트 
    private Dictionary<string, Sprite> itemSprites = new Dictionary<string, Sprite>();

    private void Awake()
    {
        // 싱글톤 설정
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

        // 아이템 스프라이트 로드
        LoadItemSprites();

        // 기본 아이템 생성
        CreateDefaultItems();
    }

    // 아이템 스프라이트 로드
    private void LoadItemSprites()
    {
        // Resources 폴더에서 아이템 스프라이트 로드
        Sprite[] sprites = Resources.LoadAll<Sprite>("ItemSprites");
        foreach (Sprite sprite in sprites)
        {
            itemSprites[sprite.name] = sprite;
        }
    }

    // 기본 아이템 생성
    private void CreateDefaultItems()
    {
        // 무기 아이템 생성
        CreateWeaponItems();

        // 방어구 아이템 생성
        CreateArmorItems();
    }

    // 무기 아이템 생성
    private void CreateWeaponItems()
    {
        CreateSwords();  // 검 아이템 (4등급)
        CreateBows();    // 활 아이템 (4등급)
        CreateScythes(); // 낫 아이템 (4등급)
    }

    // 검 아이템 생성
    private void CreateSwords()
    {
        // 일반 철검
        ItemData sword1 = new ItemData();
        sword1.itemId = "sword_common";
        sword1.itemName = "일반철검";
        sword1.itemDescription = "기본적인 철검입니다.";
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

        // 좋은 철검
        ItemData sword2 = new ItemData();
        sword2.itemId = "sword_uncommon";
        sword2.itemName = "좋은철검";
        sword2.itemDescription = "품질이 좋은 철검입니다.";
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

        // 더좋은 철검
        ItemData sword3 = new ItemData();
        sword3.itemId = "sword_rare";
        sword3.itemName = "더좋은철검";
        sword3.itemDescription = "희귀한 철검입니다.";
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

        // 너무좋은 철검
        ItemData sword4 = new ItemData();
        sword4.itemId = "sword_epic";
        sword4.itemName = "너무좋은철검";
        sword4.itemDescription = "전설적인 철검입니다.";
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

    // 활 아이템 생성도 유사한 방식으로 구현
    private void CreateBows()
    {
        // 일반 활, 좋은 활, 더좋은 활, 너무좋은 활 생성 (위 패턴과 동일)
    }

    // 낫 아이템 생성도 유사한 방식으로 구현
    private void CreateScythes()
    {
        // 일반 낫, 좋은 낫, 더좋은 낫, 너무좋은 낫 생성 (위 패턴과 동일)
    }

    // 방어구 아이템 생성
    private void CreateArmorItems()
    {
        CreateHelmets(); // 투구 아이템 (4등급)
        CreateArmors();  // 갑옷 아이템 (4등급)
        CreateBoots();   // 신발 아이템 (4등급)
    }

    // 투구 아이템 생성
    private void CreateHelmets()
    {
        // 일반 투구
        ItemData helmet1 = new ItemData();
        helmet1.itemId = "helmet_common";
        helmet1.itemName = "일반투구";
        helmet1.itemDescription = "기본적인 투구입니다.";
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

        // 좋은, 더좋은, 너무좋은 투구도 유사한 방식으로 구현
    }

    // 갑옷과 신발 아이템도 유사한 방식으로 구현
    private void CreateArmors() { /* 생략 */ }
    private void CreateBoots() { /* 생략 */ }

    // 이름으로 아이템 데이터 가져오기
    public ItemData GetItemDataByName(string name)
    {
        if (itemDataDict.TryGetValue(name, out ItemData data))
        {
            return data;
        }

        Debug.LogWarning($"아이템을 찾을 수 없음: {name}");
        return null;
    }

    // ID로 아이템 데이터 가져오기
    public ItemData GetItemDataById(string id)
    {
        if (itemDataDict.TryGetValue(id, out ItemData data))
        {
            return data;
        }

        Debug.LogWarning($"아이템 ID를 찾을 수 없음: {id}");
        return null;
    }

    // 게임 내 아이템 생성하기
    public GameObject SpawnItem(Vector3 position, ItemData dataToSpawn)
    {
        if (dataToSpawn == null || itemPrefab == null)
        {
            Debug.LogError("아이템 또는 프리팹이 null입니다");
            return null;
        }

        // 아이템 인스턴스 생성
        GameObject itemInstance = Instantiate(itemPrefab, position, Quaternion.identity);

        // 아이템 컴포넌트 초기화
        Item item = itemInstance.GetComponent<Item>();
        if (item != null)
        {
            item.Initialize(dataToSpawn);
        }
        else
        {
            Debug.LogError("생성된 인스턴스에 Item 컴포넌트가 없습니다");
            Destroy(itemInstance);
            return null;
        }

        return itemInstance;
    }

    // 무기 타입별 아이템 목록 가져오기
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

    // 방어구 타입별 아이템 목록 가져오기
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
