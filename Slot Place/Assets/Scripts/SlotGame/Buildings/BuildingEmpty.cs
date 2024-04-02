using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingEmpty : BuildingSlot
{

    public override void BuildEffect()
    {
        
    }

    public override float GetRewardForTime(float time)
    {
        return 0;
    }

    public override bool TryPlaceBuilding()
    {
        return true;
    }
}
