using UnityEngine;

public class ChangeGameSpeed : MonoBehaviour
{
    [SerializeField] private float Distance;
    [SerializeField] private float TempoInterval;
    [SerializeField] private float TempoCooldown;
    [SerializeField] private float ObjectSpeedIncrement;
    [SerializeField] private float BackgroundSpeedIncrement;
    [SerializeField] private float xPositionDifference;
    [SerializeField] private bool HasGameSpeedChanged;
    [SerializeField] private GameObject TutorialText;



    void Start()
    {
        //Add the interval to the cooldown with the time to prevent mistimings during the game
        TempoCooldown = TempoInterval + Time.time;
    }

    //Changes the game tempo based on the time interval
    void ChangeGameTempo()
    {
        //If the cooldown of the tempo is less than the time
        if (TempoCooldown < Time.time)
        {
            //Loop through the pooled objects
            foreach (GameObject Item in ObjectPool.CurrentInstance.ReturnPooledObjects())
            {
                //Obtain the move objects script
                MoveObject MoveObjectScript = Item.GetComponent<MoveObject>();

                //If the script exists in the object, increase the object's speed
                if (MoveObjectScript != null)
                {
                    MoveObjectScript.AddNewMoveSpeed(ObjectSpeedIncrement);
                }

                //Obtain the move backgrounds script
                MoveBackground MoveBackgroundScript = Item.GetComponent<MoveBackground>();

                //If the script exists in the object, increase the background's speed
                if (MoveBackgroundScript != null)
                {
                    MoveBackgroundScript.AddNewSpeed(BackgroundSpeedIncrement);
                }
            }
            //Add the interval to the cooldown with the time
            TempoCooldown = TempoInterval + Time.time;
        }
    }

    void FixedUpdate()
    {
        ChangeGameTempo();
    }

}
