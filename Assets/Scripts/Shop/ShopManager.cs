using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private List<Item> itemsForSale;

    private Inventory inventory = new Inventory();//임시
    
    private void AddShopItems()
    {
        //스테이지 올라갈수록 레어리티 상승
        //itemsForSale.Add();
    }

    public void OpenShop()
    {


        CreateShopItemUI();
        //버튼이 여러개 공 방 체  원래는 랜덤
        //클릭하면 buyitem
        //아이템(무기), 아이템(갑옷) ... 샵아이템리스트
    }

    private void CreateStatUI()
    {

    }
    private void CreateShopItemUI()
    {
        //아이템 스프라이트 위치 지정, 효과 설명, 
        

    }
    private void BuyStat()
    {
        //스탯리스트 
    }

    private void BuyItem(Item item) //클릭한 아이템
    {
        if (false)//임시, 플레이어 돈 없으면 
        {
            Debug.Log("골드가 부족합니다.");
            return;
        }
        //골드 빠져나가고
        inventory.AddItem(item);


    }
}
