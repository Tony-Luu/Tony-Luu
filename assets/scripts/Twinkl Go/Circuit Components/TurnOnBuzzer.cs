using UnityEngine;

public class TurnOnBuzzer : MonoBehaviour
{
    [SerializeField]
    private AudioClip BuzzerSound;

    [SerializeField]
    private AudioSource BuzzerSource;

    [SerializeField]
    private bool IsBuzzerSwitchedOn;


    public void ToggleBuzzerSound(bool Toggle)
    {
        IsBuzzerSwitchedOn = Toggle;
    }

    void PlayBuzzerSound()
    {
        if (IsBuzzerSwitchedOn)
        {
            BuzzerSource.clip = BuzzerSound;
            BuzzerSource.Play();
        }
        else
        {
            BuzzerSource.Stop();
        }
    }

    void Update()
    {
        PlayBuzzerSound();
    }

}
