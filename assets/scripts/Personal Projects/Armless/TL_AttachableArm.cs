using UnityEngine;

public class TL_AttachableArm : MonoBehaviour
{
    private Animator ArmAnimator;
    private Rigidbody ArmRigidbody;
    private Rigidbody PlayerRigidbody;
    private BoxCollider ArmCollider;
    private GameObject Player;
    private AudioSource ArmPopSound;
    private TL_MoveCharacter MoveCharacterScript;
    private TL_RopeSwing RopeSwingScript;
    private TL_ArmFollow ArmFollowScript;

        
    void Start()
    {
        ArmAnimator = GetComponent<Animator>();
        ArmCollider = GetComponent<BoxCollider>();
        ArmRigidbody = GetComponent<Rigidbody>();
        ArmPopSound = GetComponent<AudioSource>();        

        Player = GameObject.FindGameObjectWithTag("Player");        
        PlayerRigidbody = Player.GetComponent<Rigidbody>();
        MoveCharacterScript = Player.GetComponent<TL_MoveCharacter>();
        RopeSwingScript = Player.GetComponent<TL_RopeSwing>();
        ArmFollowScript = GetComponent<TL_ArmFollow>();
    }

    //Enable the arm collider for the animation
    public void EnableArmCollider()
    {
        ArmCollider.enabled = true;
    }

    //Disable the arm collider for the animation
    public void DisableArmCollider()
    {
        ArmCollider.enabled = false;
    }

    //Sets the parent for the attachable arms
    void SetParentForAttachableArm(Transform Parent)
    {
        //Destroy the arm follow script and animator component
        Destroy(ArmFollowScript);
        Destroy(ArmAnimator);

        //Set the parent for this gameobject
        transform.SetParent(Parent);
    }

    //Sets the anchor for the hinge joint
    void SetHingeJointAnchor(HingeJoint HingeAnchor, Vector3 AnchorPosition, Vector3 ConnectedAnchorPosition)
    {
        HingeAnchor.autoConfigureConnectedAnchor = false;
        HingeAnchor.anchor = AnchorPosition;
        HingeAnchor.connectedAnchor = ConnectedAnchorPosition;
    }

    //Check if the joint is attached to a hinge joint
    bool IsJointAttachedToHingeJoint(Rigidbody Joint)
    {
        //Searches for all of the grabbable joints
        GameObject[] GrabbableJoints = GameObject.FindGameObjectsWithTag("Grabbable");
                
        foreach (GameObject GrabbedObject in GrabbableJoints)
        {
            //If the grabbed object contains the hinge joint component, return true...
            if (GrabbedObject.GetComponent<HingeJoint>() != null)
            {
                Rigidbody GrabbedJoint = GrabbedObject.GetComponent<HingeJoint>().connectedBody;
                if (GrabbedJoint == Joint)
                {
                    return true;
                }
            }
        }
        //...if not then return false
        return false;
    }

    //Sets the limits of the hinge joints
    void SetHingeJointLimits(HingeJoint HingeAnchor)
    {
        //Obtain the limits of the hinge joint
        JointLimits HingeLimit = HingeAnchor.limits;

        //Set the values of the limits
        HingeLimit.min = 5f;
        HingeLimit.max = 40f;
        HingeAnchor.useLimits = true;
        HingeAnchor.limits = HingeLimit;
    }

    void OnCollisionEnter(Collision collision)
    {
        //If the arm touches the wall and the arms are still parented to the player character
        if (collision.transform.CompareTag("Wall") && transform.parent != null)
        {
            //Play the arm pop sound
            ArmPopSound.Play();

            //Unchild the arm
            SetParentForAttachableArm(null);

            //Set both the rigidbody of the player and the arm to 0
            PlayerRigidbody.velocity = Vector3.zero;
            ArmRigidbody.velocity = Vector3.zero;

            //Freeze the axis of the arm
            ArmRigidbody.constraints = RigidbodyConstraints.FreezeAll;

            //Set Kinematic to true
            ArmRigidbody.isKinematic = true;

            //Set the size of the arm's collider
            ArmCollider.size = new Vector3(1.5f, ArmCollider.size.y, ArmCollider.size.z);
        }

        //If the arm touches a grabbable object
        if (collision.transform.CompareTag("Grabbable"))
        {
            //Obtain the hinge joint from the collided object
            HingeJoint GrabbableHingeJoint = collision.gameObject.GetComponent<HingeJoint>();

            //If the grabbed object doesn't have a hinge joint
            if (GetComponent<HingeJoint>() == null)
            {
                //Add the hinge joint component to the arm
                HingeJoint ArmHingeJoint = gameObject.AddComponent<HingeJoint>();

                //Obtain the rigidbody from the collided gameobject
                Rigidbody JointRigidbody = collision.gameObject.GetComponent<Rigidbody>();

                //If the collided gameobject doesn't have a hinge joint
                if (!IsJointAttachedToHingeJoint(JointRigidbody))
                {
                    //Play the arm pop sound
                    ArmPopSound.Play();

                    //Unchild the arm
                    SetParentForAttachableArm(null);

                    //Set the rotation to 0
                    transform.localRotation = Quaternion.identity;

                    //Set both the rigidbody of the player and the arm to 0
                    ArmRigidbody.velocity = Vector3.zero;
                    PlayerRigidbody.velocity = Vector3.zero;

                    //Set the gravity to true
                    ArmRigidbody.useGravity = true;

                    //Set the connected body to the collided gameobject
                    ArmHingeJoint.connectedBody = JointRigidbody;

                    //Set the new anchor for the arm
                    SetHingeJointAnchor(ArmHingeJoint, new Vector3(0f, 1f, 0f), new Vector3(0f, -1.5f, 0f));

                    //Set the new limits of the hinge joint to the arm
                    SetHingeJointLimits(ArmHingeJoint);
                }
                else
                {
                    //Add the hinge joint to the player character
                    HingeJoint PlayerHingeJoint = transform.parent.gameObject.AddComponent<HingeJoint>();

                    //Set the connected body to the collided gameobject
                    PlayerHingeJoint.connectedBody = JointRigidbody;

                    //Set the new anchor for the player character
                    SetHingeJointAnchor(PlayerHingeJoint, new Vector3(0f, 0f, 0f), new Vector3(-2.5f, -2f, 5f));

                    //Disable the move character script
                    MoveCharacterScript.enabled = false;

                    //Enable the rope swing script
                    RopeSwingScript.enabled = true;
                }
            }

            //If the grabbed hinge joint is null then disable the collider
            if (GrabbableHingeJoint == null)
            {
                collision.collider.enabled = false;
            }

            //Set this gameobject tag to Grabbable
            transform.tag = "Grabbable";
        }
    }

}
