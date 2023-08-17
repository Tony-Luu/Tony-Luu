using UnityEngine;

public class SpawnBackgrounds : MonoBehaviour
{
    [SerializeField] private int SpawnedBackgroundIndex;    
    [SerializeField] private float BackgroundScrollingSpeed;
    [SerializeField] private string CurrentBackgroundTag;
    [SerializeField] private Canvas MainCanvas;
    [SerializeField] private GameObject TutorialText;
    [SerializeField] private MoveBackground MoveBackgroundScript;
    GameObject StartingBackground;


    public string ReturnCurrentBackgroundTag()
    {
        return CurrentBackgroundTag;
    }

    void SpawnStartingBackground()
    {
        StartingBackground = ObjectPool.CurrentInstance.GetPooledObject("Sofa");
        StartingBackground.SetActive(true);
        StartingBackground.transform.SetParent(MainCanvas.transform);
        StartingBackground.transform.SetSiblingIndex(0);
        StartingBackground.transform.localPosition = new Vector3(0f, 0f, 0f);
        StartingBackground.transform.localScale = new Vector3(1f, 1f, 1f);

        RectTransform BackgroundRectTransform = StartingBackground.GetComponent<RectTransform>();
        BackgroundRectTransform.anchoredPosition = new Vector2(0f, 0f);
        BackgroundRectTransform.sizeDelta = new Vector2(0f, 0f);

        MoveBackgroundScript = StartingBackground.GetComponent<MoveBackground>();
        MoveBackgroundScript.SetNewSpeed(BackgroundScrollingSpeed);
    }

    void Start()
    {
        MainCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        SpawnStartingBackground();
    }

    void ChangeSpawnedBackground()
    {
        switch (SpawnedBackgroundIndex)
        {
            case 5:
                CurrentBackgroundTag = "Sofa";
                break;

            case 6:
                CurrentBackgroundTag = "Shelf";
                break;

            case 7:
                CurrentBackgroundTag = "Front Door";
                break;

            case 8:
                CurrentBackgroundTag = "Fireplace";
                break;
        }
    }

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

    void SpawnBackground()
    {
        //If the tutorial is already done
        if(TutorialText == null)
        {
            //Find the current background in the scene
            GameObject CurrentBackground = GameObject.FindGameObjectWithTag(CurrentBackgroundTag);

            //Find the spawned background in the object pool
            GameObject SpawnedBackground = ObjectPool.CurrentInstance.GetPooledObject(CurrentBackgroundTag);

            //If the background is already spawned
            if (SpawnedBackground != null)
            {
                //Obtain the spawned background and enable it
                MoveBackground MoveBackgroundScript = SpawnedBackground.GetComponent<MoveBackground>();
                MoveBackgroundScript.enabled = true;

                //Obtain the starting background and enable it
                MoveBackgroundScript = StartingBackground.GetComponent<MoveBackground>();
                MoveBackgroundScript.enabled = true;

                //Set the position of the spawned background and enable it
                SpawnedBackground.transform.SetParent(MainCanvas.transform);
                SpawnedBackground.transform.SetSiblingIndex(0);
                SpawnedBackground.transform.localScale = new Vector3(1f, 1f, 1f);

                RectTransform CanvasRectTransform = MainCanvas.GetComponent<RectTransform>();

                RectTransform SpawnedBackgroundRectTransform = SpawnedBackground.GetComponent<RectTransform>();
                SpawnedBackgroundRectTransform.anchoredPosition = new Vector2(CanvasRectTransform.sizeDelta.x - 10f, 0f);
                SpawnedBackgroundRectTransform.sizeDelta = new Vector2(0f, 0f);
                SpawnedBackgroundRectTransform.transform.gameObject.SetActive(true);
            }

            //If the current background is still active and it reached the centre of the scene
            if (CurrentBackground != null && CurrentBackground.transform.localPosition.x <= 0f)
            {
                //Increase the index
                SpawnedBackgroundIndex++;

                //If the index reaches 8
                if (SpawnedBackgroundIndex > 8)
                {
                    //Set it back to 5
                    SpawnedBackgroundIndex = 5;
                }
            }
        }
    }

    void FixedUpdate()
    {
        SpawnBackground();
        ChangeSpawnedBackground();
    }

}
