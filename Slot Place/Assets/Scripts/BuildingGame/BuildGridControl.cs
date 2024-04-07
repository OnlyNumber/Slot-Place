using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class BuildGridControl : MonoBehaviour, ISlotControl
{
    [SerializeField]
    private BuildButton buttonBuild;

    [SerializeField]
    private List<BuildButton> _buttonsBuild;

    [SerializeField]
    private RectTransform _buttonGrid;

    [SerializeField,FormerlySerializedAs("_buildingsImage")]
    public BuildingsInfoFabric BuildingsImage;

    [SerializeField]
    private List<BuildingSlot> _buildingSlots = new List<BuildingSlot>();

    private PlayerData _player;

    private ShopSkinContainer _shopSkinContainer;

    [Inject]
    public void Initialize(PlayerData player, ShopSkinContainer shopSkinContainer)
    {
        _player = player;

        _player.OnSkinChanged += ChangeAllBuildings;

        _shopSkinContainer = shopSkinContainer;

        BuildingsImage = _shopSkinContainer.BuildingsInfoFabrics[_player.CurrentSkins[(int)SkinType.buildings]];

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

            _buildingSlots.Add(BuildingsSlotFabric.Get(player.BuildingsInfo[i].CurrentBuildingType));

            //_buildingSlots[i].Intialize()

            if (player.BuildingsInfo[i].CurrentBuildingType >= BuildingType.Symbol0 && player.BuildingsInfo[i].CurrentBuildingType <= BuildingType.Symbol10)
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

    public void ChangeAllBuildings()
    {
        BuildingsImage = _shopSkinContainer.BuildingsInfoFabrics[_player.CurrentSkins[(int)SkinType.buildings]];


        for (int i = 0; i < _buttonsBuild.Count; i++)
        {
            _buttonsBuild[i].ImageButton.sprite = BuildingsImage.Get(_player.BuildingsInfo[i].CurrentBuildingType).Icon;

        }
        
        
    }

    private void OnDestroy()
    {
        _player.OnSkinChanged -= ChangeAllBuildings;
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

    public BuildingType GetTypeCell(int x, int y)
    {

        if (x < 0 || x > StaticFields.MATRIX_SIZE || y < 0 || y > StaticFields.MATRIX_SIZE)
        {
            return BuildingType.Empty;
        }
        return _player.BuildingsInfo[x + y * StaticFields.MATRIX_SIZE].CurrentBuildingType;

    }

    public SlotItem GetItem(int x, int y)
    {
        return null;

    }
}
