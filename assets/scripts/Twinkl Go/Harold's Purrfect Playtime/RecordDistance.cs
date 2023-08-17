using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecordDistance : MonoBehaviour
{
    [SerializeField] private float CurrentDistance;
    [SerializeField] private TextMeshProUGUI DistanceText;
    [SerializeField] private GameObject TutorialText;
    [SerializeField] private GameObject EndScreen;
    [SerializeField] private GameObject Harold;


    void Start()
    {
        DistanceText = GetComponent<TextMeshProUGUI>();
        Harold = GameObject.FindGameObjectWithTag("Harold");
    }

    void DisplayDistance()
    {
        if (TutorialText == null && Harold != null)
        {
            //Set the current distance as the time since level load
            CurrentDistance = Time.timeSinceLevelLoad;

            //Dispaly the distance as text without any decimals
            DistanceText.text = "Distance: " + CurrentDistance.ToString("F0");
        }
    }

    void DisplayGameOverText()
    {
        if (EndScreen.activeInHierarchy)
        {
            //Obtain the text component
            Text GameOverText = EndScreen.transform.GetChild(0).GetChild(1).GetComponent<Text>();

            //Set the text to display the distance travelled and prompt the player to try again
            GameOverText.text = "Score: " + CurrentDistance.ToString("F0") +
                "\n\n Want to try again? Click on the round arrow to play again or click the home button to return to the title screen.";

            //Resets all background and object speed to default
            ResetAllSpeedToDefault();
        }
    }

    //Resets all of the object and background speed to default
    void ResetAllSpeedToDefault()
    {
        //Loop through all of the objects in the pool
        foreach (ObjectPoolItem Item in ObjectPool.CurrentInstance.ItemPool)
        {
            //Obtain the move object script from the object
            MoveObject MoveObjectScript = Item.ObjectToPool.GetComponent<MoveObject>();

            //If the object has the script, reset the speed
            if (MoveObjectScript != null)
            {
                MoveObjectScript.ResetSpeed();
            }
            
            //Obtain the move background script from the object
            MoveBackground MoveBackgroundScript = Item.ObjectToPool.GetComponent<MoveBackground>();

            //If the object has the script, reset the speed
            if (MoveBackgroundScript != null)
            {
                MoveBackgroundScript.ResetSpeed();
            }
        }
    }

    void FixedUpdate()
    {
        DisplayDistance();
        DisplayGameOverText();
    }

}
