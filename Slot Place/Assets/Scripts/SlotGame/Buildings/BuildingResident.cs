using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingResident : BuildingSlot
{
    private float increaseCoeficient = 1.1f;

    private float upgradeCoeficient = 0.2f;


    public override void BuildEffect()
    {
        
        slotMaster.GetItem(X, Y).CurrentCoeficient *= (increaseCoeficient +(upgradeCoeficient * UpgradeIndex));

        Debug.Log(slotMaster.GetItem(X, Y).CurrentCoeficient + " =" + increaseCoeficient + "+ " + (upgradeCoeficient * UpgradeIndex));
    }

    public override float GetRewardForTime(float time)
    {
        if(UpgradeIndex >= 10)
        {
            CoinsPerSecond = 0.03f + UpgradeIndex * 0.01f;
        }
        else
        {
            CoinsPerSecond = 0;
        }


        return CoinsPerSecond * time;
    }

    public override bool TryPlaceBuilding()
    {
        throw new System.NotImplementedException();
    }
}
