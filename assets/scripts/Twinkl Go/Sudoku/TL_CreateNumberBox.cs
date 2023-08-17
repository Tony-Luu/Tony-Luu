using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TL_CreateNumberBox : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private Canvas MainCanvas;

    [SerializeField]
    private GameObject NumberBox;

    [SerializeField]
    private string NumberText;

    [SerializeField]
    private int SiblingIndex;

    private RectTransform RectPosition;

    [SerializeField]
    private TL_CheckWinningCondition CheckWinningConditionScript;
    private TL_UpdateNumber UpdateNumberScript;



    void Start()
    {
        MainCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        RectPosition = GetComponent<RectTransform>();
        UpdateNumberScript = GetComponent<TL_UpdateNumber>();
        CheckWinningConditionScript = GameObject.Find("SudokuManager").GetComponent<TL_CheckWinningCondition>();
    }

    //Sets a new position, scale, and sibling index to the number object
    public void SetNewPositionToNumberObject(GameObject NumberObject)
    {
        NumberObject.transform.localPosition = RectPosition.localPosition;
        NumberObject.transform.localScale = new Vector3(1f, 1f, 0f);
        NumberObject.transform.SetSiblingIndex(SiblingIndex);
    }

    //Creates a number box on the transform
    public void CreateNumberBox()
    {
        //Instantiate the number box
        GameObject NumberBoxClone = Instantiate(NumberBox, Vector3.zero, Quaternion.identity);

        //Set the clone's parent to the canvas transform
        NumberBoxClone.transform.SetParent(MainCanvas.transform);

        //Set the new position to the number box clone
        SetNewPositionToNumberObject(NumberBoxClone);

        //Obtain the text mesh from the child of the number box
        TextMeshProUGUI NumberBoxTextMesh = NumberBoxClone.GetComponentInChildren<TextMeshProUGUI>();

        //Set the text of the number box test mesh to the new number text
        NumberBoxTextMesh.text = NumberText;

        //Set the number box to the clone
        UpdateNumberScript.SetNewNumberBox(NumberBoxClone);

        //Obtain the drag and drop object script from the number box clone
        TL_DragAndDropObject DragAndDropScript = NumberBoxClone.GetComponent<TL_DragAndDropObject>();

        //Set the new update number script to the number box clone
        DragAndDropScript.SetNewUpdateNumberScript(UpdateNumberScript);

        //Set a new number in the array based on the X and Y positions
        CheckWinningConditionScript.SetNewNumberInNumberArray(UpdateNumberScript.ReturnNumberClass().ReturnXPosition(), UpdateNumberScript.ReturnNumberClass().ReturnYPosition(), int.Parse(NumberText));

        //Check if a grid section contains a duplicate number
        CheckWinningConditionScript.CheckIfDuplicateNumberIsFoundInGridSection(UpdateNumberScript.ReturnNumberClass().ReturnXPosition(), UpdateNumberScript.ReturnNumberClass().ReturnYPosition());

        //Check if the whole grid contains numbers
        CheckWinningConditionScript.CheckWholeGrid();

        //Check if the rows and columns contains a duplicate number or not
        CheckWinningConditionScript.CheckRowsAndColumnsForDuplicateNumber();

        //Check if the number array contains a duplicate number
        CheckWinningConditionScript.ChangeTextColourForDuplicateNumbers();

        //Check if the winning condition if fulfilled or not
        CheckWinningConditionScript.CheckWinningCondition();
    }

    public void OnDrop(PointerEventData eventData)
    {
        //If the current dragged pointer is not null
        if (eventData.pointerDrag != null)
        {
            //If the current dragged pointer returning the number selection object
            if (eventData.pointerDrag.gameObject.transform.tag == "NumberSelection")
            {
                //Obtain the text from the text mesh component from the child object
                NumberText = eventData.pointerDrag.gameObject.GetComponentInChildren<TextMeshProUGUI>().text;

                //Create the number box
                CreateNumberBox();
            }
            else if (eventData.pointerDrag.gameObject.transform.tag == "NumberBox")
            {
                //Obtain the drag and drop script from the number box
                TL_DragAndDropObject DragAndDropObjectScript = eventData.pointerDrag.gameObject.GetComponent<TL_DragAndDropObject>();

                //Update the new current position for the number box
                DragAndDropObjectScript.UpdateNewCurrentPosition(RectPosition.anchoredPosition);

                //Set the position to this rect transform position
                eventData.pointerDrag.gameObject.GetComponent<RectTransform>().anchoredPosition = RectPosition.anchoredPosition;
            }
        }
    }

}
