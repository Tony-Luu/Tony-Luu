using UnityEngine;

public class CharacterAudioSource : MonoBehaviour
{
    [SerializeField] private AudioSource CharacterAudio;
    [SerializeField] private AudioClip[] AudioClips;


    //Selects an audio clip from a list with an index and plays it
    public void PlayAudioClipAtIndex(int Index)
    {
        if(CharacterAudio != null)
        {
            CharacterAudio.clip = AudioClips[Index];
            CharacterAudio.Play();
        }
    }

}
