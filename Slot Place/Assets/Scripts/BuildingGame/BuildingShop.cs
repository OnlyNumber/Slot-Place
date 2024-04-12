using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingShop : MonoBehaviour
{
    //[SerializeField]
    //private BuildingsInfoFabric _shopSkinContainer.BuildingsInfoFabrics[_player.CurrentSkins[(int)SkinType.buildings]];

    [SerializeField]
    private PanelControl _shopPanelControl;

    [SerializeField]
    private BuildingUIItemShop _buildUIItemPrefab;

    [SerializeField]
    private List<BuildingUIItemShop> buildUIItems = new List<BuildingUIItemShop>();

    [SerializeField]
    private PanelControl _shopPanel;

    [SerializeField]
    private RectTransform shopContent;

    [SerializeField]
    private BuildingUpgradePanel upgradePanel;

    [SerializeField]
    private PanelControl _buyPanel;

    [SerializeField]
    private BuildGridControl gridControl;

    [SerializeField]
    private VerticalLayoutGroup _verticalLayout;

    [SerializeField]
    private AudioClip _buildSound;

    [SerializeField]
    private AudioClip _upgradeSound;

    [SerializeField]
    private AudioClip _demolishSound;

    [SerializeField]
    private AudioClip _notEnoughMoney;


    int CurrentBuildingIndex;

    private PlayerData _player;

    private ShopSkinContainer _shopSkinContainer;

    [Zenject.Inject]
    public void Initialize(PlayerData player, ShopSkinContainer shopSkinContainer)
    {
        _player = player;
        _shopSkinContainer = shopSkinContainer;


        shopContent.sizeDelta = new Vector2(0, (int)BuildingType.Count * (_buildUIItemPrefab.GetComponent<RectTransform>().rect.size.y + _verticalLayout.spacing));

        foreach (BuildingType item in System.Enum.GetValues(typeof(BuildingType)))
        {
            if (item >= BuildingType.Count || item == BuildingType.Empty)
            {
                break;
            }

            BuildingType index = item;

            buildUIItems.Add(Instantiate(_buildUIItemPrefab, shopContent));

            buildUIItems[(int)item].IconImage.sprite = _shopSkinContainer.BuildingsInfoFabrics[_player.CurrentSkins[(int)SkinType.buildings]].Get(item).Icon;

            buildUIItems[(int)item].CostText.text = _shopSkinContainer.BuildingsInfoFabrics[_player.CurrentSkins[(int)SkinType.buildings]].Get(item).Cost.ToString();

            buildUIItems[(int)item].NameText.text = _shopSkinContainer.BuildingsInfoFabrics[_player.CurrentSkins[(int)SkinType.buildings]].Get(item).Name;

            buildUIItems[(int)item].BuyButton.onClick.AddListener(() => ChangeBuilding(index));
            buildUIItems[(int)item].BuyButton.onClick.AddListener(() => _shopPanel.SetPanel(false));

            buildUIItems[(int)item].DescriptionButton.onClick.AddListener(() => SetDiscriptionPanel(index));

            if (item >= BuildingType.Symbol0 && item <= BuildingType.Symbol10)
            {
                buildUIItems[(int)item].SymbolImage.sprite = gridControl.BuildingsImage.SlotSymbol[item - BuildingType.Symbol0];
                buildUIItems[(int)item].SymbolImage.gameObject.SetActive(true);
            }
            else
            {
                buildUIItems[(int)item].SymbolImage.gameObject.SetActive(false);
            }

        }



        for (int i = 0; i < Mathf.Pow(StaticFields.MATRIX_SIZE, 2); i++)
        {
            int index = i;
            gridControl.GetBuildButtonByIndex(i).ButtonBuilding.onClick.AddListener(() => ActivateShop(gridControl.GetBuildButtonByIndex(index), index));
        }

        _player.OnSkinChanged += ChangeAllIcons;
    }

    private void OnDestroy()
    {
        _player.OnSkinChanged -= ChangeAllIcons;
    }

    public void ChangeAllIcons()
    {
        Debug.Log("ChangeAllIcons  " + buildUIItems.Count);


        foreach (BuildingType item in System.Enum.GetValues(typeof(BuildingType)))
        {
            if (item >= BuildingType.Count)
            {
                break;
            }

//            Debug.Log(_shopSkinContainer.BuildingsInfoFabrics[_player.CurrentSkins[(int)SkinType.buildings]].Get(item));

            Debug.Log(item);

            buildUIItems[(int)item].IconImage.sprite = _shopSkinContainer.BuildingsInfoFabrics[_player.CurrentSkins[(int)SkinType.buildings]].Get(item).Icon;

        }
    }

    public void ActivateShop(BuildButton button, int index)
    {
        _shopPanel.SetPanel(true);

        _buyPanel.SetPanel(false);

        upgradePanel.UpgradePanel.SetPanel(false);

        CurrentBuildingIndex = index;

        if (_player.BuildingsInfo[CurrentBuildingIndex].CurrentUpdate <= 0)
        {
            _buyPanel.SetPanel(true);
        }
        else
        {
            SetUpgradePanel(_player.BuildingsInfo[CurrentBuildingIndex].CurrentBuildingType);
        }
    }

    public void SetUpgradePanel(BuildingType type)
    {

        upgradePanel.UpgradePanel.SetPanel(true);

        upgradePanel.UpgradeButton.gameObject.SetActive(true);
        upgradePanel.DestroyButton.gameObject.SetActive(true);
        upgradePanel.CostText.gameObject.SetActive(true);
        upgradePanel.CostText.text = (_shopSkinContainer.BuildingsInfoFabrics[_player.CurrentSkins[(int)SkinType.buildings]].Get(type).UpgradeCost * _player.BuildingsInfo[CurrentBuildingIndex].CurrentUpdate).ToString();

        upgradePanel.MoveStashButton.gameObject.SetActive(false);

        upgradePanel.NameText.text = _shopSkinContainer.BuildingsInfoFabrics[_player.CurrentSkins[(int)SkinType.buildings]].Get(type).Name;

        upgradePanel.DesciptionText.text = _shopSkinContainer.BuildingsInfoFabrics[_player.CurrentSkins[(int)SkinType.buildings]].Get(type).Description;

        upgradePanel.Icon.sprite = _shopSkinContainer.BuildingsInfoFabrics[_player.CurrentSkins[(int)SkinType.buildings]].Get(type).Icon;

        if (type >= BuildingType.Symbol0 && type <= BuildingType.Symbol10)
        {
            upgradePanel.SymbolImage.sprite = gridControl.BuildingsImage.SlotSymbol[type - BuildingType.Symbol0];
            upgradePanel.SymbolImage.gameObject.SetActive(true);
        }
        else
        {
            upgradePanel.SymbolImage.gameObject.SetActive(false);

        }
    }

    public void SetDiscriptionPanel(BuildingType type)
    {
        upgradePanel.UpgradePanel.SetPanel(true);
        upgradePanel.UpgradeButton.gameObject.SetActive(false);
        upgradePanel.DestroyButton.gameObject.SetActive(false);
        upgradePanel.CostText.gameObject.SetActive(true);
        upgradePanel.CostText.text = _shopSkinContainer.BuildingsInfoFabrics[_player.CurrentSkins[(int)SkinType.buildings]].Get(type).Cost.ToString();

        upgradePanel.MoveStashButton.gameObject.SetActive(false);

        upgradePanel.NameText.text = _shopSkinContainer.BuildingsInfoFabrics[_player.CurrentSkins[(int)SkinType.buildings]].Get(type).Name;

        upgradePanel.DesciptionText.text = _shopSkinContainer.BuildingsInfoFabrics[_player.CurrentSkins[(int)SkinType.buildings]].Get(type).Description;


        upgradePanel.Icon.sprite = _shopSkinContainer.BuildingsInfoFabrics[_player.CurrentSkins[(int)SkinType.buildings]].Get(type).Icon;

        if (type >= BuildingType.Symbol0 && type <= BuildingType.Symbol10)
        {
            upgradePanel.SymbolImage.sprite = gridControl.BuildingsImage.SlotSymbol[type - BuildingType.Symbol0];
            upgradePanel.SymbolImage.gameObject.SetActive(true);
        }
        else
        {
            upgradePanel.SymbolImage.gameObject.SetActive(false);

        }

    }


    public void ChangeBuilding(BuildingType index)
    {
        //if (_player.TryChangeCoins(
        
        /*if(!_player.TryChangeCoins(-_shopSkinContainer.BuildingsInfoFabrics[_player.CurrentSkins[(int)SkinType.buildings]].Get(index).Cost))
        {
            return;
        }*/

        BuildingSlot transferBuilding = BuildingsSlotFabric.Get(index);


        int y = CurrentBuildingIndex / StaticFields.MATRIX_SIZE;

        int x = CurrentBuildingIndex - y * StaticFields.MATRIX_SIZE;

        transferBuilding.Intialize(x, y, gridControl, 0);

        if (!_player.TryChangeCoins(-_shopSkinContainer.BuildingsInfoFabrics[_player.CurrentSkins[(int)SkinType.buildings]].Get(index).Cost) || !transferBuilding.TryPlaceBuilding())
        {
            SoundController.Instance.PlayAudioClip(_notEnoughMoney);


            return;
        }

        SoundController.Instance.PlayAudioClip(_buildSound);
        
        gridControl.ChangeType(CurrentBuildingIndex, index);
        _player.BuildingsInfo[CurrentBuildingIndex].CurrentUpdate = 1;
    }


    public void UpgradeBuilding()
    {
        if (_player.TryChangeCoins(
            -_player.BuildingsInfo[CurrentBuildingIndex].CurrentUpdate *
            _shopSkinContainer.BuildingsInfoFabrics[_player.CurrentSkins[(int)SkinType.buildings]].Get(_player.BuildingsInfo[CurrentBuildingIndex].CurrentBuildingType).UpgradeCost))
        {
            SoundController.Instance.PlayAudioClip(_upgradeSound);

            _player.BuildingsInfo[CurrentBuildingIndex].CurrentUpdate++;
            upgradePanel.CostText.text = (_shopSkinContainer.BuildingsInfoFabrics[_player.CurrentSkins[(int)SkinType.buildings]].Get(_player.BuildingsInfo[CurrentBuildingIndex].CurrentBuildingType).UpgradeCost * _player.BuildingsInfo[CurrentBuildingIndex].CurrentUpdate).ToString();

        }
        else
        {
            SoundController.Instance.PlayAudioClip(_notEnoughMoney);
        }
    }

    public void DestroyBuilding()
    {

        _player.TryChangeCoins(
            (   _shopSkinContainer.BuildingsInfoFabrics[_player.CurrentSkins[(int)SkinType.buildings]].Get(_player.BuildingsInfo[CurrentBuildingIndex].CurrentBuildingType).Cost + 
                _player.BuildingsInfo[CurrentBuildingIndex].CurrentUpdate *
                _shopSkinContainer.BuildingsInfoFabrics[_player.CurrentSkins[(int)SkinType.buildings]].Get(_player.BuildingsInfo[CurrentBuildingIndex].CurrentBuildingType).UpgradeCost) / 2);

        SoundController.Instance.PlayAudioClip(_demolishSound);


        gridControl.ChangeType(CurrentBuildingIndex, BuildingType.Empty);
        _player.BuildingsInfo[CurrentBuildingIndex].CurrentUpdate = 0;
        _shopPanel.SetPanel(false);
    }
}
