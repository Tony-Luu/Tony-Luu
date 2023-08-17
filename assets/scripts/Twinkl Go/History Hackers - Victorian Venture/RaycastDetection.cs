using UnityEngine;
using UnityEngine.SceneManagement;

public class RaycastDetection : MonoBehaviour
{
    [SerializeField] private Vector2 SelectedPosition;
    [SerializeField] private Canvas MainCanvas;
    [SerializeField] private Canvas GameplayCanvas;
    [SerializeField] private GameObject Clipboard;
    [SerializeField] private GameObject Minimap;
    [SerializeField] private GameObject TilePuzzle;
    [SerializeField] private RectTransform ClipboardButtonRectTransform;
    [SerializeField] private RectTransform MinimapButtonRectTransform;
    [SerializeField] private bool IsMouseOverButton;


    void FindButtons()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 && ClipboardButtonRectTransform == null && MinimapButtonRectTransform == null)
        {
            ClipboardButtonRectTransform = GameObject.FindGameObjectWithTag("ClipboardButton").GetComponent<RectTransform>();
            MinimapButtonRectTransform = GameObject.FindGameObjectWithTag("MinimapButton").GetComponent<RectTransform>();
        }
    }

    void FindCanvas()
    {
        if (MainCanvas == null)
        {
            MainCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        }
    }

    void FindGameplayCanvas()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 && GameplayCanvas == null)
        {
            GameplayCanvas = GameObject.Find("GameplayCanvas").GetComponent<Canvas>();
        }
    }

    void FindClipboard()
    {
        if (Clipboard == null)
        {
            Clipboard = GameObject.FindGameObjectWithTag("Clipboard");
        }
    }

    void FindMinimap()
    {
        if (Minimap == null)
        {
            Minimap = GameObject.FindGameObjectWithTag("Minimap");
        }
    }

    bool IsGameplayObjectActive(GameObject GameplayObject)
    {
        if (GameplayObject != null)
        {
            return GameplayObject.activeInHierarchy;
        }
        return false;
    }

    public Vector2 ReturnSelectedPosition()
    {
        return SelectedPosition;
    }

    bool IsCursorPositionOverButton(Vector3 CursorPosition)
    {
        if (ClipboardButtonRectTransform != null && MinimapButtonRectTransform != null)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(ClipboardButtonRectTransform, CursorPosition, GetComponent<Camera>()) ||
                   RectTransformUtility.RectangleContainsScreenPoint(MinimapButtonRectTransform, CursorPosition, GetComponent<Camera>());
        }
        return false;
    }

    void DetectMousePosition()
    {
        //Output the mouse position
        Vector3 MousePosition;

        //Convert screen point to world position for main canvas
        RectTransformUtility.ScreenPointToWorldPointInRectangle(MainCanvas.transform as RectTransform, Input.mousePosition, GetComponent<Camera>(), out MousePosition);

        //If the scene is the Main Hall
        if (SceneManager.GetActiveScene().buildIndex >= 2)
        {
            //Check if the mouse position is over the button
            IsMouseOverButton = IsCursorPositionOverButton(Input.mousePosition);

            //Check if the clipboard or minimap is active on the scene
            bool IsGameplayElementActive = IsGameplayObjectActive(Clipboard) || IsGameplayObjectActive(Minimap) || IsGameplayObjectActive(TilePuzzle);

            //If the player has clicked on the scene while the game is not pause and the mouse is not over a clipboard or minimap button and
            //neither the clipboard or minimap is active
            if (Input.GetMouseButtonDown(0) && !OptionManager.Instance.isPaused && !IsMouseOverButton && !IsGameplayElementActive)
            {
                //Set the selected position as the mouse position
                SelectedPosition = MousePosition;
            }
        }
    }

    void FindTilePuzzle()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4 && TilePuzzle == null)
        {
            TilePuzzle = GameObject.Find("TilePuzzlePanel");
        }
    }

    void Update()
    {
        FindCanvas();
        FindGameplayCanvas();
        FindButtons();
        FindClipboard();
        FindMinimap();
        FindTilePuzzle();
        DetectMousePosition();
    }

}
