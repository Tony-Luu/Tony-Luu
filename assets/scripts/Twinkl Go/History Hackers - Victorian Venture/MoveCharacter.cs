using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float MaxDistance;
    [SerializeField] private bool IsCharacterMoving;

    [Header("Position")]
    [SerializeField] private Vector2 TargetPosition;

    [Header("Components")]
    [SerializeField] private RectTransform CharacterRectTransform;
    [SerializeField] private CanvasGroup FadeCanvasPanel;
    [SerializeField] private CanvasGroup MultiTextPanel;
    [SerializeField] private RaycastDetection RaycastDetectionScript;
    


    public bool CheckIfCharacterIsMoving()
    {
        return IsCharacterMoving;
    }

    public void SetNewTargetPosition(Vector2 NewTargetPosition)
    {
        TargetPosition = NewTargetPosition;
    }

    public Vector2 ReturnTargetPosition()
    {
        return TargetPosition;
    }

    void Start()
    {
        CharacterRectTransform = GetComponent<RectTransform>();
        TargetPosition = transform.position;
        RaycastDetectionScript = GameObject.Find("Camera").GetComponent<RaycastDetection>();
        MultiTextPanel = GameObject.Find("Canvas/MultiTextPanel").GetComponent<CanvasGroup>();
        FadeCanvasPanel = GameObject.Find("FadePanel").GetComponent<CanvasGroup>();
    }

    //Left clicking on the ground moves the character
    void ClickToMoveCharacter()
    {
        //If the player left clicks and if it detects the ground while the character is not moving
        if (Input.GetMouseButtonDown(0) && FadeCanvasPanel.alpha == 0f && MultiTextPanel.alpha == 0f)
        {
            //Make the target position as the returned selected position from the raycast detection
            TargetPosition = new Vector2(RaycastDetectionScript.ReturnSelectedPosition().x, transform.position.y);

            //Set bool to true
            IsCharacterMoving = true;
        }
    }

    public void RefindScripts()
    {
        GameObject FadeCanvasObject = GameObject.Find("Canvas/FadePanel");

        if (FadeCanvasObject != null)
        {
            FadeCanvasPanel = FadeCanvasObject.GetComponent<CanvasGroup>();
        }
    }

    void Update()
    {
        ClickToMoveCharacter();
        MoveCharacterTowardsDestination();
        RefindScripts();
    }

    //Moves the character to the clicked mouse position
    void MoveCharacterTowardsDestination()
    {
        //If the character is moving
        if (IsCharacterMoving)
        {
            //Make the character move towards the target position
            CharacterRectTransform.position = Vector2.MoveTowards(transform.position, TargetPosition, MoveSpeed * Time.deltaTime);
        }

        //If the distance between the current position and target position is close enough
        if (Vector2.Distance(transform.position, TargetPosition) < 0.1f)
        {
            //Make the current position as the target position
            transform.position = TargetPosition;

            //Set the bool to false
            IsCharacterMoving = false;
        }
    }

}
