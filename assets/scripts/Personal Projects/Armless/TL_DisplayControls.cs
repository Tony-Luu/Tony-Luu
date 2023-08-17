using UnityEngine;


public class TL_DisplayControls : MonoBehaviour
{
    public GameObject DisplayText;
    private GameObject Player;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    //Reveal text with the boolean toggle
    void RevealText(bool Toggle)
    {
        DisplayText.SetActive(Toggle);
    }

    //Display the text instructions when the player is in close proximity
    void DisplayInstructions()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) <= 3f)
        {
            RevealText(true);
        }
        else
        {
            RevealText(false);
        }
    }

    void Update()
    {
        DisplayInstructions();
    }

}
