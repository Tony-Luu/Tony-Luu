using UnityEngine;

public class TL_ArmMovement : MonoBehaviour
{
    public GameObject LeftArm;
    public GameObject RightArm;
    public Animator LeftArmAnimator;
    public Animator RightArmAnimator;


    //Animate the player's arms
    void MoveArms()
    {
        //If the player clicks on the left mouse button and the animator for the arm exists
        if (Input.GetMouseButtonDown(0) && LeftArmAnimator != null)
        {
            //Set the animation trigger for the left arm
            LeftArmAnimator.SetTrigger("MoveLeftArm");
        }

        //If the player clicks on the right mouse button and the animator for the arm exists
        if (Input.GetMouseButtonDown(1) && RightArmAnimator != null)
        {
            //Set the animation trigger for the right arm
            RightArmAnimator.SetTrigger("MoveRightArm");
        }
    }

    //Remove the player's arms
    void RemoveArms()
    {
        //If the left or right arm exists but they are not parented then set the gameobject and animator variables to null
        if (LeftArm != null && LeftArm.transform.parent == null)
        {
            LeftArm = null;
            LeftArmAnimator = null;
        }
        else if (RightArm != null && RightArm.transform.parent == null)
        {
            RightArm = null;
            RightArmAnimator = null;
        }
    }

    void Update()
    {
        MoveArms();
        RemoveArms();
    }

}
