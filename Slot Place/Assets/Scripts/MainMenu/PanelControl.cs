using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelControl : MonoBehaviour
{
    public void SetPanel(bool state)
    {
        gameObject.SetActive(state);
    }
}
