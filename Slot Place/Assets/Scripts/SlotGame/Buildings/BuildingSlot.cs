using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingSlot
{
    protected ISlotControl slotMaster;

    protected int X;
    protected int Y;

    protected int UpgradeIndex;

    protected float CoinsPerSecond =0;

    public virtual void Intialize(int x, int y, ISlotControl slotMachine, int upgradeIndex)
    {
        slotMaster = slotMachine;
        this.UpgradeIndex = upgradeIndex;

        this.X = x;
        this.Y = y;

    }

    public abstract void BuildEffect();

    public abstract bool TryPlaceBuilding();

    public abstract float GetRewardForTime(float time);

}
