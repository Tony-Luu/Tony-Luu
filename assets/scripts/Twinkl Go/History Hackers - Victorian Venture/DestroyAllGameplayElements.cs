using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyAllGameplayElements : MonoBehaviour
{
    [SerializeField] private GameObject GameplayCanvas;
    [SerializeField] private GameObject PauseMenuCanvas;


    void FindAllCanvases()
    {
        GameplayCanvas = GameObject.FindGameObjectWithTag("GameplayCanvas");
        PauseMenuCanvas = GameObject.FindGameObjectWithTag("PauseMenuCanvas");
    }

    void Awake()
    {
        SceneManager.activeSceneChanged += DestroyGameplayObjectsAtSceneChange;
    }

    void DestroyGameplayObjectsAtSceneChange(Scene CurrentScene, Scene NextScene)
    {
        //If the scene is the title screen or the character selection screen
        if (NextScene.buildIndex == 0 || NextScene.buildIndex == 1)
        {
            //Find the player character and destroy it
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }

        //If the scene is the title screen
        if (NextScene.buildIndex == 0)
        {
            //Destroy all of the gameplay related canvases and the score manager
            Destroy(GameplayCanvas);
            Destroy(PauseMenuCanvas);
            Destroy(GameObject.FindGameObjectWithTag("CharacterSelection"));
            Destroy(GameObject.FindGameObjectWithTag("ScoreManager"));            
        }
        else
        {
            FindAllCanvases();
        }
    }

}
