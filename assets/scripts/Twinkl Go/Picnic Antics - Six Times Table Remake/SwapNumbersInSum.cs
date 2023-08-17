using UnityEngine;
using TMPro;

public class SwapNumbersInSum : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI SumDisplay;


    //Swap the numbers from the six tiems table sum
    public void SwapNumbers()
    {
        //Store the text from the sum as a char array
        char[] OldSumText = SumDisplay.text.ToCharArray();

        //Loop through the char array
        for (int i = 0; i < SumDisplay.text.Length; i++)
        {
            //Set the text to be 6 if the char is ? and set text to ? if the char is 6
            OldSumText[i] = (OldSumText[i] == '?') ? '6' : (OldSumText[i] == '6') ? '?' : OldSumText[i];
        }

        //Set the new text as a string from the char array
        SumDisplay.text = new string(OldSumText);

        //Set the string as the new sum display text into the playerprefs 
        PlayerPrefs.SetString("SwapNumbers", SumDisplay.text);
    }

    void Start()
    {
        SumDisplay = GetComponentInChildren<TextMeshProUGUI>();
        SumDisplay.text = PlayerPrefs.GetString("SwapNumbers", "? × 6");
    }

}
