using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDefault : MonoBehaviour
{
    protected PlayerData Player;

    [SerializeField]
    protected SkinType ShopType;

    protected List<ShopItem> ShopItems = new List<ShopItem>();

    [SerializeField]
    protected List<string> SkinNames;

    [SerializeField]
    private ShopItem _itemPrefab;

    [SerializeField]
    private RectTransform _contentTransform;

    [Zenject.Inject]
    public void Initialize(PlayerData player, ShopSkinContainer skinContainer)
    {

        ShopItem transfer;

        while (player.OpenedSkins[(int)ShopType].Skins.Count < skinContainer.GetCount(ShopType))
        {
            player.OpenedSkins[(int)ShopType].Skins.Add(false);
        }

        while (player.OpenedSkins[(int)ShopType].Skins.Count > skinContainer.GetCount(ShopType))
        {
            player.OpenedSkins[(int)ShopType].Skins.RemoveAt(player.OpenedSkins[(int)ShopType].Skins.Count - 1);
        }

        Player = player;

        

        for (int i = 0; i < skinContainer.GetCount(ShopType); i++)
        {
            transfer = Instantiate(_itemPrefab, _contentTransform);

            int index = i;

            ShopItems.Add(transfer);

            switch (ShopType)
            {
                case SkinType.background:
                    {
                        transfer.ItemImage.sprite = skinContainer.Backgrounds[i];
                        break;
                    }

                case SkinType.buildings:
                    {
                        transfer.ItemImage.sprite = skinContainer.BuildingsInfoFabrics[i].Get(BuildingType.Residential).Icon;
                        break;
                    }

                case SkinType.line:
                    {
                        transfer.ItemImage.material = skinContainer.LineMaterials[i];
                        break;
                    }
                
            }
            if (ShopType == SkinType.background)
            {
                transfer.ItemImage.sprite = skinContainer.Backgrounds[i];
            }
            


            transfer.CostText.text = (i * 100).ToString();

            transfer.ItemName.text = SkinNames[i];

            if (Player.OpenedSkins[(int)ShopType].Skins[i])
            {
                if (Player.CurrentSkins[(int)ShopType] == i)
                {
                    transfer.AcceptImage.gameObject.SetActive(true);
                }

                transfer.CostGO.SetActive(false);
            }

            transfer.ShopButton.onClick.AddListener(() => BuyOrEquip(ShopType, index, index * 100));
        }

        if (!Player.OpenedSkins[(int)ShopType].Skins[0])
        {
            Player.OpenedSkins[(int)ShopType].Skins[0] = true;
            BuyOrEquip(ShopType, 0, 0);
        }

    }


    public void BuyOrEquip(SkinType type, int index, float cost)
    {
        if (Player.OpenedSkins[(int)type].Skins[index])
        {
            ShopItems[index].CostGO.SetActive(false);

            ShopItems[Player.CurrentSkins[(int)type]].AcceptImage.gameObject.SetActive(false);

            Player.ChangeSkin(type, index);
            
            ShopItems[Player.CurrentSkins[(int)type]].AcceptImage.gameObject.SetActive(true);

            return;
        }


        if (Player.TryChangeCoins(-cost))
        {
            Player.OpenedSkins[(int)type].Skins[index] = true;

            ShopItems[index].CostGO.SetActive(false);

            BuyOrEquip(type, index, cost);
        }

    }
}
