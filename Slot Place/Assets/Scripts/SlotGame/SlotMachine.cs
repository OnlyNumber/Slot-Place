using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class SlotMachine : MonoBehaviour, ISlotControl
{
    [SerializeField]
    private SlotItemInfo CoeficientsInfo;

    [SerializeField]
    private List<SlotItem> _slotItems;

    [SerializeField]
    private List<BuildingSlot> _buildings = new List<BuildingSlot>();

    [SerializeField]
    private SlotItem _prefabItem;

    [SerializeField]
    private UILineRenderer _uILineRendererPrefab;

    [SerializeField]
    private List<UILineRenderer> _uILineRenderers;

    [SerializeField]
    private Transform _slotsGrid;

    [SerializeField]
    private RectTransform _panelGrid;

    public BuildingsInfoFabric BuildingsImage;

    private PlayerData _player;

    private ShopSkinContainer _shopSkinContainer;

    [SerializeField]
    private float delay;



    public System.Action<float> OnCoefiecintCreate;

    public System.Action OnStartRoll;

    public delegate bool CheckDelegate();

    public event CheckDelegate OnCheckStartRoll;

    private void Start()
    {
        for (int i = 0; i < StaticFields.MATRIX_SIZE * StaticFields.MATRIX_SIZE; i++)
        {
            _slotItems.Add(Instantiate(_prefabItem, _slotsGrid));

            _slotItems[i].SetAndStopIndex(Random.Range(0, _slotItems[i].GetSpritesCount()));
        }
    }

    [Zenject.Inject] public void Intialize(PlayerData data, ShopSkinContainer shopSkinContainer)
    {
        _player = data;

        _shopSkinContainer = shopSkinContainer;

        Debug.Log(_shopSkinContainer.gameObject.name);

        if(_player.BuildingsInfo.Count <= 0)
        {
            return;
        }

        for (int x = 0; x < StaticFields.MATRIX_SIZE; x++)
        {
            for (int y = 0; y < StaticFields.MATRIX_SIZE; y++)
            {
                BuildingSlot slot = BuildingsSlotFabric.Get(_player.BuildingsInfo[x + y * StaticFields.MATRIX_SIZE].CurrentBuildingType);

                _buildings.Add(slot);

                slot.Intialize(x, y, this, _player.BuildingsInfo[x + y * StaticFields.MATRIX_SIZE].CurrentUpdate);
            }
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

    public void StartRoll()
    {


        if(_slotItems[0].IsRolling )
        {
            return;
        }

        if(!OnCheckStartRoll())
        {
            return;
        }

        ClearLines();

        firstItems.Clear();

        foreach (var item in _slotItems)
        {
            item.IsRolling = true;
        }

        OnStartRoll?.Invoke();

        StartCoroutine(DelayResult());

    }

    IEnumerator DelayResult()
    {
        yield return new WaitForSeconds(delay);
        StopRoll();

    }

    public void StopRoll()
    {
        foreach (var item in _slotItems)
        {
            item.SetAndStopIndex(Random.Range(0, item.GetSpritesCount()));
        }

        CheckCombinations();
    }

    public List<SlotItem> firstItems = new List<SlotItem>();

    public List<List<SlotItem>> slotItemsLines = new List<List<SlotItem>>(); 

    public void CheckCombinations()
    {
        ClearLines();

        firstItems.Clear();

        slotItemsLines.Clear();

        for (int i = 0; i < StaticFields.MATRIX_SIZE; i++)
        {
            if (!IsRepetativeItem(_slotItems[i * StaticFields.MATRIX_SIZE]))
            {
                firstItems.Add(_slotItems[i * StaticFields.MATRIX_SIZE]);
            }
        }

        foreach (var item in _slotItems)
        {
            item.CurrentCoeficient = CoeficientsInfo.coeficients[item.CurrentIndex];
        }

        foreach (var item in _buildings)
        {
            item.BuildEffect();
        }

        bool IsHaveInColumn = false;

        UILineRenderer transferLine;

        List<SlotItem> transferItems;

        int pointIndex = 0;

        foreach (var slotItem in firstItems)
        {
            transferLine = Instantiate(_uILineRendererPrefab, _panelGrid);


            transferLine.rectTransform.position = _panelGrid.position;

            transferLine.Points = new Vector2[25];

            if(_shopSkinContainer == null)
            {
                Debug.Log("_shopSkinContainer == null");
            }

            transferLine.material = _shopSkinContainer.LineMaterials[_player.CurrentSkins[(int)SkinType.line]];

            pointIndex = 0;

            transferItems = new List<SlotItem>();

            transferItems.Add(slotItem);

            for (int x = 0; x < StaticFields.MATRIX_SIZE; x++)
            {
                IsHaveInColumn = false;

                for (int y = 0; y < StaticFields.MATRIX_SIZE; y++)
                {
                    if (_slotItems[x + y * StaticFields.MATRIX_SIZE].CurrentIndex == slotItem.CurrentIndex)
                    {
                        transferItems.Add(_slotItems[x + y * StaticFields.MATRIX_SIZE]);

                        transferLine.Points[pointIndex] = _slotItems[x + y * StaticFields.MATRIX_SIZE].GetComponent<RectTransform>().localPosition;
                        pointIndex++;
                        IsHaveInColumn = true;
                    }
                }

                if (!IsHaveInColumn && x == 1)
                {
                    Debug.Log("destroyed  count:" + transferItems);

                    transferItems.Clear();

                    Destroy(transferLine.gameObject);
                    break;
                }
                
                
                if (!IsHaveInColumn)
                {
                    //slotItemsLines.Add(transferItems);
                    break;
                }
            }

            if(transferItems.Count != 0)
            {
                slotItemsLines.Add(transferItems);
            }


            if (transferLine != null)
            {
                Debug.Log(transferLine.gameObject.name);

                _uILineRenderers.Add(transferLine);
            }

        }
        foreach (var item in _uILineRenderers)
        {
            item.Points = CopyArrayWithout00(item.Points);
        }

        float coeficient = 0;

        float currentCoef = 0;

        Debug.Log(slotItemsLines.Count);

        foreach (var CurrentLine in slotItemsLines)
        {
            foreach (var item in CurrentLine)
            {
                coeficient += item.CurrentCoeficient;
                currentCoef += item.CurrentCoeficient;
            }

            Debug.Log("Before coef");
            Debug.Log("CurrentLine: " + currentCoef);
            currentCoef = 0;
        }

        Debug.Log("All coef: " + coeficient);


        OnCoefiecintCreate?.Invoke(coeficient);

    }

    private void ClearLines()
    {
        for (int i = 0; i < _uILineRenderers.Count; i++)
        {
            if(_uILineRenderers[i] != null)
            Destroy(_uILineRenderers[i].gameObject);
        }

        _uILineRenderers.Clear();
    }


    public Vector2[] CopyArrayWithout00(Vector2[] array)
    {
        int lenght = 0;

        foreach (var item in array)
        {
            if (item != Vector2.zero)
            {
                lenght++;
            }
            else
            {
                break;
            }
        }

        Vector2[] copyArray = new Vector2[lenght];

        for (int i = 0; i < copyArray.Length; i++)
        {
            copyArray[i] = array[i];
        }


        return copyArray;
    }

    public bool IsRepetativeItem(SlotItem item)
    {
        foreach (var slotItem in firstItems)
        {
            if (item.CurrentIndex == slotItem.CurrentIndex)
            {
                return true;
            }

        }

        return false;
    }

    public SlotItem GetItem(int x, int y)
    {
        if(x < 0 || x >= StaticFields.MATRIX_SIZE || y < 0 || y >= StaticFields.MATRIX_SIZE)
        {
            return null;
        }

        return _slotItems[x + y * StaticFields.MATRIX_SIZE];

    }


}
