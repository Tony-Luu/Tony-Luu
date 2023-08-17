using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DisplayCharacterIcon : MonoBehaviour
{
    [SerializeField] private Image CharacterIcon;
    [SerializeField] private Sprite CharlieIcon;
    [SerializeField] private Sprite TildaIcon;
    [SerializeField] private int NextSceneIndex;
    [SerializeField] private CharacterSelection CharacterSelectionScript;


    void Start()
    {
        ChangeCharacterIcon();
    }

    void OnEnable()
    {
        
        SceneManager.activeSceneChanged += UpdateCharacterIcons;
    }

    void OnDisable()
    {
        //SceneManager.activeSceneChanged -= UpdateCharacterIcons;
    }

    //Update the character icons for the minimap when the scene changes
    void UpdateCharacterIcons(Scene CurrentScene, Scene NextScene)
    {
        NextSceneIndex = NextScene.buildIndex;
        DisplayIcon();
    }

    //Changes the character icon depending on who was selected
    void ChangeCharacterIcon()
    {
        CharacterSelectionScript = GameObject.Find("CharacterSelection").GetComponent<CharacterSelection>();
        if (CharacterSelectionScript.HasCharlieBeenSelected())
        {
            CharacterIcon.sprite = CharlieIcon;
        }
        else if (CharacterSelectionScript.HasTildaBeenSelected())
        {
            CharacterIcon.sprite = TildaIcon;
        }
    }

    //Displays the character icon on the minimap depending on where the player is
    void DisplayIcon()
    {
        switch (NextSceneIndex)
        {
            case 2:
                CharacterIcon.rectTransform.localPosition = transform.Find("MainHallPosition").localPosition;
                break;

            case 3:
                CharacterIcon.rectTransform.localPosition = transform.Find("PortraitGalleryPosition").localPosition;
                break;

            case 4:
                CharacterIcon.rectTransform.localPosition = transform.Find("HallOfInventionsPosition").localPosition;
                break;

            case 5:
                CharacterIcon.rectTransform.localPosition = transform.Find("DayToDayPosition").localPosition;
                break;
        }
    }

}
