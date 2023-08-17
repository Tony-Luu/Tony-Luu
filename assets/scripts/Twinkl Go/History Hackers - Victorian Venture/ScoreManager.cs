using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private bool[] HasQuestionBeenAnsweredCorrectly;
    [SerializeField] private List<int> CorrectQuestionNumbers;
    [SerializeField] private List<int> IncorrectQuestionNumbers;
    [SerializeField] private int TotalCorrectAnswers;
    [SerializeField] private int TotalWrongAnswers;


    public int ReturnTotalWrongAnswers()
    {
        return TotalWrongAnswers;
    }

    public int ReturnTotalCorrectAnswers()
    {
        return TotalCorrectAnswers;
    }

    public List<int> ReturnIncorrectQuestionNumbers()
    {
        return IncorrectQuestionNumbers;
    }

    public List<int> ReturnCorrectQuestionNumbersList()
    {
        return CorrectQuestionNumbers;
    }

    public void AddQuestionNumberToList(int QuestionNumber)
    {
        //Add the offset of 1 because the array for the questions starts at 0
        int Offset = QuestionNumber + 1;

        if (HasQuestionBeenAnsweredCorrectly[QuestionNumber])
        {
            CorrectQuestionNumbers.Add(Offset);
        }
        else
        {
            IncorrectQuestionNumbers.Add(Offset);
        }
    }

    public void AddAnsweredQuestionToArray(bool AnsweredQuestion, int ArrayIndex)
    {
        HasQuestionBeenAnsweredCorrectly[ArrayIndex] = AnsweredQuestion;
    }

    public void AddTotalWrongAnswers(int AddTotal)
    {
        TotalWrongAnswers += AddTotal;
    }

    public void AddTotalCorrectAnswers(int AddTotal)
    {
        TotalCorrectAnswers += AddTotal;
    }

    void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(gameObject);
        }
    }
}
