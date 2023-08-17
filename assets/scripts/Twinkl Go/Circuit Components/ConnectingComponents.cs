using UnityEngine;
using UnityEngine.EventSystems;

public class ConnectingComponents : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private AudioClip ClipSound;

    [SerializeField]
    private AudioSource ClipAudioSource;

    [SerializeField]
    private CheckCircuitProgression CheckCircuitProgressionScript;


    void Start()
    {
        ClipAudioSource = GetComponent<AudioSource>();
        CheckCircuitProgressionScript = GameObject.Find("CircuitBoxArea").GetComponent<CheckCircuitProgression>();
    }

    //=== DEBUG ONLY ===
    void OnDrawGizmos()
    {
        GameObject[] Connectors = GameObject.FindGameObjectsWithTag("Connector");

        foreach (GameObject ChildConnector in Connectors)
        {
            RectTransform ConnectorRectTransform = ChildConnector.GetComponent<RectTransform>();

            Rect ConnectorRect = ConnectorRectTransform.rect;

            Gizmos.color = Color.red;

            Vector3[] WorldCorners = new Vector3[4];

            ConnectorRectTransform.GetWorldCorners(WorldCorners);

            Vector3 WorldCenterPosition = (WorldCorners[0] + WorldCorners[2]) / 2f;

            ConnectorRect.size = Vector2.Scale(ConnectorRect.size, ConnectorRectTransform.lossyScale);

            Gizmos.DrawWireCube(WorldCenterPosition, ConnectorRect.size);
        }
    }

    //Converts a rectangle's local position to its' world position
    public Rect ReturnConvertedRectLocalPositionToRectWorldPosition(RectTransform RectLocalTransform)
    {
        Rect ConvertedRect = new Rect();

        if (RectLocalTransform != null)
        {
            //Obtain the rect from the rect transform
            Rect RectLocal = RectLocalTransform.rect;

            //Set an array of vector3 to store the world corners of the scene
            Vector3[] WorldCorners = new Vector3[4];

            //Obtain the world corners
            RectLocalTransform.GetWorldCorners(WorldCorners);

            //Obtain the world center position by adding the bottom-left corner position with the top-right corner position then half it by 2
            Vector3 WorldCenterPosition = (WorldCorners[0] + WorldCorners[2]) / 2f;

            //Scale the size of the rectangle's local size with the rect transform's lossy scale
            RectLocal.size = Vector2.Scale(RectLocal.size, RectLocalTransform.lossyScale);

            //Create a new rect with the world center position and the scaled size
            ConvertedRect = new Rect(WorldCenterPosition, RectLocal.size);

            //Convert the center of the rect from local to world position
            ConvertedRect.center = RectLocalTransform.TransformPoint(ConvertedRect.center);
        }
        //Return the new rect
        return ConvertedRect;
    }

    //Returns either true or false if the two rect transforms are overlapping with each other
    public bool IsFirstRectTransformOverlappingWithSecondRectTransform(RectTransform FirstRect, RectTransform SecondRect)
    {
        Rect RectOne = ReturnConvertedRectLocalPositionToRectWorldPosition(FirstRect);
        Rect RectTwo = ReturnConvertedRectLocalPositionToRectWorldPosition(SecondRect);

        return RectOne.Overlaps(RectTwo, true);
    }

    //Check if the child rect transform is overlapping with another child rect transform
    void CheckIfChildRectTransformIsOverlapping()
    {
        bool AreConnectorsAttached = false;

        //Find all of the gameobjects with the tag labelled connector
        GameObject[] Connectors = GameObject.FindGameObjectsWithTag("Connector");

        //Loop through all of the connectors
        foreach (GameObject Connector in Connectors)
        {
            //Obtain the rect transform component from the connector
            RectTransform ConnectorRectTransform = Connector.GetComponent<RectTransform>();

            //Loop through all of the children in this transform
            foreach (Transform Child in transform)
            {
                RectTransform ChildRectTransform = Child.GetComponent<RectTransform>();

                //If both of the connectors are from components then skip this iteration to prevent components from attaching to each other
                if (Connector.transform.parent.tag.Contains("Component") && Child.transform.parent.tag.Contains("Component"))
                {
                    continue;
                }

                //If the child is a connector and it's not the same as the gameobject in the outer loop
                if (ChildRectTransform.tag.Contains("Connector") && Connector != Child.gameObject)
                {
                    //Obtain the scripts from both components
                    AttachedComponent AttachedConnectorScript = ConnectorRectTransform.GetComponent<AttachedComponent>();
                    AttachedComponent AttachedOtherConnectorScript = ChildRectTransform.GetComponent<AttachedComponent>();

                    if (AttachedConnectorScript.ReturnComponent() == null && AttachedOtherConnectorScript.ReturnComponent() == null && 
                        !ConnectorRectTransform.name.Contains("CrocodileClipsComponent") && !ChildRectTransform.name.Contains("CrocodileClipsComponent"))
                    {
                        //Check if the rectangles of the connectors are overlapped with each other
                        AreConnectorsAttached = IsFirstRectTransformOverlappingWithSecondRectTransform(ChildRectTransform, ConnectorRectTransform);

                        //If any of the connectors are attached to each other
                        if (AreConnectorsAttached)
                        {
                            ClipAudioSource.clip = ClipSound;
                            ClipAudioSource.Play();

                            //If the child rect transform is a crocodile clip
                            if (ChildRectTransform.parent.tag.Contains("CrocodileClip"))
                            {
                                //If the connector is a left handed connector
                                if (ConnectorRectTransform.name.Contains("LeftConnector"))
                                {
                                    //Change the rotation to -90
                                    ChildRectTransform.parent.localEulerAngles = new Vector3(0f, 0f, -90f);
                                }
                                else if (ConnectorRectTransform.name.Contains("RightConnector"))
                                {
                                    //If the connector is a right handed connector, change the rotation to 90
                                    ChildRectTransform.parent.localEulerAngles = new Vector3(0f, 0f, 90f);
                                }
                                //Set the position of the child rect transform position to the connector's position
                                ChildRectTransform.parent.position = ConnectorRectTransform.position;

                                //Adjust the pivot of the crocodile clip
                                CustomPivot CustomPivotScript = ChildRectTransform.transform.parent.parent.GetComponentInChildren<CustomPivot>();
                                CustomPivotScript.RotateWithClip();

                                //Update and draw the wire of the crocodile clip
                                DrawWire DrawWireScript = ChildRectTransform.parent.parent.GetComponentInChildren<DrawWire>();
                                DrawWireScript.UpdateWire();
                            }

                            //Set the new components to the newly attached components
                            AttachedConnectorScript.SetNewComponent(ChildRectTransform.gameObject);
                            AttachedOtherConnectorScript.SetNewComponent(ConnectorRectTransform.gameObject);

                            //Add both of these attached components to the list
                            CheckCircuitProgressionScript.AddAttachedComponentsToList(AttachedConnectorScript);
                            CheckCircuitProgressionScript.AddAttachedComponentsToList(AttachedOtherConnectorScript);
                            break;
                        }
                    }
                }
            }
            //Break out of the outer loop once the connectors are attached
            if (AreConnectorsAttached)
            {
                break;
            }
        }
    }

    //Check if children's rect transform aren't connected to any components
    void CheckIfChildRectTransformConnectionIsBroken()
    {
        //If this gameobject is a crocodile clip
        if (transform.tag.Contains("CrocodileClip"))
        {
            foreach (Transform Child in transform)
            {
                //Obtain the rect transform from the child transform
                RectTransform ChildRect = Child.GetComponent<RectTransform>();

                //Obtain the attached component script from the child gameobject
                AttachedComponent AttachedComponentScript = Child.GetComponent<AttachedComponent>();

                //If the script exists and the component is attached onto another component
                if (AttachedComponentScript != null && AttachedComponentScript.ReturnComponent() != null)
                {
                    //Obtain the rect transform of the attached component
                    RectTransform AttachedComponentRect = AttachedComponentScript.ReturnComponent().GetComponent<RectTransform>();

                    //If the attached components aren't overlapping each other anymore
                    if (!IsFirstRectTransformOverlappingWithSecondRectTransform(ChildRect, AttachedComponentRect))
                    {
                        //Remove both components from the list
                        CheckCircuitProgressionScript.ReturnAttachedComponentsList().RemoveAll(x => x == AttachedComponentScript.ReturnComponent().GetComponent<AttachedComponent>());
                        CheckCircuitProgressionScript.ReturnAttachedComponentsList().RemoveAll(x => x == AttachedComponentScript);
                        AttachedComponentScript.ReturnComponent().GetComponent<AttachedComponent>().SetNewComponent(null);
                        AttachedComponentScript.SetNewComponent(null);
                    }
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            if (eventData.pointerCurrentRaycast.gameObject.tag.Contains("CircuitBox") || eventData.pointerCurrentRaycast.gameObject.tag.Contains("Component") ||
                eventData.pointerCurrentRaycast.gameObject.tag.Contains("CrocodileClip"))
            {
                //Checks if the children's rect transform are not connected to any connectors in the scene
                CheckIfChildRectTransformConnectionIsBroken();

                //Checks if the children's rect transform are overlapping any connectors in the scene
                CheckIfChildRectTransformIsOverlapping();

                //Check if the connected components are the correct ones
                CheckCircuitProgressionScript.CheckConnectedComponents();
            }
        }
    }

}
