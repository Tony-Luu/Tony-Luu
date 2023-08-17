using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private Animator CharacterAnimator;
    [SerializeField] private MoveCharacter MoveCharacterScript;

    public enum CharacterState
    {
        Idle, Walk
    }
    [SerializeField] private CharacterState CurrentState;


    void ChangeState()
    {
        //Set the integer as the current state to change animations
        CharacterAnimator.SetInteger("CharacterState", (int)CurrentState);

        switch (CurrentState)
        {
            case CharacterState.Idle:
                //If the character is moving
                if (MoveCharacterScript.CheckIfCharacterIsMoving())
                {
                    //Reset the blinking trigger
                    CharacterAnimator.ResetTrigger("Blinking");

                    //Change the current state to Walk
                    CurrentState = CharacterState.Walk;
                }
                break;

            case CharacterState.Walk:
                //If the character has stopped moving
                if (!MoveCharacterScript.CheckIfCharacterIsMoving())
                {
                    //Change current state to Idle
                    CurrentState = CharacterState.Idle;
                }
                else
                {
                    //Flip the sprite if the character changes direction
                    FlipSprite();
                }
                break;
        }
    }

    void FlipSprite()
    {
        float yRotation;

        //If the target's X position is more than the current character's X position then flip the sprite
        if (MoveCharacterScript.ReturnTargetPosition().x < transform.position.x)
        {
            //Rotate it 180 degrees
            yRotation = 180f;
        }
        else
        {
            //Set rotation to 0
            yRotation = 0;            
        }
        //Set new euler angles ased on Y rotation
        transform.localEulerAngles = new Vector3(0f, yRotation, 0f);
    }

    void Start()
    {
        CharacterAnimator = GetComponent<Animator>();
        MoveCharacterScript = GetComponent<MoveCharacter>();
    }

    void Update()
    {
        ChangeState();
    }

}
