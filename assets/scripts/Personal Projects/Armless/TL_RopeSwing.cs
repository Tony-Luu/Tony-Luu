using UnityEngine;

public class TL_RopeSwing : MonoBehaviour
{
    public float SwingForce;
    private Rigidbody PlayerRigidbody;
    private TL_MoveCharacter MoveCharacterScript;


    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();
        MoveCharacterScript = GetComponent<TL_MoveCharacter>();
    }

    //Input control for the player to swing on the rope vertically
    void SwingRope()
    {
        //Sets the float variable to the get raw axis input
        float VerticalInput = Input.GetAxisRaw("Vertical");

        //Adds forward force to the player
        PlayerRigidbody.AddForce(transform.forward * VerticalInput * SwingForce, ForceMode.Acceleration);
    }

    //Removes the hinge joint from the player's arms
    void RemoveHingeJointFromArms()
    {
        //For each of the arms on the player
        foreach (Transform Child in transform)
        {
            //Obtain the hinge joint component
            HingeJoint ChildHingeJoint = Child.GetComponent<HingeJoint>();

            //If the arms have the hinge joint component then destroy it
            if (ChildHingeJoint != null)
            {
                Destroy(ChildHingeJoint);
            }
        }
    }

    //Makes the player jump off the arm rope
    void JumpOffRope()
    {
        //If the player presses Spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Obtain the hinge joint component and destroy it
            Destroy(GetComponent<HingeJoint>());

            //Remove the hinge joint from the arms
            RemoveHingeJointFromArms();

            //Re-enable the move character script
            MoveCharacterScript.enabled = true;

            //Disable this script
            enabled = false;
        }
    }

    void Update()
    {
        JumpOffRope();
    }

    void FixedUpdate()
    {
        SwingRope();
    }

}
