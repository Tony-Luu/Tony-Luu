using UnityEngine;

public class TL_IceBlock : MonoBehaviour
{
    public AudioClip IceMeltSound;
    public bool IsAlight;
    public float BurnRate = 0.1f;
    public float BurnCooldown = 1f;
    private AudioSource IceAudioSource;


    void Start()
    {
        IceAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        MeltEffect();
    }

    //Reduces the scale of the object until it becomes small enough to destroy
    void MeltEffect()
    {
        if (IsAlight && BurnCooldown < Time.realtimeSinceStartup)
        {
            transform.localScale = new Vector3(transform.localScale.x - 0.1f, transform.localScale.y - 0.1f, 0f);
            if (transform.localScale.x < 0.1f)
            {
                Destroy(gameObject);
            }
            BurnCooldown = BurnRate + Time.realtimeSinceStartup;
        }
    }

    void OnParticleCollision(GameObject Object)
    {
        if (Object.CompareTag("TorchFire"))
        {
            //Get the audio clip and play it
            IceAudioSource.clip = IceMeltSound;
            IceAudioSource.Play();

            //Set alight to true
            IsAlight = true;
        }
    }

}
