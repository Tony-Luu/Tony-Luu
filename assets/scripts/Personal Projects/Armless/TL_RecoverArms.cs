using UnityEngine;

public class TL_RecoverArms : MonoBehaviour
{
    public GameObject LeftArmPrefab;
    public GameObject RightArmPrefab;
    public GameObject Player;
    private TL_ArmMovement ArmMovementScript;


    void Start()
    {
        ArmMovementScript = Player.GetComponent<TL_ArmMovement>();
    }

    void ObtainDetachableArms()
    {
        //If the player presses E and the distance is less than 2 between the player and the gameobject
        if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(Player.transform.position, transform.position) < 2f)
        {
            //Obtain the arm movement script from the player
            TL_ArmMovement ArmScript = Player.GetComponent<TL_ArmMovement>();

            //If the player doesn't have a left arm
            if (ArmScript.LeftArm == null)
            {
                //Set the quaternion's euler angles
                Quaternion LeftArmRotation = Quaternion.identity;
                LeftArmRotation.eulerAngles = new Vector3(180f, 0f, 0f);

                //Instantiate the left arm
                GameObject LeftArmClone = Instantiate(LeftArmPrefab, new Vector3(-0.642f, 0.067f, 0.167f), LeftArmRotation);

                //Parent the left arm to the player
                LeftArmClone.transform.SetParent(Player.transform);

                //Set the new left arm
                ArmMovementScript.LeftArm = LeftArmClone;

                //Obtain the new animator from the left arm
                ArmMovementScript.LeftArmAnimator = LeftArmClone.GetComponent<Animator>();
            }

            //If the player doesn't have a right arm
            if (ArmScript.RightArm == null)
            {
                //Set the quaternion's euler angles
                Quaternion RightArmRotation = Quaternion.identity;
                RightArmRotation.eulerAngles = new Vector3(0f, 0f, 180f);

                //Instantiate the right arm
                GameObject RightArmClone = Instantiate(RightArmPrefab, new Vector3(0.642f, 0.067f, 0.167f), RightArmRotation);

                //Parent the right arm to the player
                RightArmClone.transform.SetParent(Player.transform);

                //Set the new right arm
                ArmMovementScript.RightArm = RightArmClone;

                //Obtain the new animator from the right arm
                ArmMovementScript.RightArmAnimator = RightArmClone.GetComponent<Animator>();
            }
        }
    }

    void Update()
    {
        ObtainDetachableArms();
    }

}
