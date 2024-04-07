using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingStorage : BuildingSlot
{
    public override void BuildEffect()
    {
    }

    public override float GetRewardForTime(float time)
    {
        int banksCount = 0;

        if(slotMaster.GetTypeCell(X - 1, Y ) == BuildingType.Bank)
        {
            banksCount++;
        }
        if (slotMaster.GetTypeCell(X + 1, Y) == BuildingType.Bank)
        {
            banksCount++;
        }
        if (slotMaster.GetTypeCell(X, Y - 1) == BuildingType.Bank)
        {
            banksCount++;
        }
        if (slotMaster.GetTypeCell(X, Y + 1) == BuildingType.Bank)
        {
            banksCount++;
        }


        CoinsPerSecond = 0.01f + UpgradeIndex * 0.001f + banksCount * 0.001f;

        return time * CoinsPerSecond;
    }

    public override bool TryPlaceBuilding()
    {
        return true;
    }

    
}
