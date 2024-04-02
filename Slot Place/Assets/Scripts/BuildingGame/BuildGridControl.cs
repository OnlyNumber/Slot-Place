using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class BuildGridControl : MonoBehaviour
{
    [SerializeField]
    private BuildButton buttonBuild;

    [SerializeField]
    private List<BuildButton> _buttonsBuild;

    [SerializeField]
    private RectTransform _buttonGrid;

    [SerializeField,FormerlySerializedAs("_buildingsImage")]
    public BuildingsImageFabric BuildingsImage;

    private PlayerData _player;

    [Inject]
    public void Initialize(PlayerData player)
    {
        _player = player;

        for (int i = 0; i < StaticFields.MATRIX_SIZE * StaticFields.MATRIX_SIZE; i++)
        {
            _buttonsBuild.Add(Instantiate(buttonBuild, _buttonGrid));
        }

        while (player.BuildingsInfo.Count < _buttonsBuild.Count)
        {
            player.BuildingsInfo.Add(new BuildInfo());
        }

        while (player.BuildingsInfo.Count > _buttonsBuild.Count)
        {
            player.BuildingsInfo.RemoveAt(player.BuildingsInfo.Count);
        }



        for (int i = 0; i < _buttonsBuild.Count; i++)
        {
            int index = i;

            _buttonsBuild[i].ImageButton.sprite = BuildingsImage.Get(player.BuildingsInfo[i].CurrentBuildingType).Icon;

            if(player.BuildingsInfo[i].CurrentBuildingType >= BuildingType.Symbol0 && player.BuildingsInfo[i].CurrentBuildingType <= BuildingType.Symbol10)
            {
                _buttonsBuild[i].IconSymbol.sprite = BuildingsImage.SlotSymbol[player.BuildingsInfo[i].CurrentBuildingType - BuildingType.Symbol0];
                _buttonsBuild[i].IconSymbol.gameObject.SetActive(true);
            }
            else
            {
                _buttonsBuild[i].IconSymbol.gameObject.SetActive(false);

            }
        }

    }

    

    public BuildButton GetBuildButtonByIndex(int index)
    {
        return _buttonsBuild[index];
    }
    

    public void ChangeType(int index, BuildingType type)
    {
        _buttonsBuild[index].ImageButton.sprite = BuildingsImage.Get(type).Icon;
        _player.BuildingsInfo[index].CurrentBuildingType = type;

        if (_player.BuildingsInfo[index].CurrentBuildingType >= BuildingType.Symbol0 && _player.BuildingsInfo[index].CurrentBuildingType <= BuildingType.Symbol10)
        {
            _buttonsBuild[index].IconSymbol.sprite = BuildingsImage.SlotSymbol[_player.BuildingsInfo[index].CurrentBuildingType - BuildingType.Symbol0];
            _buttonsBuild[index].IconSymbol.gameObject.SetActive(true);
        }
        else
        {
            _buttonsBuild[index].IconSymbol.gameObject.SetActive(false);

        }

    }



}
