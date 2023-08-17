using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreateComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IEndDragHandler
{
    [SerializeField]
    private Canvas MainCanvas;

    [SerializeField]
    private Camera MainCamera;

    [SerializeField]
    private int ComponentIndex;

    [SerializeField]
    private int PreviousComponentIndex;

    [SerializeField]
    private int SiblingIndex;

    [SerializeField]
    private Image ButtonImage;

    [SerializeField]
    private List<Sprite> ComponentSpriteList;

    [SerializeField]
    private List<GameObject> ComponentList;

    [SerializeField]
    private Vector2 OriginalPosition;

    [SerializeField]
    private CheckCircuitProgression CheckCircuitProgressionScript;


    public void SetNewComponentIndex(int NewComponentIndex)
    {
        ComponentIndex = NewComponentIndex;
    }

    public int ReturnComponentIndex()
    {
        return ComponentIndex;
    }

    bool HasIndexChanged()
    {
        return ComponentIndex != PreviousComponentIndex;
    }

    void Start()
    {
        OriginalPosition = transform.position;
        PreviousComponentIndex = ComponentIndex;
        ButtonImage = transform.Find("ComponentImage").GetComponent<Image>();
        CheckCircuitProgressionScript = GameObject.Find("CircuitBoxArea").GetComponent<CheckCircuitProgression>();
    }

    void Update()
    {
        ChangeButtonSprite();
    }

    //Change the button sprite if the index has changed
    void ChangeButtonSprite()
    {
        if (HasIndexChanged())
        {
            ButtonImage.sprite = ComponentSpriteList[ComponentIndex];
            if (ComponentList[ComponentIndex].name.Contains("MotorComponent"))
            {
                ButtonImage.rectTransform.sizeDelta = new Vector2(ButtonImage.sprite.rect.width / 8f, ButtonImage.sprite.rect.height / 8f);
            }
            else if (ComponentList[ComponentIndex].name.Contains("SwitchComponent"))
            {
                ButtonImage.rectTransform.sizeDelta = new Vector2(ButtonImage.sprite.rect.width / 2f, ButtonImage.sprite.rect.height / 2f);
            }
            else
            {
                ButtonImage.rectTransform.sizeDelta = new Vector2(ButtonImage.sprite.rect.width / 4f, ButtonImage.sprite.rect.height / 4f);
            }
            PreviousComponentIndex = ComponentIndex;
        }        
    }

    //Creates a component at the mouse position
    void CreateComponentAtMousePosition(Vector2 MousePosition)
    {
        //Create a vector 2 variable for output
        Vector2 ConvertedPosition;

        //Create a component from the list
        GameObject ComponentClone = Instantiate(ComponentList[ComponentIndex], Vector2.zero, Quaternion.identity);

        //Obtain rect transforms from the component and the canvas
        RectTransform ComponentRectTransform = ComponentClone.GetComponent<RectTransform>();
        RectTransform CanvasRectTransform = MainCanvas.GetComponent<RectTransform>();

        //Convert screen point to local point
        RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvasRectTransform, MousePosition, MainCamera, out ConvertedPosition);

        //Set the parent of the component
        ComponentClone.transform.SetParent(MainCanvas.transform);

        //Remove the clone part of the name from the gameobject
        ComponentClone.name = ComponentClone.name.Replace("(Clone)", "");

        //Set the component's position to the converted position
        ComponentRectTransform.anchoredPosition = ConvertedPosition;

        //Set the new local scale
        ComponentClone.transform.localScale = new Vector3(1f, 1f, 1f);

        //Set the sibling index of the new object
        ComponentClone.transform.SetSiblingIndex(SiblingIndex);

        //Add the clone to the component list
        CheckCircuitProgressionScript.AddComponentToList(ComponentClone);

        //Update the count of components
        CheckCircuitProgressionScript.UpdateComponentCount();
    }

    void DisplayTooltipText()
    {
        string ComponentTooltip = "";

        switch (ComponentList[ComponentIndex].name)
        {
            case "BatteryComponent":
                ComponentTooltip = "Battery";
                break;

            case "BuzzerComponent":
                ComponentTooltip = "Buzzer";
                break;

            case "CellComponent":
                ComponentTooltip = "Cell";
                break;

            case "CrocodileClipsComponent":
                ComponentTooltip = "Crocodile Clips";
                break;

            case "LightbulbComponent":
                ComponentTooltip = "Lightbulb";
                break;

            case "MotorComponent":
                ComponentTooltip = "Motor";
                break;

            case "SwitchComponent":
                ComponentTooltip = "Switch";
                break;
        }

        Func<string> GetTooltipTextFunc = () =>
        {
            return ComponentTooltip;
        };
        Tooltip.ShowTooltipStatic(GetTooltipTextFunc);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OriginalPosition = transform.position;
        DisplayTooltipText();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.HideTooltipStatic();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            if (eventData.pointerCurrentRaycast.gameObject.CompareTag("CircuitBox") || eventData.pointerCurrentRaycast.gameObject.CompareTag("Component") ||
                eventData.pointerCurrentRaycast.gameObject.CompareTag("CrocodileClip"))
            {
                CreateComponentAtMousePosition(eventData.position);
            }
        }
        transform.position = OriginalPosition;
    }

}
