using UnityEngine;
using UnityEngine.SceneManagement;

public class TL_PlayerSoundManager : MonoBehaviour
{
    public AudioClip FootstepSound;
    public AudioClip SnowyFootstepSound;
    public AudioClip JumpSound;
    private AudioSource SoundSource;


    void Start()
    {
        SoundSource = GetComponent<AudioSource>();
    }

    //Return the audio source
    public AudioSource ReturnAudioSource()
    {
        return SoundSource;
    }

    public void FootstepSounds()
    {
        //Obtain the build index of the current scene
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //If the build index is equal or more than 4
        if (CurrentSceneIndex >= 4)
        {
            //Play the snowy footsteps sound
            SoundSource.PlayOneShot(SnowyFootstepSound);
        }
        else
        {
            //Play the footsteps sound
            SoundSource.PlayOneShot(FootstepSound);
        }
    }

    public void JumpSounds()
    {
        SoundSource.PlayOneShot(JumpSound);
    }

}
