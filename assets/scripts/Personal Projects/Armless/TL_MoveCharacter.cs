using UnityEngine;
using System.Collections;

public class TL_MoveCharacter : MonoBehaviour
{
    public float MoveSpeed;
    private float HorizontalMovement;
    private float VerticalMovement;
    private Vector3 MoveDirection;
    private Rigidbody CharacterRigidbody;


    void Awake()
    {
        CharacterRigidbody = GetComponent<Rigidbody>();
    }

    void MovementInput()
    {
        //Set both the horizontal and vertical movement to their respective axis
        HorizontalMovement = Input.GetAxisRaw("Horizontal");
        VerticalMovement = Input.GetAxisRaw("Vertical");

        //Set the new vector3 variable to receive the movement inputs
        MoveDirection = new Vector3(HorizontalMovement, 0f, VerticalMovement);
    }

    void Update()
    {
        //Move the player based on movement input
        MovementInput();
    }

    //Moves the player based on the axis input
    void MovePlayer(Vector3 Direction)
    {
        //Multiply the rotation from the transform with the Vector3 direction
        Direction = transform.rotation * Direction;

        //Move the player's rigidbody
        CharacterRigidbody.MovePosition(CharacterRigidbody.position + Direction * MoveSpeed * Time.fixedDeltaTime);

        //If the player's arms are still child objects then make the arms move alongside the player
        foreach (Transform Child in transform)
        {
            Rigidbody ChildRigidbody = Child.GetComponent<Rigidbody>();
            if(ChildRigidbody != null)
            {
                ChildRigidbody.MovePosition(ChildRigidbody.position + Direction * MoveSpeed * Time.fixedDeltaTime);
            }
        }
    }

    void FixedUpdate()
    {
        //Move the player with the rigidbody
        MovePlayer(MoveDirection);
    }

}
