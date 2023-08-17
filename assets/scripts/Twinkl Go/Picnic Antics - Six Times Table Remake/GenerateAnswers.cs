using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerateAnswers : MonoBehaviour
{
    [SerializeField] private List<int> UsedNumbers = new List<int>();
    [SerializeField] private TextMeshProUGUI[] AnswerDisplay = new TextMeshProUGUI[4];
    [SerializeField] private TextMeshProUGUI SumDisplay;
    [SerializeField] private int RandomNumber;
    [SerializeField] private int GeneratedAnswer;


    TextMeshProUGUI ReturnSumDisplayObject()
    {
        return SumDisplay;
    }

    List<int> ReturnUsedNumbersList()
    {
        return UsedNumbers;
    }

    int ReturnGeneratedAnswer()
    {
        return GeneratedAnswer;
    }

    //Randomize a sum from the six times tables
    void RandomizeSum()
    {
        //If the list count is less than 12
        if (UsedNumbers.Count < 12)
        {
            //Randomize a unique number
            RandomNumber = UniqueRandomNumberGenerator.ReturnRandomValue(1, 13);

            //While the randomized number is in a list
            while (UsedNumbers.Contains(RandomNumber))
            {
                //Re-randomize a unique number
                RandomNumber = UniqueRandomNumberGenerator.ReturnRandomValue(1, 13);
            }

            //Add the number in a list
            UsedNumbers.Add(RandomNumber);

            //Calculate the generated answer
            GeneratedAnswer = RandomNumber * 6;
        }        
    }

    //Displays the correct answer in a random food item
    void DisplayAnswer()
    {
        //Return a randomized number
        int RandomIndex = UniqueRandomNumberGenerator.ReturnRandomValue(0, AnswerDisplay.Length);

        //Display the number as a string in the text component
        AnswerDisplay[RandomIndex].text = GeneratedAnswer.ToString();
    }

    //Displays random numbers in the food items
    void DisplayRandomNumbers()
    {
        for (int i = 0; i < AnswerDisplay.Length; i++)
        {
            if (AnswerDisplay[i].text == "")
            {
                int RandomNumber = UniqueRandomNumberGenerator.ReturnRandomValue(1, 101);

                while (RandomNumber % 6 == 0)
                {
                    RandomNumber = UniqueRandomNumberGenerator.ReturnRandomValue(1, 101);
                }

                AnswerDisplay[i].text = RandomNumber.ToString();
            }
        }
    }

    public void DisplaySum()
    {
        string SwappedNumbersText = PlayerPrefs.GetString("SwapNumbers", "? × 6");
        SumDisplay.text = SwappedNumbersText.Replace("?", RandomNumber.ToString());
    }

    void ResetDisplayedNumbers()
    {
        for (int i = 0; i < AnswerDisplay.Length; i++)
        {
            AnswerDisplay[i].text = "";
        }
    }

    public void ReinitializeAnswers()
    {
        ResetDisplayedNumbers();
        RandomizeSum();
        DisplayAnswer();
        DisplayRandomNumbers();
        DisplaySum();
    }

    void Awake()
    {
        RandomizeSum();
        DisplayAnswer();
        DisplayRandomNumbers();
        DisplaySum();
        GameObserver.ReturnList = ReturnUsedNumbersList;
        GameObserver.CheckAnswer = ReturnGeneratedAnswer;
        GameObserver.ReturnSumDisplay = ReturnSumDisplayObject;
        GameObserver.ReinitalizeDisplayedNumbers = ReinitializeAnswers;
    }

}
