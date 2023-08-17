using UnityEngine;

public class CreateWorkerAnts : MonoBehaviour
{
    [SerializeField] private Canvas MainCanvas;
    [SerializeField] private RectTransform CanvasRectTransform;
    [SerializeField] private GameObject WorkerAnt;
    [SerializeField] private Vector2[] StartingPositions = new Vector2[4];
    [SerializeField] private Vector3[] StartingEulerAngles = new Vector3[4];
    [SerializeField] private Rect LeftSidedSquare;
    [SerializeField] private Rect RightSidedSquare;


    void DrawNewRect(Rect NewRect, Color NewColor)
    {
        if (NewRect != null)
        {
            Gizmos.color = NewColor;
            Gizmos.matrix = CanvasRectTransform.localToWorldMatrix;
            Gizmos.DrawWireCube(NewRect.center, NewRect.size);
        }
    }

    //=== DEBUG ONLY ===
    void OnDrawGizmos()
    {
        DrawNewRect(LeftSidedSquare, Color.red);
        DrawNewRect(RightSidedSquare, Color.blue);
    }

    public void SetStartingPositionsWithTargetObject(RectTransform TargetRectTransform)
    {
        //Obtain the rect transform from the worker ant
        RectTransform WorkerAntRectTransform = WorkerAnt.GetComponent<RectTransform>();

        //Left sided offsets
        float LeftSideOffsetX = CanvasRectTransform.rect.x - TargetRectTransform.anchoredPosition.x;
        float LeftSideOffsetY = CanvasRectTransform.rect.y - TargetRectTransform.anchoredPosition.y;
        float LeftSquareSize = (LeftSideOffsetX > LeftSideOffsetY) ? LeftSideOffsetX : LeftSideOffsetY;

        //Right sided offsets
        float RightSideOffsetX = Mathf.Abs(CanvasRectTransform.rect.x) - TargetRectTransform.anchoredPosition.x;
        float RightSideOffsetY = TargetRectTransform.anchoredPosition.y - CanvasRectTransform.rect.y;
        float RightSquareSize = (RightSideOffsetY < RightSideOffsetX) ? RightSideOffsetY : RightSideOffsetX;

        //Create new rectangles for the left and right sided squares
        LeftSidedSquare = new Rect(TargetRectTransform.anchoredPosition.x, TargetRectTransform.anchoredPosition.y, LeftSquareSize, LeftSquareSize);
        RightSidedSquare = new Rect(TargetRectTransform.anchoredPosition.x, TargetRectTransform.anchoredPosition.y - RightSquareSize, RightSquareSize, RightSquareSize);

        //Left side of the screen
        StartingPositions[0] = new Vector2(CanvasRectTransform.rect.x - WorkerAntRectTransform.sizeDelta.x, TargetRectTransform.anchoredPosition.y);
        StartingEulerAngles[0] = new Vector3(0f, 0f, -90f);

        //Bottom-left side of the screen
        StartingPositions[1] = new Vector2(LeftSidedSquare.xMax, LeftSidedSquare.yMax);
        StartingEulerAngles[1] = new Vector3(0f, 0f, -45f);

        //Bottom-right side of the screen
        StartingPositions[2] = new Vector2(RightSidedSquare.xMax, RightSidedSquare.yMin);
        StartingEulerAngles[2] = new Vector3(0f, 0f, 45f);

        //Right side of the screen
        StartingPositions[3] = new Vector2(Mathf.Abs(CanvasRectTransform.rect.x) + WorkerAntRectTransform.sizeDelta.x, TargetRectTransform.anchoredPosition.y);
        StartingEulerAngles[3] = new Vector3(0f, 0f, 90f);
    }

    //Spawn four worker ants with a target destination
    public void SpawnWorkerAntsWithTargetObject(RectTransform TargetRectTransform)
    {
        //Loop through all starting positions
        for (int i = 0; i < StartingPositions.Length; i++)
        {
            //Set the current instance of the worker ant from the object pool
            GameObject WorkerAntClone = ObjectPool.CurrentInstance.GetPooledObject(WorkerAnt.tag);

            //Obtain the script from the worker ant
            MoveWorkerAnt MoveWorkerAntScript = WorkerAntClone.GetComponent<MoveWorkerAnt>();

            //Set the target rect transform
            MoveWorkerAntScript.SetNewButtonRectTransform(TargetRectTransform);

            //Set the target rect transform anchored position
            MoveWorkerAntScript.SetNewTargetPosition(TargetRectTransform.anchoredPosition);

            //Set the parent of the worker ant to the canvas
            WorkerAntClone.transform.SetParent(MainCanvas.transform);

            //Set the sibling index to be below as this one to prevent the ant from overlapping the food item
            WorkerAntClone.transform.SetSiblingIndex(transform.GetSiblingIndex());

            //Set local scale to 1 to re-size the food item correctly
            WorkerAntClone.transform.localScale = new Vector3(1f, 1f, 1f);

            //Set the local position and rotation respectively of the index from the Vector2 arrays
            WorkerAntClone.transform.localPosition = StartingPositions[i];
            WorkerAntClone.transform.localEulerAngles = StartingEulerAngles[i];

            //Set the worker ant to be active in the scene
            WorkerAntClone.SetActive(true);
        }
    }

}
