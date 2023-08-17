using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjectInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image[] ObjectImage;



    //Change the image color individually or in groups
    void ChangeImageColor(Color NewColor)
    {
        if (transform.childCount > 1)
        {
            foreach (Image ObjectGroupImages in ObjectImage)
            {
                ObjectGroupImages.color = NewColor;
                ObjectGroupImages.alphaHitTestMinimumThreshold = 0.1f;
            }
        }
        else
        {
            ObjectImage[0].color = NewColor;
            ObjectImage[0].alphaHitTestMinimumThreshold = 0.1f;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Change the color to green
        ChangeImageColor(new Color(0.15f, 1f, 0f, 0.75f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Change the color back to normal
        ChangeImageColor(new Color(1f, 1f, 1f, 1f));
    }

}
