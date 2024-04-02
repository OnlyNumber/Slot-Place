using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingsImage")]
public class BuildingsImageFabric : ScriptableObject
{
    [SerializeField] private BuildSO Empty;
    [SerializeField] private BuildSO Bank;
    [SerializeField] private BuildSO Storage;
    [SerializeField] private BuildSO Residential;
    [SerializeField] private BuildSO Symbol0;
    [SerializeField] private BuildSO Symbol1;
    [SerializeField] private BuildSO Symbol2;
    [SerializeField] private BuildSO Symbol3;
    [SerializeField] private BuildSO Symbol4;
    [SerializeField] private BuildSO Symbol5;
    [SerializeField] private BuildSO Symbol6;
    [SerializeField] private BuildSO Symbol7;
    [SerializeField] private BuildSO Symbol8;
    [SerializeField] private BuildSO Symbol9;
    [SerializeField] private BuildSO Symbol10;

    public List<Sprite> SlotSymbol;


    public BuildSO Get(BuildingType type)
    {
        switch (type)
        {
            case BuildingType.Bank:
                {
                    return Bank;
                }
            case BuildingType.Storage:
                {
                    return Storage;
                }
            case BuildingType.Residential:
                {
                    return Residential;
                }
            case BuildingType.Symbol0:
                {
                    return Symbol0;
                }
            case BuildingType.Symbol1:
                {
                    
                    return Symbol1;
                }
            case BuildingType.Symbol2:
                {
                    
                    return Symbol2;
                }
            case BuildingType.Symbol3:
                {
                    
                    return Symbol3;
                }
            case BuildingType.Symbol4:
                {
                    
                    return Symbol4;
                }
            case BuildingType.Symbol5:
                {
                    
                    return Symbol5;
                }
            case BuildingType.Symbol6:
                {
                    
                    return Symbol6;
                }
            case BuildingType.Symbol7:
                {
                    
                    return Symbol7;
                }
            case BuildingType.Symbol8:
                {
                    
                    return Symbol8;
                }
            case BuildingType.Symbol9:
                {
                    
                    return Symbol9;
                }
            case BuildingType.Symbol10:
                {
                    
                    return Symbol10;
                }
        }

        //Debug.Log(type);

        return Empty;
    }



}

public enum BuildingType
{
    Bank,
    Storage,
    Residential,
    Symbol0,
    Symbol1,
    Symbol2,
    Symbol3,
    Symbol4,
    Symbol5,
    Symbol6,
    Symbol7,
    Symbol8,
    Symbol9,
    Symbol10,



    Count,
    Empty


}