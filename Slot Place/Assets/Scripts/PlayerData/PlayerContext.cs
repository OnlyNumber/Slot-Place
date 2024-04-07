using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerContext : MonoInstaller
{
    private PlayerData _playerData = null;

    [SerializeField]
    private ShopSkinContainer _skinContainer;

    public override void InstallBindings()
    {
        _playerData = SaveManager.Load<PlayerData>(StaticFields.PLAYER_DATA);

        Container.BindInterfacesAndSelfTo<PlayerData>().FromInstance(_playerData).AsSingle();

        Container.BindInterfacesAndSelfTo<ShopSkinContainer>().FromInstance(_skinContainer).AsSingle();


    }
    private void OnApplicationQuit()
    {
        SaveManager.Save(StaticFields.PLAYER_DATA, _playerData);
    }
}
