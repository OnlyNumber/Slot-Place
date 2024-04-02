using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;

public class BuildingUIItemShop : MonoBehaviour
{
    [SerializeField, FormerlySerializedAs("_iconImage")]
    public Image IconImage;

    public Image SymbolImage;

    public Button DescriptionButton;

    [SerializeField, FormerlySerializedAs("_descriptionText")]
    public TMP_Text CostText;

    [SerializeField, FormerlySerializedAs("_buyButton")]
    public Button BuyButton;

}
