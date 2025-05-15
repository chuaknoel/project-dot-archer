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
                Debug.Log("업그레이드가 모두 완료됐습니다.");
                return;
            }
            for (int i = 0; i < abilityLevel.Count; i++)
            {
                if (maxAbility.Contains(i)) //이미 존재하는 maxability
                {
                    Debug.Log("이미 존재하는 max레벨");
                    continue;
                }
                else if (abilityLevel[i] == maxLevel)   //최근에 새로 생긴 maxability
                {
                    maxAbility.Add(i);
                    Debug.Log("이제 만렙이 된 어빌레벨");
                }
            }
            int random;
            do
            {
                random = Random.Range(0, abilityLevel.Count);
                Debug.Log("랜덤 생성" + random);
            }
            while (maxAbility.Contains(random));

            switch (random % abilityLevel.Count)
            {
                case 0:   //0
                    abilityLevel[0]++;
                    GameManager.Instance.upgradeManager.permanentUpgradeData.UpgradeAttackDamage(abilityLevel[(int)ability.attackAbillity] * 5);
                    upgradeLevel++;
                    Debug.Log(abilityLevel[0]);
                    Debug.Log("업글렙" + upgradeLevel);
                    break;
                case (int)ability.defenceAbillity:  //1
                    abilityLevel[(int)ability.defenceAbillity]++;
                    GameManager.Instance.upgradeManager.permanentUpgradeData.UpgradeDefence(abilityLevel[(int)ability.defenceAbillity]);
                    upgradeLevel++;
                    Debug.Log("업글렙" + upgradeLevel);
                    break;

                case (int)ability.healthAbillity:   //2
                    abilityLevel[(int)ability.healthAbillity]++;
                    GameManager.Instance.upgradeManager.permanentUpgradeData.UpgradeHealth(abilityLevel[(int)ability.healthAbillity] * 10);
                    upgradeLevel++;
                    Debug.Log("업글렙" + upgradeLevel);
                    break;
            }
            Debug.Log("atklv" + abilityLevel[0] + "deflv" + abilityLevel[1] + "hplv" + abilityLevel[2]);
            Debug.Log("업글렙"+upgradeLevel);
        }
        else
        {
            Debug.Log("골드가 부족합니다.");
        }
    }
}