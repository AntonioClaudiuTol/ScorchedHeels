using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private static Tooltip instance;
    private Text tooltipText;
    private RectTransform backgroundRectTransform;

    [SerializeField]
    private Camera uiCamera;

    private void Awake()
    {
        instance = this;
        tooltipText = transform.Find("Text").GetComponent<Text>();
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();

        ShowTooltip("random text");
    }

    private void Update()
    {
        Vector2 localPoint;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        transform.localPosition = localPoint;
    }

    private void MoveTooltip()
    {
        //toolTipText.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    private void ShowTooltip(string toolTipString)
    {
        gameObject.SetActive(true);
        tooltipText.text = toolTipString;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 2, tooltipText.preferredHeight + textPaddingSize * 2);
        backgroundRectTransform.sizeDelta = backgroundSize;
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowStatic()
    {
        instance.ShowTooltip("YourMom");
    }

    public static void HideStatic()
    {
        instance.HideTooltip();
    }
}
