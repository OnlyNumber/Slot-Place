using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingResident : BuildingSlot
{
    private float increaseCoeficient = 1.1f;

    private float upgradeCoeficient = 0.2f;
    
    public override void BuildEffect()
    {
        bool IsStorageClose = false; ;

        if (slotMaster.GetTypeCell(X - 1, Y) == BuildingType.Storage || slotMaster.GetTypeCell(X + 1, Y) == BuildingType.Storage || slotMaster.GetTypeCell(X, Y - 1) == BuildingType.Storage || slotMaster.GetTypeCell(X, Y + 1) == BuildingType.Storage)
        {
            IsStorageClose = true;
        }

        float coeficientDecrease = 1;

        if(IsStorageClose)
        {
            coeficientDecrease = 1.5f;
        }



        slotMaster.GetItem(X, Y).CurrentCoeficient *= (increaseCoeficient +(upgradeCoeficient * UpgradeIndex)/ coeficientDecrease);

        CheckAnotherItems(X - 1, Y, coeficientDecrease);
        CheckAnotherItems(X + 1, Y, coeficientDecrease);
        CheckAnotherItems(X, Y - 1, coeficientDecrease);
        CheckAnotherItems(X, Y + 1, coeficientDecrease);


    }

    public void CheckAnotherItems(int x, int y, float decreaseCoeficient)
    {
        if (slotMaster.GetItem(x, y) != null)
        {
            slotMaster.GetItem(x, y).CurrentCoeficient *= ((increaseCoeficient + (upgradeCoeficient * UpgradeIndex) / decreaseCoeficient));
        }
    }

    public override float GetRewardForTime(float time)
    {
        if(UpgradeIndex >= NeedUpgradeForImprovement)
        {
            CoinsPerSecond = 0.005f + (UpgradeIndex + 1 - NeedUpgradeForImprovement) * 0.0005f;
        }
        else
        {
            CoinsPerSecond = 0;
        }


        return CoinsPerSecond * time;
    }

    public override bool TryPlaceBuilding()
    {
        return true;
        //throw new System.NotImplementedException();
    }
}
