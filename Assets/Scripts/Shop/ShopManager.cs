using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private List<Item> itemsForSale;

    private Inventory inventory = new Inventory();//�ӽ�
    
    private void AddShopItems()
    {
        //�������� �ö󰥼��� ���Ƽ ���
        //itemsForSale.Add();
    }

    public void OpenShop()
    {


        CreateShopItemUI();
        //��ư�� ������ �� �� ü  ������ ����
        //Ŭ���ϸ� buyitem
        //������(����), ������(����) ... �������۸���Ʈ
    }

    private void CreateStatUI()
    {

    }
    private void CreateShopItemUI()
    {
        //������ ��������Ʈ ��ġ ����, ȿ�� ����, 
        

    }
    private void BuyStat()
    {
        //���ȸ���Ʈ 
    }

    private void BuyItem(Item item) //Ŭ���� ������
    {
        if (false)//�ӽ�, �÷��̾� �� ������ 
        {
            Debug.Log("��尡 �����մϴ�.");
            return;
        }
        //��� ����������
        inventory.AddItem(item);


    }
}
