using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerData
{
    public List<BuildInfo> BuildingsInfo = new List<BuildInfo>();

    public float Coins;

    public int VolumeClip;

    public int VolumeMusic;

    public bool TryChangeCoins(float coins)
    {
        if(Coins + coins < 0)
        {
            return false;
        }
        else
        {
            Coins += coins;
            return true;
        }
    }

}
