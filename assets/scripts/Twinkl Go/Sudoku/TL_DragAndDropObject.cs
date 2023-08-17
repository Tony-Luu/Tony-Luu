using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TL_DragAndDropObject : MonoBehaviour, IPointerEnterHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField]
    private Canvas MainCanvas;

    [SerializeField]
    private CanvasGroup MainCanvasGroup;

    [SerializeField]
    private Vector3 OriginalPosition;

    [SerializeField]
    private Vector3 CurrentPosition;

    [SerializeField]
    private Vector3 PreviousPosition;

    [SerializeField]
    private int LayerMask;

    [SerializeField]
    private int SiblingIndex;

    private RectTransform NumberRectTransform;

    [SerializeField]
    private static bool IsNumberBeingDragged;

    [SerializeField]
    private AudioClip TilePlaceSound;

    [SerializeField]
    private AudioClip TilePickupSound;

    [SerializeField]
    private AudioClip TileWrongSound;

    [SerializeField]
    private AudioSource SoundSource;

    [SerializeField]
    private TL_UpdateNumber UpdateNumberScript;

    [SerializeField]
    private TL_CheckWinningCondition CheckWinningConditionScript;



    //Sets a new instance of the update number script
    public void SetNewUpdateNumberScript(TL_UpdateNumber NewUpdateNumberScript)
    {
        UpdateNumberScript = NewUpdateNumberScript;
    }

    //Returns the update number script
    public TL_UpdateNumber ReturnUpdateNumberBox()
    {
        return UpdateNumberScript;
    }

    //Checks if a number box is being dragged
    public bool IsNumberBoxCurrentlyDragging()
    {
        return IsNumberBeingDragged;
    }

    //Updates the current position with a new position
    public void UpdateNewCurrentPosition(Vector3 NewCurrentPosition)
    {
        CurrentPosition = NewCurrentPosition;
    }

    void Start()
    {
        MainCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        CheckWinningConditionScript = GameObject.Find("SudokuManager").GetComponent<TL_CheckWinningCondition>();
        MainCanvasGroup = GetComponent<CanvasGroup>();
        OriginalPosition = GetComponent<RectTransform>().anchoredPosition;
        CurrentPosition = OriginalPosition;
        NumberRectTransform = GetComponent<RectTransform>();
        SoundSource = GetComponent<AudioSource>();
}

    //Updates the previous position when the current position is changed
    public void UpdatePreviousPosition()
    {
        if (CurrentPosition != PreviousPosition)
        {
            PreviousPosition = CurrentPosition;
        }
    }

    void Update()
    {
        UpdatePreviousPosition();
    }

    public Vector3 ReturnPreviousPosition()
    {
        return PreviousPosition;
    }

    //Obtains the sibling index of the last created object to always display the selected number box first
    int ObtainLastCreatedObjectSiblingIndex()
    {
        GameObject[] Boxes = GameObject.FindGameObjectsWithTag("NumberBox");
        int LastSiblingIndexOfClone = SiblingIndex + Boxes.Length;
        return LastSiblingIndexOfClone;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //If the current number box is not being dragged
        if (!IsNumberBeingDragged)
        {
            //Change the sibling index so it's always in front of the other interactable UI elements
            transform.SetSiblingIndex(ObtainLastCreatedObjectSiblingIndex());
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Change the sibling index so it's always in front of the other interactable UI elements
        transform.SetSiblingIndex(ObtainLastCreatedObjectSiblingIndex());

        //Change the sound to the tile place sound
        ChangeSound(TilePickupSound);

        //Play the sound
        SoundSource.Play();

        //Set block raycasts to false
        MainCanvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        NumberRectTransform.anchoredPosition += eventData.delta / MainCanvas.scaleFactor;
        IsNumberBeingDragged = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Set bool to false
        IsNumberBeingDragged = false;

        //Set to block raycasts to true
        MainCanvasGroup.blocksRaycasts = true;

        //If  a duplicate number is found in either a row, column, or grid section
        if (CheckWinningConditionScript.ReturnIsDuplicateNumberFound() || CheckWinningConditionScript.ReturnIsDuplicateNumberIsFoundInGridSection())
        {
            //Change the sound to the error sound
            ChangeSound(TileWrongSound);
        }
        else
        {
            //Change the sound to the tile place sound
            ChangeSound(TilePlaceSound);
        }
        //Play the sound
        SoundSource.Play();

        //If this transform is the number selection
        if (transform.tag == "NumberSelection")
        {
            //Set position to its' original position
            NumberRectTransform.anchoredPosition = OriginalPosition;
        }

        if (eventData.pointerCurrentRaycast.gameObject == null && transform.tag == "NumberBox" ||
            eventData.pointerCurrentRaycast.gameObject != null && transform.tag == "NumberBox" &&
            eventData.pointerCurrentRaycast.gameObject.transform.tag == "NumberSelection")
        {
            //If the raycast of the current pointer returns null then destroy this gameobject
            Destroy(gameObject);
        }
        else if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            if (eventData.pointerCurrentRaycast.gameObject.transform.tag == "Box" &&
               eventData.pointerCurrentRaycast.gameObject.transform.GetComponent<TL_UpdateNumber>() == null ||
               eventData.pointerCurrentRaycast.gameObject.transform.tag == "NumberSelection" ||
               eventData.pointerCurrentRaycast.gameObject.transform.tag == "Background" ||
               eventData.pointerCurrentRaycast.gameObject.layer != LayerMask)
            {
                //If the transform is the number box and the raycast of the current pointer returns the box
                //then set position to its' original position
                NumberRectTransform.anchoredPosition = CurrentPosition;
            }
            else if (eventData.pointerCurrentRaycast.gameObject.transform.tag == "NumberBox")
            {
                //Change the sibling index so it's always in front of the other interactable UI elements
                eventData.pointerCurrentRaycast.gameObject.transform.SetSiblingIndex(ObtainLastCreatedObjectSiblingIndex());

                //Obtain the animator from the number box
                Animator NumberBoxAnimator = eventData.pointerCurrentRaycast.gameObject.GetComponent<Animator>();

                //Set the bool animation state to true
                NumberBoxAnimator.SetBool("Normal", true);
            }
        }
    }

    void ChangeSound(AudioClip NewSound)
    {
        SoundSource.clip = NewSound;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            //If the raycast of the current pointer returns the number selection and if this transform is the number box
            if (eventData.pointerDrag.gameObject.transform.tag == "NumberSelection" && transform.tag == "NumberBox")
            {
                //Obtain the text mesh from the number selection
                TextMeshProUGUI NumberSelectionTextMesh = eventData.pointerDrag.gameObject.GetComponentInChildren<TextMeshProUGUI>();

                //Obtain the text mesh from the child object of this object
                TextMeshProUGUI NumberTextMesh = GetComponentInChildren<TextMeshProUGUI>();

                //Set the number text to the number selection text
                NumberTextMesh.text = NumberSelectionTextMesh.text;

                //Set the new current number
                UpdateNumberScript.ReturnNumberClass().SetNewNumber(int.Parse(NumberTextMesh.text));

                //Set the new number in the array
                CheckWinningConditionScript.SetNewNumberInNumberArray(UpdateNumberScript.ReturnNumberClass().ReturnXPosition(), UpdateNumberScript.ReturnNumberClass().ReturnYPosition(), UpdateNumberScript.ReturnNumberClass().ReturnNumber());

                //Check if a grid section contains a duplicate number
                CheckWinningConditionScript.CheckIfDuplicateNumberIsFoundInGridSection(UpdateNumberScript.ReturnNumberClass().ReturnXPosition(), UpdateNumberScript.ReturnNumberClass().ReturnYPosition());

                //Check if the whole grid contains numbers
                CheckWinningConditionScript.CheckWholeGrid();

                //Check if the rows and columns contains a duplicate number or not
                CheckWinningConditionScript.CheckRowsAndColumnsForDuplicateNumber();

                //Check if the number array contains a duplicate number
                CheckWinningConditionScript.ChangeTextColourForDuplicateNumbers();

                //Check if the winning condition has been fulfilled
                CheckWinningConditionScript.CheckWinningCondition();
            }
        }
    }

}
