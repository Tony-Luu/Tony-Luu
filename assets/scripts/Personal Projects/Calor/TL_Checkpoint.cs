using UnityEngine;

public class TL_Checkpoint : MonoBehaviour
{
    public GameObject Campfire;
    public GameObject CampLight;
    private GameObject PlayerCharacter;
    private bool HasCheckpointReached;
    private AudioSource CampfireAudio;
    private TL_Respawn RespawnScript;
    private TL_CharacterHealth HealthScript;


    void Start()
    {
        //Find the player
        PlayerCharacter = GameObject.FindGameObjectWithTag("Player");

        //Obtain the audio source
        CampfireAudio = GetComponent<AudioSource>();

        //Obtain the respawn script and the character health script from the player character
        RespawnScript = PlayerCharacter.GetComponent<TL_Respawn>();
        HealthScript = PlayerCharacter.GetComponent<TL_CharacterHealth>();

        //Ignore the collisoin between the player character and this checkpoint
        Physics2D.IgnoreCollision(PlayerCharacter.GetComponent<BoxCollider2D>(), transform.GetComponent<BoxCollider2D>());
    }

    void OnParticleCollision(GameObject Object)
    {
        //If the torch fire has touched this checkpoint
        if (Object.CompareTag("TorchFire"))
        {
            //Set the player's current health to max
            HealthScript.SetFullHealth();

            //If the checkpoint hasn't been reached yet
            if (!HasCheckpointReached)
            {
                //Set the new respawn position for the player character
                RespawnScript.SetRespawnPosition(PlayerCharacter.transform.position.x, PlayerCharacter.transform.position.y);

                //Create the camp fire particle effect and the light and set the parent to this checkpoint
                GameObject CampfireClone = Instantiate(Campfire, transform.position + (transform.up * 1.25f), Quaternion.identity);
                CampfireClone.transform.SetParent(transform);                

                GameObject CampLightClone = Instantiate(CampLight, transform.position, Quaternion.identity);
                CampLightClone.transform.SetParent(transform);

                //Play the sound
                CampfireAudio.Play();

                //Set the reach checkpoint bool to true
                HasCheckpointReached = true;
            }
        }
    }

}
