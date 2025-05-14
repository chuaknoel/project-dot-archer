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
    private List<int> abilityLevel = Enumerable.Repeat(0, System.Enum.GetValues(typeof(Ability)).Length).ToList();
    private List<int> maxAbility = new List<int>();
    [SerializeField] private int upgradeLevel;
    public int UpgradeLevel => upgradeLevel;

    Player player;
    PlayerData playerData;
    PlayerStat playerstat;
    PermanentUpgradeData permanentUpgradeData;
    Inventory inventory;

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
        if (inventory.SpendGold(UpgradeLevel * 2))
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
                    continue;
                }
                else if (abilityLevel[i] == maxLevel)   //�ֱٿ� ���� ���� maxability
                {
                    maxAbility.Add(i);
                    //abilityLevel.Remove(i);
                }
            }
            int random;
            do
            {
                random = Random.Range(0, abilityLevel.Count);
            }
            while (maxAbility.Contains(random));

            switch (random % abilityLevel.Count)
            {
                case (int)ability.attackAbillity:   //0
                    abilityLevel[(int)ability.attackAbillity]++;
                    permanentUpgradeData.UpgradeAttackDamage(abilityLevel[(int)ability.attackAbillity] * 5);
                    upgradeLevel++;
                    break;
                case (int)ability.defenceAbillity:  //1
                    abilityLevel[(int)ability.defenceAbillity]++;
                    permanentUpgradeData.UpgradeAttackDamage(abilityLevel[(int)ability.defenceAbillity]);
                    upgradeLevel++;
                    break;

                case (int)ability.healthAbillity:   //2
                    abilityLevel[(int)ability.healthAbillity]++;
                    //ü���߰�
                    upgradeLevel++;
                    break;
            }
        }
        else
        {
            Debug.Log("��尡 �����մϴ�.");
        }
    }
}