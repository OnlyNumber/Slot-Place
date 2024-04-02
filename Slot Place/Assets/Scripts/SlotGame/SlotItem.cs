using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotItem : MonoBehaviour
{
    [SerializeField]
    private Image _slotImage;

    [SerializeField]
    private List<Sprite> sprites;

    [SerializeField]
    private float _delayBeforeChange;

    private float _currentTime;

    public float CurrentCoeficient;

    public int CurrentIndex = 0;

    public bool IsRolling = false;

    private void Start()
    {
        CurrentIndex = Random.Range(0, sprites.Count);
    }

    private void Update()
    {
        if(!IsRolling)
        {
            return;
        }

        if(_currentTime <= 0)
        {
            //CurrentIndex++;

            /*if(CurrentIndex >= sprites.Count)
            {
                CurrentIndex = 0;
            }*/

            _slotImage.sprite = sprites[Random.Range(0, sprites.Count)];

            _currentTime = _delayBeforeChange;
        }

        _currentTime -= Time.deltaTime;
    }

    public void SetAndStopIndex(int index)
    {
        IsRolling = false;

        CurrentIndex = index;

        _slotImage.sprite = sprites[CurrentIndex];
    }

    public int GetSpritesCount()
    {
        return sprites.Count;
    }

}
