using UnityEngine;
using System;
using TMPro;


public class Tooltip : MonoBehaviour
{
    public static Tooltip Instance { get; private set; }

    private Func<string> GetTooltipTextFunc;

    [SerializeField]
    private TextMeshProUGUI TextMeshPro;

    [SerializeField]
    private RectTransform BackgroundRectTransform;

    [SerializeField]
    private RectTransform CanvasRectTransform;

    [SerializeField]
    private RectTransform TooltipRectTransform;

    [SerializeField]
    private Canvas MainCanvas;


    void Awake()
    {
        Instance = this;
        MainCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        TooltipRectTransform = GetComponent<RectTransform>();
        HideTooltip();
    }

    void SetText(string TooltipText)
    {
        TextMeshPro.SetText(TooltipText);

        TextMeshPro.ForceMeshUpdate();

        Vector2 TextSize = TextMeshPro.GetRenderedValues(false);

        Vector2 PaddingSize = new Vector2(10f, 10f);

        BackgroundRectTransform.sizeDelta = TextSize + PaddingSize;
    }

    void ContainTooltipWithinCanvas()
    {
        float ScaleFactor = MainCanvas.scaleFactor;

        Vector2 AnchoredPosition = Input.mousePosition;

        if (AnchoredPosition.x + (BackgroundRectTransform.rect.width * ScaleFactor) > CanvasRectTransform.rect.width * ScaleFactor)
        {
            AnchoredPosition.x = (CanvasRectTransform.rect.width - BackgroundRectTransform.rect.width) * ScaleFactor;
        }
        if (AnchoredPosition.y + (BackgroundRectTransform.rect.height * ScaleFactor) > CanvasRectTransform.rect.height * ScaleFactor)
        {
            AnchoredPosition.y = (CanvasRectTransform.rect.height - BackgroundRectTransform.rect.height) * ScaleFactor;
        }

        TooltipRectTransform.anchoredPosition = new Vector2(AnchoredPosition.x / ScaleFactor, AnchoredPosition.y / ScaleFactor);
    }

    void Update()
    {
        ContainTooltipWithinCanvas();
    }

    void HideTooltip()
    {
        SetText("");
        TextMeshPro.gameObject.SetActive(false);
        BackgroundRectTransform.gameObject.SetActive(false);
    }

    void ShowTooltip(string TooltipText)
    {
        ShowTooltip(() => TooltipText);
    }

    private void ShowTooltip(Func<string> GetTooltipTextFunc)
    {
        this.GetTooltipTextFunc = GetTooltipTextFunc;
        TextMeshPro.gameObject.SetActive(true);
        BackgroundRectTransform.gameObject.SetActive(true);
        SetText(GetTooltipTextFunc());
    }

    public static void ShowTooltipStatic(string TooltipText)
    {
        Instance.ShowTooltip(TooltipText);
    }

    public static void ShowTooltipStatic(Func<string> GetTooltipStringFunc)
    {
        Instance.ShowTooltip(GetTooltipStringFunc);
    }

    public static void HideTooltipStatic()
    {
        Instance.HideTooltip();
    }
}
