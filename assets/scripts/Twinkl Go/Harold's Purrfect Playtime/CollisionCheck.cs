using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    [SerializeField] private float KnockbackHeight;
    [SerializeField] private float KnockbackWidth;
    [SerializeField] private GameObject EndScreen;
    [SerializeField] private CharacterVelocity CharacterVelocityScript;
    [SerializeField] private CharacterAudioSource CharacterAudioSourceScript;
    private AnimationStates AnimationStateScript;


    void Start()
    {
        CharacterAudioSourceScript = GetComponent<CharacterAudioSource>();
        CharacterVelocityScript = GetComponent<CharacterVelocity>();
        AnimationStateScript = GetComponent<AnimationStates>();
    }

    void AddKnockBack()
    {
        //Set the new X and Y values for the jump height
        CharacterVelocityScript.SetNewVelocity(KnockbackWidth, KnockbackHeight);

        //Apply the velocity effect
        CharacterVelocityScript.ApplyVelocity();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //If the cat collides into an object
        if (collision.gameObject.tag.Contains("Ball of Yarn") || collision.gameObject.tag.Contains("Books") ||
            collision.gameObject.tag.Contains("Cat Bowl") || collision.gameObject.tag.Contains("Cat Box") ||
            collision.gameObject.tag.Contains("Laptop"))
        {
            //Play an audio clip at an index
            CharacterAudioSourceScript.PlayAudioClipAtIndex(1);

            //Apply knockback effects
            AddKnockBack();

            //Set a new state to Knockedback
            AnimationStateScript.SetNewState(AnimationStates.CatState.Knockedback);
        }

        //If the cat collides into Muffin
        if (collision.gameObject.name.Contains("Muffin"))
        {
            //Obtain the animator and disable it
            Animator CatAnimator = collision.gameObject.GetComponent<Animator>();
            CatAnimator.enabled = false;

            //Destroy the cat
            Destroy(gameObject);

            //Enable the game over screen
            EndScreen.gameObject.SetActive(true);
        }
    }

}
