using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DailyRewardTODO : MonoBehaviour
{
    public PanelControl _dailyRewardPanel;

    [Zenject.Inject]
    public void Initialize(PlayerData player)
    {
        if(DateTime.Compare(player.GetDate(),DateTime.Today) < 0 || player.IsFirstLog)
        {
            _dailyRewardPanel.SetPanel(true);
            
            player.SetDate(DateTime.Today);

            player.TryChangeCoins(100);

        }
        else
        {
            _dailyRewardPanel.SetPanel(false);

        }


    }
}
