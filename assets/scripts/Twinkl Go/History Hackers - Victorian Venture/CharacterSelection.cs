using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] private GameObject Character;
    [SerializeField] private RuntimeAnimatorController CharlieAnimatorController;
    [SerializeField] private RuntimeAnimatorController TildaAnimatorController;
    [SerializeField] private Sprite CharlieSprite;
    [SerializeField] private Sprite TildaSprite;
    [SerializeField] private int IndexForMainHall;
    [SerializeField] private PageCollection Instructions;

    //Static variables
    [SerializeField] private static Vector3 StartingPosition = new Vector3(0f, 188f, 0f);
    [SerializeField] private static bool CharlieSelected;
    [SerializeField] private static bool TildaSelected;
    [SerializeField] private static bool HasInstructionsBeenDisplayedOnce;


    public bool HasCharlieBeenSelected()
    {
        return CharlieSelected;
    }

    public bool HasTildaBeenSelected()
    {
        return TildaSelected;
    }

    public void CharlieSelectedButton()
    {
        CharlieSelected = true;
        TildaSelected = false;
    }

    public void TildaSelectedButton()
    {
        TildaSelected = true;
        CharlieSelected = false;
    }

    void OnSceneWasLoad(Scene From, Scene To)
    {
        //Create the character
        GameObject CharacterClone = Instantiate(Character, Vector3.zero, Quaternion.identity);

        //Set the parent of the character to the canvas
        CharacterClone.transform.SetParent(GameObject.Find("Canvas").transform);

        //Set the sibling index in front of the background
        CharacterClone.transform.SetSiblingIndex(2);

        //Rescale the character
        CharacterClone.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);

        //Obtain the rect transform from the character
        RectTransform CharacterRectTransform = CharacterClone.GetComponent<RectTransform>();

        //Set the starting position
        CharacterRectTransform.anchoredPosition = StartingPosition;

        //Obtain the character's animator
        Animator CharacterAnimator = CharacterClone.GetComponent<Animator>();

        if (CharlieSelected)
        {
            //Set the sprite to Charlie
            CharacterClone.GetComponent<Image>().sprite = CharlieSprite;

            //Set the animator to Charlie's animator controller
            CharacterAnimator.runtimeAnimatorController = CharlieAnimatorController;
        }
        else if (TildaSelected)
        {
            //Set the sprite to Tilda
            CharacterClone.GetComponent<Image>().sprite = TildaSprite;

            //Replace the animator with the override animation controller
            CharacterAnimator.runtimeAnimatorController = TildaAnimatorController;
        }
        //Find and obtain the instructions panel
        MultiTextPanel InstructionsPanel = GameObject.Find("PauseMenuCanvas/MultiTextPanel").GetComponent<MultiTextPanel>();

        //If the instructions haven't been displayed before
        if (!HasInstructionsBeenDisplayedOnce)
        {
            //Update the contents of the text panel and open it
            InstructionsPanel.UpdateContent(Instructions);
            InstructionsPanel.Open();

            //Set the bool to true to prevent the text panel from showing again
            HasInstructionsBeenDisplayedOnce = true;
        }
    }

    public void TeleportToMainHall()
    {
        SceneManager.activeSceneChanged += OnSceneWasLoad;
        LevelManager.Instance.LoadLevel(IndexForMainHall);
        HasInstructionsBeenDisplayedOnce = false;
    }

}
