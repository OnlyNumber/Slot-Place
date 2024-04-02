using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlotControl
{
    public BuildingType GetTypeCell(int x, int y);

    public SlotItem GetItem(int x, int y);

}
