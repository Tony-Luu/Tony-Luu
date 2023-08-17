using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class TL_CheckWinningCondition : MonoBehaviour
{
    public GameObject TextDisplay;
    public GameObject WinMessage;

    [SerializeField]
    private TL_SudokuBoard SudokuBoardScript;

    [Header("Booleans for checking win conditions")]
    [SerializeField]
    private bool IsGridFull;

    [SerializeField]
    private bool IsDuplicateNumberFound;

    [SerializeField]
    private bool IsDuplicateNumberFoundInGridSection;

    [SerializeField]
    private bool HasWinningConditionBeenFulfilled;

        

    public bool ReturnIsDuplicateNumberFound()
    {
        return IsDuplicateNumberFound;
    }

    public bool ReturnIsDuplicateNumberIsFoundInGridSection()
    {
        return IsDuplicateNumberFoundInGridSection;
    }

    //Change the text colour in the number box
    void ChangeNumberBoxTextColour(Color NewColor, int X, int Y)
    {
        SudokuBoardScript.ReturnNumberClassInNumberArray(X, Y).SetNewTextColour(NewColor);
    }

    //Change text colour from the row and column
    void ChangeTextColourInRowAndColumn(Color NewColor, int Row, int Column)
    {
        SudokuBoardScript.ReturnNumberClassInNumberArray(Row, Column).SetNewTextColour(NewColor);

        GameObject[] Boxes = GameObject.FindGameObjectsWithTag("NumberBox");

        foreach (GameObject NumberBox in Boxes)
        {
            TL_DragAndDropObject DragAndDropScript = NumberBox.GetComponent<TL_DragAndDropObject>();

            if (DragAndDropScript.ReturnUpdateNumberBox().ReturnNumberClass().ReturnXPosition() == Row && DragAndDropScript.ReturnUpdateNumberBox().ReturnNumberClass().ReturnYPosition() == Column)
            {
                TextMeshProUGUI NumberTextMesh = NumberBox.GetComponentInChildren<TextMeshProUGUI>();
                NumberTextMesh.color = NewColor;
            }
        }

    }

    //Check the entire column for duplicate numbers from the starting row
    void CheckColumnForDuplicateNumbers(int Row)
    {
        //Create a new integer list to find duplicates
        List<int> DistinctNumbersInColumn = new List<int>();

        //Loop through the columns
        for (int Column = 0; Column < SudokuBoardScript.ReturnNumberArray().GetLength(1); Column++)
        {
            //If the number in the array contains a number
            if (SudokuBoardScript.ReturnNumberInNumberArray(Row, Column) != 0)
            {
                //Add the number into the list
                DistinctNumbersInColumn.Add(SudokuBoardScript.ReturnNumberInNumberArray(Row, Column));
            }
        }
        //Sort out the list with only duplicate numbers
        DistinctNumbersInColumn = DistinctNumbersInColumn.GroupBy(x => x).Where(y => y.Count() > 1).Select(z => z.Key).ToList();

        //Loop through the columns
        for (int Column = 0; Column < SudokuBoardScript.ReturnNumberArray().GetLength(1); Column++)
        {
            //If the number in the array contains a number
            if (DistinctNumbersInColumn.Contains(SudokuBoardScript.ReturnNumberInNumberArray(Row, Column)))
            {
                //Change the number box text colour to red
                ChangeNumberBoxTextColour(Color.red, Row, Column);

                //Change the text colour to red
                ChangeTextColourInRowAndColumn(Color.red, Row, Column);
            }
        }
        //Clear the list to prepare for the next iteration
        DistinctNumbersInColumn.Clear();
    }

    //Check the entire row for duplicate numbers from the starting column
    void CheckRowForDuplicateNumbers(int Column)
    {
        //Create a new integer list to find duplicates
        List<int> DistinctNumbersInRow = new List<int>();

        //Loop through the rows
        for (int Row = 0; Row < SudokuBoardScript.ReturnNumberArray().GetLength(0); Row++)
        {
            //If the number in the array contains a number
            if (SudokuBoardScript.ReturnNumberInNumberArray(Row, Column) != 0)
            {
                //Add the number into the list
                DistinctNumbersInRow.Add(SudokuBoardScript.ReturnNumberInNumberArray(Row, Column));
            }
        }
        //Sort out the list with only duplicate numbers
        DistinctNumbersInRow = DistinctNumbersInRow.GroupBy(x => x).Where(y => y.Count() > 1).Select(z => z.Key).ToList();

        //Loop through the rows
        for (int Row = 0; Row < SudokuBoardScript.ReturnNumberArray().GetLength(0); Row++)
        {
            //If the number in the array contains a number
            if (DistinctNumbersInRow.Contains(SudokuBoardScript.ReturnNumberInNumberArray(Row, Column)))
            {
                //Change the number box text colour to red
                ChangeNumberBoxTextColour(Color.red, Row, Column);

                //Change the text colour to red
                ChangeTextColourInRowAndColumn(Color.red, Row, Column);
            }
        }
        //Clear the list to prepare for the next iteration
        DistinctNumbersInRow.Clear();
    }

    //Check the entire grid in the array for duplicate numbers to change text colour
    public void ChangeTextColourForDuplicateNumbers()
    {
        //Loop through the array
        for (int y = 0; y < SudokuBoardScript.ReturnNumberArray().GetLength(1); y++)
        {
            for (int x = 0; x < SudokuBoardScript.ReturnNumberArray().GetLength(0); x++)
            {
                //If the number in the array is not 0
                if (SudokuBoardScript.ReturnNumberInNumberArray(x, y) != 0)
                {
                    //Change the number box text colour to black
                    ChangeNumberBoxTextColour(Color.black, x, y);

                    //Change the text colour to black
                    ChangeTextColourInRowAndColumn(Color.black, x, y);
                }
            }
        }

        //Loop through the array
        for (int y = 0; y < SudokuBoardScript.ReturnNumberArray().GetLength(1); y++)
        {
            for (int x = 0; x < SudokuBoardScript.ReturnNumberArray().GetLength(0); x++)
            {
                //If the number in the array is not 0
                if (SudokuBoardScript.ReturnNumberInNumberArray(x, y) != 0)
                {
                    //Check the row, column, and grid section for duplicate numbers
                    CheckRowForDuplicateNumbers(y);
                    CheckColumnForDuplicateNumbers(x);
                    CheckGridSectionForDuplicateNumbers(x, y);
                }
            }
        }
    }

    //Check the grid section for duplicate numbers from the starting X and Y position
    void CheckGridSectionForDuplicateNumbers(int x, int y)
    {
        //Create a new integer list to find duplicates
        List<int> DistinctNumbersInGridSection = new List<int>();

        //Calculate the square root of the array length
        int SquareRootNumber = (int)Mathf.Sqrt(SudokuBoardScript.ReturnNumberArray().GetLength(0));

        //Calculate starting row and column
        int StartingRow = x - x % SquareRootNumber;
        int StartingColumn = y - y % SquareRootNumber;

        //Loop through the starting row and column
        for (int i = StartingRow; i < StartingRow + SquareRootNumber; i++)
        {
            for (int j = StartingColumn; j < StartingColumn + SquareRootNumber; j++)
            {
                //If the number in the array contains a number
                if (SudokuBoardScript.ReturnNumberInNumberArray(i, j) != 0)
                {
                    //Add the number into the list
                    DistinctNumbersInGridSection.Add(SudokuBoardScript.ReturnNumberInNumberArray(i, j));
                }
            }
        }
        //Sort out the list with only duplicate numbers
        DistinctNumbersInGridSection = DistinctNumbersInGridSection.GroupBy(a => a).Where(b => b.Count() > 1).Select(c => c.Key).ToList();

        //Loop through the starting row and column
        for (int i = StartingRow; i < StartingRow + SquareRootNumber; i++)
        {
            for (int j = StartingColumn; j < StartingColumn + SquareRootNumber; j++)
            {
                //If the number in the array contains a number
                if (DistinctNumbersInGridSection.Contains(SudokuBoardScript.ReturnNumberInNumberArray(i, j)))
                {
                    //Change the number box text colour to red
                    ChangeNumberBoxTextColour(Color.red, i, j);

                    //Change the text colour to red
                    ChangeTextColourInRowAndColumn(Color.red, i, j);
                }
            }
        }
        //Clear the list to prepare for the next iteration
        DistinctNumbersInGridSection.Clear();
    }

    public void CheckIfDuplicateNumberIsFoundInGridSection(int Row, int Column)
    {
        IsDuplicateNumberFoundInGridSection = DoesGridSectionContainDuplicateNumber(Row, Column);
    }

    //Checks if a grid section contains a duplicate number
    bool DoesGridSectionContainDuplicateNumber(int Row, int Column)
    {
        //Create a new list for numbers
        List<int> NumberList = new List<int>();

        //Calculate the square root of the array length
        int SquareRootNumber = (int)Mathf.Sqrt(SudokuBoardScript.ReturnNumberArray().GetLength(0));

        //Calculate starting row and column
        int StartingRow = Row - Row % SquareRootNumber;
        int StartingColumn = Column - Column % SquareRootNumber;

        //Loop through the starting row and column
        for (int x = StartingRow; x < StartingRow + SquareRootNumber; x++)
        {
            for (int y = StartingColumn; y < StartingColumn + SquareRootNumber; y++)
            {
                //If there's a number in the array
                if (SudokuBoardScript.ReturnNumberInNumberArray(x, y) != 0)
                {
                    //If the number doesn't exist in the array
                    if (!NumberList.Contains(SudokuBoardScript.ReturnNumberInNumberArray(x, y)))
                    {
                        //Add the number in the list
                        NumberList.Add(SudokuBoardScript.ReturnNumberInNumberArray(x, y));
                    }
                    else
                    {
                        //Otherwise, return true
                        return true;
                    }
                }
            }
        }
        return false;
    }

    //Disables mouse interactions with the boxes
    void DisableInteractionWithBoxes()
    {
        //Loop through the array
        for (int x = 0; x < SudokuBoardScript.ReturnNumberArray().GetLength(0); x++)
        {
            for (int y = 0; y < SudokuBoardScript.ReturnNumberArray().GetLength(1); y++)
            {
                //Obtain the canvas group
                CanvasGroup BoxCanvasGroup = SudokuBoardScript.ReturnNumberClassInNumberArray(x, y).GetComponent<CanvasGroup>();

                //Set interactable and block raycasts to false to prevent mouse interaction
                BoxCanvasGroup.interactable = false;
                BoxCanvasGroup.blocksRaycasts = false;
            }
        }

        //Find all number selections with tag
        GameObject[] NumberSelections = GameObject.FindGameObjectsWithTag("NumberSelection");

        //Loop through all number selections
        foreach (GameObject NumberSelector in NumberSelections)
        {
            //Obtain the canvas group
            CanvasGroup NumberSelectorCanvasGroup = NumberSelector.GetComponent<CanvasGroup>();

            //Set interactable and block raycasts to false to prevent mouse interaction
            NumberSelectorCanvasGroup.interactable = false;
            NumberSelectorCanvasGroup.blocksRaycasts = false;
        }
    }

    //Searches the entire grid to check if it's filled with numbers or not
    bool IsWholeGridFilledWithNumbers()
    {
        for (int x = 0; x < SudokuBoardScript.ReturnNumberArray().GetLength(0); x++)
        {
            for (int y = 0; y < SudokuBoardScript.ReturnNumberArray().GetLength(1); y++)
            {
                if (SudokuBoardScript.ReturnNumberArray()[x, y].ReturnNumber() == 0)
                {
                    return false;
                }
            }
        }
        return true;
    }

    //Check if the grid is filled with numbers
    public void CheckWholeGrid()
    {
        IsGridFull = IsWholeGridFilledWithNumbers();
    }

    //Checks if the winning condition is fulfilled or not
    public void CheckWinningCondition()
    {
        HasWinningConditionBeenFulfilled = IsGridFull && !IsDuplicateNumberFound && !IsDuplicateNumberFoundInGridSection;

        //If the winning condition has been met then show the winning message
        if (HasWinningConditionBeenFulfilled)
        {
            DisableInteractionWithBoxes();
            WinMessage.SetActive(true);
        }
    }

    //Checks if the rows and columns contains a duplicate number
    public void CheckRowsAndColumnsForDuplicateNumber()
    {
        IsDuplicateNumberFound = DoesArrayContainDuplicateNumbers();
    }

    //Loops through the rows and columns for duplicate numbers
    bool DoesArrayContainDuplicateNumbers()
    {
        //Set X and Y default values to loop through the rows and columns
        int X = 0;
        int Y = 0;

        //Create lists to detect if there's a duplicate number in rows and columns
        List<int> DuplicateNumbersInRow = new List<int>();
        List<int> DuplicateNumbersInColumn = new List<int>();

        //While Y is less than or equal to the length of the number array
        while (Y <= SudokuBoardScript.ReturnNumberArray().GetLength(1) - 1)
        {
            //Loop through the rows
            for (int Row = 0; Row < SudokuBoardScript.ReturnNumberArray().GetLength(0); Row++)
            {
                //If the number in the array contains a number
                if (SudokuBoardScript.ReturnNumberInNumberArray(Row, Y) != 0)
                {
                    //If the list contains a duplicate number then return true
                    if (DuplicateNumbersInRow.Contains(SudokuBoardScript.ReturnNumberInNumberArray(Row, Y)))
                    {
                        return true;
                    }
                    else
                    {
                        //If not, then add the number
                        DuplicateNumbersInRow.Add(SudokuBoardScript.ReturnNumberInNumberArray(Row, Y));
                    }
                }
            }
            //Clear the list
            DuplicateNumbersInRow.Clear();

            //Increment the Y value to move onto the next row
            Y++;
        }

        //While X is less than or equal to the length of the number array
        while (X <= SudokuBoardScript.ReturnNumberArray().GetLength(0) - 1)
        {
            //Loop through the columns
            for (int Column = 0; Column < SudokuBoardScript.ReturnNumberArray().GetLength(1); Column++)
            {
                //If the number in the array contains a number
                if (SudokuBoardScript.ReturnNumberInNumberArray(X, Column) != 0)
                {
                    //If the list contains a duplicate number then return true
                    if (DuplicateNumbersInColumn.Contains(SudokuBoardScript.ReturnNumberInNumberArray(X, Column)))
                    {
                        return true;
                    }
                    else
                    {
                        //If not, then add the number
                        DuplicateNumbersInColumn.Add(SudokuBoardScript.ReturnNumberInNumberArray(X, Column));
                    }
                }
            }
            //Clear the list
            DuplicateNumbersInColumn.Clear();

            //Increment the X value to move onto the next column
            X++;
        }
        return false;
    }

    //Swaps two numbers in the array with both X and Y positions from two swapped numbers
    public void SwapNumbersInArray(int xPosition1, int yPosition1, int FirstNumber, int xPosition2, int yPosition2, int SecondNumber)
    {
        //Temporary store the first number
        int TemporaryStoredNumber = FirstNumber;

        //Set the second number into the first number's position
        SudokuBoardScript.ReturnNumberClassInNumberArray(xPosition1, yPosition1).SetNewNumber(SecondNumber);

        //Set the temporary stored number into the second number's position
        SudokuBoardScript.ReturnNumberClassInNumberArray(xPosition2, yPosition2).SetNewNumber(TemporaryStoredNumber);
    }

    void Start()
    {
        SudokuBoardScript = GetComponent<TL_SudokuBoard>();
    }

    void Update()
    {
        DisplayDuplicateNumberMessage();
    }

    //Displays the message when the duplicate number has been found
    void DisplayDuplicateNumberMessage()
    {
        //If a duplicate number is found either in the row, column, or grid sections
        if (IsDuplicateNumberFound || IsDuplicateNumberFoundInGridSection)
        {
            //Activate the text
            TextDisplay.SetActive(true);
        }
        else
        {
            //Otherwise, deactivate the text
            TextDisplay.SetActive(false);
        }
    }

    //Sets a new number in the array based on the X and Y values
    public void SetNewNumberInNumberArray(int X, int Y, int NewNumber)
    {
        //If the number in the array is not the same
        if (SudokuBoardScript.ReturnNumberInNumberArray(X, Y) != NewNumber)
        {
            //Set the new number
            SudokuBoardScript.ReturnNumberClassInNumberArray(X, Y).SetNewNumber(NewNumber);
        }
    }

}
