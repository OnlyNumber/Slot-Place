using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBank : BuildingSlot
{

    public override void BuildEffect()
    {
        if(slotMaster.GetItem(X - 1, Y) == null || slotMaster.GetItem(X + 1, Y) == null)
        {
            return;
        }

        if(slotMaster.GetItem(X -1, Y).CurrentIndex == slotMaster.GetItem(X + 1, Y).CurrentIndex)
        {
            slotMaster.GetItem(X, Y).CurrentIndex = slotMaster.GetItem(X - 1, Y).CurrentIndex;
        }
    }

    public override float GetRewardForTime(float time)
    {
        return 0;
    }

    public override bool TryPlaceBuilding()
    {
        if(CheckBank(X - 1, Y) || CheckBank(X + 1, Y) || CheckBank(X, Y - 1) || CheckBank(X, Y + 1))
        {
            return false;
        }
        return true;
    }

    public bool CheckBank(int x, int y)
    {
        if(x < 0 || x >= StaticFields.MATRIX_SIZE || y < 0 || y >= StaticFields.MATRIX_SIZE || slotMaster.GetTypeCell(x,y) == BuildingType.Bank)
        {
            return false;
        }

        return true;
    }

}
