using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingShop : MonoBehaviour
{
    [SerializeField]
    private BuildingsImageFabric _buildingsInfoFabric;

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

    int CurrentBuildingIndex;

    private PlayerData _player;

    [Zenject.Inject]
    public void Initialize(PlayerData player)
    {
        _player = player;
    }

    private void Start()
    {
        shopContent.sizeDelta = new Vector2(0, (int)BuildingType.Count * (_buildUIItemPrefab.GetComponent<RectTransform>().rect.size.y + _verticalLayout.spacing));

        foreach (BuildingType item in System.Enum.GetValues(typeof(BuildingType)))
        {
            if (item >= BuildingType.Count || item == BuildingType.Empty)
            {
                break;
            }

            BuildingType index = item;

            buildUIItems.Add(Instantiate(_buildUIItemPrefab, shopContent));

            buildUIItems[(int)item].IconImage.sprite = _buildingsInfoFabric.Get(item).Icon;

            buildUIItems[(int)item].CostText.text = _buildingsInfoFabric.Get(item).Cost.ToString();

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
        upgradePanel.MoveStashButton.gameObject.SetActive(false);

        upgradePanel.NameText.text = _buildingsInfoFabric.Get(type).Name;

        upgradePanel.DesciptionText.text = _buildingsInfoFabric.Get(type).Description;

        upgradePanel.Icon.sprite = _buildingsInfoFabric.Get(type).Icon;

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
        upgradePanel.CostText.gameObject.SetActive(false);
        upgradePanel.MoveStashButton.gameObject.SetActive(false);

        upgradePanel.NameText.text = _buildingsInfoFabric.Get(type).Name;

        upgradePanel.DesciptionText.text = _buildingsInfoFabric.Get(type).Description;


        upgradePanel.Icon.sprite = _buildingsInfoFabric.Get(type).Icon;

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
        gridControl.ChangeType(CurrentBuildingIndex, index);
        _player.BuildingsInfo[CurrentBuildingIndex].CurrentUpdate = 1;
    }


    public void UpgradeBuilding()
    {
        _player.BuildingsInfo[CurrentBuildingIndex].CurrentUpdate++;
    }

    public void DestroyBuilding()
    {
        Debug.Log("CurrentBuildingIndex " + CurrentBuildingIndex);


        gridControl.ChangeType(CurrentBuildingIndex, BuildingType.Empty);
        _player.BuildingsInfo[CurrentBuildingIndex].CurrentUpdate = 0;
        _shopPanel.SetPanel(false);
    }

    public void MoveToStahBuilding()
    {

    }

}
