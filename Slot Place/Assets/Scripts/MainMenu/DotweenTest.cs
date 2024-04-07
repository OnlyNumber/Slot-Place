using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DotweenTest : MonoBehaviour
{
    RectTransform rect;

    public RectTransform canvas;

    public float duration;

    public bool IsScaling;

    public UnityEngine.UI.Image BlockScreen;

    public DirectionType HowToShow;

    public DirectionType HowToHide;

    public Ease EaseType;


    private Vector2[] axis =
        {
        Vector2.up,
        Vector2.right,
        Vector2.down,
        Vector2.left
        };


    private void Start()
    {

        rect = GetComponent<RectTransform>();

        //canvas = FindObjectOfType<Canvas>().GetComponent<RectTransform>();

    }

    [ContextMenu("Hide")]
    public void Hide()
    {
        rect.DOKill(true);
        rect.anchoredPosition = Vector2.zero;
        
        DOTween.Sequence()
            .AppendCallback(() => BlockScreenActivation(true))
            .Append(rect.DOAnchorPos(new Vector2(axis[(int)HowToHide].x * canvas.rect.width, axis[(int)HowToHide].y * canvas.rect.height), duration,true).SetEase(EaseType))
            .AppendCallback(() => BlockScreenActivation(false));
    }

    [ContextMenu("Show")]

    public void Show()
    {
        rect.DOKill(true);

        rect.anchoredPosition = new Vector2(axis[(int)HowToShow].x * canvas.rect.width, axis[(int)HowToShow].y * canvas.rect.height);

        DOTween.Sequence()
            .AppendCallback(() => BlockScreenActivation(true))
            .Append(rect.DOAnchorPos(new Vector2(0, 0), duration, true).SetEase(EaseType))
            .AppendCallback(() => BlockScreenActivation(false));

    }

    public void BlockScreenActivation(bool active)
    {
        BlockScreen.gameObject.SetActive(active);
    }

}

public enum DirectionType
{
    up,
    right,
    down,
    left
}