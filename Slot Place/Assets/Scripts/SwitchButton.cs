using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour
{

    [SerializeField]
    private Image _image;

    [SerializeField]
    private Sprite _buttonSpriteOn;

    [SerializeField]
    private Sprite _buttonSpriteOff;

    private void Start()
    {
        if (_image == null)
        {
            _image = GetComponent<Image>();
        }
    }

    public void SwitchSprite(bool sprite)
    {
        if (_image == null)
        {
            _image = GetComponent<Image>();
        }

        if (sprite)
        {
            _image.sprite = _buttonSpriteOn;
        }
        else
        {
            _image.sprite = _buttonSpriteOff;
        }
    }


}
