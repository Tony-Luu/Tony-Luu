using UnityEngine;
using UnityEngine.Video;

public class TL_VideoPlayerURL : MonoBehaviour
{
    public string VideoClipName;
    public VideoPlayer Video;


    void Awake()
    {
        Video.url = System.IO.Path.Combine(Application.streamingAssetsPath, VideoClipName);
        Video.Prepare();
        Video.Play();
    }

}
