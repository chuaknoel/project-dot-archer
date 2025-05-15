using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSelect : MonoBehaviour
{
    public List<InGameUpgradeData> upgradeDatas;

    public int viewCardCount = 3;
    private List<int> selecedCardIndex = new List<int>();
    
    public void SetCard()
    {
        selecedCardIndex.Clear();
        int count = 0;
        while(count < viewCardCount)
        {
            int cardIndex = Random.Range(0, upgradeDatas.Count);
            if (!selecedCardIndex.Contains(cardIndex))
            {
                upgradeDatas[cardIndex].gameObject.SetActive(true);
                selecedCardIndex.Add(cardIndex);
                count++;
            }
        }
    }

    public void PickCard()
    {
        for (int i = 0; i < viewCardCount; i++)
        {
            upgradeDatas[selecedCardIndex[i]].gameObject.SetActive(false);
        }
    }
}
