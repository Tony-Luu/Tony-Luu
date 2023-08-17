using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private int Score;
    private int PreviousScore;

    [SerializeField]
    private TextMeshProUGUI ScoreDisplay;
    


    public void AddNewScore(int NewScore)
    {
        if (Score < 12)
        {
            Score += NewScore;
        }
        else
        {
            Score = 12;
        }
    }

    public int ReturnScore()
    {
        return Score;
    }

    public bool HasScoreChanged()
    {
        return Score != PreviousScore;
    }

    void Start()
    {
        PreviousScore = Score;
    }

    //Updates the previous score when the current score is changed
    void UpdateScore()
    {
        if (Score != PreviousScore)
        {
            ScoreDisplay.text = "Score | " + Score.ToString();
            PreviousScore = Score;
        }
    }

    void Update()
    {
        UpdateScore();
    }

}
