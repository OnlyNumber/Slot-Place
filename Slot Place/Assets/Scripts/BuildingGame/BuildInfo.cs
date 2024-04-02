using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BuildInfo 
{
    public BuildingType CurrentBuildingType = BuildingType.Empty;

    public int CurrentUpdate = 0;

    public string LastTimeDate;

    public BuildInfo()
    {
        SetTime(DateTime.Now);
    }

    public DateTime GetTime()
    {
        return DateTime.Parse(LastTimeDate);
    }

    public void SetTime(DateTime dateTime)
    {
        LastTimeDate = dateTime.ToString();
    }

}
