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
        CheckAnotherItems(X, Y,1);

        if(upgradeCoeficient >= NeedUpgradeForImprovement)
        {
            CheckAnotherItems(X + 1, Y, 2);

            CheckAnotherItems(X - 1, Y, 2);

            CheckAnotherItems(X, Y + 1, 2);

            CheckAnotherItems(X, Y - 1, 2);
        }
    }

    public void CheckAnotherItems(int x, int y, int decreaseCoeficient)
    {
        if (slotMaster.GetItem(x, y) != null && SymbolIndex == slotMaster.GetItem(x, y).CurrentIndex)
        {
            slotMaster.GetItem(x, y).CurrentCoeficient *= ((increaseCoeficient + (upgradeCoeficient * UpgradeIndex)/ decreaseCoeficient));
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