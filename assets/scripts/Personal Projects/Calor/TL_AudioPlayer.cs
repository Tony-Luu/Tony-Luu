using UnityEngine;

public class TL_AudioPlayer : MonoBehaviour
{
    public AudioClip IndoorAmbienceBGM;
    public AudioClip SnowyAreaBGM;
    public AudioClip IceCaveBGM;
    private AudioSource AudioBGMSource;
    static private TL_AudioPlayer Instance;


    void SingletonPrinciple()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    void Awake()
    {
        SingletonPrinciple();
        AudioBGMSource = GetComponent<AudioSource>();
        PlayIndoorBGM();
    }

    static public void PlayIndoorBGM()
    {
        if (Instance != null)
        {
            if (Instance.AudioBGMSource != null)
            {
                Instance.AudioBGMSource.Stop();
                Instance.AudioBGMSource.clip = Instance.IndoorAmbienceBGM;
                Instance.AudioBGMSource.Play();
            }
        }
    }

    static public void PlaySnowyAreaBGM()
    {
        if (Instance != null)
        {
            if (Instance.AudioBGMSource != null)
            {
                Instance.AudioBGMSource.Stop();
                Instance.AudioBGMSource.clip = Instance.SnowyAreaBGM;
                Instance.AudioBGMSource.Play();
            }
        }
    }

    static public void PlayIceCaveBGM()
    {
        if (Instance != null)
        {
            if (Instance.AudioBGMSource != null)
            {
                Instance.AudioBGMSource.Stop();
                Instance.AudioBGMSource.clip = Instance.IceCaveBGM;
                Instance.AudioBGMSource.Play();
            }
        }
    }

}
