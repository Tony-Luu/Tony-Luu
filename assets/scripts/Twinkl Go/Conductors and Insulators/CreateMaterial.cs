using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreateMaterial : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IEndDragHandler
{
    [SerializeField]
    private Canvas MainCanvas;

    [SerializeField]
    private Camera MainCamera;

    [SerializeField]
    private int MaterialIndex;

    [SerializeField]
    private int PreviousMaterialIndex;

    [SerializeField]
    private int SiblingIndex;

    [SerializeField]
    private Image ButtonImage;

    [SerializeField]
    private GameObject Material;
    private GameObject MaterialClone;

    [SerializeField]
    private GameObject PaperclipImageHolder;

    [SerializeField]
    private List<Sprite> MaterialSpriteList;

    [SerializeField]
    private List<string> MaterialName;


    public void SetNewMaterial(GameObject NewMaterial)
    {
        Material = NewMaterial;
    }

    public void SetNewMaterialIndex(int NewMaterialIndex)
    {
        MaterialIndex = NewMaterialIndex;
    }

    public List<string> ReturnMaterialNameList()
    {
        return MaterialName;
    }

    public List<Sprite> ReturnMaterialSpriteList()
    {
        return MaterialSpriteList;
    }

    bool HasIndexChanged()
    {
        return MaterialIndex != PreviousMaterialIndex;
    }

    void Start()
    {
        ButtonImage = transform.Find("MaterialImage").GetComponent<Image>();
    }

    void ChangeButton()
    {
        if (HasIndexChanged())
        {
            ButtonImage.name = MaterialName[MaterialIndex];
            ButtonImage.sprite = MaterialSpriteList[MaterialIndex];
            PreviousMaterialIndex = MaterialIndex;
        }
    }

    void Update()
    {
        ChangeButton();
    }

    string ChangeMaterialTag(string MaterialName)
    {
        string NewTag = "";

        if (MaterialName.Contains("aluminium foil") || MaterialName.Contains("gold bar") || MaterialName.Contains("horseshoe") ||
            MaterialName.Contains("paperclip") || MaterialName.Contains("sea water") || MaterialName.Contains("silver spoon"))
        {
            NewTag = "Conductor";
        }
        else if (MaterialName.Contains("ball of wool") || MaterialName.Contains("paper") || MaterialName.Contains("piece of wood") ||
            MaterialName.Contains("plastic bottle") || MaterialName.Contains("rubber eraser") || MaterialName.Contains("rubber tyre"))
        {
            NewTag = "Insulator";
        }
        return NewTag;
    }

    //Change the created material if the index has changed
    void ChangeCreatedMaterial(GameObject Material)
    {
        //Obtain the image of the material
        Image MaterialImage = Material.GetComponent<Image>();

        //Change the material's name according to the index of the material name list
        Material.name = MaterialName[MaterialIndex];

        //Change the material's tag 
        Material.tag = ChangeMaterialTag(MaterialName[MaterialIndex]);

        //Set the alpha hit test minimum threshold for the mouse pointer to detect only non-transparent parts of the image
        MaterialImage.alphaHitTestMinimumThreshold = 0.1f;

        //If the material name is a paperclip
        if (MaterialName[MaterialIndex] == "paperclip")
        {
            //Obtain the rect transform
            RectTransform MaterialRectTransform = Material.GetComponent<RectTransform>();

            //Change the size of the object to match the size of the paperclip sprite
            MaterialRectTransform.sizeDelta = new Vector2(30f, 70f);

            //Set a new color and sprite
            Color TransparentColor = new Color(0f, 0f, 0f, 0f);
            MaterialImage.color = TransparentColor;
            MaterialImage.sprite = null;

            //Create the paperclip image holder and parent it to the paperclip object
            GameObject PaperclipImageHolderClone = Instantiate(PaperclipImageHolder);
            PaperclipImageHolderClone.transform.SetParent(MaterialRectTransform);
        }
        else
        {
            //Change the sprite of the material according to the index of the material sprite list
            MaterialImage.sprite = MaterialSpriteList[MaterialIndex];
        }
    }

    //Creates a material at the box position
    void CreateMaterialAtBoxPosition(Vector2 BoxPosition)
    {
        //If the sprite material still exists
        if (MaterialSpriteList[MaterialIndex] != null)
        {
            //Create a component from the list
            MaterialClone = Instantiate(Material, Vector2.zero, Quaternion.identity);

            //Change the properities of the material
            ChangeCreatedMaterial(MaterialClone);

            //Obtain the rect transform from the material
            RectTransform MaterialRectTransform = MaterialClone.GetComponent<RectTransform>();

            //Set the parent of the component
            MaterialClone.transform.SetParent(MainCanvas.transform);

            //Remove the clone part of the name from the gameobject
            MaterialClone.name = MaterialClone.name.Replace("(Clone)", "");

            //Set the material's position to the box position
            MaterialRectTransform.position = BoxPosition;

            //Set the new local scale
            MaterialClone.transform.localScale = new Vector3(1f, 1f, 1f);

            //Set the sibling index of the new object
            MaterialClone.transform.SetSiblingIndex(SiblingIndex);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //If the button is dragged into either the box or the funnel
        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.tag.Contains("Box") &&
            !eventData.pointerCurrentRaycast.gameObject.tag.Contains("Funnel"))
        {
            //Obtain the material box script
            MaterialBox MaterialBoxScript = eventData.pointerCurrentRaycast.gameObject.GetComponent<MaterialBox>();

            //If the material box is empty
            if (MaterialBoxScript.ReturnMaterial() == null)
            {
                //Create the material at the position of the box
                CreateMaterialAtBoxPosition(eventData.pointerCurrentRaycast.gameObject.transform.position);

                //Set the new material as the created object
                MaterialBoxScript.SetNewMaterial(MaterialClone);
            }
        }
    }

    void DisplayTooltipText()
    {
        if(MaterialName[MaterialIndex] != "")
        {
            string ComponentTooltip = MaterialName[MaterialIndex];

            Func<string> GetTooltipTextFunc = () =>
            {
                return ComponentTooltip;
            };
            Tooltip.ShowTooltipStatic(GetTooltipTextFunc);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DisplayTooltipText();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.HideTooltipStatic();
    }

}
