using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager 
{
    public static T Load<T>(string saveData) where T : new()
    {
        if(PlayerPrefs.HasKey(saveData))
        {
           return JsonUtility.FromJson<T>(PlayerPrefs.GetString(saveData));
        }

        return new T();
    }

    public static void Save<T>(string saveData, T data) where T : new()
    {
        PlayerPrefs.SetString(saveData, JsonUtility.ToJson(data));
        return;
    }
}
