using UnityEngine;

public class TL_CharacterMove : MonoBehaviour
{
    public float MoveSpeed;
    private float xAxisInput;
    private float yFlipRotation;
    private float AnimationSpeed;
    private Rigidbody2D CharacterRigidbody;
    private TL_AnimationManager AnimationManagerScript;
    private TL_EdgeDetection EdgeDetectionScript;


    void Start()
    {
        DontDestroyOnLoad(gameObject);
        CharacterRigidbody = GetComponent<Rigidbody2D>();
        EdgeDetectionScript = GetComponent<TL_EdgeDetection>();
        AnimationManagerScript = GetComponent<TL_AnimationManager>();
        FlipCharacter(1f);
    }

    //Flips the gameobject to the opposite side
    void FlipCharacter(float InputValue)
    {
        //If the input value is towards the left
        if (InputValue < 0)
        {
            //Set the Y rotation to 0
            yFlipRotation = 0f;
        }
        else if (InputValue > 0)    //If the input value is towards the right
        {
            //Set the Y rotation to 180
            yFlipRotation = 180f;
        }

        //If the current animation state is either idle or walking while grabbing
        if (AnimationManagerScript.ReturnCharacterAnimator().GetCurrentAnimatorStateInfo(0).IsName("IdleGrab") ||
            AnimationManagerScript.ReturnCharacterAnimator().GetCurrentAnimatorStateInfo(0).IsName("WalkGrab"))
        {
            //Lock the Y axis
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
        }
        else
        {
            //Set the Y axis to the float variable
            transform.eulerAngles = new Vector3(0f, yFlipRotation, 0f);
        }
    }

    //Flips the grab walking animation based on the direction of the player
    void FlipGrabWalkingAnimation(float CurrentFacing, float InputAxis)
    {
        //If the current animation is the grab walking animation
        if (AnimationManagerScript.ReturnCharacterAnimator().GetCurrentAnimatorStateInfo(0).IsName("WalkGrab"))
        {
            //If the player is facing left
            if (CurrentFacing == 0f)
            {
                //If the input value is towards the left
                if (InputAxis < 0)
                {
                    //Set the animation speed to a negative value to reverse the animation
                    AnimationSpeed = -1f;
                }
                else if (InputAxis > 0)    //If the input value is towards the right
                {
                    //Set the animation speed to a positive value to play the animation normally
                    AnimationSpeed = 1f;
                }
            }
            else if (CurrentFacing == 180f)     //If the player is facing right
            {
                //If the input value is towards the left
                if (InputAxis < 0)
                {
                    //Set the animation speed to a positive value to play the animation normally
                    AnimationSpeed = 1f;
                }
                else if (InputAxis > 0)    //If the input value is towards the right
                {
                    //Set the animation speed to a negative value to reverse the animation
                    AnimationSpeed = -1f;
                    
                }
            }
            //Set the float parameter based on the value of the animation speed
            AnimationManagerScript.ReturnCharacterAnimator().SetFloat("ReverseWalk_Grab", AnimationSpeed);
        }
    }

    void MoveCharacter()
    {
        //Store the value of the horizontal X input
        xAxisInput = Input.GetAxis("Horizontal");

        //If the player is grabbing an object
        if (AnimationManagerScript.ReturnCharacterAnimator().GetCurrentAnimatorStateInfo(0).IsName("WalkGrab"))
        {
            //Check all sides of the player and the grabbed object colliders
            bool IsPlayerApproachingLeftPlatformEdge = xAxisInput < 0 && !EdgeDetectionScript.HasPlayerLeftColliderNotReachedPlatformEdge;
            bool IsPlayerApproachingRightPlatformEdge = xAxisInput > 0 && !EdgeDetectionScript.HasPlayerRightColliderNotReachedPlatformEdge;
            bool IsGrabbedObjectApproachingLeftPlatformEdge = xAxisInput < 0 && !EdgeDetectionScript.HasLeftSideColliderNotReachedPlatformEdge;
            bool IsGrabbedObjectApproachingRightPlatformEdge = xAxisInput > 0 && !EdgeDetectionScript.HasRightSideColliderNotReachedPlatformEdge;

            //If the player attempts to move the grabbed object off any edge of a platform
            if (IsPlayerApproachingLeftPlatformEdge || IsPlayerApproachingRightPlatformEdge ||
                IsGrabbedObjectApproachingLeftPlatformEdge || IsGrabbedObjectApproachingRightPlatformEdge)
            {
                //Set the X axis input to 0 to prevent moving off an edge of a platform
                xAxisInput = 0f;
            }
        }

        //Store the default value of the Y velocity from the rigidbody
        float yAxis = CharacterRigidbody.velocity.y;

        //Flip the character sprite to reflect the direction of the character
        FlipCharacter(xAxisInput);

        //Flip the grab walking animation based on the direction of the character
        FlipGrabWalkingAnimation(transform.eulerAngles.y, xAxisInput);

        //Create a vector2 variable to store the X and Y axis
        Vector2 MovePosition = new Vector2(xAxisInput * MoveSpeed, yAxis);

        //Change the velocity of the rigidbody based on the horizontal input
        CharacterRigidbody.velocity = MovePosition;
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

}
