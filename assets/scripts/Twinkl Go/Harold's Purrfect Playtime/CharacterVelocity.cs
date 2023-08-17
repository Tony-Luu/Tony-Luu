using UnityEngine;

public class CharacterVelocity : MonoBehaviour
{
    [SerializeField] private float xVelocity;
    [SerializeField] private float yVelocity;
    [SerializeField] private float VelocityMultiplier;
    [SerializeField] private Rigidbody2D CharacterRigidbody;


    public Rigidbody2D ReturnCharacterRigidbody()
    {
        return CharacterRigidbody;
    }

    //Set new X and Y values for velocity
    public void SetNewVelocity(float newXvelocity, float newYvelocity)
    {
        xVelocity = newXvelocity;
        yVelocity = newYvelocity;
    }

    //Apply velocity to the character
    public void ApplyVelocity()
    {
        //Reset current velocity to prevent side-effects
        CharacterRigidbody.velocity = Vector2.zero;

        //Set new current velocity
        Vector2 CurrentVelocity = new Vector2(xVelocity, yVelocity);

        //Set the velocity with the current values multiplied with a multiplier
        CharacterRigidbody.velocity = CurrentVelocity * VelocityMultiplier;
    }

    //Is the character airborne?
    public bool IsCharacterAirborne()
    {
        return Mathf.Floor(CharacterRigidbody.velocity.y) != 0f;
    }

    void Awake()
    {
        CharacterRigidbody = GetComponent<Rigidbody2D>();
    }

}
