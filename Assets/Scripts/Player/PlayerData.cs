using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public string playerName;
    public StatData statData;
    public ItemData equippedWeapon;
    public List<ItemData> inventoryItems;

    public PlayerData(PlayerData loadData)
    {
        playerName = loadData.playerName;
        statData = loadData.statData;
        equippedWeapon = loadData.equippedWeapon;
        inventoryItems = new List<ItemData>(loadData.inventoryItems);
    }

    public PlayerData(string playerName, StatData statData)
    {
        this.playerName = playerName;
        this.statData = statData;
    }
}
