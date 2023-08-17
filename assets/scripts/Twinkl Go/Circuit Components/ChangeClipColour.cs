using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeClipColour : MonoBehaviour
{
    [Header("Crocodile Clip")]
    [Tooltip("The chosen crocodile clip")]
    [SerializeField]
    private static GameObject SelectedClip;

    [Header("Crocodile Clip Heads")]
    [Tooltip("Different coloured heads for the crocodile clips")]
    [SerializeField]
    private Sprite RedCrocodileClipHead;

    [SerializeField]
    private Sprite GreenCrocodileClipHead;

    [SerializeField]
    private Sprite YellowCrocodileClipHead;

    [SerializeField]
    private Sprite BlackCrocodileClipHead;

    [Header("Colors")]
    [Tooltip("Available colours for the crocodile clips")]
    [SerializeField]
    private Color Red;

    [SerializeField]
    private Color Green;

    [SerializeField]
    private Color Yellow;

    [SerializeField]
    private Color Black;

    private static TMP_Dropdown DropdownMenu;


    public static void ChangeValueForSelectedObject(int ChangedValue)
    {
        DropdownMenu.SetValueWithoutNotify(ChangedValue);
    }

    void Awake()
    {
        DropdownMenu = GetComponent<TMP_Dropdown>();
        
    }

    public static void SetCrocodileClip(GameObject CrocodileClip)
    {
        SelectedClip = CrocodileClip;
    }

    void ChangeColourOfCrocodileClipHeads(Color Colour, Sprite CrocodileClipHeadColour)
    {
        //Loop through the children of the crocodile clips
        foreach (Transform Child in SelectedClip.transform.parent)
        {
            //If the child is the crocodile clip
            if (Child.tag.Contains("CrocodileClip"))
            {
                //Obtain the image component and change the sprite according to the crocodile clip head colour
                Image CrocodileClipHead = Child.GetComponent<Image>();
                CrocodileClipHead.sprite = CrocodileClipHeadColour;
            }
            else if (Child.tag.Contains("Wire"))
            {
                //Obtain the image component
                Image CrocodileClipWire = Child.GetComponent<Image>();

                //Change the colour of the crocodile clip wire
                CrocodileClipWire.color = Colour;
            }
        }
    }

    //Changes the colour of the crocodile clip depending on the value selected from the dropdown menu
    public void ChangeColour(int Value)
    {
        switch (Value)
        {
            case 0:
                ChangeColourOfCrocodileClipHeads(Red, RedCrocodileClipHead);
                break;

            case 1:
                ChangeColourOfCrocodileClipHeads(Green, GreenCrocodileClipHead);
                break;

            case 2:
                ChangeColourOfCrocodileClipHeads(Yellow, YellowCrocodileClipHead);
                break;

            case 3:
                ChangeColourOfCrocodileClipHeads(Black, BlackCrocodileClipHead);
                break;
        }
        SetCrocodileClip(null);
        ColourSelection.HideDropdownMenu();
    }

}
