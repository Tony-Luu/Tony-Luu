using UnityEngine;

public class CheckGameFocus : MonoBehaviour
{
    [SerializeField] public static bool IsGameInFocus;
    [SerializeField] private GameObject PauseScreen;
    [SerializeField] private GameObject InstructionScreen;
    [SerializeField] private GameObject GameOverScreen;


    void Awake()
    {
        IsGameInFocus = true;
    }

    void OnApplicationFocus(bool focus)
    {
        IsGameInFocus = focus;
    }

    void ToggleFreeze()
    {
        //If the game is in focus and is not paused and not in the instructions screen
        if (IsGameInFocus && !PauseScreen.activeInHierarchy && !InstructionScreen.activeInHierarchy)
        {
            Time.timeScale = 1f;
        }
        else
        {
            //If the game is not in focus and is paused
            Time.timeScale = 0f;
        }
    }

    void Update()
    {
        ToggleFreeze();
    }

}