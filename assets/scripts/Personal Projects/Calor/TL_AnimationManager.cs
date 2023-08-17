using UnityEngine;

public class TL_AnimationManager : MonoBehaviour
{
    public float PrepareJumpStateTimer = 0.1f;
    public float LandStateTimer = 0.1f;
    public float SwingTorchTimer = 0.3f;
    private Animator CharacterAnimator;
    private TL_Torch TorchScript;
    private TL_CharacterMove MoveScript;
    private TL_CharacterJump JumpScript;
    private TO_CharacterGrabBlock GrabScript;

    public enum CharacterState
    {
        Idle, Walk, PrepareJump, Jump, Fall, Land, IdleGrab, WalkGrab, SwingTorch
    }
    public CharacterState CurrentState;


    void Awake()
    {
        CurrentState = CharacterState.Idle;
        TorchScript = GetComponent<TL_Torch>();
        MoveScript = GetComponent<TL_CharacterMove>();
        JumpScript = GetComponent<TL_CharacterJump>();        
        GrabScript = GetComponent<TO_CharacterGrabBlock>();
        CharacterAnimator = GetComponent<Animator>();
    }

    //Return the character animator
    public Animator ReturnCharacterAnimator()
    {
        return CharacterAnimator;
    }

    //Return the current state
    public CharacterState ReturnCurrentState()
    {
        return CurrentState;
    }

    //Changes the current state
    public void ChangeCurrentState(CharacterState NewState)
    {
        //Change the integer based on the new state
        CharacterAnimator.SetInteger("CurrentState", (int)NewState);

        //Set the current state to the new state
        CurrentState = NewState;
    }

    //Toggles the moving and jumping scripts when the player uses a torch
    void ToggleMovingScript()
    {
        if (CurrentState == CharacterState.SwingTorch)
        {
            MoveScript.enabled = false;
            JumpScript.enabled = false;
            GrabScript.enabled = false;
        }
        else
        {
            MoveScript.enabled = true;
            JumpScript.enabled = true;
            GrabScript.enabled = true;
        }
    }

    //Controls the moving state inside the animator 
    void AnimateWalking(bool IsWalking)
    {
        //If the character is on the ground
        if (JumpScript.HasCharacterLanded())
        {
            //If the character is walking
            if (IsWalking)
            {
                //Change current state to walk
                ChangeCurrentState(CharacterState.Walk);
            }
            else    //If the character is not walking
            {
                //Change current state to idle
                ChangeCurrentState(CharacterState.Idle);
            }
        }
    }

    //Animate the jumping animation
    void AnimateJumping()
    {
        //If the jump button is pressed and the current state is not prepare jump
        if (Input.GetKey(KeyCode.Space) && CurrentState != CharacterState.PrepareJump)
        {
            //Change the state to prepare jump
            ChangeCurrentState(CharacterState.PrepareJump);
        }
    }

    //Checks if the condition of playing the falling state are active
    void TriggerFallState()
    {
        //If the character's velocity are less than 0
        if (CharacterAnimator.GetFloat("VelocityY") < 0f)
        {
            //Change the state to fall
            ChangeCurrentState(CharacterState.Fall);
        }
    }

    //Checks if the character is grabbing an object or not
    void TriggerGrabState(bool IsGrabbingObject)
    {
        //Has the character grabbed an object?
        if (IsGrabbingObject)
        {
            //Change the state to idle grab
            ChangeCurrentState(CharacterState.IdleGrab);
        }
    }

    //Checks if the character is moving or not while grabbing an object
    void TriggerWalkGrabStates(bool IsGrabbing, bool IsWalking)
    {
        //If the character has grabbed an object
        if (IsGrabbing)
        {
            //If the character is walking
            if (IsWalking)
            {
                //Change the current state to walkgrab
                ChangeCurrentState(CharacterState.WalkGrab);
            }
            else
            {
                //Change the current state to idlegrab
                ChangeCurrentState(CharacterState.IdleGrab);
            }
        }
        else    //If the character has not grabbed an object
        {
            //Change the current state to idle
            ChangeCurrentState(CharacterState.Idle);
        }
    }

    //Changes the current state based on a timer
    void TransitionToNewStateWithTimer(float Timer, CharacterState NewState)
    {
        //When the timer reaches to 0
        if (Timer <= 0f)
        {
            ChangeCurrentState(NewState);
        }
    }

    //A Finite State Machine to manage all of the animation states
    void AnimationFiniteStateMachine()
    {
        //Is the character walking?
        bool IsCharacterWalking = Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0;

        //Has the character grabbed an object?
        bool HasCharacterGrabbedObject = Input.GetKey(KeyCode.G) && GrabScript.ReturnGrabbedBox() != null;

        //If the current state is idle or walking
        if (CurrentState == CharacterState.Idle || CurrentState == CharacterState.Walk)
        {
            //Set the swing torch timer
            SwingTorchTimer = 0.3f;

            //Animate the walking state if the character is walking
            AnimateWalking(IsCharacterWalking);

            //Animate jumping if the character has pressed the jump button
            AnimateJumping();

            //Trigger the fall state if the character's velocity is less than 0
            TriggerFallState();

            //Trigger the grabbed state if the character has grabbed an object
            TriggerGrabState(HasCharacterGrabbedObject);
        }

        switch (CurrentState)
        {
            case CharacterState.PrepareJump:
                //Decrease the prepare jump timer
                PrepareJumpStateTimer -= Time.deltaTime;

                //Transition to the jump state when the timer reaches 0
                TransitionToNewStateWithTimer(PrepareJumpStateTimer, CharacterState.Jump);
                break;

            case CharacterState.Jump:
                //Set the prepare jump state timer
                PrepareJumpStateTimer = 0.1f;

                //Set the Y velocity to the float variable for the animator
                CharacterAnimator.SetFloat("VelocityY", JumpScript.PlayerRigidbody().velocity.y);

                //Trigger the fall state if the character's velocity is less than 0
                TriggerFallState();
                break;

            case CharacterState.Fall:
                //Set the land state timer
                LandStateTimer = 0.1f;

                //If the character has touched the ground
                if (CharacterAnimator.GetBool("IsGrounded"))
                {
                    //Change the state to land
                    ChangeCurrentState(CharacterState.Land);
                }
                break;

            case CharacterState.Land:
                //Animate jumping if the character has pressed the jump button
                AnimateJumping();

                //Trigger the fall state if the character's velocity is less than 0
                TriggerFallState();

                //Decrease the land state timer
                LandStateTimer -= Time.deltaTime;

                //Transition to the land state when the timer reaches 0
                TransitionToNewStateWithTimer(LandStateTimer, CharacterState.Idle);
                break;

            case CharacterState.IdleGrab:
                //Trigger either the idle grab or walk grab state if the character is moving or not while grabbing an object
                TriggerWalkGrabStates(HasCharacterGrabbedObject, IsCharacterWalking);
                break;

            case CharacterState.WalkGrab:
                //Trigger either the idle grab or walk grab state if the character is moving or not while grabbing an object
                TriggerWalkGrabStates(HasCharacterGrabbedObject, IsCharacterWalking);
                break;

            case CharacterState.SwingTorch:
                //Decrease the swing torch timer
                SwingTorchTimer -= Time.deltaTime;

                //Transition to the idle state when the timer reaches 0
                TransitionToNewStateWithTimer(SwingTorchTimer, CharacterState.Idle);
                break;
        }
    }

    //Sets the player's velocity in the animator
    void SetVelocityInAnimator()
    {
        //If the character has landed on ground
        if (JumpScript.HasCharacterLanded())
        {
            //Set the Y velocity to 0
            CharacterAnimator.SetFloat("VelocityY", 0f);
        }
        else
        {
            //Set the Y velocity to the float variable for the animator
            CharacterAnimator.SetFloat("VelocityY", JumpScript.PlayerRigidbody().velocity.y);
        }
        //Set the bool to true in the animator if the character has touched the ground
        CharacterAnimator.SetBool("IsGrounded", JumpScript.HasCharacterLanded());
    }

    //Press a key to play the torch swing animation when the animator is not playing the swing animation
    void AnimateTorchSwing()
    {
        //If the character obtained the torch
        if (TorchScript.Torch != null)
        {
            //If the player is pressing the swing button and the torch animation isn't playing
            if (Input.GetKey(KeyCode.F) && CurrentState == CharacterState.Idle && CurrentState != CharacterState.SwingTorch)
            {
                //Change the current state to swing torch
                ChangeCurrentState(CharacterState.SwingTorch);
            }
        }
    }

    void Update()
    {
        ToggleMovingScript();
        SetVelocityInAnimator();
        AnimateTorchSwing();
        AnimationFiniteStateMachine();
    }

}