using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class TL_VideoTransition : MonoBehaviour
{
    public VideoPlayer EndingVideo;

    //Checks if the ending video is still playing or not
    void CheckIfVideoIsStillPlaying()
    {
        //If the video has finished playing
        if (!EndingVideo.isPlaying)
        {
            //Set the index of the first scene
            int FirstSceneIndex = 0;

            //Loads the scene based on the index
            SceneManager.LoadScene(FirstSceneIndex);
        }
    }

    void Update()
    {
        CheckIfVideoIsStillPlaying();
    }


}
