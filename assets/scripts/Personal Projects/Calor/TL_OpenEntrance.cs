using UnityEngine;

public class TL_OpenEntrance : MonoBehaviour
{
    public Sprite OpenDoor;
    public SpriteRenderer WallSprite;
    private GameObject Player;
    private BoxCollider2D WallCollider;


    void Start()
    {
        Player = GameObject.Find("Player");
        WallCollider = GetComponent<BoxCollider2D>();
    }

    void OpenEntrance()
    {
        //If the player obtains the torch
        if (Player.transform.childCount > 0)
        {
            //Make the entrance collider a trigger
            WallCollider.isTrigger = true;

            //Change the door sprite
            WallSprite.sprite = OpenDoor;
        }
    }

    void Update()
    {
        OpenEntrance();
    }



}
