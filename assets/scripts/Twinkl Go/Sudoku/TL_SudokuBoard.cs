using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TL_SudokuBoard : MonoBehaviour
{
    [SerializeField]
    private Canvas SceneCanvas;

    [SerializeField]
    private GameObject Box;

    [SerializeField]
    private Vector2 StartingPosition;

    [SerializeField]
    private float PositionDifference;

    [SerializeField]
    private NumberClass[,] NumberArray = new NumberClass[4, 4];

    [SerializeField]
    private int AmountOfNumbers;
    private List<int> NumberList = new List<int>();



    public NumberClass ReturnNumberClassInNumberArray(int x, int y)
    {
        return NumberArray[x, y];
    }

    public int ReturnNumberInNumberArray(int x, int y)
    {
        return NumberArray[x, y].ReturnNumber();
    }

    public NumberClass[,] ReturnNumberArray()
    {
        return NumberArray;
    }

    void Awake()
    {
        InitializeSudokuGrid();
        AddNumbersInList();
        PlaceRandomNumbersInGrid();
        SolveSudokuGrid();
        RemoveNumbersFromGrid();
        RemoveScripts();
    }

    //Setup the sudoku board in the scene
    void InitializeSudokuGrid()
    {
        //Set a current spawning position for the boxes
        Vector2 CurrentSpawningPosition = StartingPosition;

        //Set a sibling index to prevent UI from overlapping each other
        int CurrentSiblingIndex = 2;

        //Loop through the array
        for (int y = 0; y < NumberArray.GetLength(1); y++)
        {
            for (int x = 0; x < NumberArray.GetLength(0); x++)
            {
                //Spawn a box on the scene with the gameobject array referencing the box
                GameObject BoxClone = Instantiate(Box, Vector2.zero, Quaternion.identity);

                //Obtain the number class from the box clone
                NumberArray[x, y] = BoxClone.GetComponent<NumberClass>();

                //Set the gameobject as the box clone
                NumberArray[x, y].SetNewBox(BoxClone);

                //Set new X position
                NumberArray[x, y].SetNewXPosition(x);

                //Set new Y position
                NumberArray[x, y].SetNewYPosition(y);

                //Set the spawned box's parent
                NumberArray[x, y].ReturnBox().transform.SetParent(SceneCanvas.transform);

                //Set the box's position, scale, and sibling index
                NumberArray[x, y].ReturnBox().transform.localPosition = CurrentSpawningPosition;
                NumberArray[x, y].ReturnBox().transform.localScale = new Vector2(1f, 1f);
                NumberArray[x, y].ReturnBox().transform.SetSiblingIndex(CurrentSiblingIndex);

                //If X equals to 1 then change the position difference to space out the mini-grid sections
                if (x == 1)
                {
                    PositionDifference = -153f;
                }
                else
                {
                    PositionDifference = -146f;
                }
                //Subtract X of the current spawn position with the positoin difference to space out boxes
                CurrentSpawningPosition.x -= PositionDifference;

                //Increment the sibling index
                CurrentSiblingIndex++;
            }
            //If Y equals to 1 then change the position difference to space out the mini-grid sections
            if (y == 1)
            {
                PositionDifference = -153f;
            }
            else
            {
                PositionDifference = -146f;
            }
            //Subtract Y of the current spawn position with the positoin difference to space out boxes
            CurrentSpawningPosition.y -= PositionDifference;

            //Reset X of the current spawning position
            CurrentSpawningPosition.x = StartingPosition.x;
        }
    }

    void AddNumbersInList()
    {
        for (int i = 1; i <= 4; i++)
        {
            NumberList.Add(i);
        }
    }

    //Returns a random number from a list
    int ReturnRandomNumber()
    {
        int Index = Random.Range(0, NumberList.Count);
        int RandomNumberFromList = NumberList[Index];
        return RandomNumberFromList;
    }

    //Randomly place numbers in the grid
    void PlaceRandomNumbersInGrid()
    {
        //While there are still numbers in the list
        while (NumberList.Count > 0)
        {
            //Square root the length of the array
            int SquareRootNumber = (int)Mathf.Sqrt(NumberArray.GetLength(0));

            //Randomize X and Y
            int RandomX = Random.Range(0, NumberArray.GetLength(0));
            int RandomY = Random.Range(0, NumberArray.GetLength(1));

            //Calculate the square root X and Y
            int RandomXSquared = RandomX - RandomX % SquareRootNumber;
            int RandomYSquared = RandomY - RandomY % SquareRootNumber;

            //Set the returned random number
            int RandomNumber = ReturnRandomNumber();

            //If a valid position and an empty space is found in the array
            if (IsPositionValidToPlaceNumber(RandomXSquared, RandomYSquared, RandomNumber) && NumberArray[RandomXSquared, RandomYSquared].ReturnNumber() == 0)
            {
                //Set the number in the array
                NumberArray[RandomXSquared, RandomYSquared].SetNewNumber(RandomNumber);

                //Remove the number from the list
                NumberList.Remove(RandomNumber);
            }
        }
    }

    //Checks each row, column, and grid section to see if the position is valid to place a number
    bool IsPositionValidToPlaceNumber(int Row, int Column, int Number)
    {
        //Loop through the column
        for (int y = 0; y < NumberArray.GetLength(1); y++)
        {
            //If the position in the array contains the same number, return false
            if (NumberArray[Row, y].ReturnNumber() == Number)
            {
                return false;
            }
        }

        //Loop through the row
        for (int x = 0; x < NumberArray.GetLength(0); x++)
        {
            //If the position in the array contains the same number, return false
            if (NumberArray[x, Column].ReturnNumber() == Number)
            {
                return false;
            }
        }

        //Calculate the square root of the array length
        int SquareRootNumber = (int)Mathf.Sqrt(NumberArray.GetLength(0));

        //Calculate starting row and column
        int StartingRow = Row - Row % SquareRootNumber;
        int StartingColumn = Column - Column % SquareRootNumber;

        //Loop through the starting row and column
        for (int i = StartingRow; i < StartingRow + SquareRootNumber; i++)
        {
            for (int j = StartingColumn; j < StartingColumn + SquareRootNumber; j++)
            {
                //If the position in the array contains the same number, return false
                if (NumberArray[i, j].ReturnNumber() == Number)
                {
                    return false;
                }
            }
        }
        //If it goes through all of the for loops unbroken then return true
        return true;
    }

    //Sudoku algorithm to check if the current grid can be solved or not
    bool CanThisSudokuGridBeSolved(NumberClass[,] NumberGrid)
    {
        //Loop through the array
        for (int y = 0; y < NumberGrid.GetLength(1); y++)
        {
            for (int x = 0; x < NumberGrid.GetLength(0); x++)
            {
                //If the grid is currently empty
                if (NumberGrid[x, y].ReturnNumber() == 0)
                {
                    //Loop through the numbers
                    for (int n = 1; n < 5; n++)
                    {
                        //If a position is valid to place a number
                        if (IsPositionValidToPlaceNumber(x, y, n))
                        {
                            //Set the number into the grid array
                            NumberGrid[x, y].SetNewNumber(n);

                            //If the current sudoku grid can be solved then return true
                            if (CanThisSudokuGridBeSolved(NumberGrid))
                            {
                                return true;
                            }
                            else
                            {
                                //Otherwise, set the number in the grid array to 0
                                NumberGrid[x, y].SetNewNumber(0);
                            }
                        }
                    }
                    //Return false if there are no valid numbers
                    return false;
                }
            }
        }
        //Return true if the algorithm goes through the for loops unbroken
        return true;
    }

    //Checks if the sudoku grid can be solved or not
    void SolveSudokuGrid()
    {
        //If the current sudoku grid can be solved
        if (CanThisSudokuGridBeSolved(NumberArray))
        {
            //Loop through the array
            for (int y = 0; y < NumberArray.GetLength(1); y++)
            {
                for (int x = 0; x < NumberArray.GetLength(0); x++)
                {
                    //If the positions in the array contains a number
                    if (NumberArray[x, y].ReturnNumber() != 0)
                    {
                        //Obtain the text mesh from the child object
                        TextMeshProUGUI NumberTextMesh = NumberArray[x, y].ReturnBox().GetComponentInChildren<TextMeshProUGUI>();

                        //Set the text as the number
                        NumberTextMesh.text = NumberArray[x, y].ReturnNumber().ToString();
                    }
                }
            }
            Debug.Log("Grid is possible to win!");
        }
        else
        {
            Debug.Log("Grid is impossible to win!");
        }
    }

    //Randomly remove numbers from the grid
    void RemoveNumbersFromGrid()
    {
        //While there are still numbers to remove
        while (AmountOfNumbers > 0)
        {
            //Rnadomize X and Y positions
            int RandomX = Random.Range(0, NumberArray.GetLength(0));
            int RandomY = Random.Range(0, NumberArray.GetLength(1));

            //If the randomized positions in the array contains a number
            if (NumberArray[RandomX, RandomY].ReturnNumber() != 0)
            {
                //Set the value to 0
                NumberArray[RandomX, RandomY].SetNewNumber(0);

                //Obtain the text mesh from the child rect transform
                TextMeshProUGUI NumberTextMesh = NumberArray[RandomX, RandomY].ReturnBox().GetComponentInChildren<TextMeshProUGUI>();

                //Set the text to blank
                NumberTextMesh.text = "";

                //Decrement the amount of numbers
                AmountOfNumbers--;
            }
        }
    }

    //Removes scripts from boxes that contains numbers
    void RemoveScripts()
    {
        //Loop through the array
        for (int y = 0; y < NumberArray.GetLength(1); y++)
        {
            for (int x = 0; x < NumberArray.GetLength(0); x++)
            {
                //If the number in the array is not 0
                if (NumberArray[x, y].ReturnNumber() != 0)
                {
                    //Destroy the scripts
                    Destroy(NumberArray[x, y].ReturnBox().GetComponent<TL_CreateNumberBox>());
                    Destroy(NumberArray[x, y].ReturnBox().GetComponent<TL_UpdateNumber>());
                }
            }
        }
    }

}
