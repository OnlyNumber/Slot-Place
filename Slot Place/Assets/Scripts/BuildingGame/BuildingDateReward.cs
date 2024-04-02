using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildingDateReward : MonoBehaviour, ISlotControl
{
    private List<BuildingSlot> _buildingSlots = new List<BuildingSlot>();

    private PlayerData _player;

    private float _rewardLimit = 0;

    [SerializeField]
    private PanelControl _rewardDatePanel;

    [SerializeField]
    private TMPro.TMP_Text _rewardText;


    [Zenject.Inject]
    public void Initialize(PlayerData player)
    {
        _player = player;

        BuildingSlot buildingSlot;


        for (int x = 0; x < StaticFields.MATRIX_SIZE; x++)
        {
            for (int y = 0; y < StaticFields.MATRIX_SIZE; y++)
            {
                buildingSlot = BuildingsSlotFabric.Get(_player.BuildingsInfo[x + y * StaticFields.MATRIX_SIZE].CurrentBuildingType);

                buildingSlot.Intialize(x, y, this, _player.BuildingsInfo[x + y * StaticFields.MATRIX_SIZE].CurrentUpdate);

                _buildingSlots.Add(buildingSlot);

                if(_player.BuildingsInfo[x + y * StaticFields.MATRIX_SIZE].CurrentBuildingType == BuildingType.Storage)
                {
                    _rewardLimit += (150 + _player.BuildingsInfo[x + y * StaticFields.MATRIX_SIZE].CurrentUpdate * 10);
                }

            }
        }


        double time;

        float sum = 0;

        for (int i = 0; i < _buildingSlots.Count; i++)
        {

            time = FromTimeToInt(DateTime.Now) - FromTimeToInt(player.BuildingsInfo[i].GetTime());
            sum += _buildingSlots[i].GetRewardForTime((float)time);
            player.BuildingsInfo[i].SetTime(DateTime.Now);
        }

        if(sum >= _rewardLimit)
        {
            sum = _rewardLimit;
        }

        _rewardDatePanel.SetPanel(true);
        _rewardText.text = sum.ToString();
        player.TryChangeCoins(sum);
    }

    public double FromTimeToInt(DateTime date)
    {
        double year = date.Year;
        double month = date.Month;
        double day = date.Day;
        double hours = date.Hour;
        double minutes = date.Minute;
        double seconds = date.Second;

        double timeToInt = year * 12 * 30 * 24 * 3600 + month * 30 * 24 * 3600 + day * 24 * 3600 + hours * 3600 + minutes * 60 + seconds;

        return timeToInt;

    }

    public BuildingType GetTypeCell(int x, int y)
    {
        return _player.BuildingsInfo[x + y * StaticFields.MATRIX_SIZE].CurrentBuildingType;
    }

    public SlotItem GetItem(int x, int y)
    {
        return null;

    }

}
