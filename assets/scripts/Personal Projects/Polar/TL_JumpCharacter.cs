using System;
using UnityEngine;

public class TL_JumpCharacter : MonoBehaviour
{
    public float JumpHeight;
    private Collider CharacterCollider;
    private Rigidbody CharacterRigidbody;
    private Animator CharacterAnimator;
    private bool readyToJump;

    [SerializeField] private int _jumpCounter = 0;
    [SerializeField] private float landingCheckDistance = 0.3f;


    void Start()
    {
        //Obtain the collider and rigidbody
        CharacterCollider = GetComponent<Collider>();
        CharacterRigidbody = GetComponent<Rigidbody>();

        //Obtain the animator
        CharacterAnimator = GetComponent<Animator>();
    }

    void Jump()
    {
        //If the player presses the jmup button and if the character is touching the ground
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Nested ifs because c# isn't short-ciruiting my conditional with the function inside the first if?
            // ~ Alex
            if (IsCharacterTouchingTheGround())
            {
                if (_jumpCounter == 0)
                {
                    _jumpCounter++;
                    readyToJump = true;
                }

            }
        }
    }

    //Checks if the character is touching the ground or not
    public bool IsCharacterTouchingTheGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, CharacterCollider.bounds.extents.y + 0.1f, 3);
    }

    //
    public bool IsCharacterNearTheGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, CharacterCollider.bounds.extents.y + landingCheckDistance, 3);
    }

    public int GetJumpCounter()
    {
        return _jumpCounter;
    }

    public void ResetJump()
    {
        _jumpCounter = 0;
    }

    private void OnCollisionExit(Collision collision)
    {
        _jumpCounter++;
    }

    void FixedUpdate()
    {
        if (readyToJump)
        {
            CharacterRigidbody.velocity = Vector3.zero;
            CharacterRigidbody.velocity = Vector3.up * JumpHeight;            
            readyToJump = false;
        }
    }

    private void Update()
    {
        if (IsCharacterTouchingTheGround())
        {
            _jumpCounter = 0;
            CharacterAnimator.SetBool("IsGrounded", true);
            CharacterAnimator.SetFloat("VelocityY", 0f);
            CharacterAnimator.SetTrigger("IsLanding");
        }
        else
        {
            CharacterAnimator.SetBool("IsGrounded", false);
            CharacterAnimator.SetFloat("VelocityY", CharacterRigidbody.velocity.y);
        }

        if (IsCharacterNearTheGround() && CharacterRigidbody.velocity.y < 0f)
        {
            CharacterAnimator.SetBool("IsNearGround", true);
        }
        else
        {
            CharacterAnimator.SetBool("IsNearGround", false);
        }

        if (CharacterRigidbody.velocity.y < -9f)
        {
            //CharacterAnimator.SetTrigger("IsLanding");
        }
    }

    private void LateUpdate()
    {
        Jump();
    }
}
