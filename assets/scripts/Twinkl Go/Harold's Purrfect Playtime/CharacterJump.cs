using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    [SerializeField] private float JumpHeight;
    [SerializeField] private CharacterVelocity CharacterVelocityScript;
    [SerializeField] private CharacterAudioSource CharacterAudioSourceScript;
    private AnimationStates AnimationStateScript;


    void Start()
    {
        CharacterAudioSourceScript = GetComponent<CharacterAudioSource>();
        CharacterVelocityScript = GetComponent<CharacterVelocity>();
        AnimationStateScript = GetComponent<AnimationStates>();
    }

    void ApplyJumpEffect()
    {
        if (CharacterVelocityScript.ReturnCharacterRigidbody().velocity.y == 0f && !CharacterVelocityScript.IsCharacterAirborne() &&
            AnimationStateScript.ReturnCurrentState() == AnimationStates.CatState.Jumping)
        {
            //Set an index value and play the audio clip
            CharacterAudioSourceScript.PlayAudioClipAtIndex(0);

            //Set the new X and Y values for the jump height
            CharacterVelocityScript.SetNewVelocity(0f, JumpHeight);

            //Apply the velocity effect
            CharacterVelocityScript.ApplyVelocity();
        }
    }

    void FixedUpdate()
    {
        ApplyJumpEffect();
    }

}
