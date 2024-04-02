using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingStorage : BuildingSlot
{
    public override void BuildEffect()
    {
        throw new System.NotImplementedException();
    }

    public override float GetRewardForTime(float time)
    {
        CoinsPerSecond = 0.01f + UpgradeIndex * 0.001f;

        return time * CoinsPerSecond;
    }

    public override bool TryPlaceBuilding()
    {
        return true;
    }

    
}
