using UnityEngine;

public class TL_CameraFollow : MonoBehaviour
{
    private Transform Player;
    private float yElevation = 2f;


    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //Force the camera to follow the player
    void FollowPlayer()
    {
        if (Player != null)
        {
            transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + yElevation, transform.position.z);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        FollowPlayer();
    }

}
