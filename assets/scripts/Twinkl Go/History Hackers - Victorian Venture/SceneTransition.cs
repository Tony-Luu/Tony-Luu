using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField] private GameObject Player;
    [SerializeField] private string SpawnPointName;
    [SerializeField] private RectTransform SpawnPoint;
    [SerializeField] private float WidthRatio;
    [SerializeField] private float HeightRatio;
    [SerializeField] private int NextSceneIndex;
    [SerializeField] private bool IsButtonPressed;
    [SerializeField] private Canvas MainCanvas;
    [SerializeField] MoveCharacter MoveCharacterScript;
    [SerializeField] RectTransform PlayerRectTransform;
    [SerializeField] AudioSource SceneTransitionSoundSource;



    public void OnPointerClick(PointerEventData eventData)
    {        
        IsButtonPressed = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject())
        {
            IsButtonPressed = false;
        }
    }

    void Start()
    {
        FindPlayer();
        FindCanvas();
        MoveCharacterScript = Player.GetComponent<MoveCharacter>();
        SceneTransitionSoundSource = GetComponent<AudioSource>();
    }

    void FindPlayer()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player != null)
        {
            PlayerRectTransform = Player.GetComponent<RectTransform>();
        }
    }

    void FindCanvas()
    {
        if (MainCanvas == null)
        {
            MainCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        }
    }

    /*void OnDrawGizmos()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3 && PlayerRectTransform != null)
        {
            Rect PlayerRect = PlayerRectTransform.rect;

            Gizmos.color = Color.red;

            Vector3[] WorldCorners = new Vector3[4];

            PlayerRectTransform.GetWorldCorners(WorldCorners);

            Vector3 WorldCenterPosition = (WorldCorners[0] + WorldCorners[2]) / 2f;

            PlayerRect.size = Vector2.Scale(PlayerRect.size, PlayerRectTransform.lossyScale);

            Gizmos.DrawWireCube(WorldCenterPosition, PlayerRect.size);


            Rect ButtonRect = ButtonRectTransform.rect;

            Gizmos.color = Color.green;

            Vector3[] WorldCornersTwo = new Vector3[4];

            ButtonRectTransform.GetWorldCorners(WorldCorners);

            Vector3 WorldCenterTwoPosition = (WorldCorners[0] + WorldCorners[2]) / 2f;

            ButtonRect.size = Vector2.Scale(ButtonRect.size, ButtonRectTransform.lossyScale);

            Gizmos.DrawWireCube(WorldCenterTwoPosition, ButtonRect.size);
        }
    }*/

    //Return the approriate spawn point based on the current scene
    Vector2 ReturnSpawnPoint()
    {
        //Find the new canvas
        FindCanvas();

        Vector2 NewSpawnPoint = Vector2.zero;

        GameObject SpawnPointObject = GameObject.Find(SpawnPointName);

        if (SpawnPointObject != null)
        {
            SpawnPoint = SpawnPointObject.GetComponent<RectTransform>();
            NewSpawnPoint = SpawnPoint.anchoredPosition;
        }

        return NewSpawnPoint;
    }

    void OnSceneLoad(Scene LoadedScene, Scene SceneMode)
    {
        //Refind the player
        FindPlayer();

        //Set the player's new target position
        MoveCharacterScript.SetNewTargetPosition(ReturnSpawnPoint());

        //Refind the scripts once the player is in a new scene
        MoveCharacterScript.RefindScripts();

        //Set the new position
        PlayerRectTransform.anchoredPosition = ReturnSpawnPoint();

        //If the scene is not the Main Hall
        if (LoadedScene.buildIndex != 2)
        {
            //Set the rotation facing left
            PlayerRectTransform.localEulerAngles = new Vector3(0f, 180f, 0f);
        }
        else
        {
            //If the scene is the Main Hall, reset rotation
            PlayerRectTransform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }
        //Delete the delegate
        SceneManager.activeSceneChanged -= OnSceneLoad;
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

    void MoveToNextSceneByIndex()
    {
        //If the button is pressed
        if (IsButtonPressed)
        {            
            //Obtain the rect transform of the button
            RectTransform ButtonRectTransform = GetComponent<RectTransform>();

            //Set a new Vector2 variable to store the X position of the button and the Y position of the player            
            Vector2 ButtonPosition = new Vector2(ButtonRectTransform.anchoredPosition.x, PlayerRectTransform.anchoredPosition.y);

            //Measure the distance of difference of the X position exclusively
            if (IsFirstRectTransformOverlappingWithSecondRectTransform(PlayerRectTransform, ButtonRectTransform))
            {
                //Play the sound effect
                SceneTransitionSoundSource.Play();

                //Load the next level based on the next scene index
                LevelManager.Instance.LoadLevel(NextSceneIndex);

                //Assign the delegate to the on scene load method
                SceneManager.activeSceneChanged += OnSceneLoad;

                //Set bool to false
                IsButtonPressed = false;
            }
        }
    }

    void Update()
    {
        MoveToNextSceneByIndex();
    }

}
