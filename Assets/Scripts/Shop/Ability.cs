using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [SerializeField] private int attackAbilityLevel = 0;
    [SerializeField] private int defenceAbilityLevel = 0;
    [SerializeField] private int healthAbilityLevel = 0;
    private int maxLevel = 9;
    private List<int> attackAbility = new List<int>();
    private List<int> defenceAbility = new List<int>();
    private List<int> healthAbility = new List<int>();
    private int upgradeLevel = 0;

    Player player;
    PlayerData playerData;
    PlayerStat playerstat;
    PermanentUpgradeData permanentUpgradeData;

    private void SetList(List<int> list)
    {
        list.Clear();
        for (int i = 0; i < maxLevel; i++)
        {
            list.Add(i);
        }
    }

    private void Rullet()
    {
        int random = Random.Range(0, 3);
        switch (random % 3)
        {
            case 0:
                if (attackAbilityLevel < maxLevel)
                {
                    attackAbilityLevel++;
                    permanentUpgradeData.UpgradeAttackDamage(attackAbilityLevel * 5);
                    upgradeLevel++;
                }
                break;
            case 1:
                defenceAbilityLevel++;
                permanentUpgradeData.UpgradeAttackDamage(defenceAbilityLevel * 5);
                upgradeLevel++;
                break;

            case 2:
                healthAbilityLevel++;

                upgradeLevel++;
                break;
        }
    }

    public void UpgradeAbility()
    {

    }


}