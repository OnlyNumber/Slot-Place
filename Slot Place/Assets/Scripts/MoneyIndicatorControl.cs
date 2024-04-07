using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyIndicatorControl : MonoBehaviour
{
    [SerializeField]
    private List<TMP_Text> moneyTexts;

    private PlayerData _player;

    [Zenject.Inject]
    public void Initialize(PlayerData player)
    {
        _player = player;
        _player.OnCoinsChanged += ChangeIndicators;
        ChangeIndicators();
    }

    private void ChangeIndicators()
    {
        foreach (var indicator in moneyTexts)
        {
            indicator.text = _player.Coins.ToString();
        }
    }

    private void OnDestroy()
    {
        _player.OnCoinsChanged -= ChangeIndicators;

    }
}
