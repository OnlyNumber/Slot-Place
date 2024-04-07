using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCurrentPanel : MonoBehaviour
{
    [SerializeField]
    private List<PanelControl> _panels;

    private PanelControl _currentPanel;

    private void Start()
    {
        _currentPanel = _panels[0];
    }

    public void ShowPanel(int index)
    {
        if(_currentPanel == _panels[index])
        {
            return;
        }

        _currentPanel.Hide();

        _currentPanel = _panels[index];

        _currentPanel.Show();
    }

}
