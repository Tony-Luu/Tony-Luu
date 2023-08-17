using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DescriptionTooltip : MonoBehaviour
{
    [SerializeField]
    private static Image TextBackground;

    [SerializeField]
    private static TextMeshProUGUI Description;



    void Start()
    {
        TextBackground = GetComponentInChildren<Image>();
        Description = GetComponentInChildren<TextMeshProUGUI>();
        HideDescriptionTooltip();
    }

    public static void SetDescriptionTooltip(string DescriptionTooltip)
    {
        Description.text = DescriptionTooltip;
    }

    public static void ShowDescriptionTooltip()
    {
        TextBackground.gameObject.SetActive(true);
        Description.gameObject.SetActive(true);
    }

    public static void HideDescriptionTooltip()
    {
        TextBackground.gameObject.SetActive(false);
        Description.gameObject.SetActive(false);
    }

}
