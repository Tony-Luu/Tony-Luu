using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TilePuzzleManager : MonoBehaviour
{
    [Header("Tile Related Objects")]
    [SerializeField] private GameObject TilePuzzlePanel;
    [SerializeField] private GameObject Tile;
    [SerializeField] private GameObject SelectedTile;
    ToggleClass ToggleClassScript;

    [Header("Tile Array and Positions")]
    [SerializeField] private Tile[,] Tiles = new Tile[4, 4];
    [SerializeField] private Sprite[] TileSprite = new Sprite[16];
    [SerializeField] private Vector2 StartingTilePosition;
    [SerializeField] private List<Vector2> TilePositions = new List<Vector2>();
    [SerializeField] private float TileSwapSpeed;

    [Header("Tile Swapping")]
    [SerializeField] private bool AreTilesBeingSwapped;
    [SerializeField] private Tile FirstSwappedTile;
    [SerializeField] private Tile SecondSwappedTile;
    [SerializeField] private Vector2 FirstSwappedTilePosition;
    [SerializeField] private Vector2 SecondSwappedTilePosition;

    [Header("Text Panel")]
    [SerializeField] private MultiTextPanel TextPanel;
    [SerializeField] private PageCollection PennyBlackStampInfo;
    [SerializeField] private EventTrigger PennyBlackStampButton;

    [Header("Audio")]
    [SerializeField] private AudioClip TileSwapSound;
    [SerializeField] private AudioClip SuccessSound;
    [SerializeField] private AudioSource TileSoundSource;
    ProgressManager ProgressManagerScript;


    void AddTilePositions()
    {
        //Loop through the 2D tile array
        for (int x = 0; x < Tiles.GetLength(0); x++)
        {
            for (int y = 0; y < Tiles.GetLength(1); y++)
            {
                //Obtain the rect transform from the tile
                RectTransform TileRectTransform = Tile.GetComponent<RectTransform>();

                //Add the positions for the tile
                TilePositions.Add(new Vector2(StartingTilePosition.x + (TileRectTransform.sizeDelta.x * x), StartingTilePosition.y - (TileRectTransform.sizeDelta.y * y)));
            }
        }
    }

    void SetupTiles()
    {
        //Set index to 0
        int TileIndex = 0;

        //Set the value of max tiles
        int MaxTiles = Tiles.GetLength(0) * Tiles.GetLength(1);

        //Loop through the list of tile positions
        for (int i = 0; i < MaxTiles; i++)
        {
            //Instantiate the tile
            GameObject TileClone = Instantiate(Tile, Vector2.zero, Quaternion.identity);

            //Set the tile to the canvas as a parent
            TileClone.transform.SetParent(TilePuzzlePanel.transform);

            //Randomize a number
            int RandomValue = Random.Range(0, TilePositions.Count);

            //Set a tile position in the canvas based on the index from the list
            TileClone.transform.localPosition = TilePositions[RandomValue];

            //Remove the tile position from the list
            TilePositions.Remove(TileClone.transform.localPosition);

            //Change the tile name
            TileClone.name = "Tile " + TileIndex.ToString();

            //Obtain the tile image
            Image TileImage = TileClone.GetComponent<Image>();

            //Set the sprite of the tile image
            TileImage.sprite = TileSprite[TileIndex];

            //Increment the index for the tile sprite
            TileIndex++;
        }
    }

    //Find a tile based on its' position and return the gameobject
    GameObject ReturnFoundTileBasedOnPosition(Vector3 Position)
    {
        GameObject[] Tiles = GameObject.FindGameObjectsWithTag("Tile");

        for (int i = 0; i < Tiles.Length; i++)
        {
            if (Tiles[i].transform.localPosition == Position)
            {
                return Tiles[i];
            }
        }
        return null;
    }

    void StoreRandomizedTilesInArray()
    {
        //Set index to 0
        int TileIndex = 0;

        //Obtain the rect transform from the tile
        RectTransform TileRectTransform = Tile.GetComponent<RectTransform>();

        //Loop through the 2D tile array
        for (int x = 0; x < Tiles.GetLength(0); x++)
        {
            for (int y = 0; y < Tiles.GetLength(1); y++)
            {
                //Set the current position based on the tile and the values from the for loops
                Vector2 CurrentPosition = new Vector2(StartingTilePosition.x + (TileRectTransform.sizeDelta.x * x), StartingTilePosition.y - (TileRectTransform.sizeDelta.y * y));

                //Find the next tile and return it based on the current position
                GameObject FoundTile = ReturnFoundTileBasedOnPosition(CurrentPosition);

                //Obtain the tile class from the tile clone
                Tiles[x, y] = FoundTile.GetComponent<Tile>();

                //Set the tile ID
                Tiles[x, y].SetNewTileID(TileIndex);

                //Set the tile into the custom class array
                Tiles[x, y].SetNewTileObject(FoundTile);

                //Store the array positions in the tile
                Tiles[x, y].SetTileArrayPositions(x, y);

                //Set the position of the tile
                Tiles[x, y].SetTilePositions(CurrentPosition.x, CurrentPosition.y);

                //Increment the index
                TileIndex++;
            }
        }
    }

    //
    void ChangeListener()
    {
        //Create a new entry for the event trigger on the Black Penny Stamp button
        EventTrigger.Entry PennyBlackStampEventTrigger = new EventTrigger.Entry();

        //Set an event ID of pointer click onto it
        PennyBlackStampEventTrigger.eventID = EventTriggerType.PointerClick;

        //Create a new event trigger
        PennyBlackStampEventTrigger.callback = new EventTrigger.TriggerEvent();

        //Add the listener of the update content function to the Penny Black Stamp
        PennyBlackStampEventTrigger.callback.AddListener(delegate { TextPanel.UpdateContent(PennyBlackStampInfo); });

        //Add the listener of the open function to the Penny Black Stamp
        PennyBlackStampEventTrigger.callback.AddListener(delegate { TextPanel.Open(); });

        //Add the event trigger for the Black Penny Stamp
        PennyBlackStampButton.triggers.Add(PennyBlackStampEventTrigger);
    }

    //Check if the tile puzzle has been completed or not
    void CheckPreviousTilePuzzleCompletion()
    {
        //If the player has completed the puzzle
        if (ProgressManagerScript.HasPlayerCompletedTilePuzzle())
        {
            //Destroy the tile puzzle panel
            Destroy(TilePuzzlePanel);

            //Clear the event triggers from the Black Penny Stamp
            PennyBlackStampButton.triggers.Clear();

            //Change the listener from the Black Penny Stamp
            ChangeListener();
        }
        else
        {
            //Add tile positions to a list
            AddTilePositions();

            //Create the tiles onto the scene
            SetupTiles();

            //Store all tile in the array and randomize their positions
            StoreRandomizedTilesInArray();

            //Set the tile puzzle panel to be inactive
            TilePuzzlePanel.SetActive(false);
        }
    }

    //Toggle the selection of all of the exhibits
    public void ToggleExhibitSelections()
    {
        //If the tile puzzle is incomplete
        if (!ProgressManagerScript.HasPlayerCompletedTilePuzzle())
        {
            //Find the canvas
            GameObject Canvas = GameObject.Find("Canvas");

            //Loop through all of the exhibits in the canvas
            foreach (Transform Exhibit in Canvas.transform)
            {
                //Obtain the image
                Image ExhibitImage = Exhibit.GetComponent<Image>();

                //If the image component exists and it's not the Penny Black Stamp
                if (ExhibitImage != null && !Exhibit.name.Contains("PennyBlackStamp"))
                {
                    //Disable the raycast target when the tile puzzle is active
                    ExhibitImage.raycastTarget = !ToggleClassScript.ReturnToggleState();
                }
            }
        }
    }

    void Start()
    {
        ToggleClassScript = GameObject.Find("TilePuzzleCanvas").GetComponent<ToggleClass>();
        ProgressManagerScript = GameObject.Find("GlobalManager").GetComponent<ProgressManager>();
        PennyBlackStampButton = GameObject.Find("Canvas/PennyBlackStamp").GetComponent<EventTrigger>();
        CheckPreviousTilePuzzleCompletion();
    }

    //Highlight all adjacent tiles from the selected tile in the 2D array
    void HighlightAdjacentTiles(Color HighlightedColor)
    {
        //Loop through the 2D array
        for (int x = 0; x < Tiles.GetLength(0); x++)
        {
            for (int y = 0; y < Tiles.GetLength(1); y++)
            {
                if (Tiles[x, y].ReturnTileObject() == SelectedTile)
                {
                    //Set up booleans to detect if the adjacent spot in the array is going outside the boundaries
                    bool DoesUpDirectionGoOutsideBoundaries = y - 1 < 0;
                    bool DoesLeftDirectionGoOutsideBoundaries = x - 1 < 0;
                    bool DoesDownDirectionGoOutsideBoundaries = y + 1 > Tiles.GetLength(1) - 1;
                    bool DoesRightDirectionGoOutsideBoundaries = x + 1 > Tiles.GetLength(0) - 1;

                    //If any direction goes outside the boundaries of the 2D array
                    if (!DoesUpDirectionGoOutsideBoundaries)
                    {
                        //Obtain the image component from the adjacent tile
                        Image TileImage = Tiles[x, y - 1].ReturnTileObject().GetComponent<Image>();

                        //Highlight the color
                        TileImage.color = HighlightedColor;
                    }

                    if (!DoesLeftDirectionGoOutsideBoundaries)
                    {
                        Image TileImage = Tiles[x - 1, y].ReturnTileObject().GetComponent<Image>();
                        TileImage.color = HighlightedColor;
                    }

                    if (!DoesDownDirectionGoOutsideBoundaries)
                    {
                        Image TileImage = Tiles[x, y + 1].ReturnTileObject().GetComponent<Image>();
                        TileImage.color = HighlightedColor;
                    }

                    if (!DoesRightDirectionGoOutsideBoundaries)
                    {
                        Image TileImage = Tiles[x + 1, y].ReturnTileObject().GetComponent<Image>();
                        TileImage.color = HighlightedColor;
                    }                    
                }
            }
        }
    }

    //Swap two selected tiles with each other
    void SwapTiles(int FirstTileID, int FirstTileXPositionArray, int FirstTileYPositionArray, float FirstTileXPosition, float FirstTileYPosition,
                   int SecondTileID, int SecondTileXPositionArray, int SecondTileYPositionArray, float SecondTileXPosition, float SecondTileYPosition)
    {
        //Create temporary variables to store the information of the second tile because it will be overwritten
        int TempTileID = SecondTileID;
        float TempTileXPosition = SecondTileXPosition;
        float TempTileYPosition = SecondTileYPosition;
        int TempTileXPositionArray = SecondTileXPositionArray;
        int TempTileYPositionArray = SecondTileYPositionArray;
        Tile TempTile = Tiles[SecondTileXPositionArray, SecondTileYPositionArray];

        //Set the second selected tile ID, position, array position, and the tile itself and set it all to the first selected tile
        Tiles[SecondTileXPositionArray, SecondTileYPositionArray].SetNewTileID(FirstTileID);
        Tiles[SecondTileXPositionArray, SecondTileYPositionArray].SetTilePositions(FirstTileXPosition, FirstTileYPosition);
        Tiles[SecondTileXPositionArray, SecondTileYPositionArray].SetTileArrayPositions(FirstTileXPositionArray, FirstTileYPositionArray);
        Tiles[SecondTileXPositionArray, SecondTileYPositionArray] = Tiles[FirstTileXPositionArray, FirstTileYPositionArray];

        //Set the first selected tile ID, position, array position, and the tile itself and set it all to the temporary selected tile
        //because the data of the second tile got overwritten
        Tiles[FirstTileXPositionArray, FirstTileYPositionArray].SetNewTileID(TempTileID);
        Tiles[FirstTileXPositionArray, FirstTileYPositionArray].SetTilePositions(TempTileXPosition, TempTileYPosition);
        Tiles[FirstTileXPositionArray, FirstTileYPositionArray].SetTileArrayPositions(TempTileXPositionArray, TempTileYPositionArray);
        Tiles[FirstTileXPositionArray, FirstTileYPositionArray] = TempTile;
    }

    void SelectTile()
    {
        //If the player has clicked on a tile
        if (Input.GetMouseButtonDown(0) && EventSystem.current.currentSelectedGameObject != null && 
            EventSystem.current.currentSelectedGameObject.tag.Contains("Tile") && !AreTilesBeingSwapped)
        {
            //Set a local variable for the image tile
            Image TileImage = null;

            //Set the default color
            Color DefaultColor = new Color(1f, 1f, 1f, 1f);

            //If the selected tile is null
            if (SelectedTile == null)
            {
                //Set the variable to the currently selected gameobject
                SelectedTile = EventSystem.current.currentSelectedGameObject;

                //Obtain the image from the selected tile
                TileImage = SelectedTile.GetComponent<Image>();

                //Highlight the tile
                TileImage.color = new Color(0f, 1f, 0.75f, 1f);

                //Highlight all adjacent tiles
                HighlightAdjacentTiles(Color.green);

            }
            else if (SelectedTile != EventSystem.current.currentSelectedGameObject && EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color == Color.green)
            {
                //If the selected tile is not the same as the current selected object from the event system and if the current selected object is highlighted
                //then obtain the first selected tile
                FirstSwappedTile = SelectedTile.GetComponent<Tile>();

                //Obtain the second selected tile
                SecondSwappedTile = EventSystem.current.currentSelectedGameObject.GetComponent<Tile>();

                //Obtain the first ans second swapped tile
                FirstSwappedTilePosition = Tiles[FirstSwappedTile.ReturnXArrayPosition(), FirstSwappedTile.ReturnYArrayPosition()].ReturnTilePosition();
                SecondSwappedTilePosition = Tiles[SecondSwappedTile.ReturnXArrayPosition(), SecondSwappedTile.ReturnYArrayPosition()].ReturnTilePosition();

                //Play the tile swap sound
                PlayAudioClip(TileSwapSound);

                //Set bool to true to start tile swapping
                AreTilesBeingSwapped = true;

                //Obtain the image from the selected tile
                TileImage = SelectedTile.GetComponent<Image>();

                //Make the color of the tile to be default
                TileImage.color = DefaultColor;

                //Unhighlight all adjacent tiles
                HighlightAdjacentTiles(DefaultColor);

                //Set the selected tile to null
                SelectedTile = null;
            }
            else
            {
                //Obtain the image from the selected tile
                TileImage = SelectedTile.GetComponent<Image>();

                //Make the color of the tile to be default
                TileImage.color = DefaultColor;

                //Unhighlight all adjacent tiles
                HighlightAdjacentTiles(DefaultColor);

                //Set the selected tile to be null
                SelectedTile = null;
            }
        }
    }

    void PlayAudioClip(AudioClip Clip)
    {
        if (TileSoundSource != null)
        {
            TileSoundSource.clip = Clip;
            TileSoundSource.Play();
        }
    }

    void AnimateMovingTiles()
    {
        //If the bool to swap tiles is true
        if (AreTilesBeingSwapped)
        {
            //Lerp both the first and second selected tiles to the second and first tile positions respectively
            SecondSwappedTile.transform.localPosition = Vector3.Lerp(SecondSwappedTile.transform.localPosition, FirstSwappedTilePosition, Time.deltaTime * TileSwapSpeed);
            FirstSwappedTile.transform.localPosition = Vector3.Lerp(FirstSwappedTile.transform.localPosition, SecondSwappedTilePosition, Time.deltaTime * TileSwapSpeed);

            //If the distance between the first and second selected tile positions are close enough
            if (Vector2.Distance(FirstSwappedTile.transform.localPosition, SecondSwappedTilePosition) <= 0.1f)
            {
                //Swap the first and second selected tiles
                SwapTiles(FirstSwappedTile.ReturnTileID(), FirstSwappedTile.ReturnXArrayPosition(), FirstSwappedTile.ReturnYArrayPosition(), FirstSwappedTile.ReturnTilePosition().x, FirstSwappedTile.ReturnTilePosition().y,
                    SecondSwappedTile.ReturnTileID(), SecondSwappedTile.ReturnXArrayPosition(), SecondSwappedTile.ReturnYArrayPosition(), SecondSwappedTile.ReturnTilePosition().x, SecondSwappedTile.ReturnTilePosition().y);

                //Set the first swapped tile position to the second tile position
                FirstSwappedTile.transform.localPosition = SecondSwappedTilePosition;

                //Set the first swapped tile to null to reset it
                FirstSwappedTile = null;

                //Set the vector 3 to zero to reset it
                SecondSwappedTilePosition = Vector3.zero;

                //Set the second swapped tile position to the first tile position
                SecondSwappedTile.transform.localPosition = FirstSwappedTilePosition;

                //Set the second swapped tile to null to reset it
                SecondSwappedTile = null;

                //Set the vector 3 to zero to reset it
                FirstSwappedTilePosition = Vector3.zero;

                //If the tile puzzle is completed
                if (IsTilePuzzleCompleted())
                {
                    //Play the success sound
                    PlayAudioClip(SuccessSound);

                    //Toggle the bool to enable it
                    ToggleClassScript.ToggleFunction();

                    //Enable the selections of all exhibits
                    ToggleExhibitSelections();

                    //Set the bool to true in the progress manager script
                    ProgressManagerScript.IsTilePuzzleCompleted(IsTilePuzzleCompleted());

                    //Destroy the tile puzzle panel
                    Destroy(TilePuzzlePanel);

                    //Clear all triggers from the event trigger
                    PennyBlackStampButton.triggers.Clear();
                    
                    //Change the listener from the Penny Black Stamp
                    ChangeListener();
                    
                    //Update the content from the text panel
                    TextPanel.UpdateContent(PennyBlackStampInfo);
                    
                    //Open the text panel
                    TextPanel.Open();
                }
                //Set the bool to false to complete the tile swapping
                AreTilesBeingSwapped = false;
            }
        }
    }

    //Checks if the tile puzzle is completed or not
    bool IsTilePuzzleCompleted()
    {
        //Set value for checking the tile ID with the tile name
        int CurrentTileID = 0;

        //Loop through the 2D array
        for (int x = 0; x < Tiles.GetLength(0); x++)
        {
            for (int y = 0; y < Tiles.GetLength(1); y++)
            {
                //If the ID in the tile name does not match the current tile ID it is on
                if (!Tiles[x, y].ReturnTileObject().name.Contains(CurrentTileID.ToString()))
                {
                    //Return false to break out of the loop
                    return false;
                }
                else
                {
                    //Increment the tile ID
                    CurrentTileID++;
                }
            }
        }
        //If the nested for loop remains unbroken then return true to indicate that the tile puzzle is complete
        return true;
    }

    void Update()
    {
        SelectTile();
        AnimateMovingTiles();
    }

}
