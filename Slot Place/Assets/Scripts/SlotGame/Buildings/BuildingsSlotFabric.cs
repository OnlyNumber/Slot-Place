using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BuildingsSlotFabric
{
    
    public static BuildingSlot Get(BuildingType type)
    {

        switch (type)
        {
            case BuildingType.Bank:
                {
                    return new BuildingBank();
                }
            case BuildingType.Storage:
                {
                    return new BuildingStorage();
                }
            case BuildingType.Residential:
                {
                    return new BuildingResident();
                }
            case BuildingType.Empty:
                {
                    return new BuildingEmpty();
                }
            default:
                {
                    return new BuildingSymbol();
                }
        }


        //return null;
    }

}
