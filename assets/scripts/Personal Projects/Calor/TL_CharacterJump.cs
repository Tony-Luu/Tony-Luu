using UnityEngine;

public class TL_CharacterJump : MonoBehaviour
{
    public float JumpHeight;
    private bool IsGrounded;
    private Rigidbody2D CharacterRigidbody;
    private TL_Torch TorchScript;
    private TL_AnimationManager AnimationManagerScript;
    private TL_PlayerSoundManager PlayerSoundManagerScript;


    void Start()
    {
        TorchScript = GetComponent<TL_Torch>();
        CharacterRigidbody = GetComponent<Rigidbody2D>();
        AnimationManagerScript = GetComponent<TL_AnimationManager>();
        PlayerSoundManagerScript = GetComponent<TL_PlayerSoundManager>();
    }

    //Returns the character's ridigbody 2D
    public Rigidbody2D PlayerRigidbody()
    {
        return CharacterRigidbody;
    }

    //Has the character landed on the ground?
    public bool HasCharacterLanded()
    {
        return IsGrounded;
    }

    //Is the character airborne?
    public bool IsCharacterAirborne()
    {
        return CharacterRigidbody.velocity.y != 0f;
    }

    void Jump()
    {
        //If the character's velocity is 0 and if the current state is jumping and not landing
        if (CharacterRigidbody.velocity.y == 0f && HasCharacterLanded() && 
            AnimationManagerScript.ReturnCurrentState() == TL_AnimationManager.CharacterState.Jump && 
            AnimationManagerScript.ReturnCurrentState() != TL_AnimationManager.CharacterState.Land)
        {
            //Play a jump sound
            if (!PlayerSoundManagerScript.ReturnAudioSource().isPlaying)
            {
                PlayerSoundManagerScript.JumpSounds();
            }

            //Store the default X velocity
            float xAxis = CharacterRigidbody.velocity.x;

            //Create a vector2 variable to store the X axis and the jump height
            Vector2 JumpPosition = new Vector2(xAxis, JumpHeight);

            //Set the velocity with the new jump position
            CharacterRigidbody.velocity = JumpPosition;
        }

        //Checks if the player is airborne
        if (IsCharacterAirborne())
        {
            //Set the bool to false if the player is airborne
            IsGrounded = false;
        }
    }

    //Toggle the torch script
    void ToggleTorch()
    {
        //Enable or disable the torch script depending on if the player has touched the ground or not
        TorchScript.enabled = HasCharacterLanded();
    }

    void Update()
    {
        ToggleTorch();
    }

    void FixedUpdate()
    {
        Jump();
    }
    void CheckGround(Vector2 Normals)
    {
        //If the Y position of the Vector2 is 1
        if (Normals == new Vector2(0f, 1f))
        {
            //Set bool to true
            IsGrounded = true;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        //If the array for the points of contact is greater than 0 to prevent array out of bounds
        if (collision.contacts.Length > 0)
        {
            //Set a Vector2 variable to obtain the surface normal from the collided gameobject
            Vector2 SurfaceNormal = collision.contacts[0].normal;

            //Call this function to check the ground
            CheckGround(SurfaceNormal);
        }
    }

}