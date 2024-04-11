using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialChecker : MonoBehaviour
{
    [SerializeField]
    private PanelControl _tutorialPanel;

    [SerializeField]
    private PanelControl _mainPanel;

    //[SerializeField]
    //private PanelControl _dailyRewardPanel;

    [SerializeField]
    private GameObject _tutorialBackground;

    [Zenject.Inject] public void Initialize(PlayerData player)
    {
        if(player.IsFirstLog)
        {
            _tutorialBackground.SetActive(true);
            _mainPanel.SetPanel(false);
            _tutorialPanel.SetPanel(true);
        }
        else
        {
            //_mainPanel.SetPanel(true);
        }
        player.IsFirstLog = false;

        //_playerData = player;

    }



}
