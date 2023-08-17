using UnityEngine;
using UnityEngine.SceneManagement;

public class TL_SceneTransition : MonoBehaviour
{    
    public string LoadSceneName;
    public Vector2 StartingPosition;
    private GameObject Player;
    private TL_Respawn RespawnScript;
    private TL_AnimationManager AnimationManagerScript;


    void Awake()
    {
        //Find the player gameobject
        Player = GameObject.FindGameObjectWithTag("Player");

        //Set the starting position
        StartingPosition = Player.transform.position;

        //Obtain the respawn script
        RespawnScript = Player.GetComponent<TL_Respawn>();

        //Obtain the animation manager script form the player
        AnimationManagerScript = Player.GetComponent<TL_AnimationManager>();
    }

    //Loads the BGM depending on the scene index
    void LoadBGM(int Index)
    {
        switch (Index)
        {
            case 4:
                TL_AudioPlayer.PlaySnowyAreaBGM();
                break;

            case 5:
                TL_AudioPlayer.PlayIceCaveBGM();
                break;
        }
    }

    //Don't destroy the player until the last scene
    void DontDestroyUntilLastScene(int Index)
    {
        //If the current scene is not the last scene
        if (Index < 8)
        {
            DontDestroyOnLoad(Player);
        }
        else if (Index == 8)    //If the current scene is the last scene
        {
            //Destroy the player
            Destroy(Player);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Obtains the index of the next scene
            int SceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            //Loads the scene based on the index
            SceneManager.LoadScene(SceneIndex);

            //Loads the BGM for the appropriate scenes
            LoadBGM(SceneIndex);

            //Sets the current position as the starting position
            collision.transform.position = StartingPosition;

            //Sets the starting position of the next scene
            RespawnScript.SetRespawnPosition(collision.transform.position.x, collision.transform.position.y);

            //Change current state to Idle
            AnimationManagerScript.ChangeCurrentState(TL_AnimationManager.CharacterState.Idle);

            //If the player has reached the end scene then destroy the player
            DontDestroyUntilLastScene(SceneIndex);
        }
    }

}
