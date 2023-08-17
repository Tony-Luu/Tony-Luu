using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayColourSelection : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private RectTransform CanvasRectTransform;


    void Awake()
    {
        CanvasRectTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();
    }

    void DeselectDropdownMenu()
    {
        //If a gameobject isn't selected while the left click button is pressed and the dropdown menu is active
        if (Input.GetMouseButtonDown(0) && EventSystem.current.currentSelectedGameObject == null && ColourSelection.ReturnDropdownMenu().gameObject.activeInHierarchy)
        {
            //Hide the menu
            ColourSelection.HideDropdownMenu();
        }
    }

    void Update()
    {
        DeselectDropdownMenu();
    }

    void ChangeValueBasedOnColour()
    {
        //Obtain the image component from the gameobject
        Image CrocodileClipSprite = gameObject.GetComponent<Image>();

        //Change the value of the dropdown menu based on the colour of the crocodile clip
        switch (CrocodileClipSprite.sprite.name)
        {
            case "RedCrocodileClipHead":
                ChangeClipColour.ChangeValueForSelectedObject(0);
                break;

            case "GreenCrocodileClipHead":
                ChangeClipColour.ChangeValueForSelectedObject(1);
                break;

            case "YellowCrocodileClipHead":
                ChangeClipColour.ChangeValueForSelectedObject(2);
                break;

            case "BlackCrocodileClipHead":
                ChangeClipColour.ChangeValueForSelectedObject(3);
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            ColourSelection.HideDropdownMenu();

            //Create a vector 2 variable for output
            Vector2 ConvertedPosition;

            //Convert screen point to local point
            RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvasRectTransform, Input.mousePosition, Camera.main, out ConvertedPosition);

            ColourSelection.ReturnDropdownMenu().anchoredPosition = ConvertedPosition;
            ColourSelection.ShowDropdownMenu();
            ChangeClipColour.SetCrocodileClip(gameObject);
            ChangeValueBasedOnColour();
        }
    }

}
