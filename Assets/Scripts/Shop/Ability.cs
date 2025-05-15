using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public enum ability
{
    attackAbillity,
    defenceAbillity,
    healthAbillity,
}
public class Ability : MonoBehaviour
{
    private int maxLevel = 9;
    private List<int> abilityLevel = Enumerable.Repeat(0, System.Enum.GetValues(typeof(ability)).Length).ToList();
    private List<int> maxAbility = new List<int>();
    [SerializeField] private int upgradeLevel;
    public int UpgradeLevel => upgradeLevel;


    private void SetList(List<int> list)
    {
        list.Clear();
        for (int i = 0; i < maxLevel; i++)
        {
            list.Add(i);
        }
    }

    public void UpgradeAbility()
    {
        if (GameManager.Instance.inventory.SpendGold(UpgradeLevel * 2))
        {
            if (abilityLevel.Count == 0)   //upgrade max
            {
                Debug.Log("���׷��̵尡 ��� �Ϸ�ƽ��ϴ�.");
                return;
            }
            for (int i = 0; i < abilityLevel.Count; i++)
            {
                if (maxAbility.Contains(i)) //�̹� �����ϴ� maxability
                {
                    Debug.Log("�̹� �����ϴ� max����");
                    continue;
                }
                else if (abilityLevel[i] == maxLevel)   //�ֱٿ� ���� ���� maxability
                {
                    maxAbility.Add(i);
                    Debug.Log("���� ������ �� �������");
                }
            }
            int random;
            do
            {
                random = Random.Range(0, abilityLevel.Count);
                Debug.Log("���� ����" + random);
            }
            while (maxAbility.Contains(random));

            switch (random % abilityLevel.Count)
            {
                case 0:   //0
                    abilityLevel[0]++;
                    GameManager.Instance.upgradeManager.permanentUpgradeData.UpgradeAttackDamage(abilityLevel[(int)ability.attackAbillity] * 5);
                    upgradeLevel++;
                    Debug.Log(abilityLevel[0]);
                    Debug.Log("���۷�" + upgradeLevel);
                    break;
                case (int)ability.defenceAbillity:  //1
                    abilityLevel[(int)ability.defenceAbillity]++;
                    GameManager.Instance.upgradeManager.permanentUpgradeData.UpgradeDefence(abilityLevel[(int)ability.defenceAbillity]);
                    upgradeLevel++;
                    Debug.Log("���۷�" + upgradeLevel);
                    break;

                case (int)ability.healthAbillity:   //2
                    abilityLevel[(int)ability.healthAbillity]++;
                    GameManager.Instance.upgradeManager.permanentUpgradeData.UpgradeHealth(abilityLevel[(int)ability.healthAbillity] * 10);
                    upgradeLevel++;
                    Debug.Log("���۷�" + upgradeLevel);
                    break;
            }
            Debug.Log("atklv" + abilityLevel[0] + "deflv" + abilityLevel[1] + "hplv" + abilityLevel[2]);
            Debug.Log("���۷�"+upgradeLevel);
        }
        else
        {
            Debug.Log("��尡 �����մϴ�.");
        }
    }
}