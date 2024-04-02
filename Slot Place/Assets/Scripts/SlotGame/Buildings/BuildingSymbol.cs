using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSymbol : BuildingSlot
{
    private float increaseCoeficient = 1.15f;

    private float upgradeCoeficient = 0.25f;

    private int SymbolIndex;

    public override void Intialize(int x, int y, ISlotControl slotMachine, int upgradeIndex)
    {
        base.Intialize(x, y, slotMachine, upgradeIndex);

        SymbolIndex = (int)slotMaster.GetTypeCell(x, y) - (int)BuildingType.Symbol0;

    }

    public override void BuildEffect()
    {
        if (SymbolIndex == slotMaster.GetItem(X, Y).CurrentIndex)
        {
            slotMaster.GetItem(X, Y).CurrentCoeficient *= (increaseCoeficient + (upgradeCoeficient * UpgradeIndex));

        }



    }

    public override bool TryPlaceBuilding()
    {
        return true;
    }

    public override float GetRewardForTime(float time)
    {
        return 0;
    }
}