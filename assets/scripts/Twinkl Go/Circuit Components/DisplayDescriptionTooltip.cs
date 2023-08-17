using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayDescriptionTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private string Description;


    public void OnPointerEnter(PointerEventData eventData)
    {
        DescriptionTooltip.SetDescriptionTooltip(Description);
        DescriptionTooltip.ShowDescriptionTooltip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DescriptionTooltip.HideDescriptionTooltip();
    }

}
