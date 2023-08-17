using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class AnimationStates : MonoBehaviour
{
    [SerializeField] private float PrepareJumpStateTimer;
    [SerializeField] private float LandingStateTimer;
    [SerializeField] private GameObject CollidedObject;    
    [SerializeField] private Animator CatAnimator;
    [SerializeField] private CharacterVelocity CharacterVelocityScript;
    [SerializeField] private CharacterAudioSource CharacterAudioSourceScript;
    private Collider2D CatCollider;


    public enum CatState
    {
        Running, PrepareJump, Jumping, Falling, Landing, Knockedback
    }
    [SerializeField] private CatState CurrentState = CatState.Running;

    public CatState ReturnCurrentState()
    {
        return CurrentState;
    }

    public void SetNewState(CatState NewState)
    {
        CurrentState = NewState;
    }

    void Start()
    {
        CatCollider = GetComponent<Collider2D>();
        CatAnimator = GetComponent<Animator>();
        CharacterAudioSourceScript = GetComponent<CharacterAudioSource>();
        CharacterVelocityScript = GetComponent<CharacterVelocity>();
    }

    //Change the alpha of the sprite's colour in-between short pauses
    IEnumerator ShowKnockbackEffect()
    {
        Image CatSprite = GetComponent<Image>();

        if(CharacterVelocityScript.IsCharacterAirborne())
        {
            yield return new WaitForSeconds(0.1f);
            CatSprite.color = new Color(255f, 255f, 255f, 0f);
            yield return new WaitForSeconds(0.1f);
            CatSprite.color = new Color(255f, 255f, 255f, 255f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    //Change into a new state when the timer reaches 0
    void TimeTransitionToNewState(float Timer, CatState NewState)
    {
        //When the timer reaches to 0
        if (Timer <= 0f)
        {
            SetNewState(NewState);
        }
    }

    //Method for preparing the jump
    void PrepareJumpFunction()
    {
        if (Time.timeScale == 1)
        {
            //If the player presses the space bar or touches the screen
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                //Set bool to true
                CatAnimator.SetBool("IsJumpingPrepared", true);

                //Set new state to Prepare Jump
                SetNewState(CatState.PrepareJump);
            }
        }
    }

    //Manage the conditions of changing states
    void ChangeStates()
    {
        CatAnimator.SetInteger("CurrentState", (int) CurrentState);
        CatAnimator.SetFloat("Velocity", CharacterVelocityScript.ReturnCharacterRigidbody().velocity.y);

        switch (CurrentState)
        {
            case CatState.Running:
                CatAnimator.SetBool("HasCollided", false);
                CatAnimator.SetBool("IsGrounded", true);
                CatAnimator.SetBool("IsJumpingPrepared", false);
                PrepareJumpStateTimer = 0.3f;
                PrepareJumpFunction();
                break;

            case CatState.PrepareJump:
                //Set the prepare jump state timer
                PrepareJumpStateTimer -= Time.deltaTime;

                //Set the timer to transistion to a new state
                TimeTransitionToNewState(PrepareJumpStateTimer, CatState.Jumping);
                break;

            case CatState.Jumping:
                CatAnimator.SetBool("IsGrounded", false);
                CatAnimator.SetBool("IsJumpingPrepared", false);
                
                //If the cat is at the apex of the jump
                if (CharacterVelocityScript.ReturnCharacterRigidbody().velocity.y < 0f)
                {
                    //Change state to Falling
                    SetNewState(CatState.Falling);
                }
                break;

            case CatState.Falling:
                //Set the landing state timer
                LandingStateTimer = 0.1f;

                //When the cat is falling towards the floor
                if (CharacterVelocityScript.ReturnCharacterRigidbody().velocity.y < -12f)
                {
                    //Play the audio clip at the index
                    CharacterAudioSourceScript.PlayAudioClipAtIndex(2);

                    //Change state to Landing
                    SetNewState(CatState.Landing);
                }
                break;

            case CatState.Landing:
                if (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    //Set the timer for preparing the jump
                    PrepareJumpStateTimer = 0.3f;

                    //Activate the prepare jump function
                    PrepareJumpFunction();
                }
                else
                {
                    //Subtract the landing state timer with delta time
                    LandingStateTimer -= Time.deltaTime;

                    //Set the timer to transistion to a new state
                    TimeTransitionToNewState(LandingStateTimer, CatState.Running);
                }                
                break;

            case CatState.Knockedback:
                CatAnimator.SetBool("IsGrounded", false);
                CatAnimator.SetBool("HasCollided", true);
                CatAnimator.SetBool("IsJumpingPrepared", false);

                //Ignore collision between this character and the collided object
                Physics2D.IgnoreCollision(CatCollider, CollidedObject.GetComponent<Collider2D>());

                //Start the coroutine of the knockback effect
                StartCoroutine(ShowKnockbackEffect());

                //If the character is grounded
                if (CharacterVelocityScript.ReturnCharacterRigidbody().velocity.y < -7f)
                {
                    //Set the landing state timer
                    LandingStateTimer = 0.1f;

                    //Set the bool to false
                    CatAnimator.SetBool("HasCollided", false);

                    //Play the audio clip at the index
                    CharacterAudioSourceScript.PlayAudioClipAtIndex(2);

                    //Change state to running
                    SetNewState(CatState.Landing);
                }
                break;
        }
    }

    void Update()
    {
        ChangeStates();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //If the cat collides into an object
        if (collision.gameObject.tag.Contains("Ball of Yarn") || collision.gameObject.tag.Contains("Books") || 
            collision.gameObject.tag.Contains("Cat Bowl") || collision.gameObject.tag.Contains("Cat Box") ||
            collision.gameObject.tag.Contains("Laptop"))
        {
            CollidedObject = collision.gameObject;
        }
    }

}
