using UnityEngine;

public class TL_EdgeDetection : MonoBehaviour
{
    private Collider2D PlayerCollider2D;
    private TO_CharacterGrabBlock GrabScript;
    private Vector2 PlayerLeftCollider2DMinBounds;
    private Vector2 PlayerLeftCollider2DMaxBounds;
    private Vector2 PlayerRightCollider2DMinBounds;
    private Vector2 PlayerRightCollider2DMaxBounds;
    private Vector2 LeftCollider2DMinBounds;
    private Vector2 LeftCollider2DMaxBounds;
    private Vector2 RightCollider2DMinBounds;
    private Vector2 RightCollider2DMaxBounds;
    public bool HasPlayerLeftColliderNotReachedPlatformEdge;
    public bool HasPlayerRightColliderNotReachedPlatformEdge;
    public bool HasLeftSideColliderNotReachedPlatformEdge;
    public bool HasRightSideColliderNotReachedPlatformEdge;


    void Start()
    {
        PlayerCollider2D = GetComponent<Collider2D>();
        GrabScript = GetComponent<TO_CharacterGrabBlock>();
    }

    void SetLinecastsForPlatformEdges()
    {
        //If the player is grabbing an object
        if (GrabScript.ReturnGrabbedBox() != null)
        {
            //Set the bounds of the collider on the player's left side with a vector2
            PlayerLeftCollider2DMinBounds = new Vector2(PlayerCollider2D.bounds.min.x, PlayerCollider2D.bounds.min.y);
            PlayerLeftCollider2DMaxBounds = new Vector2(PlayerCollider2D.bounds.min.x, transform.position.y - 2f);
            Debug.DrawLine(PlayerLeftCollider2DMinBounds, PlayerLeftCollider2DMaxBounds, Color.red);

            //Set the bounds of the collider on the player's right side with a vector2
            PlayerRightCollider2DMinBounds = new Vector2(PlayerCollider2D.bounds.max.x, PlayerCollider2D.bounds.min.y);
            PlayerRightCollider2DMaxBounds = new Vector2(PlayerCollider2D.bounds.max.x, transform.position.y - 2f);
            Debug.DrawLine(PlayerRightCollider2DMinBounds, PlayerRightCollider2DMaxBounds, Color.red);

            //Cast a linecast to detect which side of the collider is on an edge of a platform
            HasPlayerLeftColliderNotReachedPlatformEdge = Physics2D.Linecast(PlayerLeftCollider2DMinBounds, PlayerLeftCollider2DMaxBounds);
            HasPlayerRightColliderNotReachedPlatformEdge = Physics2D.Linecast(PlayerRightCollider2DMinBounds, PlayerRightCollider2DMaxBounds);

            //Obtain the 2D collider
            Collider2D GrabbedObjectCollider = GrabScript.ReturnGrabbedBox().GetComponent<Collider2D>();

            //Set the bounds of the collider on the left side of the grabbed object
            LeftCollider2DMinBounds = new Vector2(GrabbedObjectCollider.bounds.min.x, GrabbedObjectCollider.bounds.min.y);
            LeftCollider2DMaxBounds = new Vector2(GrabbedObjectCollider.bounds.min.x, GrabbedObjectCollider.transform.position.y - 2f);

            //Set the bounds of the collider on the right side of the grabbed object
            RightCollider2DMinBounds = new Vector2(GrabbedObjectCollider.bounds.max.x, GrabbedObjectCollider.bounds.min.y);
            RightCollider2DMaxBounds = new Vector2(GrabbedObjectCollider.bounds.max.x, GrabbedObjectCollider.transform.position.y - 2f);
            
            //Check if the left or right side of the collider has not reached an edge of a platform
            HasLeftSideColliderNotReachedPlatformEdge = Physics2D.Linecast(LeftCollider2DMinBounds, LeftCollider2DMaxBounds);
            HasRightSideColliderNotReachedPlatformEdge = Physics2D.Linecast(RightCollider2DMinBounds, RightCollider2DMaxBounds);

            Debug.DrawLine(LeftCollider2DMinBounds, LeftCollider2DMaxBounds, Color.red);
            Debug.DrawLine(RightCollider2DMinBounds, RightCollider2DMaxBounds, Color.red);
        }
    }

    void Update()
    {
        SetLinecastsForPlatformEdges();
    }

}
