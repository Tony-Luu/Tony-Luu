using UnityEngine;
using UnityEngine.SceneManagement;

public class TL_Respawn : MonoBehaviour
{
    public Vector2 RespawnPosition;
    public TL_Checkpoint CheckpointScript;
    public string LastSceneName;


    void Awake()
    {
        //Set the initial respawn position
        SetRespawnPosition(transform.position.x, transform.position.y);
    }

    //Set the respawn position and save last scene name
    public void SetRespawnPosition(float xPosition, float yPosition)
    {
        RespawnPosition = new Vector2(xPosition, yPosition);
        LastSceneName = SceneManager.GetActiveScene().name;
    }

    //Teleports the player character to the last respawn position
    public void TeleportToRespawnPosition()
    {        
        transform.position = new Vector3(RespawnPosition.x, RespawnPosition.y, 0f);
    }

    //Loads the last saved scene
    public void LoadLastScene()
    {
        if (LastSceneName != null)
        {
            SceneManager.LoadScene(LastSceneName);
        }        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //If this player character touches a hazard then teleport the player character back to its' respawn point
        if (collision.gameObject.tag == "Hazard")
        {
            LoadLastScene();
            TeleportToRespawnPosition();
        }
    }

}
