using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TL_SwapNumberBox : MonoBehaviour, IDropHandler
{
    private RectTransform RectTransformBox;

    [SerializeField]
    private TL_CheckWinningCondition CheckWinningConditionScript;


    void Start()
    {
        RectTransformBox = GetComponent<RectTransform>();
        CheckWinningConditionScript = GameObject.Find("SudokuManager").GetComponent<TL_CheckWinningCondition>();
    }

    //Temporary stores one number for swapping and then swaps both numbers while updating the number array
    void SwapTwoNumbers(RectTransform FirstNumber, RectTransform SecondNumber)
    {
        //If the second number still exists
        if (SecondNumber != null)
        {
            //Obtain the text mesh from the child object of the first rect transform
            TextMeshProUGUI FirstNumberBoxTextMesh = FirstNumber.GetComponentInChildren<TextMeshProUGUI>();

            //Obtain the text mesh from the child object of the second rect transform
            TextMeshProUGUI SecondNumberBoxTextMesh = SecondNumber.GetComponentInChildren<TextMeshProUGUI>();

            //Temporary store the text from the first number box
            string TemporaryStoredNumberText = FirstNumberBoxTextMesh.text;

            //Set the text from the first number box to the text from the second number box
            FirstNumberBoxTextMesh.text = SecondNumberBoxTextMesh.text;

            //Set the text from the second number box to the temporary stored text
            SecondNumberBoxTextMesh.text = TemporaryStoredNumberText;

            //Obtain the drag and drop script from the first number box
            TL_DragAndDropObject FirstDragAndDropScript = FirstNumber.GetComponent<TL_DragAndDropObject>();

            //Set the position of the first number to the previous position
            FirstNumber.localPosition = FirstDragAndDropScript.ReturnPreviousPosition();

            //Obtain the drag and drop script from the second number box
            TL_DragAndDropObject SecondDragAndDropScript = SecondNumber.GetComponent<TL_DragAndDropObject>();

            //Swap two numbers from two number boxes and update both of their positions
            CheckWinningConditionScript.SwapNumbersInArray(FirstDragAndDropScript.ReturnUpdateNumberBox().ReturnNumberClass().ReturnXPosition(),
                FirstDragAndDropScript.ReturnUpdateNumberBox().ReturnNumberClass().ReturnYPosition(), FirstDragAndDropScript.ReturnUpdateNumberBox().ReturnNumberClass().ReturnNumber(),
                SecondDragAndDropScript.ReturnUpdateNumberBox().ReturnNumberClass().ReturnXPosition(), SecondDragAndDropScript.ReturnUpdateNumberBox().ReturnNumberClass().ReturnYPosition(), 
                SecondDragAndDropScript.ReturnUpdateNumberBox().ReturnNumberClass().ReturnNumber());

            //Check if the grid sections contains a duplicate number
            CheckWinningConditionScript.CheckIfDuplicateNumberIsFoundInGridSection(FirstDragAndDropScript.ReturnUpdateNumberBox().ReturnNumberClass().ReturnXPosition(),
                FirstDragAndDropScript.ReturnUpdateNumberBox().ReturnNumberClass().ReturnYPosition());

            //Check if the grid sections contains a duplicate number
            CheckWinningConditionScript.CheckIfDuplicateNumberIsFoundInGridSection(SecondDragAndDropScript.ReturnUpdateNumberBox().ReturnNumberClass().ReturnXPosition(),
                SecondDragAndDropScript.ReturnUpdateNumberBox().ReturnNumberClass().ReturnYPosition());

            //Check if the whole grid contains numbers
            CheckWinningConditionScript.CheckWholeGrid();

            //Check if the rows and columns contains a duplicate number or not
            CheckWinningConditionScript.CheckRowsAndColumnsForDuplicateNumber();

            //Check if the number array contains a duplicate number
            CheckWinningConditionScript.ChangeTextColourForDuplicateNumbers();

            //Check if the winning condition has been fulfilled
            CheckWinningConditionScript.CheckWinningCondition();

            //Set a new current number for the first number box
            FirstDragAndDropScript.ReturnUpdateNumberBox().ReturnNumberClass().SetNewNumber(int.Parse(FirstNumberBoxTextMesh.text));

            //Set a new current number for the second number box
            SecondDragAndDropScript.ReturnUpdateNumberBox().ReturnNumberClass().SetNewNumber(int.Parse(SecondNumberBoxTextMesh.text));
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.gameObject.transform.tag == "NumberBox")
            {
                SwapTwoNumbers(eventData.pointerDrag.gameObject.GetComponent<RectTransform>(), RectTransformBox);
            }
        }
    }

}
