using UnityEngine;

public class TL_ArmFollow : MonoBehaviour
{
    private Vector3 Offset;
    private Vector3 OriginalPosition;
    private GameObject Player;
    

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        //Save the original position of the player's arm
        OriginalPosition = transform.position;

        //Calculate the offset for the arm
        Offset = transform.position - Player.transform.position;
    }
    
    //Returns the offset
    public Vector3 ReturnOffset()
    {
        return Offset;
    }

    //Returns the original position
    public Vector3 ReturnOriginalPosition()
    {
        return OriginalPosition;
    }

}
