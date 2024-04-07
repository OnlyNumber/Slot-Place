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

    public bool IsFirstLog = true;

    public List<WrapListClass> OpenedSkins = new List<WrapListClass>();

    public List<int> CurrentSkins = new List<int>();

    public Action OnCoinsChanged;

    public Action OnSkinChanged;

    public string LastDate;

    public PlayerData()
    {
        Coins = 200;

        for (int i = 0; i < (int)SkinType.count; i++)
        {
            OpenedSkins.Add(new WrapListClass());
            CurrentSkins.Add(0);
        }
    }

    public void ChangeSkin(SkinType type, int index)
    {
        OpenedSkins[(int)type].Skins[index] = true;

        CurrentSkins[(int)type] = index;

        Debug.Log("Change skin");

        OnSkinChanged?.Invoke();
    }

    public void SetDate(DateTime updateDate)
    {
        LastDate = updateDate.ToString();
    }

    public DateTime GetDate()
    {
        DateTime transfer;
        try
        {
            transfer = DateTime.Parse(LastDate);
        }
        catch
        {
            SetDate(DateTime.MinValue);
            transfer = DateTime.Parse(LastDate);
        }

        return transfer;
    }


    public bool TryChangeCoins(float coins)
    {
        if(Coins + coins < 0)
        {
            return false;
        }
        else
        {
            Coins += coins;

            OnCoinsChanged?.Invoke();
            return true;
        }
    }

}

[System.Serializable]
public class WrapListClass
{
    public List<bool> Skins = new List<bool>();
}

public enum SkinType
{
    background,
    music,
    buildings,
    line,
    count
}