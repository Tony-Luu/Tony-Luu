using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int Score;
    [SerializeField] private TMP_Text ScoreDisplay;



    int ReturnScore()
    {
        return Score;
    }

    void AddScore(int AddScore)
    {
        Score += AddScore;
        if (Score > 12)
        {
            Score = 12;
        }
    }

    void UpdateScore()
    {
        ScoreDisplay.text = Score.ToString() + "/12";
    }

    void Start()
    {
        UpdateScore();
        GameObserver.IncreaseScore = AddScore;
        GameObserver.UpdateScoreText = UpdateScore;
        GameObserver.ReturnScore = ReturnScore;
    }

}
