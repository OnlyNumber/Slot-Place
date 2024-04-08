using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;

public class BetCreator : MonoBehaviour
{
    private float _currentBet;

    [SerializeField]
    private TMP_InputField _inputFieldBet;

    [SerializeField]
    private SlotMachine _slotMachine;

    [SerializeField]
    private PanelControl _winPanel;

    [SerializeField]
    private PanelControl _losePanel;

    [SerializeField]
    private TMP_Text _winCoinText;

    [SerializeField]
    private AudioClip _winSound;

    [SerializeField]
    private List<ParticleSystem> winParticleSystems;

    [SerializeField]
    private float cheatMoney;

    private PlayerData _playerData;

    private void Start()
    {
        _inputFieldBet.onValueChanged.AddListener(BetChanged);

        _slotMachine.OnCoefiecintCreate += GetCoeficients;

        _slotMachine.OnCheckStartRoll += IsReay;
    }


    [Inject]
    public void Initialize(PlayerData player)
    {
        _playerData = player;
    }

    public bool IsReay()
    {
        _playerData.TryChangeCoins(-_currentBet);

        return _currentBet > 0;
    }

    public void BetChanged(string str)
    {
        _currentBet = float.Parse(str);

        if (_currentBet < 0)
        {
            _currentBet = 0;
        }

        if (_currentBet > _playerData.Coins)
        {
            _currentBet = _playerData.Coins;
        }

        _inputFieldBet.text = _currentBet.ToString();


    }

    public void ChangeBet(float changeNumber)
    {
        _currentBet += changeNumber;

        _inputFieldBet.text = _currentBet.ToString();
    }

    public void GetCoeficients(float coeficient)
    {
        if(coeficient == 0)
        {
            _losePanel.SetPanel(true);
            return;
        }

        foreach (var item in winParticleSystems)
        {
            item.Play();
        }

        _winPanel.SetPanel(true);

        _winCoinText.text = (coeficient * _currentBet * 2).ToString();

        _playerData.TryChangeCoins(coeficient * _currentBet * 2);

        SoundController.Instance.PlayAudioClip(_winSound);
    }

    [ContextMenu("CheatCoins")]
    public void CheatCoins()
    {
        _playerData.TryChangeCoins(cheatMoney);
    }


}
