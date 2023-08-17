using UnityEngine;
using TMPro;

public class CheckAllQuestions : MonoBehaviour
{
    [SerializeField] private ScriptableObject TeacherIncompleteQuestionsResponse;
    [SerializeField] private MultiTextPanel MultiTextPanelScript;
    [SerializeField] private ScoreManager ScoreManagerScript;


    void Start()
    {
        ScoreManagerScript = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    //Checks if all of the questions have been answered
    public void CheckIfAllQuestionsAreAnswered()
    {
        //Add up the total of correct and wrong answers
        int TotalQuestions = ScoreManagerScript.ReturnTotalCorrectAnswers() + ScoreManagerScript.ReturnTotalWrongAnswers();

        //If all questions have been answered
        if (TotalQuestions == 12)
        {
            TextMeshProUGUI TextTitleComponent = MultiTextPanelScript.transform.Find("Background/Title").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI TextContentComponent = MultiTextPanelScript.transform.Find("Background/Content").GetComponent<TextMeshProUGUI>();

            //If the player has answered all questions wrong
            if (ScoreManagerScript.ReturnTotalCorrectAnswers() == 0)
            {
                TextTitleComponent.text = "Oh No!";
                TextContentComponent.text = "You have answered no questions correctly!";
            }
            else
            {
                TextTitleComponent.text = "Well Done!";
                TextContentComponent.text = "You have sucessfully answered " + ScoreManagerScript.ReturnTotalCorrectAnswers() + " out of " + TotalQuestions + " correctly!" + "\n\n" +
                        "You have answered the following questions correctly: ";

                string CorrectQuestionNumbers = null;

                //Loop through the list
                for (int i = 0; i < ScoreManagerScript.ReturnCorrectQuestionNumbersList().Count; i++)
                {
                    //Add the question numbers that were answered correctly in a string
                    CorrectQuestionNumbers += ScoreManagerScript.ReturnCorrectQuestionNumbersList()[i] + ", ";
                }
                //Add the string from the correct question numbers and remove the comma and space from it...
                TextContentComponent.text += CorrectQuestionNumbers.Remove(CorrectQuestionNumbers.Length - 2);

                //...add the following text afterwards
                TextContentComponent.text += "\n\n" + "Would you like to try again?" + "\n\n" + "Click on the Pause button and click on Quit to start again.";
            }
        }
        else
        {
            MultiTextPanelScript.UpdateContent((PageCollection)TeacherIncompleteQuestionsResponse);
        }
    }

}
