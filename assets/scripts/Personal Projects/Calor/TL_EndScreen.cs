using UnityEngine;
using UnityEngine.Video;

public class TL_EndScreen : MonoBehaviour
{
    public VideoPlayer EndVideo;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EndVideo.Play();
        }
    }

}
