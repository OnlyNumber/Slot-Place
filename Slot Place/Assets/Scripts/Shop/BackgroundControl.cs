using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControl : MonoBehaviour
{
    private PlayerData _player;
    private ShopSkinContainer _shopSkinContainer;
    [SerializeField]
    private List<UnityEngine.UI.Image> _background;

    [Zenject.Inject]
    public void Initialize(PlayerData player, ShopSkinContainer shopSkinContainer)
    {
        _player = player;

        _shopSkinContainer = shopSkinContainer;

        _player.OnSkinChanged += ChangeBackground;

        ChangeBackground();
    }

    public void ChangeBackground()
    {
        foreach (var item in _background)
        {
            item.sprite = _shopSkinContainer.Backgrounds[_player.CurrentSkins[(int)SkinType.background]];

        }

    }

    private void OnDestroy()
    {
        _player.OnSkinChanged -= ChangeBackground;
    }


}
