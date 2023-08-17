using UnityEngine;
using UnityEngine.UI;

public class SideScrollingMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField]
    private Button PreviousButton;

    [SerializeField]
    private Button NextButton;

    [Header("Button Images")]
    [SerializeField]
    private Image PreviousButtonImage;

    [SerializeField]
    private Image NextButtonImage;

    [Header("Menu Values")]
    [SerializeField]
    private int MenuIndex;

    [SerializeField]
    private int MaxIndex;


    public int ReturnMenuIndex()
    {
        return MenuIndex;
    }

    //Change the selection of the button component
    void ChangeMaterialSelection()
    {
        foreach (Transform Button in transform)
        {
            if (Button.transform.tag.Contains("Button"))
            {
                CreateMaterial CreateMaterialScript = Button.GetComponent<CreateMaterial>();

                CreateMaterialScript.SetNewMaterialIndex(MenuIndex);
            }
        }
    }
    
    //Decrease menu index when the player clicks on the previous button
    public void DecrementMenuIndex()
    {
        MenuIndex--;
        if (MenuIndex > 0)
        {
            PreviousButtonImage.color = new Color32(223, 76, 74, 255);
        }
        else
        {
            PreviousButtonImage.color = Color.grey;
            MenuIndex = 0;
        }
        ChangeButtonColorToDefault();
        ChangeMaterialSelection();
    }

    //If the menu index is more than 0 and less than the amount of components then change the color of the buttons to default
    void ChangeButtonColorToDefault()
    {
        if (MenuIndex > 0 && MenuIndex < MaxIndex)
        {
            NextButtonImage.color = new Color32(223, 76, 74, 255);
            PreviousButtonImage.color = new Color32(223, 76, 74, 255);
        }
    }

    //Increase menu index when the player clicks on the next button
    public void IncrementMenuIndex()
    {
        MenuIndex++;
        if (MenuIndex < MaxIndex)
        {
            NextButtonImage.color = new Color32(223, 76, 74, 255);
        }
        else
        {
            NextButtonImage.color = Color.grey;
            MenuIndex = MaxIndex;
        }
        ChangeButtonColorToDefault();
        ChangeMaterialSelection();
    }

    void Start()
    {
        PreviousButtonImage = PreviousButton.GetComponent<Image>();
        NextButtonImage = NextButton.GetComponent<Image>();
    }

}
