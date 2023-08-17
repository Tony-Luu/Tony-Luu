using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TL_UpdateNumber : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private GameObject CurrentNumberBox;

    [SerializeField]
    private NumberClass NumberClassScript;

    [SerializeField]
    private TL_DragAndDropObject DragAndDropScript;

    [SerializeField]
    private TL_CheckWinningCondition CheckWinningConditionScript;

    private TextMeshProUGUI NumberTextMesh;



    public void SetNewNumberBox(GameObject NewNumberBox)
    {
        CurrentNumberBox = NewNumberBox;
    }

    public GameObject ReturnNumberBox()
    {
        return CurrentNumberBox;
    }

    public NumberClass ReturnNumberClass()
    {
        return NumberClassScript;
    }

    //Checks if the number box exists or not
    void CheckNumberBox()
    {
        //If the number box doesn't exist and the current number is not 0
        if (CurrentNumberBox == null && ReturnNumberClass().ReturnNumber() != 0)
        {
            //Update the current number to 0
            ReturnNumberClass().SetNewNumber(0);

            //Set the number in the array to 0
            CheckWinningConditionScript.SetNewNumberInNumberArray(ReturnNumberClass().ReturnXPosition(), ReturnNumberClass().ReturnYPosition(), 0);

            //Check if a grid section contains a duplicate number
            CheckWinningConditionScript.CheckIfDuplicateNumberIsFoundInGridSection(ReturnNumberClass().ReturnXPosition(), ReturnNumberClass().ReturnYPosition());

            //Check if the whole grid contains numbers
            CheckWinningConditionScript.CheckWholeGrid();

            //Check if the number array contains a duplicate number
            CheckWinningConditionScript.CheckRowsAndColumnsForDuplicateNumber();

            //Check if the number array contains a duplicate number
            CheckWinningConditionScript.ChangeTextColourForDuplicateNumbers();

            //Check if the winning condition has been fulfilled
            CheckWinningConditionScript.CheckWinningCondition();
        }
    }

    void Start()
    {
        NumberClassScript = GetComponent<NumberClass>();
        CheckWinningConditionScript = GameObject.Find("SudokuManager").GetComponent<TL_CheckWinningCondition>();
    }

    void Update()
    {
        CheckNumberBox();
    }

    //Updates a number from the previous position
    void UpdateNumberInPreviousPosition(int PreviousXPosition, int PreviousYPosition)
    {
        //Set 0 in the array with the previous positions
        CheckWinningConditionScript.SetNewNumberInNumberArray(PreviousXPosition, PreviousYPosition, 0);

        //Check if a grid section contains a duplicate number in the previous position
        CheckWinningConditionScript.CheckIfDuplicateNumberIsFoundInGridSection(PreviousXPosition, PreviousYPosition);

        //Set the new current number in the new current positions
        CheckWinningConditionScript.SetNewNumberInNumberArray(ReturnNumberClass().ReturnXPosition(), ReturnNumberClass().ReturnYPosition(), ReturnNumberClass().ReturnNumber());

        //Check if a grid section contains a duplicate number in the current position
        CheckWinningConditionScript.CheckIfDuplicateNumberIsFoundInGridSection(ReturnNumberClass().ReturnXPosition(), ReturnNumberClass().ReturnYPosition());

        //Check if the whole grid contains numbers
        CheckWinningConditionScript.CheckWholeGrid();

        //Check if the rows and columns contains a duplicate number or not
        CheckWinningConditionScript.CheckRowsAndColumnsForDuplicateNumber();

        //Check if the number array contains a duplicate number
        CheckWinningConditionScript.ChangeTextColourForDuplicateNumbers();

        //Check if the winning condition has been fulfilled
        CheckWinningConditionScript.CheckWinningCondition();
    }

    public void OnDrop(PointerEventData eventData)
    {
        //If the pointer drag contains a gameobject
        if (eventData.pointerDrag != null)
        {
            //If the pointer drag returns the number selection object
            if (eventData.pointerDrag.gameObject.transform.tag == "NumberSelection")
            {
                //Obtain the text mesh from the child object
                NumberTextMesh = eventData.pointerDrag.gameObject.GetComponentInChildren<TextMeshProUGUI>();

                //Set the new current number from the text mesh
                ReturnNumberClass().SetNewNumber(int.Parse(NumberTextMesh.text));
                
                //Set a new number in the array with new positions
                CheckWinningConditionScript.SetNewNumberInNumberArray(ReturnNumberClass().ReturnXPosition(), ReturnNumberClass().ReturnYPosition(), ReturnNumberClass().ReturnNumber());

                //Check if a grid section contains a duplicate number
                CheckWinningConditionScript.CheckIfDuplicateNumberIsFoundInGridSection(ReturnNumberClass().ReturnXPosition(), ReturnNumberClass().ReturnYPosition());

                //Check if the whole grid contains numbers
                CheckWinningConditionScript.CheckWholeGrid();

                //Check if the rows and columns contains a duplicate number or not
                CheckWinningConditionScript.CheckRowsAndColumnsForDuplicateNumber();

                //Check if the number array contains a duplicate number
                CheckWinningConditionScript.ChangeTextColourForDuplicateNumbers();
            }
            else if (eventData.pointerDrag.gameObject.transform.tag == "NumberBox")
            {
                //Set the current number box
                CurrentNumberBox = eventData.pointerDrag.gameObject;

                //Obtain the drag and drop script from the current number box
                DragAndDropScript = CurrentNumberBox.GetComponent<TL_DragAndDropObject>();

                //Obtain the text mesh from the child object
                NumberTextMesh = eventData.pointerDrag.gameObject.GetComponentInChildren<TextMeshProUGUI>();

                //Set the new current number from the text mesh
                ReturnNumberClass().SetNewNumber(int.Parse(NumberTextMesh.text));

                //If the update number script from the number box is not the same as this
                if (DragAndDropScript.ReturnUpdateNumberBox() != this)
                {
                    //Update the number from the previous position
                    UpdateNumberInPreviousPosition(DragAndDropScript.ReturnUpdateNumberBox().ReturnNumberClass().ReturnXPosition(), DragAndDropScript.ReturnUpdateNumberBox().ReturnNumberClass().ReturnYPosition());

                    //Return the previous number box and set the current number to 0
                    DragAndDropScript.ReturnUpdateNumberBox().ReturnNumberClass().SetNewNumber(0);

                    //Set a new update number script
                    DragAndDropScript.SetNewUpdateNumberScript(GetComponent<TL_UpdateNumber>());
                }
                //Check if a grid section contains a duplicate number
                CheckWinningConditionScript.CheckIfDuplicateNumberIsFoundInGridSection(ReturnNumberClass().ReturnXPosition(), ReturnNumberClass().ReturnYPosition());

                //Check if the whole grid contains numbers
                CheckWinningConditionScript.CheckWholeGrid();

                //Check if the rows and columns contains a duplicate number or not
                CheckWinningConditionScript.CheckRowsAndColumnsForDuplicateNumber();

                //Check if the number array contains a duplicate number
                CheckWinningConditionScript.ChangeTextColourForDuplicateNumbers();
            }
        }
    }

}
