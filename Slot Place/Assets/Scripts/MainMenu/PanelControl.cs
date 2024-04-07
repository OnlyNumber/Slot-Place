using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelControl : MonoBehaviour
{
    [SerializeField]
    RectTransform rect;

    public RectTransform canvas;

    public float duration;

    public bool IsScaling;

    public UnityEngine.UI.Image BlockScreen;

    public DirectionType HowToShow;

    public DirectionType HowToHide;

    public Ease EaseType;

    public bool IsMainPanel;

    /*private Vector2 defaultPosition;*/




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
            
        if(!IsMainPanel)
        {
            //Debug.Log(canvas.gameObject.name);

            //Debug.Log("Height " + canvas.rect.height.ToString());
            rect.anchoredPosition = new Vector2(axis[(int)HowToShow].x * canvas.rect.width, axis[(int)HowToShow].y * canvas.rect.height);

        }

        /*if(IsNotAnchoredByAllAxis)
        {
            defaultPosition = rect.anchoredPosition;
        }
        else
        {
            defaultPosition = Vector2.zero;

        }*/


        //canvas = FindObjectOfType<Canvas>().GetComponent<RectTransform>();

    }

    [ContextMenu("Check")]
    public void Check()
    {
        Debug.Log("Height " + canvas.rect.height.ToString());

    }

    [ContextMenu("Hide")]
    public void Hide()
    {
        rect.DOKill(true);
        rect.gameObject.SetActive(true);

        rect.anchoredPosition = Vector2.zero;

        DOTween.Sequence()
            .AppendCallback(() => BlockScreenActivation(true))
            .Append(rect.DOAnchorPos(new Vector2(axis[(int)HowToHide].x * canvas.rect.width, axis[(int)HowToHide].y * canvas.rect.height), duration, true).SetEase(EaseType))
            .AppendCallback(() => BlockScreenActivation(false));
    }

    [ContextMenu("Show")]

    public void Show()
    {
        rect.DOKill(true);

        Debug.Log(gameObject.name);

        rect.gameObject.SetActive(true);

        rect.anchoredPosition = new Vector2(axis[(int)HowToShow].x * canvas.rect.width, axis[(int)HowToShow].y * canvas.rect.height);

        DOTween.Sequence()
            .AppendCallback(() => BlockScreenActivation(true))
            .Append(rect.DOAnchorPos(Vector2.zero, duration, true).SetEase(EaseType))
            .AppendCallback(() => BlockScreenActivation(false));

    }

    public void BlockScreenActivation(bool active)
    {
        BlockScreen.gameObject.SetActive(active);
    }


    public void SetPanel(bool state)
    {
        if(state)
        {
            Show();
        }
        else
        {
            Hide();
        }


        //gameObject.SetActive(state);
    }
}
